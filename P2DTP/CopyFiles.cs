namespace P2DTP
{
    public class CopyFiles
    {
        public CopyFiles(List<FileAndDate> fileAndDateList, Options? options)
        {
            int copyCount = 0;
            int skipCount = 0;
            foreach (var fileAndDate in fileAndDateList)
            {
                //  https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1

                var dateString = fileAndDate.DateForNewPath.ToString(options?.DateFormat);
                var stringParts = dateString.Split(' ');
                var paths = String.Join(Path.DirectorySeparatorChar, stringParts);
                var newPath = Path.Combine(options?.DestinationPath, paths);
                Directory.CreateDirectory(newPath);

                var newFilename = newPath + Path.DirectorySeparatorChar + fileAndDate?.File?.Name;

                if (File.Exists(newFilename) && Utils.AreFileContentsEqual(fileAndDate?.File?.FullName, newFilename))
                {
                    skipCount++;
                    // discard, do not copy
                }
                else
                {
                    newFilename = Utils.WhileExistsFileGetNewName(newPath + Path.DirectorySeparatorChar + fileAndDate?.File?.Name);
                    File.Copy(fileAndDate?.File?.FullName, newFilename);
                    copyCount++;
                }
            }

            Console.WriteLine($"{copyCount} copied");
            Console.WriteLine($"{skipCount} skipped");
        }
    }
}
