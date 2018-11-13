using System;
using System.Collections.Generic;

namespace ProveedoresFimeApi.Models
{
    public partial class CatEstatus
    {
        public CatEstatus()
        {
            Cotizaciones = new HashSet<Cotizaciones>();
        }

        public int EstatusId { get; set; }
        public string Descipcion { get; set; }

        public ICollection<Cotizaciones> Cotizaciones { get; set; }
    }
}
