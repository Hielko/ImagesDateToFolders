namespace ImagesDateToFolders
{
    public class Utils
    {
        public static bool AreFileContentsEqual(string path1, string path2) =>
              File.ReadAllBytes(path1).SequenceEqual(File.ReadAllBytes(path2));


        public static string WhileExistsFileGetNewName(string filename)
        {
            int count = 1;
            var newFilename = filename;

            while (File.Exists(newFilename))
            {
                var fi = new FileInfo(filename);
                var extenstion = fi.Extension;

                newFilename = fi.FullName.Substring(0, filename.Length - extenstion.Length) + "(" + count + ")" + extenstion;
                count++;
            }
            return newFilename;
        }
    }


    public static class Extentions
    {

        static readonly string[] SizeSuffixes =
          { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string SizeSuffix(this long value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value, decimalPlaces); }

            int i = 0;
            decimal dValue = value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }
    }
}
