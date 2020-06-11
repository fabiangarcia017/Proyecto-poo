using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace proyecto
{
    public partial class Form1 : Form
    {
        Empleado miEmpleado = new Empleado();
        
        MySqlConnection conexionBD;
        MySqlCommand comando;
        MySqlDataReader reader;
        string CadenaConexion = "DataSource=127.0.0.1;port=3306;username=root;password=;database=empresa";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {


            try
            {
                miEmpleado.Nombre = txtNombre.Text;
                miEmpleado.Apellido = txtApellido.Text;
                miEmpleado.NumeroEmpleado = int.Parse(txtNumeroEmpleado.Text);
                miEmpleado.Puesto = cboPuesto.Text;
                miEmpleado.Sueldo = double.Parse(txtSueldo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datos Incorrectos\n" + ex.Message);
            }
            double SalarioNeto = miEmpleado.CalcularTotal(() => miEmpleado.Sueldo- (miEmpleado.Sueldo * .16) );
            string query = $"INSERT INTO `empleados`(`Id`, `Nombre`, `Apellido`, `Puesto`, `Numero de Empleado`, `SueldoTotal`) VALUES (NULL, '{miEmpleado.Nombre}','{miEmpleado.Apellido}','{miEmpleado.Puesto}','{miEmpleado.NumeroEmpleado}','{SalarioNeto}')";
            conexionBD = new MySqlConnection(CadenaConexion);
            comando = new MySqlCommand(query,conexionBD);
            try
            {
                conexionBD.Open();
                reader = comando.ExecuteReader();
                MessageBox.Show("Se Agrego Correctamente el empleado");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MostrarUsuarios();
        }
        public void MostrarUsuarios()
        {
            dgvClientes.Rows.Clear();
            string query = "SELECT*FROM empleados";
            conexionBD = new MySqlConnection(CadenaConexion);
            comando = new MySqlCommand(query, conexionBD);
            try
            {
                conexionBD.Open();
                reader = comando.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        dgvClientes.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }
                conexionBD.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            
            try
            {
                miEmpleado.Id = int.Parse(txtID.Text);
                miEmpleado.Nombre = txtNombre.Text;
                miEmpleado.Apellido = txtApellido.Text;
                miEmpleado.NumeroEmpleado = int.Parse(txtNumeroEmpleado.Text);
                miEmpleado.Puesto = cboPuesto.Text;
                miEmpleado.Sueldo = double.Parse(txtSueldo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datos Incorrectos\n" + ex.Message);
            }
            double SalarioNeto = miEmpleado.CalcularTotal(() => miEmpleado.Sueldo - (miEmpleado.Sueldo * .16));
            string query = $"UPDATE `empleados` SET `Nombre`='{miEmpleado.Nombre}',`Apellido`='{miEmpleado.Apellido}',`Puesto`='{miEmpleado.Puesto}',`Numero de Empleado`='{miEmpleado.NumeroEmpleado}',`SueldoTotal`='{SalarioNeto}' WHERE `Id`='{miEmpleado.Id}'";
            conexionBD = new MySqlConnection(CadenaConexion);
            comando = new MySqlCommand(query, conexionBD);
            try
            {
                conexionBD.Open();
                reader = comando.ExecuteReader();
                conexionBD.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
            MostrarUsuarios();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                miEmpleado.Id = int.Parse(txtID.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Dato Incorrecto\n" + ex.Message);
            }
            string query = $"DELETE FROM `empleados` WHERE `Id`='{miEmpleado.Id}'";
            conexionBD = new MySqlConnection(CadenaConexion);
            comando = new MySqlCommand(query, conexionBD);
            try
            {
                conexionBD.Open();
                reader = comando.ExecuteReader();
                conexionBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MostrarUsuarios();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtNumeroEmpleado.Clear();
            txtSueldo.Clear();
            txtID.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void EventoClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvClientes.CurrentRow.Selected = true;
                txtID.Text = dgvClientes.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                txtNombre.Text = dgvClientes.Rows[e.RowIndex].Cells["Nombre"].FormattedValue.ToString();
                txtApellido.Text = dgvClientes.Rows[e.RowIndex].Cells["Apellido"].FormattedValue.ToString();
                cboPuesto.Text = dgvClientes.Rows[e.RowIndex].Cells["Puesto"].FormattedValue.ToString();
                txtNumeroEmpleado.Text = dgvClientes.Rows[e.RowIndex].Cells["NumeroEmpleado"].FormattedValue.ToString();
                txtSueldo.Text = dgvClientes.Rows[e.RowIndex].Cells["Sueldo"].FormattedValue.ToString();

            }
        }
    }
}
