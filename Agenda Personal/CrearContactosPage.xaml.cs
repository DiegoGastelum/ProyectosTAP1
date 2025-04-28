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

        await DisplayAlert("Éxito", "Contacto guardado correctamente", "OK");
        await Navigation.PopAsync();
    }
}