using LoginFlow.Modelos;

namespace Agenda_Personal;

public partial class DetalleContactoPage : ContentPage
{
    public DetalleContactoPage(Contacto contacto)
    {
        InitializeComponent();
        BindingContext = contacto;
    }
}