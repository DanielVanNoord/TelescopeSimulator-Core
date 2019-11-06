using ASCOM.Alpaca.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASCOM.Alpaca.Controllers
{
    public class DeviceManager
    {
        internal static Func<int, ASCOM.DeviceInterface.ITelescopeV3> GetTelescope;

        internal static Func<IList<ASCOM.DeviceInterface.ITelescopeV3>> GetTelescopes;

        static DeviceManager()
        {

        }

        public static void  SetTelescopeAccess(Func<int, ASCOM.DeviceInterface.ITelescopeV3> telescopeAccess)
        {
            GetTelescope = telescopeAccess;
        }

        public static void SetTelescopesAccess(Func<int, ASCOM.DeviceInterface.ITelescopeV3> telescopeAccess)
        {
            GetTelescope = telescopeAccess;
        }
    }
}
