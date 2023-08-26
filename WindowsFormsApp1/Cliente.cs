using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using MySqlConnector.Logging;

namespace WindowsFormsApp1
{
    class Cliente
    {
        public int id {  get; set; }
        public string nome { get; set; }    
        public string celular { get; set;}

        MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=tds10;user id=root;password=;charset=utf8");

        public List<Cliente> listacliente()
        {
            List<Cliente> li = new List<Cliente>();
            string sql = "SELECT * FROM cliente";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cliente cliente = new Cliente();
                cliente.id = (int)dr["id"];
                cliente.nome = dr["nome"].ToString();
                cliente.celular = dr["celular"].ToString();
                li.Add(cliente);
            }
            dr.Close();
            conn.Close();
            return li;
        }

        public void Inserir(string nome , string celular)
        {
            string sql = "INSERT INTO cliente(nome,celular) VALUES ('" + nome + "', '" + celular + "')";
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            MySqlCommand cmd = new MySqlCommand (sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Atualizar(int id, string nome, string celular)
        {
            string sql = "UPDATE cliente SET  nome= '" + nome + "', celular= '" + celular + "' WHERE id= '" + id + "'";
            conn.Open ();
            MySqlCommand cmd = new MySqlCommand (sql, conn);
            conn.Close();
        }

        public void Excluir(int id)
        {
            string sql = "DELETE FROM cliente WHERE id= '"+id+"'";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Close();
        }

        public void Localizar(int id)
        {
            string sql = "SELECT * FROM cliente WHERE id= '" + id + "'";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand( sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nome = dr["nome"].ToString();
                celular = dr["celular"].ToString();
            }
            dr.Close();
            conn.Close();
        }

        public bool RegistroRepetido(string nome, string celular)
        {
            string sql = "SELECT * FROM cliente WHERE nome= '" + nome + "' AND celular= '" + celular + "'";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int) result > 0;
            }
            conn.Close();
            return false;
        }
    }
}