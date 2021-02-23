using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using AutoMapper;
using QuinelaWeb.Partidos;
using QuinelaWeb.Partidos.Dto;
using QuinelaWeb.Quinelas;
using QuinelaWeb.Quinelas.Dto;
using QuinelaWeb.Web.Models.Quinelas;

namespace QuinelaWeb.Web.Controllers {
    [AbpMvcAuthorize]
    public class HomeController : QuinelaWebControllerBase {
        private readonly IQuinelaAppService _QuinelaAppService;
        private readonly IPartidoAppService _PartidoAppService;
        public HomeController(IQuinelaAppService InitQuinelaAppService, IPartidoAppService InitPartidoAppService) {
            _QuinelaAppService = InitQuinelaAppService;
            _PartidoAppService = InitPartidoAppService;
        }
        public ActionResult Index() {
            List<PartidoDto> vListPartido = _PartidoAppService.GetList();
            PartidoDto vPartido = vListPartido.FirstOrDefault();
            try
            {
                if (vPartido.PaisLocalGol > -1) {
                    return RedirectToAction("Estadistica");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        public ActionResult Estadistica()
        {
            List<QuinelaDto> vListQuinela = _QuinelaAppService.GetList();
            List<PartidoDto> vListPartido = _PartidoAppService.GetList();
            List<QuinelaEstadisticaViewModel> vListModel = new List<QuinelaEstadisticaViewModel>();
            List<QuinelaEstadisticaViewModel> vModel = new List<QuinelaEstadisticaViewModel>();
            foreach (var Data in vListQuinela)
            {
                QuinelaEstadisticaViewModel vRecord = new QuinelaEstadisticaViewModel();
                int vPuntuacion = 0;
                vRecord.Nombre = Data.Nombre;
                vRecord.Id = Data.Id;
                foreach (var DataDetalle in Data.QuinelaDetalleJugadas)
                {
                    PartidoDto vPartido = vListPartido.Where(p => p.Numero == DataDetalle.Numero).FirstOrDefault();
                    if (vPartido.PaisLocalGol > -1)
                    {
                        if ((vPartido.PaisLocalGol == DataDetalle.PaisLocalGol) && (vPartido.PaisVisitanteGol == DataDetalle.PaisVisitanteGol))
                        {
                            vPuntuacion += 3;
                        }
                        else if ((vPartido.PaisLocalGol == vPartido.PaisVisitanteGol) && (DataDetalle.PaisLocalGol == DataDetalle.PaisVisitanteGol))
                        {
                            vPuntuacion += 1;
                        }
                        else if ((vPartido.PaisLocalGol > vPartido.PaisVisitanteGol) && (DataDetalle.PaisLocalGol > DataDetalle.PaisVisitanteGol))
                        {

                            vPuntuacion += 1;
                        }
                        else if ((vPartido.PaisLocalGol < vPartido.PaisVisitanteGol) && (DataDetalle.PaisLocalGol < DataDetalle.PaisVisitanteGol))

                        {
                            vPuntuacion += 1;
                        }
                    }
                    vRecord.Puntos = vPuntuacion;
                    
                }
                vListModel.Add(vRecord);
            }
            vModel = vListModel.OrderByDescending(p => p.Puntos).ToList();
            return View(vModel);

        }
            public ActionResult Ver(int id) {
                try {
                    return RedirectToAction("Ver", "Quinela", new { id });

                } catch {
                    return View();
                }
            }
        }
    }
