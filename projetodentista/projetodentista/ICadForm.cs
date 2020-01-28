using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetodentista
{
    interface ICadForm
    {
        void bloquerCampos();
        void desbloquearCampos();
        void limparCampos();
        void atualizarGrid();
        void lerDados();
    }
}
