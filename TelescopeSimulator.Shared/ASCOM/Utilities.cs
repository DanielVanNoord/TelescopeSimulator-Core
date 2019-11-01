//tabs=4
// --------------------------------------------------------------------------------
//
// ASCOM Simulated Telescope Hardware
//
// Description:	This implements a simulated Telescope Hardware
//
// Implements:	ASCOM Telescope interface version: 2.0
// Author:		(rbt) Robert Turner <robert@robertturnerastro.com>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 07-JUL-2009	rbt	1.0.0	Initial edit, from ASCOM Telescope Driver template
// 18-SEP-2102  Rick Burke  Improved support for simulating pulse guiding
// May/June 2014 cdr 6.1    Change the telescope hardware to use an axis based method
// --------------------------------------------------------------------------------
//

using System;
using System.Collections.Generic;

namespace ASCOM.Utilities
{
    internal class Profile : IDisposable
    {
        Dictionary<string, string> profile = new Dictionary <string, string>();

        internal void WriteValue(string pROGRAM_ID, string Name, string Value)
        {
            profile[Name] = Value;
        }

        internal void WriteValue(string DriverID, string Name, string Value, string Subkey)
        {
            profile[Name + Subkey] = Value;
        }

        internal string GetValue(string DriverID, string Name)
        {
            return profile[Name];
        }

        internal string GetValue(string DriverID, string Name, string Subkey)
        {
            return profile[Name + Subkey];
        }

        internal string GetValue(string DriverID, string Name, string Subkey, string DefaultValue)
        {
            if(profile.ContainsKey(Name+Subkey))
            {
                return profile[Name + Subkey];
            }
            else
            {
                return DefaultValue;
            }
        }

        public void Dispose()
        {
        }
    }

    public class TraceLogger
    {
        private string v1;
        private string v2;

        public TraceLogger(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public void LogMessage(string v1, string v2)
        {
        }
    }
}