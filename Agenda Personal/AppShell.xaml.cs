namespace Agenda_Personal
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ContactosPage), typeof(ContactosPage));
            Routing.RegisterRoute(nameof(CrearContactosPage), typeof(CrearContactosPage));
            Routing.RegisterRoute(nameof(ConfiguracionPage), typeof(ConfiguracionPage));
            Routing.RegisterRoute(nameof(DetalleContactoPage), typeof(DetalleContactoPage));
        }
    }
}
