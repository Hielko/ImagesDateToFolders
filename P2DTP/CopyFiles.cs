namespace P2DTP
{
    public class CopyFiles
    {
        public CopyFiles(List<Result> result, Options? options)
        {

            foreach (Result item in result)
            {

                var x = item.ExifDate.ToString(options.Format);
                var sp = x.Split(' ');
                var p = String.Join("\\", sp);

                //    var newPath = Path.Combine(options?.Desination, item.ExifDate.Year.ToString("D4"), item.ExifDate.Month.ToString("D2"), item.ExifDate.Day.ToString("D2"));

                var newPath = Path.Combine(options?.Desination, p);
                Directory.CreateDirectory(newPath);

                File.Copy(item.file.FullName, newPath + Path.DirectorySeparatorChar + item.file.Name);

            }

        }
    }
}
