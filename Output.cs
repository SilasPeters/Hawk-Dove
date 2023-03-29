namespace Hawk_Dove;

public sealed class Output : IDisposable
{
	private readonly StreamWriter stream;
	private readonly int seed;
	public string FileName => "HawkDove_1.0_" + DateTime.Now.ToString("MM-dd_HH-mm-ss") + "_" +seed.ToString() +".csv";
	public FileInfo OutputLocation => new(Path.Combine
		(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
			FileName));

	public Output(int seed)
	{
		this.seed = seed;
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
		
		Console.Out.Write($"Wrote output to {OutputLocation}");
	}
}