using ASCOM.Alpaca.Controllers;
using ASCOM.Alpaca.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ASCOM.Alpaca.Controllers
{
    [ApiController]
    public class ManagementController : Controller
    {
        private static uint transactionID = 0;

        List<AlpacaConfiguredDevice> configuredDevices = new List<AlpacaConfiguredDevice>() { new AlpacaConfiguredDevice(DeviceManager.GetTelescope(0).Name, "Telescope", 0, "EA091417-66DF-4250-8287-FAFAF38A80A6") };

        private static uint TransactionID
        {
            get
            {
                return transactionID++;
            }
        }

        [HttpGet]
        [Route("management/apiversions")]
        public IntListResponse ApiVersions(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            return new IntListResponse(ClientTransactionID, TransactionID, new int[1] { 1 });
        }

        [HttpGet]
        [Route("management/v1/description")]
        public AlpacaDescriptionResponse Description(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.ProductVersion;
            return new AlpacaDescriptionResponse(ClientTransactionID, TransactionID, new AlpacaDeviceDescription(".Net Core Telescope Simulator", "ASCOM Initiative", version, "Unknown"));
        }

        [HttpGet]
        [Route("management/v1/configureddevices")]
        public AlpacaConfiguredDevicesResponse ConfiguredDevices(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            return new AlpacaConfiguredDevicesResponse(ClientTransactionID, TransactionID, null);
        }
    }
}