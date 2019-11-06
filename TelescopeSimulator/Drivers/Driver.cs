//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM Telescope driver for Telescope
//
// Description:	ASCOM Driver for Simulated Telescope
//
// Implements:	ASCOM Telescope interface version: 2.0
// Author:		(rbt) Robert Turner <robert@robertturnerastro.com>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 07-JUL-2009	rbt	1.0.0	Initial edit, from ASCOM Telescope Driver template
// 29 Dec 2010  cdr         Extensive refactoring and bug fixes
// --------------------------------------------------------------------------------
//
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using TelescopeSimulator;

namespace ASCOM.Simulator
{
    //
    // Your driver's ID is ASCOM.Telescope.Telescope
    //
    // The Guid attribute sets the CLSID for ASCOM.Telescope.Telescope
    // The ClassInterface/None addribute prevents an empty interface called
    // _Telescope from being created and used as the [default] interface
    //

    [Guid("EA091417-66DF-4250-8287-FAFAF38A80A6")]
    [ServedClassName("Telescope Simulator for .NET Core")]
    [ProgId("ASCOM.SimulatorCore.Telescope")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Telescope : ReferenceCountedObjectBase, ITelescopeV3
    {
        //
        // Driver private data (rate collections)
        //

        private ASCOM.Utilities.Util m_Util;
        private string driverID;
        private long objectId;

        private ITelescopeV3 TelescopeInstance
        {
            get
            {
                return DeviceManager.GetTelescope(0);
            }
        }

        //
        // Constructor - Must be public for COM registration!
        //
        public Telescope()
        {
            try
            {
                driverID = Marshal.GenerateProgIdForType(this.GetType());

                m_Util = new ASCOM.Utilities.Util();
                // get a unique instance id
                objectId = TelescopeHardware.GetId();
                TelescopeHardware.TL.LogMessage("New", "Instance ID: " + objectId + ", new: " + "Driver ID: " + driverID);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Telescope New: " + ex.ToString());
            }
        }

        //
        // PUBLIC COM INTERFACE ITelescope IMPLEMENTATION
        //

        #region ITelescope Members

        public string Action(string ActionName, string ActionParameters) => TelescopeInstance.Action(ActionName, ActionParameters);

        /// <summary>
        /// Gets the supported actions.
        /// </summary>
        public ArrayList SupportedActions => TelescopeInstance.SupportedActions;

        public void AbortSlew() => TelescopeInstance.AbortSlew();

        public AlignmentModes AlignmentMode => TelescopeInstance.AlignmentMode;

        public double Altitude => TelescopeInstance.Altitude;

        public double ApertureArea => TelescopeInstance.ApertureArea;

        public double ApertureDiameter => TelescopeInstance.ApertureDiameter;

        public bool AtHome => TelescopeInstance.AtHome;

        public bool AtPark => TelescopeInstance.AtPark;

        public IAxisRates AxisRates(TelescopeAxes Axis) => TelescopeInstance.AxisRates(Axis);

        public double Azimuth => TelescopeInstance.Azimuth;

        public bool CanFindHome => TelescopeInstance.CanFindHome;

        public bool CanMoveAxis(TelescopeAxes Axis) => TelescopeInstance.CanMoveAxis(Axis);

        public bool CanPark => TelescopeInstance.CanPark;

        public bool CanPulseGuide => TelescopeInstance.CanPulseGuide;

        public bool CanSetDeclinationRate => TelescopeInstance.CanSetDeclinationRate;

        public bool CanSetGuideRates => TelescopeInstance.CanSetGuideRates;

        public bool CanSetPark => TelescopeInstance.CanSetPark;

        public bool CanSetPierSide => TelescopeInstance.CanSetPierSide;

        public bool CanSetRightAscensionRate => TelescopeInstance.CanSetRightAscensionRate;

        public bool CanSetTracking => TelescopeInstance.CanSetTracking;

        public bool CanSlew => TelescopeInstance.CanSlew;

        public bool CanSlewAltAz => TelescopeInstance.CanSlewAltAz;

        public bool CanSlewAltAzAsync => TelescopeInstance.CanSlewAltAzAsync;

        public bool CanSlewAsync => TelescopeInstance.CanSlewAsync;

        public bool CanSync => TelescopeInstance.CanSync;

        public bool CanSyncAltAz => TelescopeInstance.CanSyncAltAz;

        public bool CanUnpark => TelescopeInstance.CanUnpark;

        public void CommandBlind(string Command, bool Raw) => TelescopeInstance.CommandBlind(Command, Raw);

        public bool CommandBool(string Command, bool Raw) => TelescopeInstance.CommandBool(Command, Raw);

        public string CommandString(string Command, bool Raw) => TelescopeInstance.CommandString(Command, Raw);

        public bool Connected
        {
            get { return TelescopeInstance.Connected; }
            set { TelescopeInstance.Connected = value; }
        }


        public double Declination => TelescopeInstance.Declination;

        public double DeclinationRate
        {
            get { return TelescopeInstance.DeclinationRate; }
            set { TelescopeInstance.DeclinationRate = value; }
        }

        public string Description => TelescopeInstance.Description;

        public PierSide DestinationSideOfPier(double RightAscension, double Declination) => TelescopeInstance.DestinationSideOfPier(RightAscension, Declination);

        public bool DoesRefraction
        {
            get { return TelescopeInstance.DoesRefraction; }
            set { TelescopeInstance.DoesRefraction = value; }
        }

        public string DriverInfo => TelescopeInstance.DriverInfo;

        public string DriverVersion => TelescopeInstance.DriverVersion;

        public EquatorialCoordinateType EquatorialSystem => TelescopeInstance.EquatorialSystem;

        public void FindHome() => TelescopeInstance.FindHome();

        public double FocalLength => TelescopeInstance.FocalLength;

        public double GuideRateDeclination
        {
            get { return TelescopeInstance.GuideRateDeclination; }
            set { TelescopeInstance.GuideRateDeclination = value; }
        }

        public double GuideRateRightAscension
        {
            get { return TelescopeInstance.GuideRateRightAscension; }
            set { TelescopeInstance.GuideRateRightAscension = value; }
        }

        public short InterfaceVersion => TelescopeInstance.InterfaceVersion;

        public bool IsPulseGuiding => TelescopeInstance.IsPulseGuiding;

        public void MoveAxis(TelescopeAxes Axis, double Rate) => TelescopeInstance.MoveAxis(Axis, Rate);

        public string Name => TelescopeInstance.Name;

        public void Park() => TelescopeInstance.Park();

        public void PulseGuide(GuideDirections Direction, int Duration) => TelescopeInstance.PulseGuide(Direction, Duration);

        public double RightAscension => TelescopeInstance.RightAscension;

        public double RightAscensionRate
        {
            get { return TelescopeInstance.RightAscensionRate; }
            set { TelescopeInstance.RightAscensionRate = value; }
        }

        public void SetPark() => TelescopeInstance.SetPark();

        public void SetupDialog()
        {
            //Normally cross platform drivers do not have a setup dialog. However this one is special.
            TelescopeInstance.SetupDialog();
        }

        public PierSide SideOfPier
        {
            get { return TelescopeInstance.SideOfPier; }
            set { TelescopeInstance.SideOfPier = value; }
        }

        public double SiderealTime => TelescopeInstance.SiderealTime;

        public double SiteElevation
        {
            get { return TelescopeInstance.SiteElevation; }
            set { TelescopeInstance.SiteElevation = value; }
        }

        public double SiteLatitude
        {
            get { return TelescopeInstance.SiteLatitude; }
            set { TelescopeInstance.SiteLatitude = value; }
        }

        public double SiteLongitude
        {
            get { return TelescopeInstance.SiteLongitude; }
            set { TelescopeInstance.SiteLongitude = value; }
        }

        public short SlewSettleTime
        {
            get { return TelescopeInstance.SlewSettleTime; }
            set { TelescopeInstance.SlewSettleTime = value; }
        }

        public void SlewToAltAz(double Azimuth, double Altitude) => TelescopeInstance.SlewToAltAz(Azimuth, Altitude);

        public void SlewToAltAzAsync(double Azimuth, double Altitude) => TelescopeInstance.SlewToAltAzAsync(Azimuth, Altitude);

        public void SlewToCoordinates(double RightAscension, double Declination) => TelescopeInstance.SlewToCoordinates(RightAscension, Declination);

        public void SlewToCoordinatesAsync(double RightAscension, double Declination) => TelescopeInstance.SlewToCoordinatesAsync(RightAscension, Declination);

        public void SlewToTarget() => TelescopeInstance.SlewToTarget();

        public void SlewToTargetAsync() => TelescopeInstance.SlewToTargetAsync();

        public bool Slewing => TelescopeInstance.Slewing;

        public void SyncToAltAz(double Azimuth, double Altitude) => TelescopeInstance.SyncToAltAz(Azimuth, Altitude);

        public void SyncToCoordinates(double RightAscension, double Declination) => TelescopeInstance.SyncToCoordinates(RightAscension, Declination);

        public void SyncToTarget() => TelescopeInstance.SyncToTarget();

        public double TargetDeclination
        {
            get
            { 
                return TelescopeInstance.TargetDeclination;
            }
            set
            {
                TelescopeInstance.TargetDeclination = value;
            }
        }

        public double TargetRightAscension
        {
            get
            {
                return TelescopeInstance.TargetRightAscension;
            }
            set
            {
                TelescopeInstance.TargetRightAscension = value;
            }
        }

        public bool Tracking
        {
            get
            {
                return TelescopeInstance.Tracking;
            }
            set
            {
                TelescopeInstance.Tracking = value;
            }
        }

        public DriveRates TrackingRate
        {
            get
            {
                return TelescopeInstance.TrackingRate;
            }
            set
            {
                TelescopeInstance.TrackingRate = value;
            }
        }

        public ITrackingRates TrackingRates => TelescopeInstance.TrackingRates;

        public DateTime UTCDate
        {
            get
            {
                return TelescopeInstance.UTCDate;
            }
            set
            {
                TelescopeInstance.UTCDate = value;
            }
        }

        public void Unpark() => TelescopeInstance.Unpark();


        #endregion ITelescope Members

     

        #region IDisposable Members

        public void Dispose()
        {
            //ToDo this is a bad idea unless the server is done.
            //TelescopeInstance.Dispose();
        }

        #endregion IDisposable Members
    }
}