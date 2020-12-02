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

namespace teste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static OleDbConnection conectar()
        {
            OleDbConnection conexao = null;

            
            try
            {
                string caminho = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DataBaseTeste\teste.accdb";
                conexao = new OleDbConnection(caminho);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na conexao", ex.Message);
            }
            return conexao;
        }
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            OleDbConnection conexao = conectar();
            OleDbCommand comando = null;

           // string caminho = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\DataBaseTeste\teste.accdb";

            try
            {
                // conexao = new OleDbConnection(caminho);
                conexao.Open();
                string inserir = "insert into teste(codigo,nome) values(?,?)";
                comando = new OleDbCommand(inserir, conexao);
                comando.Parameters.Add(new OleDbParameter("codigo", int.Parse(txtCodigo.Text)));
                comando.Parameters.Add(new OleDbParameter("nome", txtNome.Text));
                comando.ExecuteNonQuery();
               // conexao.Close();
                MessageBox.Show("Cadastrado com sucesso");
                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no cadastro", ex.Message);
            }
            //finally { conexao.Close(); }
            txtCodigo.Text = "";
            txtNome.Text = "";
        }
        public static ArrayList recuperar()
        {
            OleDbConnection conexao = conectar();
            ArrayList listar = new ArrayList();
            OleDbDataReader chamar = null;
            OleDbCommand comando = null;
            try
            {
                conexao.Open();
                string selecao = "select * from teste ";
                comando = new OleDbCommand(selecao, conexao);
                /*comando.Parameters.Add(new OleDbParameter("codigo", int.Parse(txtCodigo.Text)));
                comando.ExecuteNonQuery();*/
                chamar = comando.ExecuteReader();

                while (chamar.Read())
                {
                    listar.Add(new Teste(chamar.GetInt32(0), chamar.GetString(1)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao procurar", ex.Message);
            }
            finally { conexao.Close(); }
            return listar;
        }
        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            ArrayList listar = recuperar();
            lstvTabela.Items.Clear();

            foreach (Teste te in listar)
            {
                ListViewItem adiconarNaTabela = new ListViewItem();
                adiconarNaTabela.Text = te.getCodigo() + "";
                adiconarNaTabela.SubItems.Add(te.getNome());

                lstvTabela.Items.Add(adiconarNaTabela);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            OleDbConnection conexao = conectar();
            OleDbCommand comando = null;

            try
            {
                conexao.Open();
                string update = "update teste set nome = ? where codigo = ?";
                comando = new OleDbCommand(update, conexao);
                comando.Parameters.Add(new OleDbParameter("nome", txtNome.Text));
                comando.Parameters.Add(new OleDbParameter("codigo",int.Parse(txtCodigo.Text)));
                comando.ExecuteNonQuery();
                MessageBox.Show("Actualizado com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao actualizar", ex.Message);
            }
            finally { conexao.Close(); }
            txtCodigo.Text = "";
            txtNome.Text = "";
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            OleDbConnection conexao = conectar();
            OleDbCommand comando = null;

            try
            {
                conexao.Open();
                string delete = "delete from teste where codigo =?";
                comando = new OleDbCommand(delete, conexao);
                comando.Parameters.Add(new OleDbParameter("codigo",int.Parse(txtCodigo.Text)));
                comando.ExecuteNonQuery();
                MessageBox.Show("Apagado com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao deletar", ex.Message);
            }
            finally { conexao.Close(); }
        }
    }
}
