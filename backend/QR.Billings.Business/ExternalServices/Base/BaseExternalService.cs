using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QR.Billings.Business.ExternalServices.Base
{
    public abstract class BaseExternalService
    {
        public StringContent CreateJsonContent<T>(T content, string contentType = "application/json")
        {
            var json = JsonSerializer.Serialize(content);
            return new StringContent(json, Encoding.UTF8, contentType);
        }

        public async Task<T?> DeserializeResponseContent<T>(HttpResponseMessage response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}
