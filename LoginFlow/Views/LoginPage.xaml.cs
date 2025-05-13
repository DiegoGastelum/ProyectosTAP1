namespace LoginFlow.Views;

public partial class LoginPage : ContentPage
{
    Dictionary<string, string> usuariosReg = new();

    public LoginPage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }

    private async void TapRecuperarContrase�a_Tapped(object sender, TappedEventArgs e)
    {
        string usuario = await DisplayPromptAsync("Recuperar contrase�a", "Ingresa tu nombre de usuario:");
        if (string.IsNullOrWhiteSpace(usuario)) return;

        if (usuariosReg.ContainsKey(usuario))
        {
            string contrasena = usuariosReg[usuario];
            await DisplayAlert("Recuperaci�n", $"Tu contrase�a es: {contrasena}", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Usuario no encontrado", "OK");
        }
    }

    private async void TapRegistrarUsuario_Tapped(object sender, TappedEventArgs e)
    {
        string nuevoUsuario = await DisplayPromptAsync("Registro", "Ingresa un nombre de usuario:");
        if (string.IsNullOrWhiteSpace(nuevoUsuario)) return;

        if (usuariosReg.ContainsKey(nuevoUsuario))
        {
            await DisplayAlert("Error", "El usuario ya est� registrado", "OK");
            return;
        }

        string nuevaContrase�a = await DisplayPromptAsync("Registro", "Ingresa una contrase�a:");
        if (string.IsNullOrWhiteSpace(nuevaContrase�a)) return;

        usuariosReg[nuevoUsuario] = nuevaContrase�a;
        await DisplayAlert("�xito", $"Usuario {nuevoUsuario} registrado correctamente", "OK");
    }

    private async void BotonIniciarSesion_Clicked(object sender, EventArgs e)
    {
        string usuario = Username.Text?.Trim();
        string contrasena = Password.Text?.Trim();

        if (CredencialesSonValidas(usuario, contrasena))
        {
            Preferences.Set("UsuarioActual", usuario);
            await SecureStorage.SetAsync("SesionIniciada", "true");
            await Shell.Current.GoToAsync("///home");
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contrase�a incorrectos", "OK");
        }
    }

    bool CredencialesSonValidas(string usuario, string contrase�a)
    {
        return (usuario == "admin" && contrase�a == "1234") ||
               (usuariosReg.ContainsKey(usuario) && usuariosReg[usuario] == contrase�a);
    }
}
