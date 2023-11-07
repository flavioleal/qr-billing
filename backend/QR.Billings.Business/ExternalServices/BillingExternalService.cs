﻿using Microsoft.Extensions.Options;
using QR.Billings.Business.Configuration;
using QR.Billings.Business.ExternalServices.Base;
using QR.Billings.Business.Interfaces.ExternalServices;
using QR.Billings.Business.IO.BillingExternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QR.Billings.Business.ExternalServices
{
    public class BillingExternalService : BaseExternalService, IBillingExternalService
    {
        public readonly HttpClient _httpClient;
        public readonly IOptions<ApiExternalSettings> _settings;

        public BillingExternalService(HttpClient httpClient, IOptions<ApiExternalSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<BillingExternalCancelOutput?> Cancel(string idTransaction)
        {
            _httpClient.BaseAddress = new Uri(_settings.Value.CancelBillingUrl);

            var jsonContent = CreateJsonContent(new BillingExternalCancelInput(idTransaction));
            var response = await _httpClient.PostAsync("/", jsonContent);

            if (response.StatusCode == HttpStatusCode.BadRequest) return null;

            return await DeserializeResponseContent<BillingExternalCancelOutput>(response);
        }

        public async Task<BillingExternalCreateOutput?> Create(decimal value)
        {
            _httpClient.BaseAddress = new Uri(_settings.Value.CreateBillingUrl);

            var jsonContent = CreateJsonContent(new BillingExternalCreateInput(value));
            var response = await _httpClient.PostAsync("/", jsonContent);

            if (response.StatusCode == HttpStatusCode.BadRequest) return null;

            return await DeserializeResponseContent<BillingExternalCreateOutput>(response);
        }
    }
}
