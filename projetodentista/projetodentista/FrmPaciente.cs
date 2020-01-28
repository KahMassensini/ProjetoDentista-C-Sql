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
    public partial class FrmPaciente : Form
    {
        Paciente objPaciente;
        Conexao con;
         
        public FrmPaciente()
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

        private void FrmPaciente_Load(object sender, EventArgs e)
        {
            atualizarGrid();
            bloquearCampos();
        }

        private void atualizarGrid()
        {
            List<Paciente> listPaciente = new List<Paciente>();
            con.conectar();
            SqlDataReader reader;
            reader = con.exeConsulta("select * from tb_paciente");
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.Id = reader.GetInt32(0);
                    paciente.Nome = reader.GetString(1);
                    paciente.CPF = reader.GetString(2);
                    paciente.Endereco = reader.GetString(3);
                    paciente.Telefone = reader.GetString(4);
                    paciente.DtNasc = reader.GetDateTime(5);
                    paciente.Sexo = reader.GetValue(6) == null ? "" : reader.GetValue(6).ToString();
                    paciente.Instagram = reader.GetValue(7).ToString() == "True" ? 1 : 0;
                    paciente.Facebook = reader.GetValue(8).ToString() == "True" ? 1 : 0;
                    paciente.Twitter = reader.GetValue(9).ToString() == "True" ? 1 : 0;
                    paciente.Linkedin = reader.GetValue(10).ToString() == "True" ? 1 : 0;

                    listPaciente.Add(paciente);

                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }

            dgvPaciente.DataSource = null;
            dgvPaciente.DataSource = listPaciente;
        }

        private void limparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtCpf.Text = "";
            txtEndereco.Text = "";
            txtTel.Text = "";

        }

        private void desbloquearCampos()
        {
            txtId.ReadOnly = false;
            txtCpf.ReadOnly = false;
            txtNome.ReadOnly = false;
            txtTel.ReadOnly = false;
            txtEndereco.ReadOnly = false;
        }

        private void bloquearCampos()
        {
            txtId.ReadOnly = true;
            txtCpf.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtTel.ReadOnly = true;
            txtEndereco.ReadOnly = true;
        }

        private void lerDados()
        {
            objPaciente = new Paciente();

            objPaciente.Id = int.Parse(txtId.Text.Trim());
            objPaciente.Nome = txtNome.Text;
            objPaciente.CPF = txtCpf.Text;
            objPaciente.Endereco = txtEndereco.Text;
            objPaciente.Telefone = txtTel.Text;
            objPaciente.DtNasc = dtpDtnasc.Value;

            objPaciente.Instagram = chbInstagram.Checked ? 1 : 0;
            objPaciente.Twitter = chbTwitter.Checked ? 1 : 0;
            objPaciente.Facebook = chbFacebook.Checked ? 1 : 0;
            objPaciente.Linkedin = chbLinkedin.Checked ? 1 : 0;

            objPaciente.Sexo = rbFeminino.Checked ? "F" : "M";

        }

        private void dgvPaciente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dgvPaciente.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgvPaciente.CurrentRow.Cells[2].Value.ToString();
            txtCpf.Text = dgvPaciente.CurrentRow.Cells[1].Value.ToString();
            txtEndereco.Text = dgvPaciente.CurrentRow.Cells[3].Value.ToString();
            txtTel.Text = dgvPaciente.CurrentRow.Cells[4].Value.ToString();
            dtpDtnasc.Text = dgvPaciente.CurrentRow.Cells[5].Value.ToString();

            chbInstagram.Checked = dgvPaciente.CurrentRow.Cells[7].Value.Equals(1);
            chbFacebook.Checked = dgvPaciente.CurrentRow.Cells[8].Value.Equals(1);
            chbTwitter.Checked = dgvPaciente.CurrentRow.Cells[9].Value.Equals(1);
            chbLinkedin.Checked = dgvPaciente.CurrentRow.Cells[10].Value.Equals(1);

            rbFeminino.Checked = dgvPaciente.CurrentRow.Cells[6].Value.Equals("F") ? true : false;

            rbMasculino.Checked = dgvPaciente.CurrentRow.Cells[6].Value.Equals("M") ? true : false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lerDados();

            String sql = "insert into tb_paciente " +
                "values (" + objPaciente.Id + ", '" +
                objPaciente.Nome + "','" +
                objPaciente.CPF + "','" +
                objPaciente.Endereco + "','" +
                objPaciente.Telefone + "','" +
                objPaciente.DtNasc + "','" +
                objPaciente.Sexo + "', " +
                objPaciente.Instagram + ", " +
                objPaciente.Facebook + ", " +
                objPaciente.Twitter + ", " +
                objPaciente.Linkedin + ") ";

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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            String id = dgvPaciente.CurrentRow.Cells[0].Value.ToString();
            String sql = "delete from tb_paciente where id = " + id;
            con.executar(sql);
            atualizarGrid();
        }
    }
}
