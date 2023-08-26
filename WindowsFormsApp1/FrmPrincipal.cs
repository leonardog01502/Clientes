using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Internal;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            List<Cliente> clientes = cliente.listacliente();
            dgvCliente.DataSource = clientes;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                if (cliente.RegistroRepetido(txtNome.Text, txtCelular.Text))
                {
                    MessageBox.Show("Cliente já existe em nossa base de dados!", "Registro Repetido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Text = "";
                    txtCelular.Text = "";
                    return;
                }
                else
                {
                    cliente.Inserir(txtNome.Text, txtCelular.Text);
                    MessageBox.Show("Cliente inserido com sucesso!", "Cliente registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Cliente> clientes = cliente.listacliente();
                    dgvCliente.DataSource = clientes;
                    txtNome.Text = "";
                    txtCelular.Text = "";
                    this.txtNome.Focus();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK);

            }
        }

        private void btnlocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtID.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Localizar(id);
                txtNome.Text = cliente.nome;
                txtCelular.Text = cliente.celular;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtID.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Atualizar(id, txtNome.Text, txtCelular.Text);
                MessageBox.Show("Cliente atualizado com sucesso !!!", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> clientes = cliente.listacliente();
                dgvCliente.DataSource = clientes;
                txtNome.Text = "";
                txtCelular.Text = "";
                txtID.Text = "";
                this.txtNome.Focus();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
               int id = Convert.ToInt32(txtID.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Excluir(id);
                MessageBox.Show("Cliente excluído com sucesso!!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> clientes = cliente.listacliente();
                dgvCliente.DataSource = clientes;
                txtNome.Text = "";
                txtCelular.Text = "";
                txtID.Text = "";
                this.txtNome.Focus();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvCliente.Rows[e.RowIndex];
                this.dgvCliente.Rows[e.RowIndex].Selected = true;
                txtID.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtCelular.Text = row.Cells[2].Value.ToString();
            }
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}