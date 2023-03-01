namespace Hawk_Dove;

public sealed class Output : IDisposable
{
	private readonly StreamWriter stream;
	public string FileName => "HawkDove_1.0_" + DateTime.Now.ToString("MM-dd_HH-mm-ss") + ".csv";
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

	public void Dispose()
	{
		stream.Flush();
		stream.Dispose();
	}
}