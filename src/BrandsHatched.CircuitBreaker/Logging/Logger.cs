using System.IO;

namespace BrandsHatched.CircuitBreaker.Logging
{
	public class Logger : ILog
	{
		public void Log(string message)
		{
			File.AppendAllText(@"C:\temp\circuitbreaker.txt", message + @"\r\n");
		}
	}
}