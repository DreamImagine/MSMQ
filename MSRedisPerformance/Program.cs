using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Diagnostics;

namespace MSRedisPerformance
{
    class Program
    {
        static void Main(string[] args)
        {


            // 循环发送10000 次

            var count = 50000;




            //var startTime = DateTime.Now;

            //SeneMessage(count);

            //var endTime = DateTime.Now;

            //var execTime = (endTime - startTime).TotalSeconds;

            //Console.WriteLine("发送消息队列" + count + "条,花费" + execTime);





            var startTime = DateTime.Now;

            SendRedis(count);

            var endTime = DateTime.Now;

            var execTime = (endTime - startTime).TotalSeconds;

            Console.WriteLine("发送Redis 消息" + count + "条,花费" + execTime);


            //开始计时
            Stopwatch watch = Stopwatch.StartNew();

            SendRedis(count);

            //计时结束
            watch.Stop();

            Console.WriteLine("发送Redis 消息" + count + "条,花费" + watch.ElapsedMilliseconds);




            Console.Read();

            Console.Read();
            Console.Read();
        }

        /// <summary>
        /// 发送消息队列
        /// </summary>
        /// <param name="count">发送的次数</param>
        public static void SeneMessage(int count)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    //连接到队列

                    MessageQueue myQueue = new MessageQueue(@"FormatName:Direct=TCP:192.168.31.57\private$\myQ");
                    Message myMessage = new Message();

                    myMessage.Body = i.ToString();
                    myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                    myQueue.Send(myMessage);                    

                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        /// <summary>
        /// 发送Redis 消息
        /// /// </summary>
        /// <param name="count">发送的次数</param>
        public static void SendRedis(int count)
        {

            var connectionString = "192.168.31.57:6379,abortConnect=false,connectTimeout=5000";

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);

            IDatabase db = redis.GetDatabase();

            for (int i = 0; i < count; i++)
            {

                //db.SetAddAsync(i.ToString(), "1111"); 

                //同步发送 
                db.StringSet(i.ToString(), "1111");

                // 异步发送
                //db.StringSetAsync(i.ToString(), "1111");
            }


        }

    }
}
