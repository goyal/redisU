using System;
using System.Threading;
using redisU.framework;

namespace redisU.tests
{
	public class Counter
	{
		public Counter()
		{
			
		}
		
		public void Count()
		{
				Thread counter1 = new Thread(new ThreadStart(this.CountThread1));
				Thread counter2 = new Thread(new ThreadStart(this.CountThread2));
				counter1.Start();
				counter2.Start();
		}
		
		public void CountThread1()
		{
			RedisConnection redisConnection = new RedisConnection();
			while(true)
			{
				redisConnection.AddWatch("sync-counter");
				redisConnection.BeginTransaction();
				redisConnection.Increment<string>("sync-counter");
				if(redisConnection.CommitTransaction() != null)
					Console.WriteLine("Thread 1:" + redisConnection.Get<int, string>("sync-counter"));
				else
					Console.WriteLine("Thread 1 Transaction failed, retrying.");
			}
		}
		
		public void CountThread2()
		{
			RedisConnection redisConnection = new RedisConnection();
			while(true)
			{
				redisConnection.AddWatch("sync-counter");
				redisConnection.BeginTransaction();
				redisConnection.Increment<string>("sync-counter");
				if(redisConnection.CommitTransaction() != null)
					Console.WriteLine("Thread 2:" + redisConnection.Get<int, string>("sync-counter"));
				else
					Console.WriteLine("Thread 2 Transaction failed. Retrying");
			}
		}
	}
}

