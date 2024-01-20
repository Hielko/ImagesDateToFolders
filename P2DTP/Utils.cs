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
}
