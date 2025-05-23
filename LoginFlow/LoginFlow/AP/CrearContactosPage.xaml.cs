using LoginFlow;
using LoginFlow.Modelos;

namespace Agenda_Personal;

public partial class CrearContactosPage : ContentPage
{
    public CrearContactosPage()
    {
        InitializeComponent();
    }

    private async void GuardarContacto(object sender, EventArgs e)
    {
        var nuevoContacto = new Contacto
        {
            Nombre = NombreEntry.Text,
            Telefono = TelefonoEntry.Text,
            Correo = CorreoEntry.Text,
            Direccion = DireccionEntry.Text
        };

        string usuarioActual = Preferences.Get("UsuarioActual", null);

        if (!string.IsNullOrEmpty(usuarioActual))
        {
            var usuario = await App.BaseDatos.GetUsuarioPorNombreAsync(usuarioActual);

            if (usuario != null)
            {
                // Asignar el Id del usuario actual al nuevo contacto
                nuevoContacto.UsuarioId = usuario.Id;

                await App.BaseDatos.GuardarContactoAsync(nuevoContacto);

                ((App)Application.Current).ListaContactos.Add(nuevoContacto);

                await DisplayAlert("Éxito", "Contacto guardado correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se encontró el usuario actual", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "No hay usuario actual", "OK");
        }
    }
}
