using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelL
{
    public class Historial
    {
        public int IdHistorial { get; set; }
        public int Numero { get; set; }
        public int Resultado { get; set; }
        public DateTime Fecha { get; set; }
        public List<Historial> Historiales { get; set; }
        public List<List<int>> DetalleSuperDigito { get; set; }
    }
}