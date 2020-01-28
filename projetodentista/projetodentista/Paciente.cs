using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetodentista
{
    class Paciente:Pessoa
    {
       
        public String CPF { get; set; }
                
        public String Telefone { get; set; }

        public String Endereco { get; set; }

        public DateTime DtNasc { get; set; }

        public String Sexo { get; set; }

        public int Instagram { get; set; }

        public int Facebook { get; set; }

        public int Twitter { get; set; }

        public int Linkedin { get; set; }

    }
}
