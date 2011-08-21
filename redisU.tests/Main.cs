using System;
using System.Text;
using redisU.utils;
using redisU.framework;
using redisU.events;

namespace redisU.tests
{
	class MainClass
	{
		private static RedisConnection redis = null;
		private static RedisSubscription consumer = null;
		
		public static void Main (string[] args)
		{
			redis = new RedisConnection("localhost", 6379);
			
			redis.ClearAllDB();
			redis.Set<string, int>("sync-counter", 0);
		 	Counter syncCounter = new Counter();
			syncCounter.Count();
			
			/*redis._Set<string, string>("testkey1", "testval1");
			
			redis._Set<string, string>("testkey2", "testval2");
			
			Log("Get testkey1", redis.Get<string, string>("testkey1"));
			
			Log("Set testkey1", redis.Set<string, string>("testkey1", "testval1"));
			
			Log("Set testkey2", redis.Set<string, string>("testkey2", "testval2"));
			
			Log("Set testkey4", redis.Set<string, int>("testkey4",  5));
			
			Log("Random key", redis.GetRandomKey());
			
			Log("Exist testkey1", redis.IfExists<string>("testkey1"));
			
			Log("Exist testkey3", redis.IfExists<string>("testkey3"));
			
			Log("Type of testkey2", redis.GetKeyType<string>("testkey2"));
			
			Log("Pattern *1", redis.GetKeysByPattern("*1"));
			
			Log("Increment testkey4", redis.Increment<string>("testkey4"));
			
			Log("Increment testkey4 by 4", redis.IncrementBy<string>("testkey4", 4));
			
			Log("Decrement testkey4", redis.Decrement<string>("testkey4"));
			
			Log("Decrement testkey4 by 3", redis.DecrementBy<string>("testkey4", 3));
			
			Log("Get testkey4", redis.Get<string, string>("testkey4"));
			
			Log("Get testkey4", redis.Get<int, string>("testkey4"));
			
			Log("Get testkey1", redis.Get<string, string>("testkey1"));
			
			Log("TTL testkey4", redis.GetTTL<string>("testkey4"));
			
			Log("Multi Get", redis.MultiGet<string, string>(new string[] {"testkey1", "testkey2", "testkey4"}));
			
			Log("Persist testkey4", redis.Persist<string>("testkey4"));
			
			Log("Rename testkey4 to testkey5", redis.RenameKey<string, string>("testkey4", "testkey5"));
			
			Log("Get testkey5", redis.Get<string, string>("testkey5"));
			
			Log("Rename testkey5 NX", redis.RenameKeyIfNotExist<string, string>("testkey5", "testkey6"));
			
			Log("Get testkey6", redis.Get<string, string>("testkey6"));
			
			Log("Set testkey1 expiry", redis.SetKeyExpiry<string>("testkey1", 3));
			
			Log("Set testkey7 with expiry", redis.SetKeyWithExpiry<string, string>("testkey7", 5, "testval7"));
			
			Log("Length testkey2", redis.Strlen<string>("testkey2"));
			
			Log("SS add member+score", redis.SSAddMemberWithScore("ss1", 6, "ss1mem1"));
			
			Log("Get SS1 mem1", redis.SSGetScore("ss1", "ss1mem1"));
			
			Log("Get SS1 Card", redis.SSGetCardinality("ss1"));
			
			//Log("Server Info", redis.GetServerInfo());
			
			//Log("Last save timestamp", redis.GetLastSaveTimestamp());
			
			//Log("Hash code", redis.GetHashCode());
			
			Log("DB Size", redis.GetDBSize());
			
			//redis.ClearAllDB();
			
			consumer = new RedisSubscription();
			
			consumer.OnMessageReceived += HandleConsumerOnMessageReceived;
			consumer.OnSubscribe += HandleConsumerOnSubscribe;
			consumer.OnUnsubscribe += HandleConsumerOnUnsubscribe;
			consumer.Subscribe(new string[] {"gameplay"});*/
			
			//consumer.Unsubscribe(new string[] {"gameplay"});
			//connection.DestroySubscription();
			/*Console.WriteLine(connection.Get<string, string>("vagabond"));
			connection.Set<string, string>("vagabond", "redisU");
			Console.WriteLine(connection.Get<string, string>("vagabond"));
			Console.WriteLine(connection.DeleteKeys<string>("vagabond", "notexixts"));
			Console.WriteLine(connection.GetKeyType<string>("game"));*/
		}
		
		private static void Log(string command, string output)
		{
			Console.WriteLine(command + "\t-\t" + output);
		}
		
		private static void Log(string command, int output)
		{
			Console.WriteLine(command + "\t-\t" + output);
		}
		
		private static void Log(string command, string[] output)
		{
			StringBuilder outputBuilder = new StringBuilder();
			foreach(string data in output)
			{
				if(outputBuilder.ToString().Length > 0)
					outputBuilder.Append("|");
				
				outputBuilder.Append(data);
			}
			Console.WriteLine(command + "\t-\t" + outputBuilder.ToString());
		}
		
		private static void Log(string command, Constants.StatusCode status)
		{
			Console.WriteLine(command + "\t-\t" + status.ToString());
		}
		
		private static void Log(string command, Constants.KeyType type)
		{
			Console.WriteLine(command + "\t-\t" + type.ToString());
		}
		
		private static void Log(string command, bool val)
		{
			Console.WriteLine(command + "\t-\t" + val.ToString());
		}

		static void HandleConsumerOnUnsubscribe (object sender, SubscriptionEventArgs args)
		{
			Console.WriteLine("Unsubscribed from channel :" + args.GetChannel());
			//redis.Publish("gameplay", "WTF");
		}

		static void HandleConsumerOnSubscribe (object sender, SubscriptionEventArgs args)
		{
			Console.WriteLine("Subscribed to channel : " + args.GetChannel());
			redis.Publish("gameplay", "I m Awesome!!");
			redis.Publish("gameplay", "I m Super Awesome!!");
			//consumer.Unsubscribe(new string[] {"gameplay"});
		}

		static void HandleConsumerOnMessageReceived (object sender, MessageEventArgs args)
		{
			Console.WriteLine("Message Received from channel <  " + args.GetChannel() + " > with data <  " + args.GetMessage() + " >");
			consumer.Unsubscribe();
		}
	}
}