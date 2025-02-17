using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using InvestSure.Domain.Entities;
using InvestSure.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace InvestSure.Infra.Repository
{
    public class ExternalAPIRepository : IExternalAPIRepository
    {

        private IConfiguration _configuration;
        public ExternalAPIRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ExchangeRateResponse> GetAsync(string currencyPayingWith)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{_configuration["ExchangeRateResponse:path"]}/{currencyPayingWith}");
                if (response.IsSuccessStatusCode)
                {
                    var responseToString = await response.Content.ReadAsStringAsync();
                    ExchangeRateResponse exchangeRateResponse = JsonSerializer.Deserialize<ExchangeRateResponse>(responseToString,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                        );
                    return exchangeRateResponse;

                }
                else
                {
                    throw new Exception("Houve um erro ao gravar dados cambiais");
                }
            }
        }
    }
}
