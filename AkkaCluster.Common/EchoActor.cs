using System;
using Akka.Actor;

namespace AkkaCluster.Common
{
	public class EchoActor : UntypedActor
	{
		protected override void OnReceive(object message)
		{
			Console.WriteLine("Received: {0}", message);

			Sender.Tell("Echo " + message);
		}
	}
}
