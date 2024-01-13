namespace P2DTP
{
    public class CopyFiles
    {
        public CopyFiles(List<FileAndDate> fileAndDateList, Options? options)
        {
            int copyCount = 0;
            foreach (FileAndDate item in fileAndDateList)
            {
                //  https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
                var dateString = item.DateForNewPath.ToString(options?.DateFormat);
                var stringParts = dateString.Split(' ');
                var paths = String.Join(Path.DirectorySeparatorChar, stringParts);

                var newPath = Path.Combine(options?.DestinationPath, paths);
                Directory.CreateDirectory(newPath);


                var newFilename = newPath + Path.DirectorySeparatorChar + item?.File?.Name;

                if (File.Exists(newFilename) && Utils.AreFileContentsEqual(item?.File?.FullName, newFilename))
                {
                    // discard, do not copy
                }
                else
                {
                    newFilename = Utils.WhileExistsFileGetNewName(newPath + Path.DirectorySeparatorChar + item?.File?.Name);
                    File.Copy(item?.File?.FullName, newFilename);
                    copyCount++;
                }

            }
            Console.WriteLine($"{copyCount} copied");

        }
    }
}
