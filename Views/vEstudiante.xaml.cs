using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace kpullopaxis6.Views;

public partial class vEstudiante : ContentPage
{
	private const string Url = "http://192.168.1.105//semana6/estudiantews.php";
	private readonly HttpClient cliente= new HttpClient();
	private ObservableCollection<Models.Estudiante> est;

    public vEstudiante()
	{
		InitializeComponent();
		mostrar();

    }
	public async void mostrar() 
	{
		var content = await cliente.GetStringAsync(Url);	
		List<Models.Estudiante> mostrar = JsonConvert.DeserializeObject<List<Models.Estudiante>>(content);
		est = new ObservableCollection<Models.Estudiante>(mostrar);
        ListaEstudiantes.ItemsSource = est;

	}
}