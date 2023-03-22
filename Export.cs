namespace Hawk_Dove;

public sealed class Export : IDisposable
{
	private const char CsvSeparator = ';';
	private readonly StreamWriter stream;
	public static string FileName => $"HawkDove {DateTime.Now:MM-dd_HH-mm-ss}.csv";
	public static FileInfo OutputLocation => new(Path.Combine
		(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FileName));

	public Export()
	{
		stream = OutputLocation.CreateText();
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