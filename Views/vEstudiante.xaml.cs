using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace kpullopaxis6.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "http://192.168.1.105/semana6/estudiantews.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Models.Estudiante> est;

    public vEstudiante()
    {
        InitializeComponent();
        est = new ObservableCollection<Models.Estudiante>();
        mostrar();
    }

    public async void mostrar()
    {
        try
        {
            var content = await cliente.GetStringAsync(Url);
            var mostrar = JsonConvert.DeserializeObject<List<Models.Estudiante>>(content);
            est = new ObservableCollection<Models.Estudiante>(mostrar);
            ListaEstudiantes.ItemsSource = est;
        }
        catch (JsonReaderException jsonEx)
        {
            await DisplayAlert("Error", "Error al deserializar el JSON: " + jsonEx.Message, "OK");
        }
        catch (HttpRequestException httpEx)
        {
            await DisplayAlert("Error", "Error en la solicitud HTTP: " + httpEx.Message, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Error inesperado: " + ex.Message, "OK");
        }
    }
}
