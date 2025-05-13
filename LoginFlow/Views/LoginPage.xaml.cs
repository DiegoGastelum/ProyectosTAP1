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

    private async void TapRecuperarContraseña_Tapped(object sender, TappedEventArgs e)
    {
        string usuario = await DisplayPromptAsync("Recuperar contraseña", "Ingresa tu nombre de usuario:");
        if (string.IsNullOrWhiteSpace(usuario)) return;

        if (usuariosReg.ContainsKey(usuario))
        {
            string contrasena = usuariosReg[usuario];
            await DisplayAlert("Recuperación", $"Tu contraseña es: {contrasena}", "OK");
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
            await DisplayAlert("Error", "El usuario ya está registrado", "OK");
            return;
        }

        string nuevaContraseña = await DisplayPromptAsync("Registro", "Ingresa una contraseña:");
        if (string.IsNullOrWhiteSpace(nuevaContraseña)) return;

        usuariosReg[nuevoUsuario] = nuevaContraseña;
        await DisplayAlert("Éxito", $"Usuario {nuevoUsuario} registrado correctamente", "OK");
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
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }

    bool CredencialesSonValidas(string usuario, string contraseña)
    {
        return (usuario == "admin" && contraseña == "1234") ||
               (usuariosReg.ContainsKey(usuario) && usuariosReg[usuario] == contraseña);
    }
}
