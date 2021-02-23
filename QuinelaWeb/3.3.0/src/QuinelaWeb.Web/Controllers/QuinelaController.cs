using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using AutoMapper;
using Microsoft.AspNet.Identity;
using QuinelaWeb.Authorization.Roles;
using QuinelaWeb.Authorization.Users;
using QuinelaWeb.MultiTenancy;
using QuinelaWeb.Partidos;
using QuinelaWeb.Partidos.Dto;
using QuinelaWeb.Quinelas;
using QuinelaWeb.Quinelas.Dto;
using QuinelaWeb.Users;
using QuinelaWeb.Usuarios;
using QuinelaWeb.Usuarios.Dto;
using QuinelaWeb.Web.Models.Quinelas;
using QuinelaWeb.Web.Utilitario;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuinelaWeb.Web.Controllers {
    [AbpMvcAuthorize]
    public class QuinelaController : QuinelaWebControllerBase {

        private readonly IQuinelaAppService _QuinelaAppService;
        private readonly IAbpSession _session;
        private readonly IUserAppService _userAppService;
        private readonly IUsuarioAppService _UsuarioAppService;
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IPartidoAppService _PartidoAppService;
        public QuinelaController(IQuinelaAppService InitQuinelaAppService, IAbpSession session, IUserAppService InitUserAppService,
              IUsuarioAppService InitUsuarioAppService,
                 TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManager unitOfWorkManager,
            IMultiTenancyConfig multiTenancyConfig,
             IPartidoAppService InitPartidoAppService) {
            _QuinelaAppService = InitQuinelaAppService;
            _session = session;
            _userAppService = InitUserAppService;
            _UsuarioAppService = InitUsuarioAppService;
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWorkManager = unitOfWorkManager;
            _multiTenancyConfig = multiTenancyConfig;
            _PartidoAppService = InitPartidoAppService; 
        }

        // GET: Quinela
        public ActionResult Index() {
            List<QuinelaViewModel> vQuinelaViewModel = new List<QuinelaViewModel>();
            List<PartidoDto> vListPartido = _PartidoAppService.GetList();
            PartidoDto vPartido = vListPartido.FirstOrDefault();
            long vId= AbpSession.GetUserId();
            var users = _userManager.Users.Where(u => u.Id == vId).ToList().FirstOrDefault();
            UsuarioDto vUsuarioDto =   _UsuarioAppService.GetByCorreo(users.EmailAddress);
            if(users.UserName == "admin") {
                ViewData["Anfitrion"] = "Anfitrion";
            }

            if (vUsuarioDto != null) {
                if (!vUsuarioDto.Invitado) {
                    ViewData["Anfitrion"] = "Anfitrion";
                }

            }
            List<QuinelaDto> vRecord = _QuinelaAppService.GetListByUser();
            if (vRecord != null) {
                vQuinelaViewModel = Mapper.Map<List<QuinelaDto>, List<QuinelaViewModel>>(vRecord);
            }

            if (vPartido.PaisLocalGol > -1)
            {
                return RedirectToAction("Index","Home");
            }

            return View(vQuinelaViewModel);
        }

        // GET: Quinela/Details/5
        public ActionResult Details(int id) {
           
            return View();
        }

        // GET: Quinela/Create
        public ActionResult Create() {
            QuinelaViewModel vModel = new QuinelaViewModel();
            vModel = GetQuinelaViewModel();
            return View(vModel);
        }

        // POST: Quinela/Create
        [HttpPost]
        public ActionResult Create(QuinelaViewModel valQuinelaViewModel) {
            try {

                valQuinelaViewModel.Nombre = "001";
                valQuinelaViewModel.Id = 0;
                QuinelaDto vRecord = Mapper.Map<QuinelaViewModel, QuinelaDto>(valQuinelaViewModel);
                _QuinelaAppService.CreateAsync(vRecord);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Quinela/Edit/5
        public ActionResult Edit(int id) {
            try {
                QuinelaViewModel vModel = new QuinelaViewModel();
                QuinelaDto vRecord = _QuinelaAppService.Get(id);
                vModel = Mapper.Map<QuinelaDto, QuinelaViewModel>(vRecord);
                vModel.QuinelaDetalleJugadas = SetImage(vModel.QuinelaDetalleJugadas);
                return View(vModel);

            } catch {
                return View();
            }
        }

        // POST: Quinela/Edit/5
        [HttpPost]
        public ActionResult Edit(QuinelaViewModel valQuinelaViewModel) {
            try {
                QuinelaDto vRecord = Mapper.Map<QuinelaViewModel, QuinelaDto>(valQuinelaViewModel);
                _QuinelaAppService.Update(vRecord);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Quinela/Delete/5
        public ActionResult Delete(int id) {
            try {

                _QuinelaAppService.Delete(id);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // POST: Quinela/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            try {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        QuinelaViewModel GetQuinelaViewModel() {
            QuinelaViewModel vRecord = new QuinelaViewModel();
            vRecord.Nombre = "Demo";
            vRecord.QuinelaDetalleJugadas = new List<QuinelaDetalle>();
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/Partidos.txt"));
            var vLineas = fileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var vPartido in vLineas) {
                string[] vData = vPartido.Split(';');
                vRecord.QuinelaDetalleJugadas.Add(SetQuinelaDetalle(0, vData[0], Convert.ToInt32(vData[1]), vData[2], vData[3], 0, vData[4], 0, 1));
            }
            return vRecord;
        }

        QuinelaDetalle SetQuinelaDetalle(int valId, string valFecha, int valNumero, string valGrupo, string valPaisLocal, int valPaisLocalGol,
            string valPaisVisitante, int valPaisVisitanteGol, int valQuinelaId) {
            QuinelaDetalle vResult = new QuinelaDetalle();
            vResult.Id = valId;
            vResult.Numero = valNumero;
            vResult.Fecha = valFecha;
            vResult.Grupo = valGrupo;
            vResult.PaisLocal = valPaisLocal;
            vResult.PaisLocalGol = valPaisLocalGol;
            vResult.PaisVisitante = valPaisVisitante;
            vResult.PaisVisitanteGol = valPaisVisitanteGol;
            vResult.QuinelaId = valQuinelaId;
            vResult.ImagePathLocal = GetUrl(valPaisLocal);
            vResult.ImagePathVisitante = GetUrl(valPaisVisitante);
            return vResult;
        }
        List<QuinelaDetalle> SetImage(List<QuinelaDetalle> valQuinelaDetalle) {
            List<QuinelaDetalle> vResult = new List<QuinelaDetalle>();
            foreach (var vData in valQuinelaDetalle) {
                vData.ImagePathVisitante = GetUrl(vData.PaisVisitante);
                vData.ImagePathLocal = GetUrl(vData.PaisLocal);
                vResult.Add(vData);
            }
            return vResult;
        }

        List<QuinelaResultadoDetalle> SetImage(List<QuinelaResultadoDetalle> valQuinelaDetalle) {
            List<QuinelaResultadoDetalle> vResult = new List<QuinelaResultadoDetalle>();
            foreach (var vData in valQuinelaDetalle) {
                vData.ImagePathVisitante = GetUrl(vData.PaisVisitante);
                vData.ImagePathLocal = GetUrl(vData.PaisLocal);
                vResult.Add(vData);
            }
            return vResult;
        }

        

        string GetUrl(string valPais) {
            string vResult = string.Empty;
            vResult = "~/Images/" + valPais + ".png";
            return vResult;
        }


        // GET: Quinela/Edit/5
        public ActionResult Invitar() {
            try {
                QuinelaInvitarViewModel vModel = new QuinelaInvitarViewModel();
             
                return View(vModel);

            } catch {
                return View();
            }
        }

        // POST: Quinela/Edit/5
        [HttpPost]
        public async Task<ActionResult> Invitar(QuinelaInvitarViewModel valQuinelaViewModel) {
            try {
                return await Register(valQuinelaViewModel);
                
            } catch {
                return View();
            }
        }


        private async Task<ActionResult> Register(QuinelaInvitarViewModel model) {
            try {
                CheckModelState();
                long vId = AbpSession.GetUserId();
                UsuarioDto vUsuarioInvitadoDto = new UsuarioDto();
                var users = _userManager.Users.Where(u => u.Id == vId).ToList().FirstOrDefault();
                vUsuarioInvitadoDto = _UsuarioAppService.GetByCorreo(users.EmailAddress);
                if(vUsuarioInvitadoDto == null) {
                    vUsuarioInvitadoDto = new UsuarioDto();
                    vUsuarioInvitadoDto.Correo = users.EmailAddress;
                    vUsuarioInvitadoDto.CorreoAnfitrion = users.EmailAddress;
                   
                }
                string vPassword = Generaclave(4);
                //Create user
                var user = new User {
                    Name = model.EmailAddress,
                    Surname = model.EmailAddress,
                    EmailAddress = model.EmailAddress,
                    IsActive = true
                };

                user.UserName = model.EmailAddress;
                user.Password = new PasswordHasher().HashPassword(vPassword);
                //Switch to the tenant
                _unitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant); //TODO: Needed?
                _unitOfWorkManager.Current.SetTenantId(AbpSession.GetTenantId());

                //Add default roles
                user.Roles = new List<UserRole>();
                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync()) {
                    user.Roles.Add(new UserRole { RoleId = defaultRole.Id });
                }

                //Save user
                CheckErrors(await _userManager.CreateAsync(user));
                UsuarioDto vUsuarioDto = new UsuarioDto();
                vUsuarioDto.Clave = vPassword;
                vUsuarioDto.Correo = model.EmailAddress;
                vUsuarioDto.Invitado = true;
                vUsuarioDto.TenantId = AbpSession.GetTenantId();
                vUsuarioDto.CorreoAnfitrion = vUsuarioInvitadoDto.Correo;
                EnviarInvitacion(vUsuarioInvitadoDto.Correo, vUsuarioDto);
                await _UsuarioAppService.RegisterAsync(vUsuarioDto);
                await _unitOfWorkManager.Current.SaveChangesAsync();
               
                return RedirectToAction("Index");
            } catch (UserFriendlyException ex) {
                ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
                ViewBag.ErrorMessage = ex.Message;
                return View("Invitar");
            }
        }


        private string Generaclave(int l) {
            Random aleatorio = new Random();
            string res = "";
            string[] vals = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            for (int i = 0; i <= l; i++) {
                res = res + vals[aleatorio.Next(vals.GetUpperBound(0) + 1)];
            }
            return res;
        }

        private void EnviarInvitacion(string valCorreoAnfitrion, UsuarioDto valUsuarioDto) {
            MailTool vMailTool = new MailTool();
            string body = string.Empty;
            string vDescripicion = "Fuiste invitado por:" + valUsuarioDto.CorreoAnfitrion + " a participar en la Quiniela web";
            body = PopulateBody(valUsuarioDto.Correo, valUsuarioDto.Clave, "https://quiniela.galac.com:30", vDescripicion);
            vMailTool.SendMail("Quinela web: Invitacion", valUsuarioDto.Correo, body);
        }
        private string PopulateBody(string valUserName, string valClave, string url, string valDescription) {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/CorreoPlantilla.html"))) {
                body = reader.ReadToEnd();
            }
            body = body.Replace("[Usuario]", valUserName);
            body = body.Replace("[clave]", valClave);
            body = body.Replace("[Description]", valDescription);
            body = body.Replace("{Url}", url);
           
            return body;
        }


        public ActionResult Ver(int id) {
            try {
                QuinelaResultadoViewModel vModel = new QuinelaResultadoViewModel();
                List<QuinelaDto> vListQuinela = _QuinelaAppService.GetList();
                QuinelaDto vRecord = vListQuinela.Where(p=>p.Id==id).FirstOrDefault();
                vModel = Mapper.Map<QuinelaDto, QuinelaResultadoViewModel>(vRecord);
                vModel.QuinelaDetalleJugadas = SetImage(vModel.QuinelaDetalleJugadas);
                return Estadistica(vModel);
            } catch {
                return View();
            }
        }

        public ActionResult Estadistica(QuinelaResultadoViewModel valModel) {
            List<QuinelaDto> vListQuinela = _QuinelaAppService.GetList();
            List<PartidoDto> vListPartido = _PartidoAppService.GetList();
            foreach (var DataDetalle in valModel.QuinelaDetalleJugadas) {
                PartidoDto vPartido = vListPartido.Where(p => p.Numero == DataDetalle.Numero).FirstOrDefault();
                if (vPartido.PaisLocalGol > -1) {
                    DataDetalle.PaisLocalGolResultado = vPartido.PaisLocalGol;
                    DataDetalle.PaisVisitanteGolResultado = vPartido.PaisVisitanteGol;
                } else {
                    DataDetalle.PaisLocalGolResultado = 0;
                    DataDetalle.PaisVisitanteGolResultado = 0;
                }
            }
            return View(valModel);
        }
    }
}
