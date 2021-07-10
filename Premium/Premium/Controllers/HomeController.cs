using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Premium.Constants;
using Premium.Models;
using Premium.Models.ViewModels;
using Premium.Services.Interface;

namespace Premium.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPremiumService premiumService;
        private readonly IMapper _mapper;

        public HomeController(IPremiumService premiumService, IMapper mapper)
        {
            this.premiumService = premiumService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public IActionResult Index()
        {           
            return View(premiumService.GetOccupationList());
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(PremiumViewModel premiumViewModel)
        {          
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Premiums>(premiumViewModel);
                var response = await premiumService.GetPremium(model);
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

    }
}
