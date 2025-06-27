using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;

class Player
{
    public string Name { get; set; }
    public int Score { get; set; } = 0;
    public bool HasStopped { get; set; } = false;
    public bool IsPlaying { get; set; } = true;
}

class Program
{
    static UdpClient server = new UdpClient(11000);
    static Dictionary<IPEndPoint, Player> players = new();
    static Random rng = new();

    static void Main()
    {
        Console.WriteLine("Servidor Blackjack iniciado na porta 11000...");
        new Thread(ListenLoop).Start();
        Console.ReadLine(); 
    }

    static void ListenLoop()
    {
        while (true)
        {
            IPEndPoint client = new IPEndPoint(IPAddress.Any, 0);
            string msg = Encoding.UTF8.GetString(server.Receive(ref client));
            Console.WriteLine($"Recebido de {client}: {msg}");
            ProcessMessage(msg, client);
        }
    }

    static void ProcessMessage(string message, IPEndPoint client)
    {
        if (message.StartsWith("ENTRAR:"))
        {
            string name = message.Split(':')[1];
            if (!players.ContainsKey(client))
            {
                players[client] = new Player { Name = name };
                Send(client, $"MENSAGEM:Bem-vindo, {name}!");
                DealCard(client);
            }
        }
        else if (message == "PEDIR_CARTA")
        {
            DealCard(client);
        }
        else if (message == "PARAR")
        {
            players[client].HasStopped = true;
            players[client].IsPlaying = false;
            Send(client, $"MENSAGEM:Você parou com {players[client].Score} pontos.");
            CheckEndOfGame();
        }
    }

    static void DealCard(IPEndPoint client)
    {
        int card = rng.Next(1, 12);
        players[client].Score += card;
        Send(client, $"CARTA:{card}");

        if (players[client].Score > 21)
        {
            players[client].IsPlaying = false;
            Send(client, "RESULTADO:perdeu");
            CheckEndOfGame();
        }
    }

    static void CheckEndOfGame()
    {
        if (!players.Values.Any(p => p.IsPlaying))
        {
            int maxScore = players.Values.Where(p => p.Score <= 21).DefaultIfEmpty().Max(p => p.Score);
            foreach (var kvp in players)
            {
                string result = kvp.Value.Score == maxScore && maxScore <= 21 ? "ganhou" : "perdeu";
                Send(kvp.Key, $"RESULTADO:{result}");
            }
            Console.WriteLine("Rodada encerrada.");
        }
    }

    static void Send(IPEndPoint client, string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        server.Send(data, data.Length, client);
    }
}
