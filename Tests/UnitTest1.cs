using P2DTP;

namespace Tests
{
    public class Tests
    {
        Options options;

        [SetUp]
        public void Setup()
        {
            options = new Options { };
        }

        [Test]
        public void Test1()
        {

            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory));
            var samplesPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + Path.DirectorySeparatorChar + "samples";

            string[] paths = new string[1] { samplesPath };
            options.DestinationPath = Path.Combine(solution_dir, "Result");

            var files = new Files().GetFiles(paths);

            var result = new List<FileAndDate>();

            new Collector(files, out result);
            new CopyFiles(result, options);

            var checkPath = Path.Combine(options.DestinationPath, "2007", "07");
            var DirExists = Directory.Exists(checkPath);

            Assert.IsTrue(DirExists);

            var copiedFiles = Directory.EnumerateFiles(checkPath);
            Assert.That(copiedFiles?.Count(), Is.EqualTo(1));

        }
    }
}