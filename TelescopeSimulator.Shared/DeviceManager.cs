using ASCOM.Simulator;
using ASCOM.Standard.Interfaces;
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
        static ASCOM.Simulator.TelescopeSimulator telescope = new ASCOM.Simulator.TelescopeSimulator();
        static DeviceManager()
        {
        }

        public static ASCOM.Simulator.TelescopeSimulator GetTelescope(int deviceID)
        {
            if(deviceID == 0)
            {
                return telescope;
            }
            throw new ASCOM.InvalidValueException(String.Format("Telescope {0} does not exist on this system", deviceID));
        }
    }
}
