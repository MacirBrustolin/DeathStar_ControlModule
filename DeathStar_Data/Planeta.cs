using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStar
{
    public class Planeta
    {
        [Key]
        public int PlanetaId { get; set; }
        public string Nome { get; set; }
        public double Rotacao { get; set; }
        public double Orbita { get; set; }
        public double Diametro { get; set; }
        public string Clima { get; set; }
        public int Populacao { get; set; }

        public ICollection<Piloto> Pilotos { get; set; }
    }
}
