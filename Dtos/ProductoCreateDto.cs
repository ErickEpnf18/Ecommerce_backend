using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WepApiCRUD.Dtos
{
    public class ProductoCreateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }

        
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }

        public ProductoCreateDto()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }
    }
}