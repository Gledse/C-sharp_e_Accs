using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;

namespace GestaoDoProfessor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            OleDbCommand comando = null;
            OleDbConnection conexao = conectar();

            try
            {
                conexao.Open();
                string inserir = "insert into professor(codigo,nome,contacto,sexo,estado_civil,nivel_academico,salario_hora,carga_horaria) values(?,?,?,?,?,?,?,?)";
                comando = new OleDbCommand(inserir, conexao);
                comando.Parameters.Add(new OleDbParameter("codigo", int.Parse(txtCodigo.Text)));
                comando.Parameters.Add(new OleDbParameter("nome", txtNome.Text));
                comando.Parameters.Add(new OleDbParameter("contacto", int.Parse(txtContacto.Text)));

                string sexo = "";
                if (rbtFeminino.Checked) { sexo = rbtFeminino.Text; }
                if (rbtMasculino.Checked) { sexo = rbtMasculino.Text; }
                comando.Parameters.Add(new OleDbParameter("sexo",sexo));

                string estadoCivil = "";
                if (rbtCasado.Checked == true) { estadoCivil = rbtCasado.Text; }
                if (rbtDivorciado.Checked == true ) { estadoCivil = rbtDivorciado.Text; }
                if (rbtSolteiro.Checked == true ) { estadoCivil = rbtSolteiro.Text; }
                if (rbtViuvo.Checked == true) { estadoCivil = rbtViuvo.Text; }
                comando.Parameters.Add(new OleDbParameter("estado_civi", estadoCivil));

                comando.Parameters.Add(new OleDbParameter("nivel_academico", cboNivelAcademico.Text));

                comando.Parameters.Add(new OleDbParameter("salario_hora", double.Parse(txtSalarioHora.Text)));
                comando.Parameters.Add(new OleDbParameter("carga_horaria", int.Parse(txtCargaHoraria.Text)));
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastrado com sucesso", MessageBoxButtons.OK + ", " + MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha no cadastro", ex.Message + ", " + MessageBoxButtons.OK + ", " + MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }

        public static OleDbConnection conectar()
        {
            OleDbConnection conexao = null;

            try
            {
                conexao.Open();

                string caminho = @"Provider = Microsoft.ACE.OLEDB.12.0;Data Souce=c:\bdaula\Professor.accbd";
                conexao = new OleDbConnection(caminho);
                MessageBox.Show("Conectou com sucesso", MessageBoxButtons.OK + ", " + MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na conexao", ex.Message + ", " + MessageBoxButtons.OK + ", " + MessageBoxIcon.Error);
            }
            finally
            { conexao.Close(); }
            return conexao;
        }
        public static ArrayList recuperarProfessor()
        {
            OleDbConnection conexao = conectar();
            OleDbDataReader registros = null;
            OleDbCommand comando = null;
            ArrayList professor = new ArrayList();

            try
            {
                conexao.Open();
                string selecao = " select * from professor";
                comando = new OleDbCommand(selecao, conexao);
                registros = comando.ExecuteReader();
                while (registros.Read())
                {
                    professor.Add(new Professor(registros.GetInt32(0), registros.GetString(1), registros.GetInt32(2), registros.GetString(3),
                                               registros.GetString(4), registros.GetString(5), registros.GetDouble(6), registros.GetInt32(7)));

                    /*professor.Add(new Professor(registros.GetInt32(0), registros.GetString(1), registros.GetInt32(2), registros.GetString(4),
                                               registros.GetString(5), registros.GetString(6), registros.GetDouble(47), registros.GetInt32(8)));*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha a recuperar professor", ex.Message + ", " + MessageBoxButtons.OK + ", " + MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
            return professor;
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            ArrayList professor = recuperarProfessor();

            lstvListar.Items.Clear();
            foreach (Professor prof in professor)
            {
                ListViewItem tabela = new ListViewItem();
                tabela.Text = prof.getCodigo() + "";
                tabela.SubItems.Add(prof.getNome());
                tabela.SubItems.Add(prof.getContacto() + "");
                tabela.SubItems.Add(prof.getSexo());
                tabela.SubItems.Add(prof.getEstadoCivil());
                tabela.SubItems.Add(prof.getNivelAcademico());
                tabela.SubItems.Add(prof.getSalarioHora() + "");
                tabela.SubItems.Add(prof.getCargaHoraria() + "");

                lstvListar.Items.Add(tabela);
            }

        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            OleDbCommand comando = null;
            OleDbConnection conexao = conectar();

            int codigo = int.Parse(txtCodigo.Text);

            try
            {
                conexao.Open();
                string remover = "Delete from professor where codigo = ?;";
                comando = new OleDbCommand(remover, conexao);
                comando.Parameters.Add(new OleDbParameter("codigo", codigo));
                comando.ExecuteNonQuery();
                MessageBox.Show("Removido com sucesso", MessageBoxButtons.OK + ", " + MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na remocao do professor", ex.Message + ", " + MessageBoxButtons.OK + ", " + MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(txtCodigo.Text);
            string nome = txtNome.Text;
            int contacto = int.Parse(txtContacto.Text);

            string sexo = "";
            if (rbtFeminino.Checked == true) { sexo = rbtFeminino.Text; }
            if (rbtMasculino.Checked == true) { sexo = rbtMasculino.Text; }

            string estadoCivil = "";
            if (rbtCasado.Checked == true) { estadoCivil = rbtCasado.Text; }
            if (rbtDivorciado.Checked == true) { estadoCivil = rbtDivorciado.Text; }
            if (rbtSolteiro.Checked == true) { estadoCivil = rbtSolteiro.Text; }
            if (rbtViuvo.Checked == true) { estadoCivil = rbtViuvo.Text; }

            string nivelAcademico = cboNivelAcademico.Text;
            double salarioHora = double.Parse(txtSalarioHora.Text);
            int cargaHoraria = int.Parse(txtCargaHoraria.Text);

            OleDbCommand comando = null;
            OleDbConnection conexao = conectar();

            try
            {
                conexao.Open();
                string update = "update professor set nome = ?,contacto = ?,sexo = ?,estado_civil = ?,nivel_academico = ?,salario_hora = ?,carga_horaria = ? where codigo = ?";
                comando = new OleDbCommand(update, conexao);
                comando.Parameters.Add(new OleDbParameter("codigo", int.Parse(txtCodigo.Text)));
                comando.Parameters.Add(new OleDbParameter("nome", txtNome.Text));
                comando.Parameters.Add(new OleDbParameter("contacto", int.Parse(txtContacto.Text)));
                comando.Parameters.Add(new OleDbParameter("sexo",sexo));
                comando.Parameters.Add(new OleDbParameter("estado_civi",estadoCivil));
                comando.Parameters.Add(new OleDbParameter("nivel_academico",cboNivelAcademico.Text));
                comando.Parameters.Add(new OleDbParameter("salario_hora", double.Parse(txtSalarioHora.Text)));
                comando.Parameters.Add(new OleDbParameter("carga_horaria", int.Parse(txtCargaHoraria.Text)));
                comando.ExecuteNonQuery();
                MessageBox.Show("Actualizado com sucesso", MessageBoxButtons.OK + ", " + MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na actualizacao", ex.Message + ", " + MessageBoxButtons.OK + ", " + MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
