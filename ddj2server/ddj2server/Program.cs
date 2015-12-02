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
                            dll.IMPredictFrMainCal(
                                int.Parse(param[1]),
                                int.Parse(param[2]),
                                int.Parse(param[3]),
                                double.Parse(param[4]),
                                double.Parse(param[5]),
                                int.Parse(param[6]),
                                "", (s) => socket.Send(s));
                            socket.Send("IMPredictFrMainCal,reply,end");
                            break;
                        case "IMDbEcStatorCalMain":
                            socket.Send("IMDbEcStatorCalMain,reply,begin");
                            dll.IMDbEcStatorCalMain("",
                                double.Parse(param[1]),
                                double.Parse(param[2]),
                                int.Parse(param[3]),
                                int.Parse(param[4]),
                                double.Parse(param[5]),
                                double.Parse(param[6]),
                                double.Parse(param[7]),
                                double.Parse(param[8]),
                                int.Parse(param[9]),
                                int.Parse(param[10]),
                                double.Parse(param[11]),
                                int.Parse(param[12]),
                                double.Parse(param[13]),
                                double.Parse(param[14]),
                                double.Parse(param[15]),
                                double.Parse(param[16]),
                                double.Parse(param[17]),
                                double.Parse(param[18]),
                                double.Parse(param[19]),
                                double.Parse(param[20]),
                                int.Parse(param[21]),
                                int.Parse(param[22]),
                                double.Parse(param[23]),
                                double.Parse(param[24]),
                                int.Parse(param[25]),
                                double.Parse(param[26]),
                                double.Parse(param[27]),
                                double.Parse(param[28]),
                                double.Parse(param[29]),
                                double.Parse(param[30]),
                                double.Parse(param[31]),
                                double.Parse(param[32]),
                                double.Parse(param[33]),
                                double.Parse(param[34]),
                                double.Parse(param[35]),
                                double.Parse(param[36]),
                                double.Parse(param[37]),
                                double.Parse(param[38]),
                                double.Parse(param[39]),
                                double.Parse(param[40]),
                                int.Parse(param[41]),
                                int.Parse(param[42]),
                                int.Parse(param[43]),
                                int.Parse(param[44]),
                                int.Parse(param[45]),
                                double.Parse(param[46]),
                                (s) => socket.Send(s));
                            socket.Send("IMDbEcStatorCalMain,reply,end");
                            break;
                        case "IMMeEcStatorCalMain":
                            socket.Send("IMMeEcStatorCalMain,reply,begin");
                            dll.IMMeEcStatorCalMain("",
                                int.Parse(param[1]),
                                double.Parse(param[2]),
                                double.Parse(param[3]),
                                double.Parse(param[4]),
                                int.Parse(param[5]),
                                int.Parse(param[6]),
                                double.Parse(param[7]),
                                double.Parse(param[8]),
                                int.Parse(param[9]),
                                double.Parse(param[10]),
                                double.Parse(param[11]),
                                double.Parse(param[12]),
                                double.Parse(param[13]),
                                double.Parse(param[14]),
                                double.Parse(param[15]),
                                int.Parse(param[16]),
                                int.Parse(param[17]),
                                int.Parse(param[18]),
                                int.Parse(param[19]),
                                double.Parse(param[20]),
                                double.Parse(param[21]),
                                double.Parse(param[22]),
                                double.Parse(param[23]),
                                int.Parse(param[24]),
                                int.Parse(param[25]),
                                double.Parse(param[26]),
                                double.Parse(param[27]),
                                int.Parse(param[28]),
                                (s) => socket.Send(s));
                            socket.Send("IMMeEcStatorCalMain,reply,end");
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
