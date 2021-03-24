using System;
using System.Diagnostics;
using System.IO;

namespace MP4ToImage
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string input = $"{directory}/Input/Sample.mp4";
            string output = $"{directory}/Output/output.jpg";
            string ffmpegFilePath = $"{directory}/ffmpeg.exe";
            string arguments = @" -y -ss 7  -i ""{0}""  -vframes 1  -vf ""scale =-2:-2""  -filter:v ""crop = 400:400:400:400""  ""{1}"" ";

            arguments = string.Format(arguments, input, output);

            new Program().Execute(arguments, ffmpegFilePath);
        }

        public void Execute(string arguments, string ffmpegFilePath)
        {
            ProcessStartInfo startInfo = GenerateStartInfo(ffmpegFilePath, arguments);
            Execute(startInfo);
        }

        private ProcessStartInfo GenerateStartInfo(string ffmpegPath, string arguments) => new ProcessStartInfo
        {
            Arguments = arguments,
            FileName = ffmpegPath,
            CreateNoWindow = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            WindowStyle = ProcessWindowStyle.Hidden
        };

        private void Execute(ProcessStartInfo startInfo)
        {
            using (Process ffmpegProcess = Process.Start(startInfo))
            {
                try
                {
                    ffmpegProcess.WaitForExit();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
