using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStar
{
    public class Nave
    {
        [Key]
        public int NaveId { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public int Passageiros { get; set; }
        public double Carga { get; set; }
        public string Classe { get; set; }

        
        public virtual ICollection<HistoricoViagem> HistoricoViagens { get; set; }
        public ICollection<Piloto> Pilotos { get; set; }
        
    }
}
