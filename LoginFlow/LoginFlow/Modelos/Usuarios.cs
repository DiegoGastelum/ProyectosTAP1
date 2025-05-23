using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace LoginFlow.Modelos
{
    public class Usuarios
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Usuario { get; set; } // Nombre de usuario único

        [MaxLength(100)]
        public string Contrasena { get; set; }
    }
}
