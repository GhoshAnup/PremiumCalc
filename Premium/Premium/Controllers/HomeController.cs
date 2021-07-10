using System;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Premium.Constants;
using Premium.Models;
using Premium.Models.ViewModels;
using Premium.Services.Interface;

namespace Premium.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPremiumService premiumService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IPremiumService premiumService, IMapper mapper)
        {
            _logger = logger;
            this.premiumService = premiumService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public IActionResult Index()
        {           
            return View(premiumService.GetOccupationList());
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult Calculate(PremiumViewModel premiumViewModel)
        {          
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Premiums>(premiumViewModel);
                var response = premiumService.GetPremium(model).Result;
                if (response.IsSuccess)
                {
                    ViewBag.result = response.Premium;
                }
                else { ViewBag.error = response.ResponseMessage; }                 
            }
            else
            {
                ViewBag.error = Constant.WrongInput;
            }
            return View("Index", premiumService.GetOccupationList());
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
