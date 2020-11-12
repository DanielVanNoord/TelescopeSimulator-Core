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
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

        private TelescopeSimulator TelescopeInstance
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

                TrackingRates = new TrackingRateObject(TelescopeInstance.TrackingRates);
            }
            catch (Exception ex)
            {
                EventLogCode.LogEvent("ASCOM.SimulatorCore.Telescope", "Exception on New", EventLogEntryType.Error, GlobalConstants.EventLogErrors.TelescopeSimulatorNew, ex.ToString());
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
        public ArrayList SupportedActions => new ArrayList(TelescopeInstance.SupportedActions.ToArray());

        public void AbortSlew() => TelescopeInstance.AbortSlew();

        public AlignmentModes AlignmentMode => (AlignmentModes) TelescopeInstance.AlignmentMode;

        public double Altitude => TelescopeInstance.Altitude;

        public double ApertureArea => TelescopeInstance.ApertureArea;

        public double ApertureDiameter => TelescopeInstance.ApertureDiameter;

        public bool AtHome => TelescopeInstance.AtHome;

        public bool AtPark => TelescopeInstance.AtPark;

        public IAxisRates AxisRates(TelescopeAxes Axis) 
        {
            return new AxisRatesObject(TelescopeInstance.AxisRates((Standard.Interfaces.TelescopeAxis)Axis));
        }

        public double Azimuth => TelescopeInstance.Azimuth;

        public bool CanFindHome => TelescopeInstance.CanFindHome;

        public bool CanMoveAxis(TelescopeAxes Axis) => TelescopeInstance.CanMoveAxis((Standard.Interfaces.TelescopeAxis) Axis);

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

        public PierSide DestinationSideOfPier(double RightAscension, double Declination) => (PierSide) TelescopeInstance.DestinationSideOfPier(RightAscension, Declination);

        public bool DoesRefraction
        {
            get { return TelescopeInstance.DoesRefraction; }
            set { TelescopeInstance.DoesRefraction = value; }
        }

        public string DriverInfo => TelescopeInstance.DriverInfo;

        public string DriverVersion => TelescopeInstance.DriverVersion;

        public EquatorialCoordinateType EquatorialSystem => (EquatorialCoordinateType)TelescopeInstance.EquatorialSystem;

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

        public void MoveAxis(TelescopeAxes Axis, double Rate) => TelescopeInstance.MoveAxis((Standard.Interfaces.TelescopeAxis)Axis, Rate);

        public string Name => TelescopeInstance.Name;

        public void Park() => TelescopeInstance.Park();

        public void PulseGuide(GuideDirections Direction, int Duration) => TelescopeInstance.PulseGuide((Standard.Interfaces.GuideDirection)Direction, Duration);

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
            get { return (PierSide) TelescopeInstance.SideOfPier; }
            set { TelescopeInstance.SideOfPier = (ASCOM.Standard.Interfaces.PointingState) value; }
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
                return (DriveRates) TelescopeInstance.TrackingRate;
            }
            set
            {
                TelescopeInstance.TrackingRate = (Standard.Interfaces.DriveRate) value;
            }
        }

        public ITrackingRates TrackingRates { get; private set; }

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

        public void Unpark() => TelescopeInstance.UnPark();


        #endregion ITelescope Members

     

        #region IDisposable Members

        public void Dispose()
        {
            //ToDo this is a bad idea unless the server is done.
            //TelescopeInstance.Dispose();
        }

        #endregion IDisposable Members

        public class TrackingRateObject : ITrackingRates, IEnumerable, IEnumerator, IDisposable
        {
            private List<DriveRates> m_TrackingRates = new List<DriveRates>();
            private int _pos = -1;
            //
            // Default constructor - Internal prevents public creation
            // of instances. Returned by Telescope.AxisRates.
            //
            public TrackingRateObject(Standard.Interfaces.ITrackingRates rates)
            {
                foreach(Standard.Interfaces.DriveRate rate in rates)
                {
                    m_TrackingRates.Add((DriveRates)rate);
                }
            }

            #region ITrackingRates Members

            public int Count
            {
                get { return m_TrackingRates.Count; }
            }

            public IEnumerator GetEnumerator()
            {
                _pos = -1; //Reset pointer as this is assumed by .NET enumeration
                return this as IEnumerator;
            }


            public DriveRates this[int index]
            {
                get
                {
                    if (index < 1 || index > this.Count)
                        throw new InvalidValueException("TrackingRates.this", index.ToString(CultureInfo.CurrentCulture), string.Format(CultureInfo.CurrentCulture, "1 to {0}", this.Count));
                    return m_TrackingRates[index - 1];
                }   // 1-based
            }
            #endregion

            #region IEnumerator implementation

            public bool MoveNext()
            {
                if (++_pos >= m_TrackingRates.Count) return false;
                return true;
            }

            public void Reset()
            {
                _pos = -1;
            }

            public object Current
            {
                get
                {
                    if (_pos < 0 || _pos >= m_TrackingRates.Count) throw new System.InvalidOperationException();
                    return m_TrackingRates[_pos];
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            // The bulk of the clean-up code is implemented in Dispose(bool)
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    // free managed resources
                    /* Following code commented out in Platform 6.4 because m_TrackingRates is a global variable for the whole driver and there could be more than one 
                     * instance of the TrackingRates class (created by the calling application). One instance should not invalidate the variable that could be in use
                     * by other instances of which this one is unaware.

                    m_TrackingRates = null;

                    */
                }
            }
            #endregion
        }

        public class AxisRatesObject : IAxisRates, IEnumerable, IEnumerator, IDisposable
        {
            private List<Rate> m_Rates = new List<Rate>();
            private int pos = -1;

            //
            // Constructor - Internal prevents public creation
            // of instances. Returned by Telescope.AxisRates.
            //
            public AxisRatesObject(Standard.Interfaces.IAxisRates Axis)
            {
                foreach (Standard.Interfaces.IRate rate in Axis)
                {
                    m_Rates.Add(new Rate(rate.Minimum, rate.Maximum));
                }
                pos = -1;
            }

            #region IAxisRates Members

            public int Count
            {
                get { return m_Rates.Count; }
            }

            public IEnumerator GetEnumerator()
            {
                pos = -1; //Reset pointer as this is assumed by .NET enumeration
                return this as IEnumerator;
            }

            public IRate this[int index]
            {
                get
                {
                    if (index < 1 || index > this.Count)
                        throw new InvalidValueException("AxisRates.index", index.ToString(CultureInfo.CurrentCulture), string.Format(CultureInfo.CurrentCulture, "1 to {0}", this.Count));
                    return (IRate)m_Rates[index - 1];   // 1-based
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            // The bulk of the clean-up code is implemented in Dispose(bool)
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    // free managed resources
                    m_Rates = null;
                }
            }

            #endregion

            #region IEnumerator implementation

            public bool MoveNext()
            {
                if (++pos >= m_Rates.Count) return false;
                return true;
            }

            public void Reset()
            {
                pos = -1;
            }

            public object Current
            {
                get
                {
                    if (pos < 0 || pos >= m_Rates.Count) throw new System.InvalidOperationException();
                    return m_Rates[pos];
                }
            }

            #endregion
        }

        public class Rate : IRate, IDisposable
        {
            private double m_dMaximum = 0;
            private double m_dMinimum = 0;

            //
            // Default constructor - Internal prevents public creation
            // of instances. These are values for AxisRates.
            //
            internal Rate(double Minimum, double Maximum)
            {
                m_dMaximum = Maximum;
                m_dMinimum = Minimum;
            }

            #region IRate Members

            public IEnumerator GetEnumerator()
            {
                return null;
            }

            public double Maximum
            {
                get { return m_dMaximum; }
                set { m_dMaximum = value; }
            }

            public double Minimum
            {
                get { return m_dMinimum; }
                set { m_dMinimum = value; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            // The bulk of the clean-up code is implemented in Dispose(bool)
            protected virtual void Dispose(bool disposing)
            {
                // nothing to do?
            }

            #endregion
        }
    }
}