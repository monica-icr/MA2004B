using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoMonteCarlo.Clases
{
    public  class Pedido
    {
        public int IdPedido { get; set; }
        public int IdExperimento { get; set; }

        public int Cantidad { get; set; }

        public int FechaLimiteEntrega { get; set; }
        public int IdProducto { get; set; }


        public Pedido() 
        {
        }
        public Pedido(int idPedido, int idExperimento, int cantidad, int fechaLimiteEntrega, int idProducto) 
        {
            IdPedido = idPedido;
            IdExperimento = idExperimento;  
            Cantidad = cantidad;
            FechaLimiteEntrega = fechaLimiteEntrega;
            IdProducto = idProducto;

        }
    }

}
