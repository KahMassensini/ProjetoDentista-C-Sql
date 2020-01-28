using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetodentista
{
    class Consulta
    {
        public int Id { get; set; }

        public DateTime Dt_consulta { get; set; }

        public String Motivo { get; set; }

        public String Diagnostico { get; set; }

        public String Receita { get; set; }

        public int IdPaciente { get; set; }

        public int IdDentista { get; set; }

        public String Retorno { get; set; }        
              
        public DateTime Dt_retorno { get; set; }

        

    }
}
