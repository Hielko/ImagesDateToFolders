using P2DTP;

var currentDir = Directory.GetCurrentDirectory();

Console.WriteLine("");

var argOptions = new GetOpt(args);
var options = new Options();

if (!argOptions.TryGetValue("-s", out var sourcePath))
{
    Console.WriteLine("No -s specified");
    return;
}


string[] paths = new string[] { sourcePath };

if (!argOptions.TryGetValue("-d", out var desination))
{
    Console.WriteLine("No -d specified");
    return;
}
options.Desination = desination;


if (!argOptions.TryGetValue("-f", out var format))
{
    format = "yyyy MMMM";
}
options.Format = format;




Console.WriteLine("Paths: ");
foreach (string path in paths) { Console.Write(path + " "); }
Console.WriteLine();

var files = new Files().GetFiles(paths);
Console.WriteLine($"Files: {files.Count}");

List<Result> result = new List<Result>();

new Worker(files, out result);

new CopyFiles(result, options);

Console.WriteLine($"Done");

