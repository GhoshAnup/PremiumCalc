using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Premium.Constants;
using Premium.Models;
using Premium.Models.Response;
using Premium.Models.ViewModels;
using Premium.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string ProjectApiUrl { get; set; }
        public PremiumService(IConfiguration config,
            IHttpClientFactory httpClientFactory)
        {
            this.config = config;
            _httpClientFactory = httpClientFactory ??
              throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public PremiumViewModel GetOccupationList()
        {
            var premiumViewModel = GetOccupationList(config["OccupationName/Type"]);
            return premiumViewModel;
        }  
        public async Task<PremiumResponse> GetPremium(Premiums premium)
        {
            var result = new PremiumResponse();
            ProjectApiUrl = config["ProjectApiUrl"];
            var ratingFactor = config[premium.OccupationType];
            if (!string.IsNullOrEmpty(ratingFactor))
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient();
                    var userDetail = new UserDetail
                    {
                        Age = Convert.ToInt32(premium.Age),
                        RatingFactor = Convert.ToDecimal(ratingFactor),
                        SumInsured = premium.SumInsured
                    };
                    var user = JsonConvert.SerializeObject(userDetail);
                    var stringContent = new StringContent(user, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(ProjectApiUrl, stringContent);
                    response.EnsureSuccessStatusCode();
                    result = JsonConvert.DeserializeObject<PremiumResponse>(await response.Content.ReadAsStringAsync());
                }
                catch(Exception)
                {
                    result.IsSuccess = false;
                    result.ResponseMessage = Constant.FaliureMessage ;
                }
            }
            else
            {
                result.IsSuccess = false;
                result.ResponseMessage = $"{Constant.WrongRatingFactor} {premium.OccupationType}.";
            }
            return result;
        }
        private PremiumViewModel GetOccupationList(string occupationList)
        {
            var premiumViewModel = new PremiumViewModel();
            List<Occupation> occupationItems = new List<Occupation>();
            if (!string.IsNullOrEmpty(occupationList))
            {
                var collection = occupationList.Split(',').ToList();
                foreach (var item in collection)
                {
                    var obj = item.Split('/');
                    if (obj.Length > 1)
                    {
                        var occupation = new Occupation
                        {
                            Value = obj[1],
                            Text = obj[0]
                        };
                        occupationItems.Add(occupation);
                    }
                }
            }
            else
            {
                var occupation = new Occupation
                {
                    Value = string.Empty,
                    Text = string.Empty
                };
                occupationItems.Add(occupation);
            }
            premiumViewModel.Occupation = occupationItems;
            return premiumViewModel;
        }
    }
}
