using System;
using Akka.Actor;
using AkkaCluster.Common;

namespace AkkaCluster.Instance1
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var clusterSystem = ActorSystem.Create("mycluster"))
			{

				var echoActor = clusterSystem.ActorOf(Props.Create(() => new EchoActor()), "echo");
				Console.WriteLine(echoActor);

				echoActor.Tell("Hello World");

				var echoSelection = clusterSystem.ActorSelection("/user/echo");
				echoSelection.Tell("Hello from the this side");

				Console.ReadKey();
			}
		}
	}
}
