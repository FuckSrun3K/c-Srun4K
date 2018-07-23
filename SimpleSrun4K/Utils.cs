using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSrun4K
{
    static class HardCode
    {
        public static string START_PATH = System.Windows.Forms.Application.StartupPath;

        public static string CONFIGFILE_PATH = START_PATH;
        public static string CONFIGFILE_BACKUPPATH =
            START_PATH + Path.AltDirectorySeparatorChar + "Backup";
        public static string CONFIGFILE_NAME = "Srun4KConfig.json";

        public static string RESOURCES_PATH = "SimpleSrun4K.Resources";
        public static string JSONNET_NAME = "Newtonsoft.Json.dll";
        public static string RELEASE_PATH = START_PATH;
        //public static string SYSTEM32_PATH = System.Windows.Forms.Application.StartupPath;

    }
    class Utils
    {
        public static CreationReturnInfo ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                return CreationReturnInfo.Create(null, false, 0x0030001);
            }

            string result = null;

            try
            {
                result = File.ReadAllText(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                return CreationReturnInfo.Create(null, false, 0x0030002);
            }

            return CreationReturnInfo.Create(result);
        }

        public static BasicReturnInfo WriteFile(string path, string text)
        {
            try
            {
                File.WriteAllText(path, text, Encoding.UTF8);
            }
            catch (Exception e)
            {
                return BasicReturnInfo.Create(false, 0x0030003);
            }

            return BasicReturnInfo.Create();
        }

        /// <summary>
        /// 释放文件
        /// </summary>
        /// <param name="resourcePath">资源在DLL中的路径(不包含资源名称)</param>
        /// <param name="resourceName">资源在DLL中的名称</param>
        /// <param name="releasePath">文件释放路径(不包含文件名称)</param>
        /// <param name="fileName">文件名称</param>
        public static void ReleaseFile(string resourcePath, string resourceName, string releasePath, string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream resourceStream = assembly.GetManifestResourceStream(
                string.Format(@"{0}.{1}", resourcePath, resourceName));

            // 如果路径不存在，创建目录
            if (!Directory.Exists(releasePath))
            {
                Directory.CreateDirectory(releasePath);
            }

            // 释放文件 完整位置
            string fullPath = string.Format(@"{0}{1}{2}", releasePath, Path.AltDirectorySeparatorChar, fileName);

            if (File.Exists(fullPath))
            {
                // 文件存在，则比对MD5

                FileStream fileStream = new FileStream(fullPath, FileMode.Open);
                string resourceMD5 = null;
                string fileMD5 = null;

                if (GetStreamMD5(fileStream, out fileMD5) &&
                    GetStreamMD5(resourceStream, out resourceMD5) &&
                    fileMD5 != resourceMD5)
                {
                    // 关闭文件流，因为之前打开文件的参数是open，没有写入权限
                    fileStream.Close();
                    // 重新打开文件流，create参数
                    fileStream = new FileStream(fullPath, FileMode.Create);
                    resourceStream.CopyTo(fileStream);
                    fileStream.Close();
                }
            }
            else
            {
                // 文件不存在，则写入文件

                FileStream fileStream = new FileStream(fullPath, FileMode.Create);
                resourceStream.CopyTo(fileStream);
                fileStream.Close();
            }

            resourceStream.Close();

        }

        /// <summary>
        /// 计算文件流的 MD5
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="md5Str">输出 MD5</param>
        /// <returns></returns>
        public static bool GetStreamMD5(Stream fileStream, out string md5Str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5Bytes = md5.ComputeHash(fileStream);
            fileStream.Close();

            StringBuilder md5StrBuilder = new StringBuilder();
            for (int i = 0; i < md5Bytes.Length; i++)
            {
                md5StrBuilder.Append(md5Bytes[i].ToString("x2"));
            }

            md5Str = md5StrBuilder.ToString();
            return true;
        }

        public static CreationReturnInfo OpenRegKey(string name, bool writable, RegistryHive hive = RegistryHive.CurrentUser)
        {
            // we are building x86 binary for both x86 and x64, which will
            // cause problem when opening registry key
            // detect operating system instead of CPU
            if (string.IsNullOrEmpty(name))
                return CreationReturnInfo.Create(null, false, 0x0030004);
            try
            {
                RegistryKey userKey = RegistryKey.OpenBaseKey(hive,
                        Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)
                    .OpenSubKey(name, writable);
                return CreationReturnInfo.Create(userKey);
            }
            catch (ArgumentException ae)
            {
                return CreationReturnInfo.Create(null, false, 0x0030005);
            }
            catch (Exception e)
            {
                return CreationReturnInfo.Create(null, false, 0x0030006);
            }
        }
    }
}
