using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static UdpClient client = new UdpClient();
    static IPEndPoint server = new IPEndPoint(IPAddress.Loopback, 11000); // Altere para IP do servidor se necessário
    static bool gameRunning = true;

    static void Main()
    {
        Console.Write("Digite seu nome: ");
        string name = Console.ReadLine();
        Send($"ENTRAR:{name}");

        new Thread(ListenLoop).Start();

        while (gameRunning)
        {
            Console.WriteLine("1 - PEDIR_CARTA | 2 - PARAR");
            string input = Console.ReadLine();
            if (input == "1") Send("PEDIR_CARTA");
            else if (input == "2") Send("PARAR");
        }

        Console.WriteLine("Jogo encerrado. Pressione ENTER para sair.");
        Console.ReadLine();
    }

    static void ListenLoop()
    {
        while (true)
        {
            IPEndPoint remote = null;
            byte[] data = client.Receive(ref remote);
            string message = Encoding.UTF8.GetString(data);
            Console.WriteLine($"Servidor: {message}");

            if (message.StartsWith("RESULTADO"))
            {
                gameRunning = false;
                break;
            }
        }
    }

    static void Send(string msg)
    {
        byte[] data = Encoding.UTF8.GetBytes(msg);
        client.Send(data, data.Length, server);
    }
}
