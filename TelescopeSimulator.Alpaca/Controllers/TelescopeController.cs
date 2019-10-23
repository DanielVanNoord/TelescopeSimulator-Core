using ASCOM.Alpaca.Responses;
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
        [Route(APIRoot + "{DeviceNumber}/action")]
        public StringResponse PutAction(int DeviceNumber, [FromForm] string Action, [FromForm] string Parameters, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/commandblind")]
        public Response PutCommandBlind(int DeviceNumber, [FromForm] string Command, [FromForm] bool Raw = false, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/commandbool")]
        public BoolResponse PutCommandBool(int DeviceNumber, [FromForm] string Command, [FromForm] bool Raw = false, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/commandstring")]
        public StringResponse PutCommandString(int DeviceNumber, [FromForm] string Command, [FromForm] bool Raw = false, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/connected")]
        public BoolResponse GetConnected(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/connected")]
        public Response PutConnected(int DeviceNumber, [FromForm] bool Connected, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/description")]
        public StringResponse GetDescription(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/driverinfo")]
        public StringResponse GetDriverInfo(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/driverversion")]
        public StringResponse GetDriverVersion(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/interfaceversion")]
        public IntResponse GetInterfaceVersion(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/name")]
        public StringResponse GetName(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/supportedactions")]
        public StringListResponse GetSupportedActions(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/abortslew")]
        public Response PutAbortSlew(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
                DeviceManager.GetTelescope(DeviceNumber).AbortSlew();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/alignmentmode")]
        public AlignmentModeResponse GetAlignmentMode(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new AlignmentModeResponse(ClientTransactionID, TransactionID, (ASCOM.Alpaca.Interfaces.AlignmentMode)DeviceManager.GetTelescope(DeviceNumber).AlignmentMode);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<AlignmentModeResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/altitude")]
        public DoubleResponse GetAltitude(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/aperturearea")]
        public DoubleResponse GetApertureArea(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/aperturediameter")]
        public DoubleResponse GetApertureDiameter(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/athome")]
        public BoolResponse GetAtHome(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/atpark")]
        public BoolResponse GetAtPark(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/AxisRates")]
        public AxisRatesResponse AxisRates(int DeviceNumber, [FromForm] ASCOM.DeviceInterface.TelescopeAxes Axis, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                List<ASCOM.Alpaca.Interfaces.AxisRate> rates = new List<ASCOM.Alpaca.Interfaces.AxisRate>();

                foreach (ASCOM.DeviceInterface.IRate rate in DeviceManager.GetTelescope(DeviceNumber).AxisRates(Axis))
                {
                    rates.Add(new ASCOM.Alpaca.Interfaces.AxisRate(rate.Minimum, rate.Maximum));
                }
                return new AxisRatesResponse(ClientTransactionID, TransactionID, rates);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<AxisRatesResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/azimuth")]
        public DoubleResponse GetAzimuth(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/canfindhome")]
        public BoolResponse GetCanFindHome(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CanMoveAxis")]
        public BoolResponse CanMoveAxis(int DeviceNumber, [FromForm] ASCOM.DeviceInterface.TelescopeAxes Axis, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/canpark")]
        public BoolResponse GetCanPark(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/canpulseguide")]
        public BoolResponse GetCanPulseGuide(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/cansetdeclinationrate")]
        public BoolResponse GetCanSetDeclinationRate(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/cansetguiderates")]
        public BoolResponse GetCanSetGuideRates(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/cansetpark")]
        public BoolResponse GetCanSetPark(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/cansetpierside")]
        public BoolResponse Getcansetpierside(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/cansetrightascensionrate")]
        public BoolResponse GetCanSetRightAscensionRate(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/cansettracking")]
        public BoolResponse GetCanSetTracking(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/canslew")]
        public BoolResponse GetCanSlew(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public BoolResponse CanSlewAltAz(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/canslewaltazasync")]
        public BoolResponse GetCanSlewAltAzAsync(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/canslewasync")]
        public BoolResponse GetCanSlewAsync(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/cansync")]
        public BoolResponse GetCanSync(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/cansyncaltaz")]
        public BoolResponse GetCanSyncAltAz(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/canunpark")]
        public BoolResponse GetCanUnpark(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/declination")]
        public DoubleResponse GetDeclination(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/declinationrate")]
        public DoubleResponse DeclinationRate(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/declinationrate")]
        public Response DeclinationRate(int DeviceNumber, [FromForm] double DeclinationRate, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public PointingStateResponse GetDestinationSideOfPier(int DeviceNumber, double RightAscension, double Declination, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new PointingStateResponse(ClientTransactionID, TransactionID, (ASCOM.Alpaca.Interfaces.PointingState)DeviceManager.GetTelescope(DeviceNumber).DestinationSideOfPier(RightAscension, Declination));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<PointingStateResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/doesrefraction")]
        public BoolResponse GetDoesRefraction(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/doesrefraction")]
        public Response PutDoesRefraction(int DeviceNumber, [FromForm] bool DoesRefraction, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
                DeviceManager.GetTelescope(DeviceNumber).DoesRefraction = DoesRefraction;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/equatorialsystem")]
        public EquatorialCoordinateTypeResponse GetEquatorialSystem(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new EquatorialCoordinateTypeResponse(ClientTransactionID, TransactionID, (ASCOM.Alpaca.Interfaces.EquatorialCoordinateType)DeviceManager.GetTelescope(DeviceNumber).EquatorialSystem);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<EquatorialCoordinateTypeResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/findhome")]
        public Response PutDoesRefraction(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
                DeviceManager.GetTelescope(DeviceNumber).FindHome();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/focallength")]
        public DoubleResponse GetFocalLength(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/guideratedeclination")]
        public DoubleResponse GetGuideRateDeclination(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        [Route(APIRoot + "{DeviceNumber}/guideratedeclination")]
        public Response PutGuideRateDeclination(int DeviceNumber, [FromForm] double GuideRateDeclination, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public DoubleResponse GetGuideRateRightAscension(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response PutGuideRateRightAscension(int DeviceNumber, [FromForm] double GuideRateRightAscension, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public BoolResponse GetIsPulseGuiding(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response MoveAxis(int DeviceNumber, [FromForm] ASCOM.DeviceInterface.TelescopeAxes Axis, [FromForm] double Rate, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response Park(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response PulseGuide(int DeviceNumber, [FromForm] ASCOM.DeviceInterface.GuideDirections Direction, [FromForm] int Duration, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public DoubleResponse GetRightAscensionn(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public DoubleResponse GetRightAscensionRate(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response PutRightAscensionRate(int DeviceNumber, [FromForm] double RightAscensionRate, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response PutSetPark(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public PointingStateResponse GetSideOfPier(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new PointingStateResponse(ClientTransactionID, TransactionID, (ASCOM.Alpaca.Interfaces.PointingState)DeviceManager.GetTelescope(DeviceNumber).SideOfPier);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<PointingStateResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/SideOfPier")]
        public Response PutSideOfPier(int DeviceNumber, [FromForm] ASCOM.Alpaca.Interfaces.PointingState SideOfPier, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
                DeviceManager.GetTelescope(DeviceNumber).SideOfPier = (ASCOM.DeviceInterface.PierSide)SideOfPier;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SiderealTime")]
        public DoubleResponse GetSiderealTime(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public DoubleResponse GetSiteElevation(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response PutSiteElevation(int DeviceNumber, [FromForm] double SiteElevation, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public DoubleResponse GetSiteLatitude(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response PutSiteLatituden(int DeviceNumber, [FromForm] double SiteLatitude, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public DoubleResponse GetSiteLongitude(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response PutSiteLongitude(int DeviceNumber, [FromForm] double SiteLongitude, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public IntResponse GetSlewSettleTime(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response PutSlewSettleTime(int DeviceNumber, [FromForm] short SlewSettleTime, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response PutSlewToAltAz(int DeviceNumber, [FromForm] double Azimuth, [FromForm]double Altitude, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response SlewToAltAzAsync(int DeviceNumber, [FromForm] double Azimuth, [FromForm]double Altitude, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response SlewToCoordinates(int DeviceNumber, [FromForm] double RightAscension, [FromForm]double Declination, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response SlewToCoordinatesAsync(int DeviceNumber, [FromForm] double RightAscension, [FromForm]double Declination, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response SlewToTarget(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response SlewToTargetAsync(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public BoolResponse Slewing(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response SyncToAltAz(int DeviceNumber, [FromForm] double Azimuth, [FromForm]double Altitude, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response SyncToCoordinates(int DeviceNumber, [FromForm] double RightAscension, [FromForm]double Declination, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public Response SyncToTarget(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public DoubleResponse TargetDeclination(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response PutTargetDeclination(int DeviceNumber, [FromForm] double TargetDeclination, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public DoubleResponse TargetRightAscension(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response PutTargetRightAscension(int DeviceNumber, [FromForm] double TargetRightAscension, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public BoolResponse Tracking(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response Tracking(int DeviceNumber, [FromForm] bool Tracking, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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
        public DriveRateResponse TrackingRate(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new DriveRateResponse(ClientTransactionID, TransactionID, (ASCOM.Alpaca.Interfaces.DriveRate)DeviceManager.GetTelescope(DeviceNumber).TrackingRate);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DriveRateResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/TrackingRate")]
        public Response TrackingRate(int DeviceNumber, [FromForm] ASCOM.Alpaca.Interfaces.DriveRate TrackingRate, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
                DeviceManager.GetTelescope(DeviceNumber).TrackingRate = (ASCOM.DeviceInterface.DriveRates) TrackingRate;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/TrackingRates")]
        public DriveRatesResponse TrackingRates(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                List<ASCOM.Alpaca.Interfaces.DriveRate> rates = new List<ASCOM.Alpaca.Interfaces.DriveRate>();

                ASCOM.DeviceInterface.ITrackingRates data = DeviceManager.GetTelescope(DeviceNumber).TrackingRates;

                foreach (ASCOM.DeviceInterface.DriveRates rate in data)
                {
                    rates.Add((ASCOM.Alpaca.Interfaces.DriveRate) rate);
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
        public DateTimeResponse UTCDate(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
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
        public Response UTCDate(int DeviceNumber, [FromForm] DateTime UTCDate, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
                DeviceManager.GetTelescope(DeviceNumber).UTCDate = UTCDate;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Unpark")]
        public Response Unpark(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
                DeviceManager.GetTelescope(DeviceNumber).Unpark();

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
        public Response Dispose(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote disconnects
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