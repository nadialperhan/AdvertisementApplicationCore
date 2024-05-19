using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Entites;
using AdvertisementApp.UI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProvidedServiceService _providedService;
        private readonly IAdvertisementService _advertisementService;
        public HomeController(IProvidedServiceService providedService, IAdvertisementService advertisementService)
        {
            _providedService = providedService;
            _advertisementService = advertisementService;
        }
        public async Task< IActionResult> Index()
        {
            var data =await _providedService.GetAllAsync();
            return this.ResponseView(data);
        }
        public async Task< IActionResult> HumanResource()
        {
            var response =await _advertisementService.GetActives();
            return this.ResponseView(response);
        }
    }
}
