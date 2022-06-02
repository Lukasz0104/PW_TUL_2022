using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataLayer
{
    internal class Logger
    {
        /// <summary>
        /// Logging interval in seconds.
        /// </summary>
        public static int LoggingInterval = 10;

        private bool loggerRunning = false;

        public void Log(List<Ball> balls)
        {
            loggerRunning = true;
            Task.Run(() =>
            {
                while (loggerRunning)
                {
                    using (StreamWriter stream = new StreamWriter("logs.json", true))
                    using (StringWriter sw = new StringWriter())
                    {
                        for (int i = 0; i < balls.Count; i++)
                        {
                            BallDetails details = new BallDetails(i, balls[i]);
                            sw.WriteLine(JsonSerializer.Serialize(details));
                        }

                        stream.Write(sw.ToString());
                    }

                    Task.Delay(LoggingInterval * 1000).Wait();
                }
            });
        }

        public void Stop()
        {
            loggerRunning = false;
        }
    }
}
