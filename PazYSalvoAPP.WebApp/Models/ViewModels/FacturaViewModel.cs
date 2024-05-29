using PazYSalvoAPP.Data.Context;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace PazYSalvoAPP.WebApp.Models.ViewModels
{
    public class FacturaViewModel
    {
        public int Id { get; set; }

        public decimal? Saldo { get; set; }

        public int? ClienteId { get; set; }

        public int? ServicioAdquiridoId { get; set; }

        public int? MedioDePagoId { get; set; }

        [Display(Name = "Estado")]
        public int? EstadoId { get; set; }

        public DateTime? FechaDeCreacion { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual ICollection<DetallesDeFactura> DetallesDeFacturas { get; set; } = new List<DetallesDeFactura>();

        public virtual Estado? Estado { get; set; }

        public virtual MediosDePago? MedioDePago { get; set; }


        public virtual Servicio? ServicioAdquirido { get; set; }

        public FacturaViewModel()
        {

        }


    }
}
