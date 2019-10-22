using ASCOM.Alpaca.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TelescopeSimulator.Alpaca.Controllers
{
    public class ManagementController : Controller
    {
        private static uint transactionID = 0;

        private static uint TransactionID
        {
            get
            {
                return transactionID++;
            }
        }

        [HttpGet]
        [Route("management/apiversions")]
        public IntListResponse GetApiVersions(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            return new IntListResponse(ClientTransactionID, TransactionID, new int[1] { 1 });
        }
    }
}