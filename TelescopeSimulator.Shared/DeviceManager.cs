using ASCOM.DeviceInterface;
using ASCOM.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelescopeSimulator
{
    public static class DeviceManager
    {
        static ITelescopeV3 telescope = new Telescope();
        static DeviceManager()
        {
        }

        public static ITelescopeV3 GetTelescope(int deviceID)
        {
            if(deviceID == 0)
            {
                return telescope;
            }
            throw new ASCOM.InvalidValueException(String.Format("Telescope {0} does not exist on this system", deviceID));
        }
    }
}
