using P2DTP;

var currentDir = Directory.GetCurrentDirectory();

Console.WriteLine("");

var argOptions = new GetOpt(args);
var options = new Options();

if (!argOptions.TryGetValue("-s", out var sourcePath))
{
    sourcePath = currentDir;
    Console.WriteLine($"No -s specified, use {currentDir}");
}


string[] paths = new string[] { sourcePath };

if (!argOptions.TryGetValue("-d", out var desination))
{
    Console.WriteLine("No -d specified, exit");
    return;
}
options.DestinationPath = desination;


if (!argOptions.TryGetValue("-f", out var format))
{
    format =  Options.DefaultDateFormat;
    Console.WriteLine($"No -f specified, using {format}");
}
options.DateFormat = format;


Console.WriteLine("Paths: ");
foreach (string path in paths) { Console.Write(path + " "); }
Console.WriteLine();

var files = new Files().GetFiles(paths);
Console.WriteLine($"Files: {files.Count}");

List<FileAndDate> result = new List<FileAndDate>();

new Collector(files, out result);

new CopyFiles(result, options);

Console.WriteLine($"Done");
