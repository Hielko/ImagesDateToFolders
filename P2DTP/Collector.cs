using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace P2DTP
{
    public class Collector
    {
        public Collector(List<FileInfo> files, out List<FileAndDate> fileAndDateList)
        {
            fileAndDateList = new List<FileAndDate>();
            foreach (FileInfo file in files)
            {
                if (Utils.IsImageExtenstion(file.FullName))
                {
                    using (Image image = Image.Load(file.FullName))
                    {
                        if (image.Metadata.ExifProfile != null)
                        {
                            if (image.Metadata.ExifProfile.TryGetValue(ExifTag.DateTime, out var ExifStringDate))
                            {
                                if (ExifStringDate != null)
                                {
                                    var correctDate = ExifStringDate?.ToString()?.Substring(0, 10).Replace(":", "/") + ExifStringDate?.ToString()?.Substring(10, 9);

                                    if (DateTime.TryParse(correctDate, out DateTime date))
                                    {
                                        Console.WriteLine(date);
                                        fileAndDateList.Add(new FileAndDate { DateForNewPath = date, File = file });
                                    }
                                    else
                                    {
                                        fileAndDateList.Add(new FileAndDate { DateForNewPath = file.CreationTime, File = file });
                                        // Console.WriteLine("Invalid date " + ExifStringDate.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
