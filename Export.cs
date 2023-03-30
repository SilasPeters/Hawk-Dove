namespace Hawk_Dove;

public sealed class Export : IDisposable
{
	public const char CsvSeparator = ';';
	private readonly int seed;
	private readonly StreamWriter stream;
	public string FileName => $"HawkDove {DateTime.Now:MM-dd_HH-mm-ss} - {seed}.csv";
	public FileInfo OutputLocation => new(Path.Combine
		(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FileName));

	public Export(int seed)
	{
		this.seed = seed;
		stream    = OutputLocation.CreateText();
	}
	
	public void AddRow(params string[] values)
	{
		stream.WriteLine(string.Join(CsvSeparator, values));
	}

	public void Dispose()
	{
		stream.Flush();
		stream.Dispose();
		
		Console.Out.Write($"Wrote output to {OutputLocation}");
	}
}
