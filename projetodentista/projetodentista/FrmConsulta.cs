using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace projetodentista
{
    public partial class FrmConsulta : Form
    {
        Consulta objConsulta;
        Conexao con; 

        public FrmConsulta()
        {
            InitializeComponent();
            con = new Conexao();

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            con.conectar();
            desbloquearCampos();
        }

        private void FrmConsulta_Load(object sender, EventArgs e)
        {
            atualizarGrid();
            bloquearCampos();
            comboboxPaciente();
            comboboxDentista();
        }

        private void atualizarGrid()
        {
            List<Consulta> listConsulta = new List<Consulta>();
            con.conectar();
            SqlDataReader reader;
            reader =  con.exeConsulta("select * from tb_consulta");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Consulta consulta = new Consulta();
                    consulta.Id = reader.GetInt32(0);
                    consulta.Motivo = reader.GetString(1);
                    consulta.Dt_consulta = reader.GetDateTime(2);
                    consulta.Diagnostico = reader.GetString(3);
                    consulta.Receita = reader.GetString(4);
                    consulta.Dt_retorno = reader.GetDateTime(5);
                    consulta.Retorno = reader.GetString(6);
                    consulta.IdPaciente = reader.GetInt32(7);
                    consulta.IdDentista = reader.GetInt32(8);

                    listConsulta.Add(consulta);

                }

                reader.Close();
            }

            else
            {
                Console.WriteLine("Não retornou dados");
            }

            dgvConsulta.DataSource = null;
            dgvConsulta.DataSource = listConsulta;
        }

        private void dgvConsulta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdConsulta.Text = dgvConsulta.CurrentRow.Cells[0].Value.ToString();
            dtpDtConsulta.Value = DateTime.Parse(dgvConsulta.CurrentRow.Cells[1].Value.ToString());
            txtMotivo.Text = dgvConsulta.CurrentRow.Cells[2].Value.ToString();            
            txtDiagnostico.Text = dgvConsulta.CurrentRow.Cells[3].Value.ToString();
            txtReceita.Text = dgvConsulta.CurrentRow.Cells[4].Value.ToString();
            cmbIdPaciente.Text = dgvConsulta.CurrentRow.Cells[5].Value.ToString();
            cmbIdDentista.Text = dgvConsulta.CurrentRow.Cells[6].Value.ToString();
            txtRetorno.Text = dgvConsulta.CurrentRow.Cells[7].Value.ToString();
            

            if (!dgvConsulta.CurrentRow.Cells[8].Value.ToString().Equals("01/01/0001 00:00:00"))
            {
                dtpDtRetorno.Value = DateTime.Parse(dgvConsulta.CurrentRow.Cells[8].Value.ToString());
            }

        }

        public void lerDados()
        {
            objConsulta = new Consulta();

            objConsulta.Id = int.Parse(txtIdConsulta.Text.Trim());
            objConsulta.Dt_consulta = dtpDtConsulta.Value;
            objConsulta.Motivo = txtMotivo.Text;
            objConsulta.Diagnostico = txtDiagnostico.Text;
            objConsulta.Receita = txtReceita.Text;            
            objConsulta.IdPaciente = int.Parse(cmbIdPaciente.SelectedValue.ToString());
            objConsulta.IdDentista = int.Parse(cmbIdDentista.SelectedValue.ToString());
            objConsulta.Dt_retorno = dtpDtRetorno.Value;
            objConsulta.Retorno = txtRetorno.Text;            
            

        }

        private void limparCampos()
        {
            txtIdConsulta.Text = "";
            txtMotivo.Text = "";
            txtDiagnostico.Text = "";
            txtReceita.Text = "";
            txtRetorno.Text = "";
            dtpDtConsulta.Value = DateTime.Today;
            dtpDtRetorno.Value = DateTime.Today;
            cmbIdDentista.Text = "";
            cmbIdPaciente.Text = "";

        }

        private void desbloquearCampos()
        {
            txtIdConsulta.ReadOnly = false;
            txtMotivo.ReadOnly = false;
            txtDiagnostico.ReadOnly = false;
            txtReceita.ReadOnly = false;
            dtpDtConsulta.Enabled = true;
            cmbIdDentista.Enabled = true;
            cmbIdPaciente.Enabled = true;

        }

        private void bloquearCampos()
        {
            txtIdConsulta.ReadOnly = true;
            txtMotivo.ReadOnly = true;
            txtDiagnostico.ReadOnly = true;
            txtReceita.ReadOnly = true;
            txtRetorno.ReadOnly = true;
            dtpDtConsulta.Enabled = false;
            dtpDtRetorno.Enabled = false;
            cmbIdDentista.Enabled = false;
            cmbIdPaciente.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lerDados();

            String sql = "insert into tb_consulta " +
          "values (" + objConsulta.Id + ", '" +
          objConsulta.Motivo + "'," +
          "convert(date , '" + objConsulta.Dt_consulta.ToShortDateString() + "' , 103) ,'" +
          objConsulta.Diagnostico + "','" +
          objConsulta.Receita + "'," +
          "convert(date , '" + objConsulta.Dt_retorno.ToShortDateString() + "' , 103) ,'" +
          objConsulta.Retorno + "'," +                              
          objConsulta.IdPaciente + ", " +
          objConsulta.IdDentista + ") "; 
          

            if (con.executar(sql) == 1)
            {
                MessageBox.Show("Dados salvos com sucesso!");
            }
            else
            {
                MessageBox.Show("Dados não foram salvos!");
            }

            bloquearCampos();
            atualizarGrid();


        }

        private void rbSim_CheckedChanged(object sender, EventArgs e)
        {
            txtRetorno.ReadOnly = false;
            dtpDtRetorno.Enabled = true;

        }

        private void rbNao_CheckedChanged(object sender, EventArgs e)
        {
            txtRetorno.ReadOnly = true;
            dtpDtRetorno.Enabled = false;
          
        }

        private void comboboxPaciente()
        {
            List<Paciente> listPaciente = new List<Paciente>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select id, nome from tb_Paciente");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.Id = reader.GetInt32(0);
                    paciente.Nome = reader.GetString(1);
                    listPaciente.Add(paciente);
                }
            }

            else
            {
                Console.WriteLine("Não retornou dados.");
            }

            reader.Close();
            cmbIdPaciente.DataSource = listPaciente;
            cmbIdPaciente.DisplayMember = "Nome";
            cmbIdPaciente.ValueMember = "Id";
        }

        private void comboboxDentista()
        {
            {
                List<Dentista> listDentista = new List<Dentista>();
                con.conectar();
                SqlDataReader reader;
                reader = con.exeConsulta("select id, nome from tb_Dentista");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dentista dentista = new Dentista();
                        dentista.Id = reader.GetInt32(0);
                        dentista.Nome = reader.GetString(1);
                        listDentista.Add(dentista);
                    }
                }

                else
                {
                    Console.WriteLine("Não retornou dados.");
                }

                reader.Close();
                cmbIdDentista.DataSource = listDentista;
                cmbIdDentista.DisplayMember = "Nome";
                cmbIdDentista.ValueMember = "Id";
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            String id = dgvConsulta.CurrentRow.Cells[0].Value.ToString();
            String sql = "delete from tb_consulta where id = " + id;
            con.executar(sql);
            atualizarGrid();
        }
    }
}
