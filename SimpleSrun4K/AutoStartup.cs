using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SimpleSrun4K
{
    static class AutoStartup
    {
        // Don't use Application.ExecutablePath
        // see https://stackoverflow.com/questions/12945805/odd-c-sharp-path-issue
        private static readonly string ExecutablePath = Assembly.GetEntryAssembly().Location;

        private static string Key = "SimpleSrun4K";

        public static BasicReturnInfo Set(bool enabled)
        {
            RegistryKey runKey = null;
            try
            {
                runKey = Utils.OpenRegKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).Instance as RegistryKey;
                if (runKey == null)
                {
                    // Logging.Error(@"Cannot find HKCU\Software\Microsoft\Windows\CurrentVersion\Run");
                    return BasicReturnInfo.Create(false, 0x0040001);
                }
                if (enabled)
                {
                    runKey.SetValue(Key, ExecutablePath);
                }
                else
                {
                    runKey.DeleteValue(Key);
                }
                return BasicReturnInfo.OK();
            }
            catch (Exception e)
            {
                // Logging.LogUsefulException(e);
                return BasicReturnInfo.Create(false, 0x0040002);
            }
            finally
            {
                if (runKey != null)
                {
                    try
                    {
                        runKey.Close();
                        runKey.Dispose();
                    }
                    catch (Exception e)
                    { 
                        //Logging.LogUsefulException(e); 
                    }
                }
            }
        }

        public static BasicReturnInfo Check()
        {
            RegistryKey runKey = null;
            try
            {
                runKey = Utils.OpenRegKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).Instance as RegistryKey;
                if (runKey == null)
                {
                    // Logging.Error(@"Cannot find HKCU\Software\Microsoft\Windows\CurrentVersion\Run");
                    return BasicReturnInfo.Create(false, 0x0040003);
                }
                string[] runList = runKey.GetValueNames();
                foreach (string item in runList)
                {
                    if (item.Equals(Key, StringComparison.OrdinalIgnoreCase))
                        return BasicReturnInfo.OK();
                    else if (item.Equals("SimpleSrun4K", StringComparison.OrdinalIgnoreCase)) // Compatibility with older versions
                    {
                        string value = Convert.ToString(runKey.GetValue(item));
                        if (ExecutablePath.Equals(value, StringComparison.OrdinalIgnoreCase))
                        {
                            runKey.DeleteValue(item);
                            runKey.SetValue(Key, ExecutablePath);
                            return BasicReturnInfo.OK();
                        }
                    }
                }
                return BasicReturnInfo.Create(false, 0x0040004);
            }
            catch (Exception e)
            {
                // Logging.LogUsefulException(e);
                return BasicReturnInfo.Create(false, 0x0040005);
            }
            finally
            {
                if (runKey != null)
                {
                    try
                    {
                        runKey.Close();
                        runKey.Dispose();
                    }
                    catch (Exception e)
                    { 
                        //Logging.LogUsefulException(e); 
                    }
                }
            }
        }
    }
}
