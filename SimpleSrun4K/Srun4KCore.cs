using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace SimpleSrun4K
{
    /// <summary>
    /// 配置信息
    /// </summary>
    class Config
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LoginUrl { get; set; }
        public string LogoutUrl { get; set; }
        public string CheckOnlineUrl { get; set; }
        public bool AutoStart { get; set; }
        public bool AutoLogin { get; set; }

        public Config()
        {
            UserName = string.Empty;
            Password = string.Empty;
            LoginUrl = @"http://10.0.0.55/cgi-bin/srun_portal";
            LogoutUrl = @"http://10.0.0.55/cgi-bin/srun_portal";
            CheckOnlineUrl = @"http://10.0.0.55/cgi-bin/rad_user_info";
            AutoStart = false;
            AutoLogin = false;
        }

        public static CreationReturnInfo CreateFromJson(string json)
        {
            try
            {
                Config config = JsonConvert.DeserializeObject<Config>(json);
                return CreationReturnInfo.Create(config);
            }
            catch(Exception e)
            {
                return CreationReturnInfo.Create(null, false, 0x0010001, e.ToString());
            }
        }

        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// 加密类
    /// </summary>
    static class Encrypt
    {
        /// <summary>
        /// 密码加密类
        /// </summary>
        public static class Password
        {
            // column表，下标为0 和 1 的一定取不到，所以写啥都行
            static char[] column_key = { (char)0, (char)0, 'd', 'c', 'j', 'i', 'h', 'g' };
            // row表
            static char[,] row_key = {
            {'6','7','8','9',':',';','<','=','>','?','@','A','B','C','D','E'},
	        {'?','>','A','@','C','B','E','D','7','6','9','8',';',':','=','<'},
	        {'>','?','@','A','B','C','D','E','6','7','8','9',':',';','<','='},
	        {'=','<',';',':','9','8','7','6','E','D','C','B','A','@','?','>'},
	        {'<','=',':',';','8','9','6','7','D','E','B','C','@','A','>','?'},
	        {';',':','=','<','7','6','9','8','C','B','E','D','?','>','A','@'},
	        {':',';','<','=','6','7','8','9','B','C','D','E','>','?','@','A'},
	        {'9','8','7','6','=','<',';',':','A','@','?','>','E','D','B','C'},
	        {'8','9','6','7','<','=',':',';','@','A','>','?','D','E','B','C'},
	        {'7','6','8','9',';',':','=','<','?','>','A','@','C','B','D','E'}
        };

            public static string Encrypt(string rawPassword)
            {
                // 字符计数
                int i = 0;
                // 用于连接字符串
                StringBuilder strBuilder = new StringBuilder();

                foreach (char item in rawPassword)
                {
                    // 取得 ascii值
                    int item_ascii = (int)item;

                    // 使用 ascii值 的 前四位 查表得 column 对应字符 
                    char char_c = column_key[item_ascii >> 4];
                    // 使用 ascii值 的 后四位 查表得 row 对应字符
                    char char_r = row_key[i % 10, item_ascii & 0xF];

                    // 根据 奇偶数 决定 顺序
                    if ((i % 2) == 1)
                    {
                        strBuilder.Append(char_c);
                        strBuilder.Append(char_r);
                    }
                    else
                    {
                        strBuilder.Append(char_r);
                        strBuilder.Append(char_c);
                    }

                    i++;
                }

                // 返回加密后字符串
                return strBuilder.ToString();
            }
        }
    }

    /// <summary>
    /// Srun4K 核心操作
    /// </summary>
    static class Srun4KCore
    {
        #region Login
        public static BasicReturnInfo Login(string url, string username, string password)
        {
            string payload = CreateLoginPayload(username, password);
            byte[] payload_bytes = Encoding.Default.GetBytes(payload);

            HttpWebResponse response = null;
            try
            {
                response = Srun4KPostMethod(url, payload_bytes);
            }
            catch(Exception e)
            {
                return BasicReturnInfo.Create(false, 0x0020001, e.ToString());
            }
            
            if(response.StatusCode == HttpStatusCode.OK)
            {
                // http 没问题
                string text = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (text.IndexOf("login_ok") != -1)
                {
                    return BasicReturnInfo.Create(info:text);
                }
                else
                {
                    return BasicReturnInfo.Create(false, 0x0020002, text);
                }
            }
            else
            {
                // http 有问题
                return BasicReturnInfo.Create(false, 0x0020003, response.StatusCode.ToString());
            }
        }

        private static string CreateLoginPayload(string username, string password)
        {
            string pwd = Encrypt.Password.Encrypt(password);

            JObject payload = new JObject();

            payload.Add("action", "login");
            payload.Add("username", username);
            payload.Add("password", pwd);
            payload.Add("drop", 0);
            payload.Add("pop", 0);
            payload.Add("type", 2);
            payload.Add("n", 117);
            payload.Add("mbytes", 0);
            payload.Add("minutes", 0);
            payload.Add("ac_id", 1);

            return payload.ToString();
        }

        

        #endregion

        #region CheckOnline

        public static BasicReturnInfo CheckOnline(string url)
        {
            HttpWebResponse response = null;
            try
            {
                response = Srun4KGetMethod(url);
            }
            catch (Exception e)
            {
                return BasicReturnInfo.Create(false, 0x0020001, e.ToString());
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // http 没问题
                string text = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (text.IndexOf("not_online") == -1)
                {
                    return BasicReturnInfo.Create(info:text);
                }
                else
                {
                    return BasicReturnInfo.Create(false, 0x0020004, text);
                }
            }
            else
            {
                // http 有问题
                return BasicReturnInfo.Create(false, 0x0020003, response.StatusCode.ToString());
            }

        }

        #endregion

        #region Logout

        public static BasicReturnInfo Logout(string url, string username)
        {
            string payload = CreateLogoutPayload(username);
            byte[] payload_bytes = Encoding.Default.GetBytes(payload);

            HttpWebResponse response = null;
            try
            {
                response = Srun4KPostMethod(url, payload_bytes);
            }
            catch (Exception e)
            {
                return BasicReturnInfo.Create(false, 0x0020001, e.ToString());
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // http 没问题
                string text = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (text.IndexOf("logout_ok") != -1)
                {
                    return BasicReturnInfo.Create();
                }
                else
                {
                    return BasicReturnInfo.Create(false, 0x0020002, text);
                }
            }
            else
            {
                // http 有问题
                return BasicReturnInfo.Create(false, 0x0020003, response.StatusCode.ToString());
            }
        }

        private static string CreateLogoutPayload(string username)
        {
            JObject payload = new JObject();

            payload.Add("action", "logout");
            payload.Add("ac_id", 1);
            payload.Add("username", username);
            payload.Add("type", 2);

            return payload.ToString();
        }

        #endregion

        #region Force Logout

        public static BasicReturnInfo ForceLogout(string url, string username, string password)
        {
            string payload = CreateForceLogoutPayload(username, password);
            byte[] payload_bytes = Encoding.Default.GetBytes(payload);

            HttpWebResponse response = null;
            try
            {
                response = Srun4KPostMethod(url, payload_bytes);
            }
            catch (Exception e)
            {
                return BasicReturnInfo.Create(false, 0x0020001, e.ToString());
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // http 没问题
                string text = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (text.IndexOf("logout_ok") != -1)
                {
                    return BasicReturnInfo.Create();
                }
                else
                {
                    return BasicReturnInfo.Create(false, 0x0020002, text);
                }
            }
            else
            {
                // http 有问题
                return BasicReturnInfo.Create(false, 0x0020003, response.StatusCode.ToString());
            }
        }

        private static string CreateForceLogoutPayload(string username, string password)
        {
            JObject payload = new JObject();

            payload.Add("action", "logout");
            payload.Add("username", username);
            payload.Add("password", password);
            payload.Add("drop", 0);
            payload.Add("type", 1);
            payload.Add("n", 117);
            payload.Add("ac_id", 1);

            return payload.ToString();
        }

        #endregion

        #region POST & GET

        private static HttpWebResponse Srun4KPostMethod(string url, byte[] payload)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            request.Method = "POST";
            request.UserAgent = "pySrun4k";
            request.ContentType = "application/json";

            request.ContentLength = payload.Length;
            request.GetRequestStream().Write(payload, 0, payload.Length);
            request.GetRequestStream().Close();

            return request.GetResponse() as HttpWebResponse;
        }

        private static HttpWebResponse Srun4KGetMethod(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            request.Method = "GET";
            request.UserAgent = "pySrun4k";

            return request.GetResponse() as HttpWebResponse;
        }

        #endregion
    }
}
