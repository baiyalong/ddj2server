using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ddj2server
{
    class Program
    {
        static void Main(string[] args)
        {

            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://0.0.0.0:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    //allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                    string[] param = message.Split(',');
                    switch (param[0])
                    {
                        case "IMPredictFrMainCal":
                            socket.Send("IMPredictFrMainCal,reply,begin");
                            dll.IMPredictFrMainCal(int.Parse(param[1]), int.Parse(param[2]), int.Parse(param[3]), double.Parse(param[4]), double.Parse(param[5]), int.Parse(param[6]), "", (s) => socket.Send(s));
                            socket.Send("IMPredictFrMainCal,reply,end");
                            break;
                        default:
                            socket.Send("err params");
                            break;
                    }
                };
            });


            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }
                input = Console.ReadLine();
            }
        }
    }
}
