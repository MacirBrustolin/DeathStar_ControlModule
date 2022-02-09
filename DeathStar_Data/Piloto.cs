using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStar
{
    public class Piloto
    {
        [Key]
        public int PilotoId { get; set; }
        public string Nome { get; set; }
        public string AnoNascimento { get; set; }
        public int PlanetasId { get; set; }
        public Planeta Planetas { get; set; }
        public virtual ICollection<HistoricoViagem> HistoricoViagens { get; set; }
        public ICollection<Nave> Naves { get; set; }

        
    }
}
