namespace LoginFlow.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (App.UsuarioActual != null)
        {
            lblNombre.Text = $"Hola, {App.UsuarioActual.Usuario}";
        }
        else
        {
            lblNombre.Text = "Usuario no identificado";
        }
    }
}