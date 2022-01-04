using System;
using System.IO;
using Calculator;
using Grpc.Core;

namespace server
{
    internal class Program
    {
        private const int Port = 50052;

        private static void Main(string[] args)
        {
            Server server = null;

            try
            {
                server = new Server
                {
                    Services = {CalculatorService.BindService(new CalculatorServiceImpl())},
                    Ports = {new ServerPort("localhost", Port, ServerCredentials.Insecure)}
                };

                server.Start();
                Console.WriteLine("The server is listening on the port : " + Port);
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("The server failed to start : " + e.Message);
                throw;
            }
            finally
            {
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}