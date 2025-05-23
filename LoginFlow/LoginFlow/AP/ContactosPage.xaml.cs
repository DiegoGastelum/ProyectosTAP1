using System.Collections.ObjectModel;
using LoginFlow;
using LoginFlow.Modelos;

namespace Agenda_Personal;

public partial class ContactosPage : ContentPage
{
    public ObservableCollection<Contacto> Contactos => ((App)Application.Current).ListaContactos;

    public ContactosPage()
    {
        InitializeComponent();
        CargarContactosAsync();
        BindingContext = this;
    }

    private async void CargarContactosAsync()
    {
        string usuarioActual = Preferences.Get("UsuarioActual", null);

        if (string.IsNullOrEmpty(usuarioActual))
        {
            await DisplayAlert("Error", "No hay sesión activa.", "OK");
            return;
        }

        var usuario = await App.BaseDatos.GetUsuarioPorNombreAsync(usuarioActual);
        if (usuario == null)
        {
            await DisplayAlert("Error", "Usuario no encontrado en la base de datos.", "OK");
            return;
        }

        // Obtener solo los contactos del usuario actual
        var contactosDesdeDb = await App.BaseDatos.ObtenerContactosPorUsuarioAsync(usuario.Id);

        Contactos.Clear();

        foreach (var c in contactosDesdeDb)
            Contactos.Add(c);
    }
}
