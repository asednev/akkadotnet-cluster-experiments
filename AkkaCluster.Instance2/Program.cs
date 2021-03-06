﻿using System;
using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Sharding;
using AkkaCluster.Common;

namespace AkkaCluster.Instance2
{
	class Program
	{
	    // ReSharper disable once UnusedParameter.Local
		static void Main(string[] args)
		{
			using (var clusterSystem = ActorSystem.Create("mycluster"))
			{
                var cluster = Cluster.Get(clusterSystem);

                Console.Title = $"Other Side - {cluster.SelfAddress}";

                cluster.RegisterOnMemberUp(() => Console.WriteLine("Cluster Member is up"));

                var sharding = ClusterSharding.Get(clusterSystem);
                var shardRegion = sharding.Start(
                    typeName: "echo",
                    entityProps: Props.Create<EchoActor>(),
                    settings: ClusterShardingSettings.Create(clusterSystem),
                    messageExtractor: new MessageExtractor(2));

                shardRegion.Tell(new ShardEnvelope("foo", "Hello from the other side"));

			    while (Console.ReadKey().Key != ConsoleKey.Q)
			    {
                    shardRegion.Tell(new ShardEnvelope("foo", $"Other side time is {DateTime.Now}"));
                }
               
			}
		}
	}
}
