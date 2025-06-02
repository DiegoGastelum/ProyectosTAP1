using LoginFlow.Modelos;

namespace LoginFlow;
public partial class ModificarContactoPage : ContentPage
{
    private Contacto _contacto;

    public ModificarContactoPage(Contacto contacto)
    {
        InitializeComponent();
        _contacto = contacto;

        NombreEntry.Text = _contacto.Nombre;
        TelefonoEntry.Text = _contacto.Telefono;
        CorreoEntry.Text = _contacto.Correo;
        DireccionEntry.Text = _contacto.Direccion;
    }

    private async void GuardarCambios_Clicked(object sender, EventArgs e)
    {
        _contacto.Nombre = NombreEntry.Text;
        _contacto.Telefono = TelefonoEntry.Text;
        _contacto.Correo = CorreoEntry.Text;
        _contacto.Direccion = DireccionEntry.Text;

        await App.BaseDatos.GuardarContactoAsync(_contacto);

        await DisplayAlert("Éxito", "Contacto modificado correctamente", "OK");
        await Navigation.PopAsync();
    }
}
