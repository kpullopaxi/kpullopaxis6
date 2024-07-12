using kpullopaxis6.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace kpullopaxis6.Views;

public partial class ActEliminar : ContentPage
{
    public ActEliminar(Estudiante datos)
    {
        InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        var codigo = txtCodigo.Text;
        var nombre = txtNombre.Text;
        var apellido = txtApellido.Text;
        var edad = txtEdad.Text;

        if (string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(edad))
        {
            await DisplayAlert("Error", "Por favor, llene todos los campos", "OK");
            return;
        }

        var estudiante = new
        {
            codigo = codigo,
            nombre = nombre,
            apellido = apellido,
            edad = edad
        };

        var json = JsonConvert.SerializeObject(estudiante);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync($"http://192.168.1.105/semana6/estudiantews.php?codigo={codigo}&nombre={nombre}&apellido={apellido}&edad={edad}", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "OK");
                    await Navigation.PushAsync(new vEstudiante());
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo actualizar el estudiante", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var codigo = txtCodigo.Text;

        if (string.IsNullOrEmpty(codigo))
        {
            await DisplayAlert("Error", "Por favor, ingrese el código del estudiante", "OK");
            return;
        }

        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"http://192.168.1.105/semana6/estudiantews.php?codigo={codigo}");

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "OK");
                    await Navigation.PushAsync(new vEstudiante());
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el estudiante", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "OK");
        }
    }
}
