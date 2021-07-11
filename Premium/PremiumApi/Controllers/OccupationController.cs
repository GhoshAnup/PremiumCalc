using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PremiumApi.Services.Interfaces;

namespace PremiumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationController : ControllerBase
    {
        private readonly IOccupationService occupationService;
        public OccupationController(IOccupationService occupationService)
        {
            this.occupationService = occupationService;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var result = occupationService.GetOccupations();
            return Ok(result);
        }
    }
}
