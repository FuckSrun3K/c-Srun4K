using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSrun4K
{
    class BasicReturnInfo
    {
        public bool Success { get; protected set; }
        public long Code { get; protected set; }
        public string Info { get; protected set; }

        protected BasicReturnInfo(bool success = true, long code = 0, string info = "")
        {
            Success = success;
            Code = code;
            Info = info;
        }

        public static BasicReturnInfo Create(bool success = true, long code = 0, string info = "")
        {
            return new BasicReturnInfo(success, code, info);
        }

        public static BasicReturnInfo CopyStatus(BasicReturnInfo info)
        {
            return new BasicReturnInfo(info.Success, info.Code, info.Info);
        }

        public static BasicReturnInfo OK()
        {
            return new BasicReturnInfo();
        }

        public static implicit operator bool(BasicReturnInfo info)
        {
            return info.Success;
        }
    }

    class CreationReturnInfo : BasicReturnInfo
    {
        public object Instance { get; protected set; }

        protected CreationReturnInfo(object instance,bool success = true, int code = 0, string info = "") :
            base(success,code,info)
        {
            Instance = instance;
        }

        public static CreationReturnInfo Create(object instance,bool success = true, int code = 0, string info = "")
        {
            return new CreationReturnInfo(instance, success, code, info);
        }
    }
}
