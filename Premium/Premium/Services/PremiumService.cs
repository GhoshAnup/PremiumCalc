﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Premium.Constants;
using Premium.Models;
using Premium.Models.Response;
using Premium.Models.ViewModels;
using Premium.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Premium.Services
{
    public class PremiumService : IPremiumService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration config;
        public string CalculatePremiumApiUrl { get; set; }
        public PremiumService(IConfiguration config,
            IHttpClientFactory httpClientFactory)
        {
            this.config = config;
            _httpClientFactory = httpClientFactory ??
              throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public async Task<PremiumViewModel> GetOccupationList()
        {
            var premiumViewModel = new PremiumViewModel
            {
                Occupation = await GetOccupations()
            };
            return premiumViewModel;
        }
        public async Task<PremiumResponse> GetPremium(Premiums premium)
        {
            var result = new PremiumResponse();
            CalculatePremiumApiUrl = config["CalculatePremiumApiUrl"];
            var ratingFactor = premium.FactorRating;
            if (!string.IsNullOrEmpty(ratingFactor))
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("PremiumService");
                    var userDetail = new UserDetail
                    {
                        Age = Convert.ToInt32(premium.Age),
                        RatingFactor = Convert.ToDecimal(ratingFactor),
                        SumInsured = premium.SumInsured
                    };
                    var user = JsonConvert.SerializeObject(userDetail);
                    var stringContent = new StringContent(user, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"api/CalculatePremium", stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsByteArrayAsync();
                        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                        result = System.Text.Json.JsonSerializer.Deserialize<PremiumResponse>(content, options);
                    }
                }
                catch (Exception)
                {
                    result.IsSuccess = false;
                    result.ResponseMessage = Constant.FaliureMessage;
                }
            }
            else
            {
                result.IsSuccess = false;
            }
            return result;
        }

        public async Task<List<OccupationFactor>> GetOccupations()
        {            
            var result = new List<OccupationFactor>();
            try
            {
                var uri = config["OccupationApiUrl"];
                var httpClient = _httpClientFactory.CreateClient("OccupationService");
                var response = await httpClient.GetAsync($"api/Occupation");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    result = System.Text.Json.JsonSerializer.Deserialize<List<OccupationFactor>>(content, options);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }      
    }
}
