using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PremiumApi.Models;
using PremiumApi.Services.Interfaces;

namespace PremiumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatePremiumController : ControllerBase
    {
        private readonly IPremiumCalculatorService premiumCalculatorService;

        public CalculatePremiumController(IPremiumCalculatorService premiumCalculatorService)
        {
            this.premiumCalculatorService = premiumCalculatorService;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "" };
        }   
        [HttpPost]
        public ActionResult Post([FromBody] UserDetail userDetail)
        {
            var result = premiumCalculatorService.CalculatePremium(userDetail);
            return Ok(result);
        }       
    }
}
