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

    private async void Contacto_Tapped(object sender, EventArgs e)
    {
        var grid = sender as Grid;
        var contacto = grid?.BindingContext as Contacto;

        if (contacto == null) return;

        await Navigation.PushAsync(new DetalleContactoPage(contacto));
    }

    private async void ModificarContacto_Invoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var contacto = swipeItem?.BindingContext as Contacto;

        if (contacto == null) return;

        await Navigation.PushAsync(new ModificarContactoPage(contacto));
    }

    private async void EliminarContacto_Invoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var contacto = swipeItem?.BindingContext as Contacto;

        if (contacto == null) return;

        bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar contacto {contacto.Nombre}?", "Sí", "No");
        if (!confirm) return;

        await App.BaseDatos.EliminarContactoAsync(contacto);
        Contactos.Remove(contacto);
    }
}
