namespace ImagesDateToFolders
{
    public class Options
    {
        public const string DefaultDateFormat = "yyyy MM";

        public string? DateFormat { get; set; } = Options.DefaultDateFormat;
        public string? DestinationPath { get; set; }
    }
}
