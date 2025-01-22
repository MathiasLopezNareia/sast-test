using Newtonsoft.Json;

namespace sast_test;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);

		string maliciousJson = @"{
                '$type': 'System.Diagnostics.Process, System',
                'StartInfo': {
                    'FileName': 'cmd.exe',
                    'Arguments': '/c echo Deserialización insegura explotada!'
                }
            }";

		try
		{
			var obj = JsonConvert.DeserializeObject(maliciousJson);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
	}
}

