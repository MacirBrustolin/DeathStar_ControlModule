using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStar_ControlModule.Data
{
    public class HistoricoViagem
    {
        [Key]
        public int ViagemId { get; set; }
        public Nave Naves { get; set; }

        public Piloto Pilotos { get; set; }

        public DateTime DtSaida { get; set; }
        public DateTime DtChegada { get; set; }
    }
}
