using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetodentista
{
    public partial class MenuDentibao : Form
    {
        public MenuDentibao()
        {
            InitializeComponent();
        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            FrmPaciente paciente = new FrmPaciente();
            paciente.Show();
        }

        private void btnDentista_Click(object sender, EventArgs e)
        {
            FrmDentista dentista = new FrmDentista();
            dentista.Show();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            FrmConsulta consulta = new FrmConsulta();
            consulta.Show();
        }
    }
}
