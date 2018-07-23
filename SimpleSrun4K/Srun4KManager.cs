using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleSrun4K
{
    class Srun4KManager
    {
        #region 单例模式

        private static Srun4KManager SManager;
        private static object SManagerLock;

        static Srun4KManager()
        {
            SManager = null;
            SManagerLock = new object();
        }

        public static Srun4KManager Create()
        {
            if(SManager == null)
            {
                lock(SManagerLock)
                {
                    if (SManager == null)
                    {
                        SManager = new Srun4KManager();
                    }
                }
            }

            return SManager;
        }

        #endregion

        public Config ConfigInfo { get; protected set; }
        public bool ConfigIsChange { get; set; }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <returns></returns>
        private BasicReturnInfo SaveConfig()
        {
            BasicReturnInfo writeFileResult = Utils.WriteFile(
                HardCode.CONFIGFILE_PATH + Path.AltDirectorySeparatorChar + HardCode.CONFIGFILE_NAME,
                ConfigInfo.ConvertToJson());

            if (!writeFileResult)
            {
                // 写文件出错

                // ErrorDetail: 0x0010001, "保存配置文件失败"
                return BasicReturnInfo.Create(false, 0x0010001);
            }

            return BasicReturnInfo.OK();
        }

        /// <summary>
        /// 加载配置信息
        /// </summary>
        /// <returns></returns>
        private BasicReturnInfo LoadConfig()
        {
            // 检查文件是否存在
            if (!File.Exists(HardCode.CONFIGFILE_PATH + Path.AltDirectorySeparatorChar + HardCode.CONFIGFILE_NAME))
            {
                // 不存在 则 直接使用缺省配置
                ConfigInfo = new Config();

                // 写入文件
                BasicReturnInfo writeFileResult = SaveConfig();

                // ErrorDetial: 0x0010002, "配置文件不存在"
                // ErrorDetail: 0x0010003, "配置文件不存在" + "保存配置文件出错"
                return BasicReturnInfo.Create(false, 0x0010002 | writeFileResult.Code);
            }
            else
            {
                // 配置文件 存在

                // 读取配置文件
                CreationReturnInfo readFileResult = Utils.ReadFile(
                    HardCode.CONFIGFILE_PATH + Path.AltDirectorySeparatorChar + HardCode.CONFIGFILE_NAME);

                if (!readFileResult)
                {
                    // 读取失败

                    // 使用缺省配置
                    ConfigInfo = new Config();

                    // ErrorDetail: 0x0010004, "读取配置文件失败"
                    return BasicReturnInfo.Create(false, 0x0010004);
                }
                else
                {
                    // 读取 成功

                    // 解析 配置
                    CreationReturnInfo createConfigResult = Config.CreateFromJson(readFileResult.Instance as string);

                    if (!createConfigResult)
                    {
                        // 解析 失败

                        // 使用缺省配置
                        ConfigInfo = new Config();

                        // ErrorDetial: 0x0010008: "解析配置失败"
                        return BasicReturnInfo.Create(false, 0x0010008);
                    }
                    else
                    {
                        // 解析 成功

                        ConfigInfo = createConfigResult.Instance as Config;

                        return BasicReturnInfo.OK();
                    }
                }
            }
        }

        /// <summary>
        /// 重新加载配置文件并刷新配置信息
        /// </summary>
        /// <returns></returns>
        public BasicReturnInfo RefreshConfig()
        {
            // 保存当前配置信息
            Config temp = ConfigInfo;

            BasicReturnInfo reloadResult = LoadConfig();

            if (reloadResult)
            {
                // 加载 成功
                return BasicReturnInfo.OK();
            }
            else
            {
                // 加载 失败

                // 写回 配置
                ConfigInfo = temp;

                // ErrorDetial: 0x0010010: "重新加载配置失败"
                return BasicReturnInfo.Create(false, 0x0010010 | reloadResult.Code);
            }
        }

        /// <summary>
        /// 配置 重新写入文件
        /// </summary>
        /// <returns></returns>
        public BasicReturnInfo ReWriteConfig()
        {
            // 备份 配置文件
            Directory.CreateDirectory(HardCode.CONFIGFILE_BACKUPPATH);
            File.Copy(
                HardCode.CONFIGFILE_PATH + Path.AltDirectorySeparatorChar + HardCode.CONFIGFILE_NAME,
                HardCode.CONFIGFILE_BACKUPPATH + Path.AltDirectorySeparatorChar + HardCode.CONFIGFILE_NAME,
                true);

            BasicReturnInfo rewriteResult = SaveConfig();

            if (rewriteResult)
            {
                // 写入 成功

                Directory.Delete(HardCode.CONFIGFILE_BACKUPPATH, true);
                return BasicReturnInfo.OK();
            }
            else
            {
                // 写入 失败

                // 恢复 配置文件
                File.Copy(
                    HardCode.CONFIGFILE_BACKUPPATH + Path.AltDirectorySeparatorChar + HardCode.CONFIGFILE_NAME,
                    HardCode.CONFIGFILE_PATH + Path.AltDirectorySeparatorChar + HardCode.CONFIGFILE_NAME,
                    true);

                Directory.Delete(HardCode.CONFIGFILE_BACKUPPATH, true);

                // ErrorDetial: 0x0010020: "重新保存配置文件失败"
                return BasicReturnInfo.Create(false, 0x0010020 | rewriteResult.Code);
            }
        }

        public BasicReturnInfo Login()
        {
            return Srun4KCore.Login(ConfigInfo.LoginUrl, ConfigInfo.UserName, ConfigInfo.Password);
        }

        public BasicReturnInfo Logout()
        {
            return Srun4KCore.Logout(ConfigInfo.LogoutUrl, ConfigInfo.UserName);
        }

        public BasicReturnInfo CheckOnline()
        {
            return Srun4KCore.CheckOnline(ConfigInfo.CheckOnlineUrl);
        }

        public BasicReturnInfo ForceLogout()
        {
            return Srun4KCore.ForceLogout(ConfigInfo.LogoutUrl, ConfigInfo.UserName, ConfigInfo.Password);
        }

        public BasicReturnInfo ChangeAutoStartState()
        {
            return AutoStartup.Set(ConfigInfo.AutoStart);
        }

        public BasicReturnInfo ChangeAutoLoginState(bool flg)
        {
            return null;
        }

        public BasicReturnInfo AutoLogin()
        {
            return Login();
        }

        protected Srun4KManager()
        {
            // 尝试释放 Newtonsoft.Json.dll
            Utils.ReleaseFile(
                HardCode.RESOURCES_PATH, HardCode.JSONNET_NAME, 
                HardCode.RELEASE_PATH, HardCode.JSONNET_NAME);

            //Utils.ReleaseFile(
            //    HardCode.RESOURCES_PATH, HardCode.JSONNET_NAME,
            //    HardCode.SYSTEM32_PATH, HardCode.JSONNET_NAME);

            BasicReturnInfo initResult = LoadConfig();

            if (!initResult)
            {
                // 加载 配置信息 失败

                MessageBox.Show("错误码: 0x" + initResult.Code.ToString("x").PadLeft(8, '0'));
            }

            ConfigIsChange = false;
        }
    }
}
