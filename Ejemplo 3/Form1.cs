using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejemplo_3
{
    public partial class Form1 : Form
    {
        Queue<Empleados> Trabajadores = new Queue<Empleados>();
        public Form1()
        {
            InitializeComponent();
        }
        public void Limpiar()
        {
            txtCarnet.Clear();
            txtNombre.Clear();
            txtSalario.Clear();
        }
        private void btnRegistro_Click(object sender, EventArgs e)
        {
            BorrarError();
            if (validarCampos())
            {
                Empleados empleados = new Empleados();
                empleados.Carnet = txtCarnet.Text;
                empleados.Nombre = txtNombre.Text;
                empleados.Salario = Decimal.Parse(txtSalario.Text);
                empleados.Fecha = dateTimePicker1.Value;
                Trabajadores.Enqueue(empleados);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Trabajadores.ToArray();
                Limpiar();
                txtCarnet.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(Trabajadores.Count != 0)
            {
                Empleados empleado = new Empleados();
                empleado = Trabajadores.Dequeue();
                txtCarnet.Text = empleado.Carnet;
                txtNombre.Text = empleado.Nombre;
                txtSalario.Text = empleado.Salario.ToString();
                dateTimePicker1.Value = empleado.Fecha;
                dataGridView1.DataSource = Trabajadores.ToList();
                MessageBox.Show("Se elimino el registro en cola", "Aviso");
                Limpiar();
            }
            else
            {
                MessageBox.Show("No hay empleados en cola", "AVISO");
                Limpiar();
            }
            txtCarnet.Focus();
        }
        private bool validarCampos()
        {

            bool validado = true;
            if (txtCarnet.Text == "" )
            {
                validado = false;
                errorProvider1.SetError(txtCarnet, "Ingresar Carnet");

            }
            if(txtNombre.Text == "")
            {
                validado = false;
                errorProvider1.SetError(txtNombre, "Ingresar Nombre");
            }
            if(txtSalario.Text == "" )
            {
                validado = false;
                errorProvider1.SetError(txtSalario, "Datos incorrectos");
            }
            return validado;
        }
        private void BorrarError()
        {
            errorProvider1.SetError(txtCarnet, "");
            errorProvider1.SetError(txtNombre, "");
            errorProvider1.SetError(txtSalario, "");
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if ((e.KeyChar == '.') && (!txtSalario.Text.Contains(".")))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo se admiten datos numéricos" ,"Aviso",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo se admiten letras", "Aviso",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
