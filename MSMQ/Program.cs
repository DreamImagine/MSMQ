using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MSMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            #region         http://www.cnblogs.com/beniao/archive/2008/06/26/1229934.html

            var msmqPaht = @".\private$\myQueue";

            //连接到本地的队列
            var myQueue = MSMQHelper.CreateQueue(msmqPaht);

            Message myMessage = new Message();
            myMessage.Body = "423123";

            myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            myQueue.Send(myMessage);

            //myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });


            Message myMessage2 = new Message();
            myMessage2.Body = "523123";

            myMessage2.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            myQueue.Send(myMessage2);

            // 清空队列的消息
            //myQueue.Purge();


            //删除现有的消息队列
            //MessageQueue.Delete(@".\private$\myQueue");



            //Message myMessage2 = myQueue.Receive();

            //string context = (string)myMessage2.Body;

            //Console.WriteLine("消息内容为：" + context);



            //GetAllMessage();

            PublicMSMQ();

            Console.ReadKey();
            #endregion
        }

        /// <summary>
        /// 连接队列并获得队列的全部消息
        /// </summary>
        public static void GetAllMessage()
        {
            var msmqPaht = @".\private$\myQueue";

            using (var myQueue = new MessageQueue(msmqPaht))
            {
                var messages = myQueue.GetAllMessages();
                XmlMessageFormatter formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                for (int i = 0; i < messages.Length; i++)
                {
                    messages[i].Formatter = formatter;

                    if (i != 1)
                    {
                        myQueue.Receive();
                        Console.WriteLine("删除了" + i + ":" + messages[i].Body.ToString());
                    }

                    Console.WriteLine(i + ":" + messages[i].Body.ToString());
                }

            }
        }



        public static void PublicMSMQ()
        {

            var msmqPaht = @".\myQueue";

            //连接到本地的队列
            var myQueue = MSMQHelper.CreateQueue(msmqPaht);

            Message myMessage = new Message();
            myMessage.Body = "423123";

            myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            myQueue.Send(myMessage);

        }
    }
}
