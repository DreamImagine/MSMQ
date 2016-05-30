using System;
using Microsoft.SqlServer.Server;
using NetMQ;
using NetMQ.Sockets;

namespace MSMQ
{
	public class NetMQHelper
	{
		public void Init()
		{
			Test1();

		}

		public void Test1()
		{
			using (var server = new ResponseSocket("@tcp://localhost:5556"))
			{
				using (var client = new RequestSocket(">tcp://localhost:5556"))
				{
					client.SendFrame("Hello");

					string m1 = server.ReceiveFrameString();

					Console.WriteLine("From Client:{0}", m1);

				}
			}
		}
	}
}