using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Data.Context;
using PazYSalvoAPP.WebApp.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PazYSalvoAPP.WebApp.Controllers
{
    public class FacturaController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Mensaje = "Hola Clase";
            return View();
        }
        private readonly IFacturaService _facturaService;
        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarFacturas()
        {
            IQueryable<Factura>? consultaDeFacturas = await _facturaService.LeerTodos();

            List<FacturaViewModel> listadoDeFacturas = consultaDeFacturas.Select(f => new FacturaViewModel
            {
                Id = f.Id,
                Saldo = f.Saldo,
                ClienteId = f.ClienteId,
                ServicioAdquiridoId = f.ServicioAdquiridoId,
                MedioDePagoId = f.MedioDePagoId,
                EstadoId = f.EstadoId,


            }).ToList();

            return PartialView("_ListadoDeFacturas",
                              listadoDeFacturas);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarFacturas([FromBody] FacturaViewModel model)
        {
            Factura factura = new Factura()
            {
                Saldo = model.Saldo,
                ClienteId = model.ClienteId,
                ServicioAdquiridoId = model.ServicioAdquiridoId,
                MedioDePagoId = model.MedioDePagoId,
                EstadoId = model.EstadoId,

            };

            bool response = await _facturaService.Insertar(factura);

            return StatusCode(StatusCodes.Status200OK,
                              new { valor = response });

        }

        [HttpPost]
        public async Task<IActionResult> ActualizarFacturas([FromBody] FacturaViewModel model)
        {
            Factura factura = new Factura()
            {
                Id = model.Id,
                Saldo = model.Saldo,
                ClienteId = model.ClienteId,
                ServicioAdquiridoId = model.ServicioAdquiridoId,
                MedioDePagoId = model.MedioDePagoId,
                EstadoId = model.EstadoId,

            };

            bool response = await _facturaService.Actualizar(factura);

            return StatusCode(StatusCodes.Status200OK,
                              new { valor = response });

        }

    }
}
