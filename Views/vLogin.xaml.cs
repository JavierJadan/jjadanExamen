namespace jjadanExamen.Views;

public partial class vLogin : ContentPage
{
    public vLogin()
    {
        InitializeComponent();
        txtUsuario.Text = "";
        txtContrasena.Text = "";
    }
    
    
    private readonly string[,] usuarios = new string[4, 2]
    {
        { "recepcion",  "gym2026"  },
        { "entrenador", "fit_01"   },
        { "uisrael",    "2026"     },
        { "gerente",    "admin123" }
    };

    private async void  Btniniciar_OnClicked(object? sender, EventArgs e)
    {
        string usuario = txtUsuario.Text;
        string contrasena = txtContrasena.Text;

        if (usuario == "" || contrasena == "")
        {
            await DisplayAlertAsync("Error", "Complete el usuario y la contraseña.", "ok");
            return;
        }

        for (int fila = 0; fila < usuarios.GetLength(0); fila++)
        {
            if (usuarios[fila, 0] == usuario &&
                usuarios[fila, 1] == contrasena)
            {
                await DisplayAlertAsync("Acceso correcto",
                    $"Bienvenido, {usuario}", "OK");
                await Navigation.PushAsync(new vRegistro(usuario));
                return;
            }
        }

        await DisplayAlertAsync("Error",
            "Usuario o contraseña incorrectos", "OK");
        
        
    }

    private async void Btnacerca_OnClicked(object? sender, EventArgs e)
    {
        await DisplayAlertAsync("Acerca de", "Nombre: Jonnathan Javier\n" +
                                                 "Carrera: Informática\n" +
                                                "Paralelo: Séptimo A", "OK");
    }
}
