using System.Collections.Generic;
using System.Threading.Tasks;
using LoginFlow.Modelos;
using SQLite;

namespace LoginFlow.Datos
{
    public class ContactoDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public ContactoDatabase(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Contacto>().Wait();
            _db.CreateTableAsync<Usuarios>().Wait();
        }

        // Contactos
        public Task<List<Contacto>> ObtenerContactosAsync() =>
            _db.Table<Contacto>().ToListAsync();

        public Task<List<Contacto>> ObtenerContactosPorUsuarioAsync(int usuarioId) =>
            _db.Table<Contacto>()
               .Where(c => c.UsuarioId == usuarioId && c.Activo)
               .ToListAsync();

        public Task<Contacto> GetItemAsync(int id) =>
            _db.Table<Contacto>().Where(i => i.Id == id).FirstOrDefaultAsync();

        public Task<int> GuardarContactoAsync(Contacto contacto) =>
            contacto.Id != 0 ? _db.UpdateAsync(contacto) : _db.InsertAsync(contacto);

        public Task<int> EliminarContactoAsync(Contacto contacto) =>
            _db.DeleteAsync(contacto);

        // Usuarios
        public Task<Usuarios> GetUsuarioPorNombreAsync(string nombreUsuario) =>
            _db.Table<Usuarios>().Where(u => u.Usuario == nombreUsuario).FirstOrDefaultAsync();

        public Task<int> GuardarUsuarioAsync(Usuarios usuario) =>
            usuario.Id != 0 ? _db.UpdateAsync(usuario) : _db.InsertAsync(usuario);

        public Task<int> EliminarUsuarioAsync(Usuarios usuario) =>
            _db.DeleteAsync(usuario);

        public async Task<bool> ValidarUsuarioAsync(string nombreUsuario, string contrasena)
        {
            var usuario = await GetUsuarioPorNombreAsync(nombreUsuario);
            return usuario?.Contrasena == contrasena;
        }
    }
}
