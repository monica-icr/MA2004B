using AlgoritmoMonteCarlo.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoMonteCarlo.Algoritmos
{
    public class EscribirRatesEnCsv
    {

        public void EscribirRatesEnCSV(List<Rate> listaRate, string rutaArchivo)
        {
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("IdMaquina,IdProducto,Rate");

            foreach (var rate in listaRate)
                foreach (var tiempo in rate.TiempoProduccion)
                {
                    {
                        csvContent.AppendLine($"{rate.IdMaquina},{tiempo.Key},{tiempo.Value}");
                    }
                }
            File.WriteAllText(rutaArchivo, csvContent.ToString());
        }
    }

}
