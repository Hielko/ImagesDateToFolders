namespace ImagesDateToFolders
{
    public class Files
    {
        public List<FileInfo> GetFiles(string[] paths, string wildcard = "*.*")
        {
            var files = new List<FileInfo>();

            foreach (var path in paths)
            {
                foreach (var file in Directory.EnumerateFiles(path, wildcard, new EnumerationOptions { RecurseSubdirectories = true }))
                {
                    files.Add(new FileInfo(file));
                }
            }

            return files;
        }
    }
}
