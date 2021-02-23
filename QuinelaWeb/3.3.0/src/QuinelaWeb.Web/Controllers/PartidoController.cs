using Abp.Web.Mvc.Authorization;
using AutoMapper;
using QuinelaWeb.Authorization;
using QuinelaWeb.Partidos;
using QuinelaWeb.Partidos.Dto;
using QuinelaWeb.Web.Models.Partidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuinelaWeb.Web.Controllers {
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class PartidoController : QuinelaWebControllerBase {


        private readonly IPartidoAppService _PartidoAppService;

        public PartidoController(IPartidoAppService InitPartidoAppService) {
            _PartidoAppService = InitPartidoAppService;
        }

        // GET: Partido
        public ActionResult Index() {
            List<PartidoViewModel> vPartidoViewModel = new List<PartidoViewModel>();
            List<PartidoDto> vRecord = _PartidoAppService.GetList();
            vPartidoViewModel = Mapper.Map<List<PartidoDto>, List<PartidoViewModel>>(vRecord);
            return View(vPartidoViewModel);
        }

        // GET: Partido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Partido/Create
        public ActionResult Create()
        {
            PartidoViewModel vPartidoViewModel = new PartidoViewModel();
            return View(vPartidoViewModel);
        }

        // POST: Partido/Create
        [HttpPost]
        public ActionResult Create(PartidoViewModel valPartidoViewModel)
        {
            try {
                PartidoDto vRecord= Mapper.Map<PartidoViewModel,PartidoDto>(valPartidoViewModel);
                _PartidoAppService.CreateAsync(vRecord);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Edit/5
        public ActionResult Edit(int id) {
            PartidoDto vRecord = _PartidoAppService.Get(id);
            PartidoViewModel vModel = Mapper.Map<PartidoDto,PartidoViewModel>(vRecord);
            return View(vModel);
        }

        // POST: Partido/Edit/5
        [HttpPost]
        public ActionResult Edit(PartidoViewModel valPartidoViewModel)
        {
            try
            {
                PartidoDto vRecord = Mapper.Map<PartidoViewModel, PartidoDto>(valPartidoViewModel);
                _PartidoAppService.Update(vRecord);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partido/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
