using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARecuperacion.Models
{
    public class PlanetDetailsResponse
    {
        [JsonProperty("items")]
        public List<Character> Characters { get; set; }

    }
}
