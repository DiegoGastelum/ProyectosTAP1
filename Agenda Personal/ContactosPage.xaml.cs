using System.Collections.ObjectModel;

namespace Agenda_Personal;

public partial class ContactosPage : ContentPage
{
    public ObservableCollection<Contacto> Contactos { get; set; } = new ObservableCollection<Contacto>();

    public ContactosPage()
    {
        InitializeComponent();
        Contactos.Add(new Contacto { Nombre = "Ana Lopez", Telefono = "123456789", Correo = "ana@correo.com" });
        Contactos.Add(new Contacto { Nombre = "Juan Perez", Telefono = "987654321", Correo = "juan@correo.com" });
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