using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARecuperacion.Models
{
    public class Meta
    {
        public int TotalItems { get; set; }
        public int ItemCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
