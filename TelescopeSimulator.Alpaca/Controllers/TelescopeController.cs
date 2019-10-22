using ASCOM.Alpaca.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

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

        /*[HttpGet]
        [Route(APIRoot + "{DeviceNumber}/supportedactions")]
        public StringListResponse GetSupportedActions(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringListResponse(ClientTransactionID, TransactionID, new List<string>(DeviceManager.GetTelescope(DeviceNumber).SupportedActions));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringListResponse>(ex, ClientTransactionID, TransactionID);
            }
        }*/

        #endregion Common Methods

    }
}