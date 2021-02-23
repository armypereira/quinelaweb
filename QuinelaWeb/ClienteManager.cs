using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Session;
using Abp.UI;
using Galac.CRM.Configuracion;
using Galac.CRM.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Galac.CRM.Ventas {
    public class ClienteManager : DomainService, IClienteManager, ITransientDependency {
        private readonly IRepository<Cliente> _ClienteRepository;
        private readonly IRepository<ClienteDetalleContacto> _ClienteDetalleContactoRepository;
        private readonly IRepository<ClienteDetalleDireccion> _ClienteDetalleDireccionRepository;
        private readonly IRepository<ClienteDetalleDocumento> _ClienteDetalleDocumentoRepository;
        private readonly IRepository<Contacto> _ContactoRepository;
        private readonly IRepository<ContactoDetalleCorreo> _ContactoCorreoRepository;
        private readonly IRepository<ContactoDetalleDireccion> _ContactoDireccionRepository;
        private readonly IRepository<ContactoDetalleTelefono> _ContactoTelefonoRepository;
        private readonly IRepository<Vendedor> _VendedorRepository;
        private readonly IRepository<Ciudad> _CiudadRepository;
        private readonly IRepository<EstadoProvincia> _EstadoProvinciaRepository;
        private readonly IRepository<MunicipioCanton> _MunicipioCantonRepository;
        private readonly IRepository<Parroquia> _ParroquiaRepository;
        private readonly IRepository<SectorDeNegocio> _SectorDeNegocioRepository;
        private readonly IRepository<UsuarioVendedor> _UsuarioVendedorRepository;
        public IAbpSession AbpSession { get; set; }

        public ClienteManager(IRepository<Cliente> InitClienteRepository,
            IRepository<ClienteDetalleContacto> InitClienteDetalleContacto,
            IRepository<ClienteDetalleDireccion> InitClienteDetalleDireccion,
            IRepository<ClienteDetalleDocumento> InitClienteDetalleDocumento,
            IRepository<Contacto> InitContacto,
            IRepository<ContactoDetalleCorreo> InitContactoDetalleCorreos,
            IRepository<ContactoDetalleDireccion> InitContactoDetalleDireccion,
            IRepository<ContactoDetalleTelefono> InitContactoDetalleTelefono,
            IRepository<Vendedor> InitVendedor,
            IRepository<Ciudad> InitCiudad,
            IRepository<EstadoProvincia> InitEstadoProvincia,
            IRepository<MunicipioCanton> InitMunicipioCanton,
            IRepository<Parroquia> InitParroquia,
            IRepository<SectorDeNegocio> InitSectorDeNegocio,
            IRepository<UsuarioVendedor> InitUsuarioVendedorRepository) {
            _ClienteRepository = InitClienteRepository;
            _ClienteDetalleContactoRepository = InitClienteDetalleContacto;
            _ClienteDetalleDireccionRepository = InitClienteDetalleDireccion;
            _ClienteDetalleDocumentoRepository = InitClienteDetalleDocumento;
            _ContactoRepository = InitContacto;
            _ContactoCorreoRepository = InitContactoDetalleCorreos;
            _ContactoDireccionRepository = InitContactoDetalleDireccion;
            _ContactoTelefonoRepository = InitContactoDetalleTelefono;
            _VendedorRepository = InitVendedor;
            _CiudadRepository = InitCiudad;
            _EstadoProvinciaRepository = InitEstadoProvincia;
            _MunicipioCantonRepository = InitMunicipioCanton;
            _ParroquiaRepository = InitParroquia;
            _SectorDeNegocioRepository = InitSectorDeNegocio;
            _UsuarioVendedorRepository = InitUsuarioVendedorRepository;
            AbpSession = NullAbpSession.Instance;

        }

        public IEnumerable<Cliente> BuscarPorFiltro(string valFiltro, int valTenantId) {
            IEnumerable<Cliente> vResult = new List<Cliente>();
            UsuarioVendedor vUsuarioVendedor = new UsuarioVendedor();
            string vAsterisco = "*";
            var vFiltro = valFiltro.ToCharArray().Where(p => !vAsterisco.Contains(p)).ToArray();
            string vCadena = new String(vFiltro);
            if (AplicarCarteraDeCliente(ref vUsuarioVendedor)) {
                vResult = _ClienteRepository.GetAllList().Where(p => (p.Nombre.CaseInsensitiveContains(vCadena) || p.NroIdentificacion.CaseInsensitiveContains(vCadena)) && p.Status == Cliente.eStatusCliente.Activo && p.VendedorId == vUsuarioVendedor.VendedorId);
            } else {
                vResult = _ClienteRepository.GetAllList().Where(p => (p.Nombre.CaseInsensitiveContains(vCadena) || p.NroIdentificacion.CaseInsensitiveContains(vCadena)) && p.Status == Cliente.eStatusCliente.Activo );
            }
            return vResult;
        }

        public Task Crear(Cliente valCliente) {
            try {
                var vResult = _ClienteRepository.FirstOrDefault(p => p.NroIdentificacion == valCliente.NroIdentificacion && p.TenantId == valCliente.TenantId);
                if (vResult == null) {
                    _ClienteRepository.Insert(valCliente);
                    return Task.CompletedTask;
                } else {
                    throw new UserFriendlyException("Cliente ya se encuentra registrado");
                }
            } catch (Exception vEx) {
                throw new UserFriendlyException(vEx.ToString());
            }
        }

        public void Modificar(Cliente valCliente) {
            var vRecord = _ClienteRepository.FirstOrDefault(p => p.Id == valCliente.Id && p.TenantId == valCliente.TenantId);
            if (vRecord != null) {
                if (ExisteCliente(valCliente.NroIdentificacion) && valCliente.Id != _ClienteRepository.FirstOrDefault(p => p.NroIdentificacion == valCliente.NroIdentificacion).Id) {
                    throw new UserFriendlyException("Este nro de identificación ya se encuentra registrado ");
                } else {
                    vRecord.NroIdentificacion = valCliente.NroIdentificacion;
                }
                vRecord.Nombre = valCliente.Nombre;
                vRecord.Observacion = valCliente.Observacion;
                vRecord.EsExtranjero = valCliente.EsExtranjero;
                vRecord.EsContribuyente = valCliente.EsContribuyente;
                vRecord.TipoDeSujeto = valCliente.TipoDeSujeto;
                vRecord.Telefono = valCliente.Telefono;
                vRecord.Fax = valCliente.Fax;
                vRecord.CorreoElectronico = valCliente.CorreoElectronico;
                vRecord.TipoDocumento = valCliente.TipoDocumento;
                vRecord.VendedorId = valCliente.VendedorId;
                vRecord.SectorNegocioId = valCliente.SectorNegocioId;
                vRecord.NivelDePrecio = valCliente.NivelDePrecio;
                vRecord.Status = valCliente.Status;
                vRecord.StatusCRM = valCliente.StatusCRM;
                vRecord.EstaSincronizado = valCliente.EstaSincronizado;
                vRecord.FechaSincronizacion = valCliente.FechaSincronizacion;
                ModificarClienteDetalleDireccion(valCliente, ref vRecord);
                ModificarClienteDetalleContacto(valCliente, ref vRecord);
                ModificarClienteDetalleDocumento(valCliente, ref vRecord);
            }
        }

        public void ModificarClienteDetalleDireccion(Cliente valCliente, ref Cliente refRecord) {

            List<int> vIdEnVista = new List<int>();
            List<int> vIdEnBDD = new List<int>();
            foreach (ClienteDetalleDireccion vDireccion in valCliente.ClienteDetalleDirecciones.ToList()) {
                vIdEnVista.Add(vDireccion.Id);
            }
            foreach (ClienteDetalleDireccion vDireccion in _ClienteDetalleDireccionRepository.GetAllList(p => p.ClienteId == valCliente.Id && p.IsDeleted == false)) {
                vIdEnBDD.Add(vDireccion.Id);
            }
            var vItemsABorrar = vIdEnBDD.Except(vIdEnVista);
            foreach (int vItem in vItemsABorrar) {
                _ClienteDetalleDireccionRepository.Delete(vItem);
            }

            foreach (ClienteDetalleDireccion vDetalleDireccion in valCliente.ClienteDetalleDirecciones) {
                var vDireccion = _ClienteDetalleDireccionRepository.FirstOrDefault(vDetalleDireccion.Id);
                if (vDireccion == null) {
                    refRecord.ClienteDetalleDirecciones.Add(vDetalleDireccion);
                } else {
                    vDireccion.Direccion = vDetalleDireccion.Direccion;
                    vDireccion.ParroquiaId = vDetalleDireccion.ParroquiaId;
                    vDireccion.PuntoDeReferencia = vDetalleDireccion.PuntoDeReferencia;
                    vDireccion.Ubigeo = vDetalleDireccion.Ubigeo;
                    vDireccion.ZonaPostal = vDetalleDireccion.ZonaPostal;
                    vDireccion.TipoDireccion = vDetalleDireccion.TipoDireccion;
                    vDireccion.CiudadId = vDetalleDireccion.CiudadId;
                }
            }
        }

        public void ModificarClienteDetalleContacto(Cliente valCliente, ref Cliente vRecord) {
            List<int> vIdEnVista = new List<int>();
            List<int> vIdEnBDD = new List<int>();
            foreach (ClienteDetalleContacto vContacto in valCliente.ClienteDetalleContactos.ToList()) {
                vIdEnVista.Add(vContacto.ContactoId);
            }
            foreach (ClienteDetalleContacto vContacto in _ClienteDetalleContactoRepository.GetAllList(p => p.ClienteId == valCliente.Id && p.IsDeleted == false)) {
                vIdEnBDD.Add(vContacto.ContactoId);
            }
            var vItemsABorrar = vIdEnBDD.Except(vIdEnVista);
            foreach (int vItem in vItemsABorrar) {
                _ClienteDetalleContactoRepository.Delete(p => p.ContactoId == vItem && p.ClienteId == valCliente.Id);
            }

            foreach (ClienteDetalleContacto vDetalleContacto in valCliente.ClienteDetalleContactos) {
                var vContacto = _ClienteDetalleContactoRepository.FirstOrDefault(p => p.ContactoId == vDetalleContacto.ContactoId && p.ClienteId == vDetalleContacto.ClienteId);
                if (vContacto == null) {
                    vRecord.ClienteDetalleContactos.Add(vDetalleContacto);
                }
            }
        }

        public void ModificarClienteDetalleDocumento(Cliente valCliente, ref Cliente vRecord) {

            List<int> vIdEnVista = new List<int>();
            List<int> vIdEnBDD = new List<int>();
            foreach (ClienteDetalleDocumento vDocumento in valCliente.ClienteDetalleDocumentos.ToList()) {
                vIdEnVista.Add(vDocumento.Id);
            }
            foreach (ClienteDetalleDocumento vDocumento in _ClienteDetalleDocumentoRepository.GetAllList(p => p.ClienteId == valCliente.Id && p.IsDeleted == false)) {
                vIdEnBDD.Add(vDocumento.Id);
            }
            var vItemsABorrar = vIdEnBDD.Except(vIdEnVista);
            foreach (int vItem in vItemsABorrar) {
                _ClienteDetalleDocumentoRepository.Delete(vItem);
            }

            foreach (ClienteDetalleDocumento vDetalleDocumento in valCliente.ClienteDetalleDocumentos) {
                var vDocumento = _ClienteDetalleDocumentoRepository.FirstOrDefault(vDetalleDocumento.Id);
                if (vDocumento == null) {
                    vRecord.ClienteDetalleDocumentos.Add(vDetalleDocumento);
                }
            }
        }

        public void Eliminar(Cliente valCliente) {
            try {
                var vResult = _ClienteRepository.FirstOrDefault(p => p.Id == valCliente.Id);
                if (vResult != null) {
                    _ClienteRepository.Delete(valCliente.Id);
                } else {
                    throw new UserFriendlyException("El cliente no existe");
                }
            } catch (Exception vEx) {
                throw vEx;
            }
        }
        public IEnumerable<Cliente> ListarPorTenant(int valTenantId) {
            return _ClienteRepository.GetAll().Where(p => p.TenantId == valTenantId);
        }

        public List<Contacto> BuscarContactoPorNombre(string valNombre, int valTenantId) {
            var vContactos = _ContactoRepository.GetAllIncluding(p => p.ContactoDetalleCorreos,
                                                                 p => p.ContactoDetalleDirecciones,
                                                                 p => p.ContactoDetalleTelefonos).Where(p => p.Nombre.Contains(valNombre) && p.TenantId == valTenantId).ToList();
            return vContactos;
        }

        public List<Vendedor> ConsultarVendedoresPorTenant(int valTenantId) {
            return _VendedorRepository.GetAll().Where(p => p.TenantId == valTenantId).ToList();
        }

        public List<Ciudad> ConsultarCiudadPorEstadoProvincia(int valEstadoProvinciaId) {
            return _CiudadRepository.GetAll().Where(p => p.EstadoProvinciaId == valEstadoProvinciaId).ToList();
        }

        public List<EstadoProvincia> ListarEstadoProvincia() {
            return _EstadoProvinciaRepository.GetAll().ToList();
        }

        public List<MunicipioCanton> ListarMunicipioCanton() {
            return _MunicipioCantonRepository.GetAll().ToList();
        }

        public List<Parroquia> ListarParroquia() {
            return _ParroquiaRepository.GetAll().ToList();
        }

        public List<SectorDeNegocio> ConsultarSectorDeNegocioPorTenant(int valTenantId) {
            return _SectorDeNegocioRepository.GetAll().Where(p => p.TenantId == valTenantId).ToList();
        }

        public MunicipioCanton ObtenerMunicipioCantonPorParroquiaId(int valId) {
            return _MunicipioCantonRepository.FirstOrDefault(p => p.Id == valId);
        }

        public Ciudad ObtenerCiudadPorId(int valId) {
            return _CiudadRepository.FirstOrDefault(p => p.Id == valId);
        }

        public EstadoProvincia ObtenerEstadoProvinciaPorMunicipioId(int valId) {
            return _EstadoProvinciaRepository.FirstOrDefault(p => p.Id == valId);

        }
        public Parroquia ObtenerParroquiaPorId(int valId) {
            return _ParroquiaRepository.FirstOrDefault(p => p.Id == valId);
        }

        public List<Contacto> ObtenerContactosDeCliente(int valClienteId) {
            List<Contacto> vResult = new List<Contacto>();
            var vDetallesContacto = _ClienteDetalleContactoRepository.GetAllIncluding(p => p.Contacto).Where(p => p.ClienteId == valClienteId).ToList();
            foreach (ClienteDetalleContacto vDetalle in vDetallesContacto) {
                vResult.Add(_ContactoRepository.GetAllIncluding(p => p.ContactoDetalleCorreos,
                      p => p.ContactoDetalleDirecciones,
                      p => p.ContactoDetalleTelefonos).FirstOrDefault(p => p.Id == vDetalle.ContactoId));
            }
            return vResult;
        }
        public bool ExisteCliente(string valNroIdentificacion) {
            Cliente vCliente = _ClienteRepository.FirstOrDefault(p => p.NroIdentificacion == valNroIdentificacion);
            bool vResult = vCliente == null ? false : true;
            return vResult;
        }

        public Cliente ConsultarCliente(int valClienteId, int valTenantId) {
            return _ClienteRepository.GetAllIncluding(p => p.ClienteDetalleContactos,
                                                      p => p.ClienteDetalleDirecciones,
                                                      p => p.ClienteDetalleDocumentos,
                                                      p => p.SectorNegocio,
                                                      p => p.Vendedor
                                                      ).FirstOrDefault(p => p.Id == valClienteId && p.TenantId == valTenantId);
        }

        bool AplicarCarteraDeCliente(ref UsuarioVendedor refUsuarioVendedor) {
            bool vResult = false;
            refUsuarioVendedor = _UsuarioVendedorRepository.FirstOrDefault(p => p.UsuarioId == AbpSession.UserId);
            if (refUsuarioVendedor != null) {
                vResult = true;
            }
            return vResult;
        }

        public void ModificarStatusCliente(int valClienteId, Cliente.eStatusClienteCRM valStatusCrm) {
          Cliente vRecord =  _ClienteRepository.FirstOrDefault(valClienteId);
          vRecord.StatusCRM = valStatusCrm;

        }
        

        
    }
}
