namespace Agenda_Personal;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }
    private async void IrListaContactos(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ContactosPage());
    }

    private async void IrCrearContacto(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearContactosPage());
    }

    private async void IrConfiguracion(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ConfiguracionPage());
    }

}
