﻿using ASCOM.Alpaca.Responses;
using ASCOM.Standard.Helpers;
using ASCOM.Standard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TelescopeSimulator.Alpaca.Controllers
{
    public class TelescopeController : Controller
    {
        private const string APIRoot = "api/v1/telescope/";

        private static uint transactionID = 0;

        private static uint TransactionID
        {
            get
            {
                return transactionID++;
            }
        }

        [HttpGet]
        [Route("setup/")]
        public ActionResult GetServerSetup()
        {
            var path = Path.Combine(@"web/html/setup.html");

            return new ContentResult
            {
                Content = System.IO.File.ReadAllText(path),
                ContentType = "text/html",
                StatusCode = 200
            };
        }

        #region Common Methods

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Action")]
        public StringResponse Action(int DeviceNumber, [FromForm] string Action, [FromForm] string Parameters, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).Action(Action, Parameters));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandBlind")]
        public Response CommandBlind(int DeviceNumber, [FromForm] string Command, [FromForm] bool Raw = false, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).CommandBlind(Command, Raw);
                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandBool")]
        public BoolResponse CommandBool(int DeviceNumber, [FromForm] string Command, [FromForm] bool Raw = false, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CommandBool(Command, Raw));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandString")]
        public StringResponse CommandString(int DeviceNumber, [FromForm] string Command, [FromForm] bool Raw = false, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CommandString(Command, Raw));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Connected")]
        public BoolResponse Connected(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).Connected);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Connected")]
        public Response Connected(int DeviceNumber, [FromForm] bool Connected, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
                DeviceManager.GetTelescope(DeviceNumber).Connected = Connected;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Description")]
        public StringResponse Description(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).Description);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DriverInfo")]
        public StringResponse DriverInfo(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).DriverInfo);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DriverVersion")]
        public StringResponse DriverVersion(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).DriverVersion);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/InterfaceVersion")]
        public IntResponse InterfaceVersion(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new IntResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).InterfaceVersion);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<IntResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Name")]
        public StringResponse Name(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).Name);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SupportedActions")]
        public StringListResponse SupportedActions(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringListResponse(ClientTransactionID, TransactionID, new List<string>(DeviceManager.GetTelescope(DeviceNumber).SupportedActions.Cast<string>().ToList()));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringListResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        #endregion Common Methods


        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/AbortSlew")]
        public Response AbortSlew(int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).AbortSlew();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/AlignmentMode")]
        public AlignmentModeResponse AlignmentMode(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new AlignmentModeResponse(ClientTransactionID, TransactionID, (ASCOM.Standard.Interfaces.AlignmentMode)DeviceManager.GetTelescope(DeviceNumber).AlignmentMode);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<AlignmentModeResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Altitude")]
        public DoubleResponse Altitude(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).Altitude);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/ApertureArea")]
        public DoubleResponse ApertureArea(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).ApertureArea);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/ApertureDiameter")]
        public DoubleResponse ApertureDiameter(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).ApertureDiameter);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/AtHome")]
        public BoolResponse AtHome(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).AtHome);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/AtPark")]
        public BoolResponse AtPark(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).AtPark);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/AxisRates")]
        public AxisRatesResponse AxisRates(int DeviceNumber, TelescopeAxis Axis, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                List<ASCOM.Standard.Interfaces.AxisRate> rates = new List<ASCOM.Standard.Interfaces.AxisRate>();

                foreach (IRate rate in DeviceManager.GetTelescope(DeviceNumber).AxisRates(Axis))
                {
                    rates.Add(new ASCOM.Standard.Interfaces.AxisRate(rate.Minimum, rate.Maximum));
                }
                return new AxisRatesResponse(ClientTransactionID, TransactionID, rates);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<AxisRatesResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Azimuth")]
        public DoubleResponse Azimuth(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).Azimuth);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanFindHome")]
        public BoolResponse CanFindHome(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanFindHome);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanMoveAxis")]
        public BoolResponse MoveAxis(int DeviceNumber, TelescopeAxis Axis, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanMoveAxis(Axis));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }


        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanPark")]
        public BoolResponse CanPark(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanPark);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanPulseGuide")]
        public BoolResponse CanPulseGuide(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanPulseGuide);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSetDeclinationRate")]
        public BoolResponse CanSetDeclinationRate(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSetDeclinationRate);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSetGuideRates")]
        public BoolResponse CanSetGuideRates(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSetGuideRates);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSetPark")]
        public BoolResponse CanSetPark(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSetPark);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSetPierSide")]
        public BoolResponse CanSetPierSide(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSetPierSide);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSetRightAscensionRate")]
        public BoolResponse CanSetRightAscensionRate(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSetRightAscensionRate);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSetTracking")]
        public BoolResponse CanSetTracking(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSetTracking);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSlew")]
        public BoolResponse CanSlew(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSlew);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSlewAltAz")]
        public BoolResponse CanSlewAltAz(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSlewAltAz);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSlewAltAzAsync")]
        public BoolResponse CanSlewAltAzAsync(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSlewAltAzAsync);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSlewAsync")]
        public BoolResponse CanSlewAsync(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSlewAsync);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSync")]
        public BoolResponse CanSync(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSync);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanSyncAltAz")]
        public BoolResponse CanSyncAltAz(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanSyncAltAz);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CanUnpark")]
        public BoolResponse CanUnpark(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).CanUnpark);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Declination")]
        public DoubleResponse Declination(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).Declination);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DeclinationRate")]
        public DoubleResponse DeclinationRate(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).DeclinationRate);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/DeclinationRate")]
        public Response DeclinationRate(int DeviceNumber, [FromForm] double DeclinationRate, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).DeclinationRate = DeclinationRate;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DestinationSideOfPier")]
        public PointingStateResponse DestinationSideOfPier(int DeviceNumber, double RightAscension, double Declination, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new PointingStateResponse(ClientTransactionID, TransactionID, (ASCOM.Standard.Interfaces.PointingState)DeviceManager.GetTelescope(DeviceNumber).DestinationSideOfPier(RightAscension, Declination));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<PointingStateResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DoesRefraction")]
        public BoolResponse DoesRefraction(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).DoesRefraction);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/DoesRefraction")]
        public Response DoesRefraction(int DeviceNumber, [FromForm] bool DoesRefraction, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).DoesRefraction = DoesRefraction;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/EquatorialSystem")]
        public EquatorialCoordinateTypeResponse EquatorialSystem(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new EquatorialCoordinateTypeResponse(ClientTransactionID, TransactionID, (ASCOM.Standard.Interfaces.EquatorialCoordinateType)DeviceManager.GetTelescope(DeviceNumber).EquatorialSystem);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<EquatorialCoordinateTypeResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/FindHome")]
        public Response FindHome(int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).FindHome();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/FocalLength")]
        public DoubleResponse FocalLength(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).FocalLength);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/GuideRateDeclination")]
        public DoubleResponse GuideRateDeclination(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).GuideRateDeclination);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/GuideRateDeclination")]
        public Response GuideRateDeclination(int DeviceNumber, [FromForm] double GuideRateDeclination, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).GuideRateDeclination = GuideRateDeclination;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/GuideRateRightAscension")]
        public DoubleResponse GuideRateRightAscension(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).GuideRateRightAscension);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/GuideRateRightAscension")]
        public Response GuideRateRightAscension(int DeviceNumber, [FromForm] double GuideRateRightAscension, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).GuideRateRightAscension = GuideRateRightAscension;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/IsPulseGuiding")]
        public BoolResponse IsPulseGuiding(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).IsPulseGuiding);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/MoveAxis")]
        public Response MoveAxis(int DeviceNumber, [FromForm] TelescopeAxis Axis, [FromForm] double Rate, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).MoveAxis(Axis, Rate);

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Park")]
        public Response Park(int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).Park();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/PulseGuide")]
        public Response PulseGuide(int DeviceNumber, [FromForm] GuideDirection Direction, [FromForm] int Duration, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).PulseGuide(Direction, Duration);

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/RightAscension")]
        public DoubleResponse RightAscensionn(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).RightAscension);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/RightAscensionRate")]
        public DoubleResponse RightAscensionRate(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).RightAscensionRate);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/RightAscensionRate")]
        public Response RightAscensionRate(int DeviceNumber, [FromForm] double RightAscensionRate, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).RightAscensionRate = RightAscensionRate;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SetPark")]
        public Response SetPark(int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SetPark();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SideOfPier")]
        public PointingStateResponse SideOfPier(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new PointingStateResponse(ClientTransactionID, TransactionID, (ASCOM.Standard.Interfaces.PointingState)DeviceManager.GetTelescope(DeviceNumber).SideOfPier);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<PointingStateResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SideOfPier")]
        public Response SideOfPier(int DeviceNumber, [FromForm] PointingState SideOfPier, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SideOfPier = SideOfPier;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SiderealTime")]
        public DoubleResponse SiderealTime(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).SiderealTime);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SiteElevation")]
        public DoubleResponse SiteElevation(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).SiteElevation);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SiteElevation")]
        public Response SiteElevation(int DeviceNumber, [FromForm] double SiteElevation, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SiteElevation = SiteElevation;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SiteLatitude")]
        public DoubleResponse SiteLatitude(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).SiteLatitude);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SiteLatitude")]
        public Response SiteLatitude(int DeviceNumber, [FromForm] double SiteLatitude, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SiteLatitude = SiteLatitude;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SiteLongitude")]
        public DoubleResponse SiteLongitude(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).SiteLongitude);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SiteLongitude")]
        public Response SiteLongitude(int DeviceNumber, [FromForm] double SiteLongitude, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SiteLongitude = SiteLongitude;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SlewSettleTime")]
        public IntResponse SlewSettleTime(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new IntResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).SlewSettleTime);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<IntResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SlewSettleTime")]
        public Response SlewSettleTime(int DeviceNumber, [FromForm] short SlewSettleTime, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SlewSettleTime = SlewSettleTime;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SlewToAltAz")]
        public Response SlewToAltAz(int DeviceNumber, [FromForm] double Azimuth, [FromForm]double Altitude, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SlewToAltAz(Azimuth, Altitude);

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SlewToAltAzAsync")]
        public Response SlewToAltAzAsync(int DeviceNumber, [FromForm] double Azimuth, [FromForm]double Altitude, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SlewToAltAzAsync(Azimuth, Altitude);

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SlewToCoordinates")]
        public Response SlewToCoordinates(int DeviceNumber, [FromForm] double RightAscension, [FromForm]double Declination, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SlewToCoordinates(RightAscension, Declination);

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SlewToCoordinatesAsync")]
        public Response SlewToCoordinatesAsync(int DeviceNumber, [FromForm] double RightAscension, [FromForm]double Declination, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SlewToCoordinatesAsync(RightAscension, Declination);

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SlewToTarget")]
        public Response SlewToTarget(int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SlewToTarget();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SlewToTargetAsync")]
        public Response SlewToTargetAsync(int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SlewToTargetAsync();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Slewing")]
        public BoolResponse Slewing(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).Slewing);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }



        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SyncToAltAz")]
        public Response SyncToAltAz(int DeviceNumber, [FromForm] double Azimuth, [FromForm]double Altitude, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SyncToAltAz(Azimuth, Altitude);

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SyncToCoordinates")]
        public Response SyncToCoordinates(int DeviceNumber, [FromForm] double RightAscension, [FromForm]double Declination, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SyncToCoordinates(RightAscension, Declination);

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SyncToTarget")]
        public Response SyncToTarget(int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).SyncToTarget();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/TargetDeclination")]
        public DoubleResponse TargetDeclination(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).TargetDeclination);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/TargetDeclination")]
        public Response TargetDeclination(int DeviceNumber, [FromForm] double TargetDeclination, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).TargetDeclination = TargetDeclination;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/TargetRightAscension")]
        public DoubleResponse TargetRightAscension(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).TargetRightAscension);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/TargetRightAscension")]
        public Response TargetRightAscension(int DeviceNumber, [FromForm] double TargetRightAscension, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).TargetRightAscension = TargetRightAscension;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Tracking")]
        public BoolResponse Tracking(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).Tracking);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Tracking")]
        public Response Tracking(int DeviceNumber, [FromForm] bool Tracking, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).Tracking = Tracking;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/TrackingRate")]
        public DriveRateResponse TrackingRate(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DriveRateResponse(ClientTransactionID, TransactionID, (ASCOM.Standard.Interfaces.DriveRate)DeviceManager.GetTelescope(DeviceNumber).TrackingRate);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DriveRateResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/TrackingRate")]
        public Response TrackingRate(int DeviceNumber, [FromForm] DriveRate TrackingRate, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).TrackingRate = TrackingRate;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/TrackingRates")]
        public DriveRatesResponse TrackingRates(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                List<ASCOM.Standard.Interfaces.DriveRate> rates = new List<ASCOM.Standard.Interfaces.DriveRate>();

                ITrackingRates data = DeviceManager.GetTelescope(DeviceNumber).TrackingRates;

                foreach (DriveRate rate in data)
                { 
                     rates.Add(rate);
                }
                return new DriveRatesResponse(ClientTransactionID, TransactionID, rates);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DriveRatesResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/UTCDate")]
        public DateTimeResponse UTCDate(int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            try
            {
                return new DateTimeResponse(ClientTransactionID, TransactionID, DeviceManager.GetTelescope(DeviceNumber).UTCDate);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DateTimeResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/UTCDate")]
        public Response UTCDate(int DeviceNumber, [FromForm] DateTime UTCDate, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).UTCDate = UTCDate;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/UnPark")]
        public Response UnPark(int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetTelescope(DeviceNumber).UnPark();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        #region IDisposable Members

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Dispose")]
        public Response Dispose(int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote Dispose
                DeviceManager.GetTelescope(DeviceNumber).Dispose();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        #endregion IDisposable Members

    }
}