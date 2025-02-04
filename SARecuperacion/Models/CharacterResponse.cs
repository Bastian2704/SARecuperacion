using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARecuperacion.Models
{
    public class CharacterResponse
    {
        public List<Character> Items { get; set; }
        public Meta Meta { get; set; }
        public Links Links { get; set; }
    }
}
