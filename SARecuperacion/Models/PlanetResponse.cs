using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARecuperacion.Models
{
    public class PlanetResponse
    {
        public List<Planet> Items { get; set; }
        public Meta Meta { get; set; }
        public Links Links { get; set; }

    }
}
