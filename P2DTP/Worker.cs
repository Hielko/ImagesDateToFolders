using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace P2DTP
{
    public class Worker
    {
        public Worker(List<FileInfo> files, out List<Result> result)
        {
            result = new List<Result>();
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
                                        result.Add(new Result { ExifDate = date, file = file });
                                    }
                                    else
                                    {
                                        result.Add(new Result { ExifDate = file.CreationTime, file = file });
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
