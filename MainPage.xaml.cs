using System.Text.RegularExpressions;

namespace mnauparis1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            string identificacion = txtIdentificacion.Text?.Trim() ?? "";
            string nombres = txtNombres.Text?.Trim() ?? "";
            string apellidos = txtApellidos.Text?.Trim() ?? "";
            string telefono = txtTelefono.Text?.Trim() ?? "";
            string correo = txtCorreo.Text?.Trim() ?? "";
            string confirmarCorreo = txtConfirmarCorreo.Text?.Trim() ?? "";

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(identificacion) ||
                string.IsNullOrWhiteSpace(nombres) ||
                string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(confirmarCorreo))
            {
                await DisplayAlert(
                    "Validación",
                    "Todos los campos son obligatorios.",
                    "Aceptar");

                return;
            }

            // Validar solo números
            if (!SoloNumeros(identificacion))
            {
                await DisplayAlert(
                    "Validación",
                    "La identificación debe contener solo números.",
                    "Aceptar");

                return;
            }

            // Validar solo letras
            if (!SoloLetras(nombres))
            {
                await DisplayAlert(
                    "Validación",
                    "Los nombres deben contener solo letras.",
                    "Aceptar");

                return;
            }

            if (!SoloLetras(apellidos))
            {
                await DisplayAlert(
                    "Validación",
                    "Los apellidos deben contener solo letras.",
                    "Aceptar");

                return;
            }

            // Validar teléfono
            if (!SoloNumeros(telefono))
            {
                await DisplayAlert(
                    "Validación",
                    "El teléfono debe contener solo números.",
                    "Aceptar");

                return;
            }

            // Validar correos iguales
            if (correo != confirmarCorreo)
            {
                await DisplayAlert(
                    "Validación",
                    "Los correos electrónicos no coinciden.",
                    "Aceptar");

                return;
            }

            // Validar formato de correo
            if (!CorreoValido(correo))
            {
                await DisplayAlert(
                    "Validación",
                    "Ingrese un correo electrónico válido.",
                    "Aceptar");

                return;
            }

            // Guardar en TXT
            await GuardarArchivoTxt(
                identificacion,
                nombres,
                apellidos,
                telefono,
                correo);

            await DisplayAlert(
                "Correcto",
                "Los datos fueron guardados correctamente.",
                "Aceptar");

            LimpiarCampos();
        }

        private bool SoloNumeros(string texto)
        {
            return Regex.IsMatch(texto, @"^[0-9]+$");
        }

        private bool SoloLetras(string texto)
        {
            return Regex.IsMatch(
                texto,
                @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
        }

        private bool CorreoValido(string correo)
        {
            return Regex.IsMatch(
                correo,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private async Task GuardarArchivoTxt(
            string identificacion,
            string nombres,
            string apellidos,
            string telefono,
            string correo)
        {
            string contenido =
                $"Identificación: {identificacion}\n" +
                $"Nombres: {nombres}\n" +
                $"Apellidos: {apellidos}\n" +
                $"Teléfono: {telefono}\n" +
                $"Correo: {correo}\n" +
                $"Fecha: {DateTime.Now}\n" +
                "----------------------------------\n";

            string ruta = Path.Combine(
                FileSystem.AppDataDirectory,
                "inscripciones.txt");

            await File.AppendAllTextAsync(
                ruta,
                contenido);
        }

        private void LimpiarCampos()
        {
            txtIdentificacion.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtConfirmarCorreo.Text = "";
        }
    }
}