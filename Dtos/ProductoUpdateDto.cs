using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WepApiCRUD.Dtos
{
    public class ProductoUpdateDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }

    }
}