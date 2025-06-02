using LoginFlow;
using LoginFlow.Modelos;

namespace Agenda_Personal;

public partial class CrearContactosPage : ContentPage
{
    private Contacto _contactoActual;

    public CrearContactosPage()
    {
        InitializeComponent();
    }

    public CrearContactosPage(Contacto contacto)
    {
        InitializeComponent();

        _contactoActual = contacto;

        NombreEntry.Text = _contactoActual.Nombre;
        TelefonoEntry.Text = _contactoActual.Telefono;
        CorreoEntry.Text = _contactoActual.Correo;
        DireccionEntry.Text = _contactoActual.Direccion;
    }

    private async void GuardarContacto(object sender, EventArgs e)
    {
        if (_contactoActual == null)
        {
            _contactoActual = new Contacto();
        }

        _contactoActual.Nombre = NombreEntry.Text;
        _contactoActual.Telefono = TelefonoEntry.Text;
        _contactoActual.Correo = CorreoEntry.Text;
        _contactoActual.Direccion = DireccionEntry.Text;

        string usuarioActual = Preferences.Get("UsuarioActual", null);

        if (!string.IsNullOrEmpty(usuarioActual))
        {
            var usuario = await App.BaseDatos.GetUsuarioPorNombreAsync(usuarioActual);
            if (usuario != null)
            {
                _contactoActual.UsuarioId = usuario.Id;

                await App.BaseDatos.GuardarContactoAsync(_contactoActual);

                if (!((App)Application.Current).ListaContactos.Contains(_contactoActual))
                    ((App)Application.Current).ListaContactos.Add(_contactoActual);

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