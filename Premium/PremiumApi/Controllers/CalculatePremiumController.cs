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
        private readonly IOccupationService occupationService;

        public CalculatePremiumController(IPremiumCalculatorService premiumCalculatorService)
        {
            this.premiumCalculatorService = premiumCalculatorService;
           
        }
        [HttpGet]
        public ActionResult Get()
        {
            var result = string.Empty;
            return Ok(result);
        }   
        [HttpPost]
        public ActionResult Post([FromBody] UserDetail userDetail)
        {
            var result = premiumCalculatorService.CalculatePremium(userDetail);
            return Ok(result);
        }       
    }
}
