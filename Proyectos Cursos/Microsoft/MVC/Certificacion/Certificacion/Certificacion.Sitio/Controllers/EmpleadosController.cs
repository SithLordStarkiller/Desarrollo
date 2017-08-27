using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Certificacion.LogicaNegocios;
using Certificacion.Modelos;
using Certificacion.Sitio.Models;

namespace Certificacion.Sitio.Controllers
{
    public class EmpleadosController : Controller
    {
        public ActionResult ListaDepartamentos()
        {
            var listaDepartamentos = new DepartamentosLn().ObtenesListaDeparatamentos();

            return View(listaDepartamentos);
        }

        public ActionResult ListaEmpleados(int idDepartamento)
        {
            var listaEmpleados = new EmpleadosLn().ObtenerEmpleados().Where(x => x.Departamento == idDepartamento).Select(c => new EmpleadosModel
            {
                IdEmpleado = c.IdEmpleado,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Genero = c.Genero,
                FechaIngreso = c.FechaIngreso,
                Salario = c.Salario,
                IdDepartemento = (int)c.Departamento
            }).ToList();

            TempData["IdDepartamento"] = idDepartamento;

            return View(listaEmpleados);
        }

        public ActionResult CrearEmpleado(int idDepartamento)
        {
            TempData["IdDepartamento"] = idDepartamento;

            return View();
        }

        public ActionResult InsertarEmpleado(EmpleadosModel empleadoModel)
        {
            empleadoModel.IdDepartemento = (int)TempData["IdDepartamento"];

            var empleado = new Empleados
            {
                Nombre = empleadoModel.Nombre,
                Apellido = empleadoModel.Apellido,
                Genero = empleadoModel.Genero,
                Salario = empleadoModel.Salario,
                FechaIngreso = empleadoModel.FechaIngreso,
                Departamento = empleadoModel.IdDepartemento
            };

            var entidad = new EmpleadosLn().InsertaEmpleado(empleado);

            ViewData["IdDepartamento"] = entidad.Departamento;

            return View("ListaEmpleados");
        }
    }
}