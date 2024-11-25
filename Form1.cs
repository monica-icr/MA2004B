using AlgoritmoMonteCarlo.Algoritmos;
using AlgoritmoMonteCarlo.Clases;

namespace AlgoritmoMonteCarlo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") ||
                   textBox2.Text.Equals("") ||
                   textBox3.Text.Equals("") ||
                   textBox4.Text.Equals("") ||
                   textBox5.Text.Equals("") ||
                    textBox9.Text.Equals("")) 
            {
                MessageBox.Show("Los números tienen que ser diferente de VACÍOS");
                return;
            }

            double limiteInferior = Convert.ToDouble(textBox1.Text);
            double limiteSuperior = Convert.ToDouble(textBox2.Text);
            int totalPedidos = Convert.ToInt32(textBox3.Text);

            int totalExperimentos = Convert.ToInt32(textBox4.Text);
            int seleccionado = Convert.ToInt32(textBox5.Text);
            int cantidadProductos = Convert.ToInt32(textBox9.Text);

            if (limiteInferior > limiteSuperior)
            {
                MessageBox.Show("Limite Superior debe ser mayor que limite inferior");
                return;
            }

            if (seleccionado < 0)
            {
                MessageBox.Show("El pedido seleccionado no puede ser menor que 0");
                return;
            }

            if (totalPedidos - seleccionado < 0)
            {
                MessageBox.Show("El pedido seleccionado no puede ser mayor que el total de pedidos");
                return;
            }

            List<double> listaSalida = new List<double>();
            SimulacionMonteCarlo simulacion = new SimulacionMonteCarlo();
            listaSalida = simulacion.MonteCarlo(limiteInferior, limiteSuperior, totalPedidos, totalExperimentos, seleccionado, cantidadProductos);

            textBox7.Text = listaSalida[0].ToString();
            textBox6.Text = listaSalida[1].ToString();
            textBox8.Text = listaSalida[2].ToString();

            List<Pedido> listaPaneles = simulacion.ListaPedidos;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Guardar archivo CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = saveFileDialog.FileName;
                    simulacion.EscribirPedidosEnCSV(listaPaneles, rutaArchivo,seleccionado);
                    MessageBox.Show($"La simulación se ha guardado en {rutaArchivo}");
                }
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            return;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
