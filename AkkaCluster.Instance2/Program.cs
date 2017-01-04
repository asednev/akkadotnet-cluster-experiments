using System;
using Akka.Actor;

namespace AkkaCluster.Instance2
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var clusterSystem = ActorSystem.Create("mycluster"))
			{
				Console.ReadKey();

				Console.WriteLine("Sending echo to the other side");

				var echoSelection = clusterSystem.ActorSelection("/user/echo");
				echoSelection.Tell("Hello from the other side");

				//http://stackoverflow.com/questions/35634127/akka-net-access-remote-actors-in-cluster
				/*
				// register actor type as a sharded entity
				var region = ClusterSharding.Get(system).Start(
					typeName: "my-actor",
					entityProps: Props.Create<MyActor>(),
					settings: ClusterShardingSettings.Create(system),
					messageExtractor: new MessageExtractor());
	
				// send message to entity through shard region
				region.Tell(new Envelope(shardId: 1, entityId: 1, message: "hello"))
				*/
				Console.ReadKey();
			}
		}
	}
}
