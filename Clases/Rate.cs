using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoMonteCarlo.Clases
{
    public class Rate
    {
        public int IdMaquina { get; set; }
        public Dictionary<int, int> TiempoProduccion { get; set; }



        public Rate()
        {
        }
        public Rate(int idMaquina)
        {
            IdMaquina = idMaquina;
        }
        public Rate(Dictionary<int,int>tiempoProduccion, int idMaquina)
        {
            TiempoProduccion = tiempoProduccion;
            IdMaquina = idMaquina;
            
        }
        public void GenerarRatesAleatorios(double limiteInferior,
           double limiteSuperior, int CantidadProductos)
        {
            List<Rate> listaSalida = new List<Rate>();
            Dictionary<int, int> RatePorProducto = new Dictionary<int, int>();
            for (int i = 0; i < CantidadProductos; i++)
            {
                Random random = new Random();
                int valor = (int)((limiteSuperior - limiteInferior) * random.NextDouble() + limiteInferior);
                int idProducto = i;
                RatePorProducto.Add(idProducto, valor);

            }
            TiempoProduccion = RatePorProducto;
        }
    }
}
