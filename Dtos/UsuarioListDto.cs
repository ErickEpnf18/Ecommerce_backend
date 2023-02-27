using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WepApiCRUD.Dtos
{
    public class UsuarioListDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }
    }
}