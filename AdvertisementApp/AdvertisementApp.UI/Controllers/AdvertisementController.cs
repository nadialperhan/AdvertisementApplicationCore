﻿using Advertisement.Dto;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.UI.Extensions;
using AdvertisementApp.UI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvertisementApp.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IAdvertisementAppUserService  _advertisementAppUserService;
        private readonly IMapper  _mapper;

        public AdvertisementController(IAppUserService appUserService, IAdvertisementAppUserService advertisementAppUserService, IMapper mapper)
        {
            _appUserService = appUserService;
            _advertisementAppUserService = advertisementAppUserService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Send(int advertisementId)
        {
            var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
            var userresponse = await _appUserService.GeTByIdAsync<AppUserListDto>(userId);

            ViewBag.GenderId = userresponse.Data.GenderID;
            var items = Enum.GetValues(typeof(MilitaryStatusType));

            var list = new List<MilitaryStatusListDto>();
            foreach (int item in items)
            {
                list.Add(new MilitaryStatusListDto()
                {
                    Id = item,
                    Definition = Enum.GetName(typeof(MilitaryStatusType), item)
                });
            }
            ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definition");
            return View(new AdvertisementAppUserCreateModel()
            {
                AdvertisementId = advertisementId,
                AppUserId = userId
            });
        }
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Send(AdvertisementAppUserCreateModel model)
        {
            var mappedDto = _mapper.Map<AdvertisementAppUserCreateDto>(model);
            if (model.CvFile!=null)
            {
                var filename = Guid.NewGuid().ToString();
                var extName = Path.GetExtension(model.CvFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvFiles", filename + extName);// filename+extName;
                var stream = new FileStream(path, FileMode.Create);
                await model.CvFile.CopyToAsync(stream);
                mappedDto.CvPath = path;
            }
            var result = await _advertisementAppUserService.CreateAsync(mappedDto);
            if (result.ResponseType==Common.ResponseType.ValidationError)
            {
                foreach (var error in result.ValidationErrors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
                var userresponse = await _appUserService.GeTByIdAsync<AppUserListDto>(userId);

                ViewBag.GenderId = userresponse.Data.GenderID;
                var items = Enum.GetValues(typeof(MilitaryStatusType));

                var list = new List<MilitaryStatusListDto>();
                foreach (int item in items)
                {
                    list.Add(new MilitaryStatusListDto()
                    {
                        Id = item,
                        Definition = Enum.GetName(typeof(MilitaryStatusType), item)
                    });
                }
                ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definition");
                return View(model);
            }
            return this.ResponseRedirectAction(result,"HumanResource","Home");
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task< IActionResult> List(AdvertisementAppUserStatusType type)
        {
            var list=await _advertisementAppUserService.Getlist(AdvertisementAppUserStatusType.Basvurdu);
            return View(list);
        }
        [Authorize(Roles ="Admin")]
        public async Task< IActionResult> SetStatus(int AdvertisementAppUserId, AdvertisementAppUserStatusType type)
        {
            await _advertisementAppUserService.SetStatusAsync(AdvertisementAppUserId,type);
            return RedirectToAction("List", "Advertisement");
        }
        [HttpGet]
        [Authorize(Roles =("Admin"))]
        public async Task<IActionResult> ApprovedList()
        {
            var approvelist =await _advertisementAppUserService.Getlist(AdvertisementAppUserStatusType.Mülakat);
            return View(approvelist);
        }
        [HttpGet]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> RejectedList()
        {
            var rejectedlist = await _advertisementAppUserService.Getlist(AdvertisementAppUserStatusType.Olumsuz);
            return View(rejectedlist);
        }
    }
}
