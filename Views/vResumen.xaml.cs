namespace jjadanExamen.Views;

public partial class vResumen : ContentPage
{
    public vResumen(string usuario, string nombre, string apellido, int edad,
        DateTime fechaInicio, string plan, bool tieneCasillero,
        decimal montoInicial, decimal pagoMensual, decimal pagoTotal)
    {
        InitializeComponent();
        lblUsuarioConectado.Text = $"Usuario conectado: {usuario}";
        lblNombre.Text = nombre;
        lblApellido.Text = apellido;
        lblEdad.Text = edad.ToString();
        lblFechaInicio.Text = fechaInicio.ToString("d");
        lblPlan.Text = plan;

        if (tieneCasillero == true)
        {
            lblCasillero.Text = "Sí";
        }
        else
        {
            lblCasillero.Text = "No";
        }

        lblMontoInicial.Text = montoInicial.ToString("0.00");
        lblPagoMensual.Text = pagoMensual.ToString("0.00");
        lblPagoTotal.Text = pagoTotal.ToString("0.00");
    }

    private async void OnExportarClicked(object? sender, EventArgs e)
    {
        string contenido =
            lblUsuarioConectado.Text + "\n" +
            "Nombre: " + lblNombre.Text + "\n" +
            "Apellido: " + lblApellido.Text + "\n" +
            "Edad: " + lblEdad.Text + "\n" +
            "Fecha de inicio: " + lblFechaInicio.Text + "\n" +
            "Plan: " + lblPlan.Text + "\n" +
            "Casillero: " + lblCasillero.Text + "\n" +
            "Monto inicial: " + lblMontoInicial.Text + "\n" +
            "Pago mensual: " + lblPagoMensual.Text + "\n" +
            "Pago total: " + lblPagoTotal.Text;

        string ruta = Path.Combine(FileSystem.AppDataDirectory, "resumen_socio.txt");
        File.WriteAllText(ruta, contenido);

        await DisplayAlertAsync("Exportar", "Archivo guardado en:\n" + ruta, "OK");
    }

    private async void OnCerrarSesionClicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
