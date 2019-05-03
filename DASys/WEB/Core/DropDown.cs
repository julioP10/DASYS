using Entidad;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WEB.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utilis;
using WEB.Controllers;
using System;

namespace WEB
{
    public class DropDown :BaseController
    {
        public DropDown(IServiceProvider serviceProvider) : base(serviceProvider)
        {
 
        }
        public static IEnumerable<DropDownDto> ListaArea(string consulta, string Empresa)
        {
            List<DropDownDto> lista = Utils.ListaArea(consulta,Empresa);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaCategoria(string codigo,string empresa)
        {
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaCategoria(codigo,empresa);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaConfiguracion(string codigo)
        {
            List<DropDownDto> lista = Utils.ListaConfiguracion(codigo);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaEntidad( string codigo)
        {
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaEntidad(codigo);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaMarcaEntidad(string codigo)
        {
            codigo = (codigo == null) ? "" : codigo;
            List<DropDownDto> lista = Utils.ListaMarcaEntidad(codigo);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaEmpresa(string codigo)
        {
            List<DropDownDto> lista = Utils.ListaEmpresa(codigo);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaEmpresaColaborador(string codigo,string empresa)
        {
            List<DropDownDto> lista = Utils.ListaEmpresaColaborador(codigo,empresa);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaEmpresaXempresa(string codigo)
        {
            List<DropDownDto> lista = Utils.ListaEmpresaXempresa(codigo);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaEmpresaSuperUsuario(string codigo)
        {
            List<DropDownDto> lista = Utils.ListaEmpresaSuperUsuario(codigo);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaEstado()
        {
            List<DropDownDto> lista = Utils.ListaEstado("");
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaMarca(string codigo,string entidad)
        {
            List<DropDownDto> lista = Utils.ListaMarca(codigo, entidad);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaModelo(string codigo,string empresa)
        {
            List<DropDownDto> lista = Utils.ListaModelo(codigo, empresa);
            return lista;
        }
        
        public static IEnumerable<DropDownDto> ListaOperador()
        {
            List<DropDownDto> lista = Utils.ListaOperador("");
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaTipo(string codigo)
        {
            List<DropDownDto> lista = Utils.ListaTipo(codigo);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaTerminal()
        {
            List<DropDownDto> lista = Utils.ListaTerminal("");
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaUbicacion()
        {
            List<DropDownDto> lista = Utils.ListaUbicacion("");
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaUDepartamento()
        {
            List<DropDownDto> lista = Utils.ListaUDepartamento("");
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaDepartamento(string consulta)
        {
            List<DropDownDto> lista = Utils.ListaDepartamento(consulta);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaEmail()
        {
            List<DropDownDto> lista = Utils.ListaEmail("");
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaRegimen(string consulta)
        {
            List<DropDownDto> lista = Utils.ListaRegimen(consulta);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaCondicion(string consulta)
        {
            List<DropDownDto> lista = Utils.ListaCondicion(consulta);
            return lista;
        }

        public static IEnumerable<DropDownDto> ListaModulo(string consulta)
        {
            List<DropDownDto> lista = Utils.ListaModulo(consulta);
            return lista;
        }
        public static IEnumerable<DropDownDto> ListaPerfil()
        {
            List<DropDownDto> lista = Utils.ListaPerfil("");
            return lista;
        }

        public static IEnumerable<DropDownDto> ListaTipoLugar(string consulta)
        {
            List<DropDownDto> lista = Utils.ListaTipoLugar(consulta);
            return lista;
        }

        public static IEnumerable<DropDownDto> ListaProveedor(string consulta)
        {
            List<DropDownDto> lista = Utils.ListaProveedor(consulta);
            return lista;
        }

        
    }
}