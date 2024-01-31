using ImagesDateToFolders;

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
    format = Options.DefaultDateFormat;
    Console.WriteLine($"No -f specified, using {format}");
    Console.WriteLine(DateFormats());
}
options.DateFormat = format;


Console.WriteLine($"Path: {sourcePath}");

var files = new Files().GetFiles(sourcePath);
Console.WriteLine($"Files: {files.Count}");

List<FileAndDate> result = new List<FileAndDate>();

new Collector(files, out result);

new CopyFiles(result, options);

Console.WriteLine($"Done");



string DateFormats()
{
    var text = @"

    d: Represents the day of the month as a number from 1 through 31.
    dd: Represents the day of the month as a number from 01 through 31.
    ddd: Represents the abbreviated name of the day (Mon, Tues, Wed, etc).
    dddd: Represents the full name of the day (Monday, Tuesday, etc).
    h: 12-hour clock hour (e.g. 4).
    hh: 12-hour clock, with a leading 0 (e.g. 06)
    H: 24-hour clock hour (e.g. 15)
    HH: 24-hour clock hour, with a leading 0 (e.g. 22)
    m: Minutes
    mm: Minutes with a leading zero
    M: Month number(eg.3)
    MM: Month number with leading zero(eg.04)
    MMM: Abbreviated Month Name (e.g. Dec)
    MMMM: Full month name (e.g. December)
    s: Seconds
    ss: Seconds with leading zero
    t: Abbreviated AM / PM (e.g. A or P)
    tt: AM / PM (e.g. AM or PM
    y: Year, no leading zero (e.g. 2015 would be 15)
    yy: Year, leading zero (e.g. 2015 would be 015)
    yyy: Year, (e.g. 2015)
    yyyy: Year, (e.g. 2015)
    K: Represents the time zone information of a date and time value (e.g. +05:00)
    z: With DateTime values represent the signed offset of the local operating system's time zone from
    Coordinated Universal Time (UTC), measured in hours. (e.g. +6)
    zz: As z, but with leading zero (e.g. +06)
    zzz: With DateTime values represents the signed offset of the local operating system's time zone from UTC, measured in hours and minutes. (e.g. +06:00)
    f: Represents the most significant digit of the seconds fraction; that is, it represents the tenths of a second in a date and time value.
    ff: Represents the two most significant digits of the second's fraction in date and time
    fff: Represents the three most significant digits of the second's fraction; that is, it represents the milliseconds in a date and time value.
    ffff: Represents the four most significant digits of the second's fraction; that is, it represents the ten-thousandths of a second in a date and time value. While it is possible to display the ten thousandths of a second component of a time value, that value may not be meaningful.
    fffff: Represents the five most significant digits of the second's fraction; that is, it represents the hundred-thousandths of a second in a date and time value.
    ffffff: Represents the six most significant digits of the second's fraction; that is, it represents the millionths of a second in a date and time value.
    fffffff: Represents the seven most significant digits of the second's fraction; that is, it represents the ten-millionths of a second in a date and time value.
";

    return text;

}