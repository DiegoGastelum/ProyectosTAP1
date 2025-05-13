using System.Collections.ObjectModel;

namespace Agenda_Personal
{
    public partial class App : Application
    {
        public ObservableCollection<Contacto> ListaContactos { get; set; } = new ObservableCollection<Contacto>();

        public App()
        {
            InitializeComponent();

            var temaGuardado = Preferences.Get("Tema", "Light");

            if (temaGuardado == "Dark")
                Application.Current.UserAppTheme = AppTheme.Dark;
            else
                Application.Current.UserAppTheme = AppTheme.Light;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }

}