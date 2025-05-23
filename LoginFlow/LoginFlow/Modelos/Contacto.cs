using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace LoginFlow.Modelos
{
    public class Contacto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Telefono { get; set; }

        [MaxLength(100)]
        public string Correo { get; set; }

        [MaxLength(100)]
        public string Direccion { get; set; }

        public bool Activo { get; set; } = true;
    }
}
