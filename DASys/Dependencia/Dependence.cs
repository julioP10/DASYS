using Datos;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
 
namespace Dependencia
{
    public static class Dependence
    {
        public static IServiceCollection AddDependencyInjectionInterfaces(this IServiceCollection services)
        {
            #region Mantenimiento

            services.AddScoped<IArea, AreaDAL>();
            services.AddScoped<ICategoria, CategoriaDAL>();
            services.AddScoped<IColaborador, ColaboradorDAL>();
            services.AddScoped<IConfiguracion, ConfiguracionDAL>();
            services.AddScoped<IDepartamento, DepartamentoDAL>();
            services.AddScoped<IDocumento, DocumentoDAL>();
            services.AddScoped<IEmail, EmailDAL>();
            services.AddScoped<IMarca, MarcaDAL>();
            services.AddScoped<IEmpresa, EmpresaDAL>();
            services.AddScoped<IDocumentoAdjunto, DocumentoAdjuntoDAL>();
            services.AddScoped<IEntidad, EntidadDAL>();
            services.AddScoped<IModelo, ModeloDAL>();
            services.AddScoped<ITerminal, TerminalDAL>();
            services.AddScoped<ITipo, TipoDAL>();
            services.AddScoped<IUbicacion, UbicacionDAL>();
            services.AddScoped<IVehiculo, VehiculoDAL>();
            services.AddScoped<IRegimen, RegimenDAL>();
            services.AddScoped<ICondicion, CondicionDAL>();
            services.AddScoped<IVehiculo, VehiculoDAL>();
            services.AddScoped<IMaquinaria, MaquinariaDAL>();
            services.AddScoped<IProveedor, ProveedorDAL>();
            #endregion

            #region Seguridad
            services.AddScoped<IModulo, ModuloDAL>();
            services.AddScoped<IOpcion, OpcionDAL>();
            services.AddScoped<IPerfil, PerfilDAL>();
            services.AddScoped<IPerfilOpcion, PerfilOpcionDAL>();
            services.AddScoped<IUsuario, UsuarioDAL>();
            services.AddScoped<IUsuarioOpcion, UsuarioOpcionDAL>();
            services.AddScoped<IUsuarioPerfil, UsuarioPerfilDAL>();
            services.AddScoped<IDigitalizacion, DigitalizacionDAL>();
            //services.AddScoped<IUtils, UtilsDAL>();
            #endregion
            #region reportes
            services.AddScoped<IReportes, ReportesDAL>();
            #endregion

            return services;
        }
    }
}
