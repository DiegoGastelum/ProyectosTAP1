using LoginFlow.Datos;
using LoginFlow.Modelos;

namespace LoginFlow.Views;

public partial class LoginPage : ContentPage
{
    private readonly ContactoDatabase _database;

    public LoginPage()
    {
        InitializeComponent();
        _database = App.BaseDatos;
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

        var usuarioObj = await _database.GetUsuarioPorNombreAsync(usuario);
        if (usuarioObj != null)
        {
            await DisplayAlert("Recuperación", $"Tu contraseña es: {usuarioObj.Contrasena}", "OK");
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

        var usuarioExistente = await _database.GetUsuarioPorNombreAsync(nuevoUsuario);
        if (usuarioExistente != null)
        {
            await DisplayAlert("Error", "El usuario ya está registrado", "OK");
            return;
        }

        string nuevaContraseña = await DisplayPromptAsync("Registro", "Ingresa una contraseña:");
        if (string.IsNullOrWhiteSpace(nuevaContraseña)) return;

        var nuevoUsuarioObj = new Usuarios
        {
            Usuario = nuevoUsuario,
            Contrasena = nuevaContraseña
        };

        await _database.GuardarUsuarioAsync(nuevoUsuarioObj);
        await DisplayAlert("Éxito", $"Usuario {nuevoUsuario} registrado correctamente", "OK");
    }

    private async void BotonIniciarSesion_Clicked(object sender, EventArgs e)
    {
        string usuario = Username.Text?.Trim();
        string contrasena = Password.Text?.Trim();

        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
        {
            await DisplayAlert("Campos vacíos", "Por favor, ingresa tanto el usuario como la contraseña.", "OK");
            return;
        }

        bool valido = await _database.ValidarUsuarioAsync(usuario, contrasena);

        if (valido)
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
}
