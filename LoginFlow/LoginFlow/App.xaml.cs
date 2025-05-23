using System.Collections.ObjectModel; 
using Agenda_Personal;
using LoginFlow.Datos;
using LoginFlow.Modelos;

#if __ANDROID__
using Android.Content.Res;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif
namespace LoginFlow;

public partial class App : Application
{
    public ObservableCollection<Contacto> ListaContactos { get; set; } = new();

    public static ContactoDatabase BaseDatos { get; private set; }


    public App()
    {
        InitializeComponent();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "contactos.db3");
        BaseDatos = new ContactoDatabase(dbPath);

        MainPage = new AppShell();

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderLine", (handler, view) =>
        {
#if __ANDROID__
            (handler.PlatformView as Android.Views.View).SetBackgroundColor(Microsoft.Maui.Graphics.Colors.Transparent.ToAndroid());
#endif
        });
    }
}
