using ImagesDateToFolders;

namespace Tests
{
    public class Tests
    {
        Options? options;

        readonly string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory));
        readonly string samplesPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + Path.DirectorySeparatorChar + "samples";

        [SetUp]
        public void Setup()
        {
            options = new Options { DestinationPath = Path.Combine(solution_dir, "Result") };
        }


        [TearDown]
        public void TearDown()
        {
            Directory.Delete(options?.DestinationPath, true);
        }

        [Test]
        public void Test1()
        {
            var files = new Files().GetFiles(samplesPath);

            var result = new List<FileAndDate>();

            new Collector(files, out result);
            new CopyFiles(result, options);

            result.ForEach(file =>
            {
                var checkPath = Path.Combine(options.DestinationPath, file.DateForNewPath?.ToString("yyyy"), file.DateForNewPath?.ToString("MM"));
                Assert.IsTrue(Directory.Exists(checkPath));
                Assert.IsTrue(File.Exists(Path.Combine(checkPath, file.File.Name)));
            });

        }
    }
}