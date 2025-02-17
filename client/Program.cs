using System;
using System.Net.Sockets;
using System.Text;

class TcpClientApp
{
    private const string ServerIp = "127.0.0.1";
    private const int Port = 3000;

    static void Main(string[] args)
    {
        try
        {
            TcpClient client = new TcpClient(ServerIp, Port);
            NetworkStream stream = client.GetStream();

            string[] commands = { "GET_TEMP", "GET_STATUS", "INVALID_COMMAND" };

            foreach (string command in commands)
            {
                byte[] data = Encoding.ASCII.GetBytes(command);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: " + command);

                // Read the response
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                // Decode the response as UTF-8
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Ensure console supports Unicode
                response = response.Replace("?", "°");
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Received: " + response);
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}