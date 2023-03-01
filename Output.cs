namespace Hawk_Dove;

public sealed class Output
{
	private readonly StreamWriter stream;
	public string FileName => "HawkDove 1.0 " + DateTime.Now.ToString("MM-dd HH-mm-ss") + ".csv";
	public FileInfo OutputLocation => new(Path.Combine
		(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
			FileName));

	public Output()
	{
		stream = OutputLocation.CreateText();
	}
	
	public void WriteLine(params string[] values)
	{
		stream.WriteLine(string.Join(';', values));
	}
}