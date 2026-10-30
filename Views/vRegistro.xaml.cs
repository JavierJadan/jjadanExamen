using System.Globalization;

namespace jjadanExamen.Views;

public partial class vRegistro : ContentPage
{
    decimal Costo = 1200;
    decimal pagoMensual;
    string usuario;

    public vRegistro(string nuevoUsuario)
    {
        InitializeComponent();
        usuario = nuevoUsuario;
        lblUsuario.Text = $"Usuario conectado: {usuario}";
        dpFechaInicio.MinimumDate = DateTime.Today;
        dpFechaInicio.Date = DateTime.Today;
    }

    private async void OnCalcularClicked(object? sender, EventArgs e)
    {
        int edad;

        if (!int.TryParse(txtEdad.Text, out edad))
        {
            await DisplayAlertAsync("Alerta", "Ingrese una edad válida.", "OK");
            return;
        }

        if (edad < 16)
        {
            await DisplayAlertAsync("Alerta", "La edad debe ser mayor o igual a 16 años.", "OK");
            return;
        }

        if (!TryObtenerMonto(out decimal montoInicial))
        {
            await DisplayAlertAsync("Alerta", "El monto inicial debe estar entre 120 y 600.", "OK");
            txtPagoMensual.Text = string.Empty;
            pagoMensual = 0;
            return;
        }

        decimal recargoPorCuota = Costo * 0.02m;
        decimal valorCasillero = swCasillero.IsToggled ? 10m : 0m;
        pagoMensual = ((Costo - montoInicial) / 5m) + recargoPorCuota + valorCasillero;
        txtPagoMensual.Text = pagoMensual.ToString("0.00", CultureInfo.CurrentCulture);
    }

    private async void OnVerResumenClicked(object? sender, EventArgs e)
    {
        string nombre = txtNombre.Text?.Trim() ?? string.Empty;
        string apellido = txtApellido.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
        {
            await DisplayAlertAsync("Alerta", "Ingrese el nombre y el apellido.", "OK");
            return;
        }

        int edad;

        if (!int.TryParse(txtEdad.Text, out edad))
        {
            await DisplayAlertAsync("Alerta", "Ingrese una edad válida.", "OK");
            return;
        }

        if (edad < 16)
        {
            await DisplayAlertAsync("Alerta", "La edad debe ser mayor o igual a 16 años.", "OK");
            return;
        }

        if (pkPlan.SelectedItem is not string plan)
        {
            await DisplayAlertAsync("Alerta", "Seleccione un plan.", "OK");
            return;
        }

        if (!TryObtenerMonto(out decimal montoInicial) || pagoMensual <= 0)
        {
            await DisplayAlertAsync("Alerta", "Ingrese un monto válido y calcule el pago mensual.", "OK");
            return;
        }

        decimal pagoTotal = montoInicial + (pagoMensual * 5m);
        await Navigation.PushAsync(new vResumen(
            usuario, nombre, apellido, edad, dpFechaInicio.Date ?? DateTime.Today, plan,
            swCasillero.IsToggled, montoInicial, pagoMensual, pagoTotal));
    }

    private bool TryObtenerMonto(out decimal montoInicial)
    {
        return decimal.TryParse(txtMontoInicial.Text, NumberStyles.Number,
                   CultureInfo.CurrentCulture, out montoInicial)
               && montoInicial >= 120m
               && montoInicial <= 600m;
    }
}
