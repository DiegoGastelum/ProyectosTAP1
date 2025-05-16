using System.Collections.ObjectModel;
using LoginFlow;

namespace Agenda_Personal;

public partial class ContactosPage : ContentPage
{
    public ObservableCollection<Contacto> Contactos => ((App)Application.Current).ListaContactos;

    public ContactosPage()
    {
        InitializeComponent();

        if (Contactos.Count == 0)
        {
            Contactos.Add(new Contacto { Nombre = "Ana Lopez", Telefono = "123456789", Correo = "ana@correo.com", Direccion = "Ensenada" });
            Contactos.Add(new Contacto { Nombre = "Juan Perez", Telefono = "987654321", Correo = "juan@correo.com", Direccion = "Ensenada" });
        }

        BindingContext = this;
    }
}
public class Contacto
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    public string Direccion { get; set; }
}