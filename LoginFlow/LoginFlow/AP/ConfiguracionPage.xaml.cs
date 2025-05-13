using Microsoft.Maui.Storage;

namespace Agenda_Personal;

public partial class ConfiguracionPage : ContentPage
{
    public ConfiguracionPage()
    {
        InitializeComponent();

        bool notificacionesActivas = Preferences.Get("NotificacionesActivas", false);
        NotificacionesSwitch.IsToggled = notificacionesActivas;
    }

    private void NotificacionesSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        bool estaActivado = e.Value;

        Preferences.Set("NotificacionesActivas", estaActivado);

        string mensaje = estaActivado ? "Notificaciones activadas" : "Notificaciones desactivadas";
        DisplayAlert("Configuración", mensaje, "OK");
    }

    private void CambiarTema_Clicked(object sender, EventArgs e)
    {
        var temaActual = Application.Current.UserAppTheme;

        if (temaActual == AppTheme.Dark)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }

        Preferences.Set("Tema", Application.Current.UserAppTheme.ToString());
    }

}
