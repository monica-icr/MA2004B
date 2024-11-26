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
                    textBox9.Text.Equals("") ||
                    textBox10.Text.Equals("") ||
                    textBox11.Text.Equals("") ||
                    textBox12.Text.Equals("") ||
                    textBox13.Text.Equals(""))
            {
                MessageBox.Show("Los números tienen que ser diferente de VACÍOS");
                return;
            }



            double limiteInferior = Convert.ToDouble(textBox1.Text);
            double limiteSuperior = Convert.ToDouble(textBox2.Text);
            int totalPedidos = Convert.ToInt32(textBox3.Text);
            int rateMinimo = Convert.ToInt32(textBox11.Text);
            int rateMaximo = Convert.ToInt32(textBox10.Text);
            int rateMinimoHorno = Convert.ToInt32(textBox13.Text);
            int rateMaximoHorno = Convert.ToInt32(textBox12.Text);

            if (!(rateMinimo > rateMaximoHorno))
            {
                MessageBox.Show("Rate Máximo Horno debe ser menor que Rate Mínimo");
                return;
            }

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
                    simulacion.EscribirPedidosEnCSV(listaPaneles, rutaArchivo, seleccionado);
                    MessageBox.Show($"La simulación se ha guardado en {rutaArchivo}");
                }
            }

            List<Rate> listaRates = new List<Rate>();
            for (int i = 0; i < 8; i++)
            {
                if (i == 3 | i == 4)
                {
                    Rate rate = new Rate(i);
                    rate.GenerarRatesAleatorios(rateMinimoHorno, rateMaximoHorno, cantidadProductos);
                    listaRates.Add(rate);
                }
                else
                {
                    Rate rate = new Rate(i);
                    rate.GenerarRatesAleatorios(rateMinimo, rateMaximo, cantidadProductos);
                    listaRates.Add(rate);
                }
            }

            EscribirRatesEnCsv escribirRates = new EscribirRatesEnCsv();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Guardar archivo CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = saveFileDialog.FileName;
                    escribirRates.EscribirRatesEnCSV(listaRates, rutaArchivo);
                    MessageBox.Show($"Los rates se han guardado en {rutaArchivo}");
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
