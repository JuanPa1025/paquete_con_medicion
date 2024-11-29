using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Glamping_Addventure.Models;
using Glamping_Addventure.Models.view;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glamping_Addventure.Controllers
{
    public class HomeController : Controller
    {
        private readonly GlampingAddventuresContext _dbcontext;

        public HomeController(GlampingAddventuresContext context)
        {
            _dbcontext = context;
        }

        public IActionResult resumenReserva()
        {
            try
            {
                DateTime FechaInicio = DateTime.Now.AddDays(-5);


                var Lista = (from data in _dbcontext.Reservas
                             where data.FechaReserva.HasValue && data.FechaReserva >= FechaInicio
                             group data by data.FechaReserva.Value.Date into gr
                             select new VMReserva
                             {
                                 FechaReserva = gr.Key, // Esto es de tipo DateTime?
                                 FechaReservaFormatted = gr.Key.ToString("yyyy-MM-dd"),
                                 Cantidad = gr.Count()
                             }).ToList();

                if (Lista == null || !Lista.Any())
                {
                    return NotFound("No se encontraron datos para las reservas.");
                }

                return Ok(Lista);

            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes loguear el error si es necesario)
                return BadRequest($"Ocurrió un error: {ex.Message}");
            }

        }

        public IActionResult resumenPaquete()
        {

            try
            {
                var lista = (from data in _dbcontext.DetalleReservaPaquetes
                             join tbpaquete in _dbcontext.Paquetes on data.Idpaquete equals tbpaquete.Idpaquete
                             group data by new { data.Idpaquete, tbpaquete.NombrePaquete } into gr
                             select new VMPaquete
                             {
                                 NombrePaquete = gr.Key.NombrePaquete ?? "Sin Nombre", // Manejo de nulos
                                 Cantidad = gr.Count()
                             })
                            .OrderByDescending(gr => gr.Cantidad) // Opcional: ordenar por cantidad
                            .Take(5)
                            .ToList();

                if (lista == null || !lista.Any())
                {
                    return NotFound("No se encontraron datos para las reservas.");
                }

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error: {ex.Message}");
            }
        }

        public IActionResult resumenServicios()
        {
            try
            {
                var Lista = (from data in _dbcontext.DetalleReservaServicios
                             join tbservicio in _dbcontext.Servicios on data.Idservicio equals tbservicio.Idservicio
                             group data by new { data.Idservicio, tbservicio .NombreServicio } into gr
                             select new VMServicios
                             {
                                 NombreServicio = gr.Key.NombreServicio ?? "sin nombre",
                                 Cantidad = gr.Count()
                             }).OrderByDescending(gr => gr.Cantidad).Take(5).ToList();

                if (Lista == null || !Lista.Any())
                {
                    return NotFound("No se encontraron datos para las reservas.");
                }

                return Ok(Lista);

            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error: {ex.Message}");
            }

        }

        //public IActionResult cantidadPersonasUltimos5Dias()
        //{

        //    try
        //    {
        //        // Fecha de inicio (últimos 5 días)
        //        DateTime fechaInicio = DateTime.Now.AddDays(-5);

        //        // Consulta LINQ ajustada
        //        var lista = (from data in _dbcontext.Reservas
        //                     where data.FechaInicio.HasValue && data.fechaInicio >= fechaInicio
        //                     group habitacion by reserva.FechaInicio.Date into gr
        //                     select new
        //                     {
        //                         Fecha = gr.Key, // Fecha de la reserva
        //                         CantidadPersonas = gr.Sum(h => h.Capacidad) // Suma de las capacidades de las habitaciones
        //                     })
        //                     .OrderBy(gr => gr.Fecha) // Ordenar por fecha ascendente
        //                     .ToList();

        //        // Validar si hay datos
        //        if (lista == null || !lista.Any())
        //        {
        //            return NotFound("No se encontraron datos para los últimos 5 días.");
        //        }

        //        // Retornar los datos como JSON
        //        return Ok(lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar errores
        //        return BadRequest($"Ocurrió un error: {ex.Message}");
        //    }
        //}
            

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
