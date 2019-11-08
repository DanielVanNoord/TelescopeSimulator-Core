using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System.Net.Http;
using System.Net.Http.Headers;

using System.IO;
using System.Reflection;
using ASCOM.Alpaca.Responses;

namespace ASCOM.Alpaca.Controllers
{
    public class RotatorController : Controller
    {
        private const string APIRoot = "api/v1/rotator/";

        private static uint transactionID = 0;

        private static uint TransactionID
        {
            get
            {
                return transactionID++;
            }
        }

        #region Common Methods

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/action")]
        public StringResponse PutAction(int DeviceNumber, [FromForm] string Action, [FromForm] string Parameters, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).Action(Action, Parameters));
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
                DeviceManager.GetRotator(DeviceNumber).CommandBlind(Command, Raw);
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
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).CommandBool(Command, Raw));
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
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).CommandString(Command, Raw));
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
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).Connected);
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
                DeviceManager.GetRotator(DeviceNumber).Connected = Connected;               
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
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).Description);
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
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).DriverInfo);
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
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).DriverVersion);
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
                return new IntResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).InterfaceVersion);
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
                return new StringResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).Name);
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
                return new StringListResponse(ClientTransactionID, TransactionID, new List<string>(DeviceManager.GetRotator(DeviceNumber).SupportedActions as IEnumerable<string>));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringListResponse>(ex, ClientTransactionID, TransactionID);
            }
        }
        #endregion

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/canreverse")]
        public BoolResponse GetCanReverse(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).CanReverse);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/ismoving")]
        public BoolResponse GetIsMoving(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).IsMoving);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/position")]
        public DoubleResponse GetPosition(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).Position);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/reverse")]
        public BoolResponse GetReverse(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).Reverse);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/reverse")]
        public Response PutReverse(int DeviceNumber, [FromForm] bool Reverse, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetRotator(DeviceNumber).Reverse = Reverse;
                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/stepsize")]
        public DoubleResponse GetStepSize(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).StepSize);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/targetposition")]
        public DoubleResponse GetTargetPosition(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new DoubleResponse(ClientTransactionID, TransactionID, DeviceManager.GetRotator(DeviceNumber).TargetPosition);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<DoubleResponse>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/halt")]
        public Response PutHalt(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetRotator(DeviceNumber).Halt();
                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/move")]
        public Response PutMove(int DeviceNumber, [FromForm] double Position, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            Exception error = null;
            try
            {
                DeviceManager.GetRotator(DeviceNumber).Move((float)Position);
                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/moveabsolute")]
        public Response PutMoveAbsolute(int DeviceNumber, [FromForm] double Position, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetRotator(DeviceNumber).MoveAbsolute((float)Position);
                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = TransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, TransactionID);
            }
        }
    }
}
