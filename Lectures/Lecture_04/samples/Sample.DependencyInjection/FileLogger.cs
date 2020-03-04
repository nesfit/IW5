using System.IO;

namespace Sample.DependencyInjection
{
    public class FileLogger : ILogger
    {
        private readonly string filePath;

        public FileLogger(string filePath)
        {
            this.filePath = filePath;
        }

        public void Log(string message)
        {
            File.AppendAllLines(filePath, new[] { message });
        }
    }
}
