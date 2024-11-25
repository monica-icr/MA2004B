using AlgoritmoMonteCarlo.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoMonteCarlo.Algoritmos
{
    public class SimulacionMonteCarlo
    {
        public List<Pedido> ListaPedidos = new List<Pedido>();

        public List<Pedido> GenerarValoresAleatorios(double limiteInferior, 
            double limiteSuperior, int totalPaneles, int totalExperimentos, int cantidadProductos) 
        {
            List<Pedido> listaSalida = new List<Pedido>();
            for (int i = 0; i < totalExperimentos; i++) 
            {
                for (int j = 0; j < totalPaneles; j++) 
                {
                    Random random = new Random();
                    int valor = (int)((limiteSuperior - limiteInferior) * random.NextDouble()+ limiteInferior);
                    int fechaLimiteEntrega = random.Next(0, 7);
                    int idProducto = random.Next(0, cantidadProductos);
                    Pedido panel = new Pedido(j,i,valor, fechaLimiteEntrega, idProducto);
                    listaSalida.Add(panel);
                }
            
            }
            return listaSalida;
        }

        public void CalcularValoresMediaVarianza(List<Pedido> listaEntrada,
            int totalExperimentos, int seleccionado, List<double> listaSalida)
        {
            double sumaParcial = 0;
            double media = 0;
            List<Pedido> listaParcial = new List<Pedido>();
            for (int i = 0; i < totalExperimentos; i++)
            {
                List<Pedido> listaFiltrada = listaEntrada.FindAll(z=>z.IdExperimento.Equals(i));
                if (listaFiltrada.Count > 0)
                {
                    listaFiltrada =listaFiltrada.OrderBy(z=>z.Cantidad).ToList();

                    Pedido pedidoSeleccionado = listaFiltrada[seleccionado];
                    listaParcial.Add(pedidoSeleccionado);

                    sumaParcial = sumaParcial + pedidoSeleccionado.Cantidad;
                }
            }
            media = sumaParcial/totalExperimentos;
            double varianza = 0;
            foreach (Pedido pedido in listaParcial) {
                varianza += Math.Pow(pedido.Cantidad - media, 2);
            }
            if(listaParcial.Count!=1)
                varianza = varianza/(listaParcial.Count()-1);
            //Sacar la raiz cuadrada
            double desviacionEstandar = 0;
            desviacionEstandar = Math.Sqrt(varianza);

            listaSalida.Add(media);
            listaSalida.Add(varianza);
            
            listaSalida.Add(desviacionEstandar);
        }
        public List<double> MonteCarlo(double limiteInferior,
            double limiteSuperior, int totalPaneles, int totalExperimentos, int seleccionado, int cantidadProductos) 
        {
            List<double> listaSalida = new List<double>();

            ListaPedidos = GenerarValoresAleatorios(limiteInferior,
            limiteSuperior, totalPaneles, totalExperimentos, cantidadProductos);


             CalcularValoresMediaVarianza(ListaPedidos,
             totalExperimentos, seleccionado, listaSalida);

            

            return listaSalida;
        }

        public List<Pedido> GetListaPaneles()
        {
            return ListaPedidos;
        }

        public void EscribirPedidosEnCSV(List<Pedido> listaPedidos, string rutaArchivo, int seleccionado)
        {
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("IdPedido,IdExperimento,Cantidad, Fecha Limite, IdProducto");

            foreach (var pedido in listaPedidos)
                if (pedido.IdPedido == seleccionado)
                {
                    {
                        csvContent.AppendLine($"{pedido.IdPedido},{pedido.IdExperimento},{pedido.Cantidad},{pedido.FechaLimiteEntrega},{pedido.IdProducto}");
                    }
                }
            File.WriteAllText(rutaArchivo, csvContent.ToString());
        }
    }

}
