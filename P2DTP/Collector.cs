using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace ImagesDateToFolders
{
    public class Collector
    {

        static bool ImagesWithExifExtenstions(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            string[] imgExtensions = new string[] { ".jpg", ".jpeg" };
            return Array.IndexOf(imgExtensions, extension) > -1;
        }


        public Collector(List<FileInfo> files, out List<FileAndDate> fileAndDateList)
        {

            var tempFileAndDateList = new List<FileAndDate>();
            Console.WriteLine($"Start reading exif");

            Parallel.ForEach(files, file =>
            {
                if (ImagesWithExifExtenstions(file.FullName))
                {
                    try
                    {
                        bool writeWithFileDate = false;
                        using (Image image = Image.Load(file.FullName))
                        {
                            if (image.Metadata.ExifProfile != null)
                            {
                                if (image.Metadata.ExifProfile.TryGetValue(ExifTag.DateTimeOriginal, out var ExifDateString))
                                {
                                    if (ExifDateString != null)
                                    {
                                        var correctDate = ExifDateString?.ToString()?.Substring(0, 10).Replace(":", "/") + ExifDateString?.ToString()?.Substring(10, 9);

                                        if (DateTime.TryParse(correctDate, out DateTime date))
                                        {
                                            tempFileAndDateList.Add(new FileAndDate { DateForNewPath = date, File = file });
                                        }
                                        else
                                        {
                                            writeWithFileDate = true;
                                        }
                                    }
                                }
                                else
                                {
                                    writeWithFileDate = true;
                                }
                            }
                            else
                            {
                                writeWithFileDate = true;
                            }
                        }
                        if (writeWithFileDate)
                        {
                            tempFileAndDateList.Add(new FileAndDate { DateForNewPath = file.LastWriteTimeUtc, File = file });
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"  Error reading {file.FullName}:  {e.Message}");
                    }



                }
                else
                {
                    // Not an image, so take the file date
                    tempFileAndDateList.Add(new FileAndDate { DateForNewPath = file.LastWriteTimeUtc, File = file });
                }

            });

            fileAndDateList = tempFileAndDateList;
        }
    }
}
