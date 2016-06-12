using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SendMSMQ
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 10; i++)
            {
                SendMessage("11_"+i);
                SendMessage2("22_"+i);
                SendMessage3("33_"+i);
            }

        }



        /// <summary>
        /// 连接消息队列并发送消息到队列
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static bool SendMessage(string sendValue = "11")
        {
            bool flag = false;
            try
            {
                //连接到本地的队列
                MessageQueue myQueue = new MessageQueue(".\\private$\\myQueue");
                Message myMessage = new Message();

                myMessage.Body = sendValue;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                myQueue.Send(myMessage);

                flag = true;

            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            return flag;
        }

        /// <summary>
        /// 连接消息队列并发送消息到队列
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static bool SendMessage2(string sendValue = "112")
        {
            bool flag = false;
            try
            {
                //连接到本地的队列
                MessageQueue myQueue = new MessageQueue(".\\private$\\myQueue2");
                Message myMessage = new Message();

                myMessage.Body = sendValue;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                myQueue.Send(myMessage);

                flag = true;

            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            return flag;
        }


        /// <summary>
        /// 连接消息队列并发送消息到队列
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static bool SendMessage3(string sendValue = "113")
        {
            bool flag = false;
            try
            {
                //连接到本地的队列
                MessageQueue myQueue = new MessageQueue(".\\private$\\myQueue3");
                Message myMessage = new Message();

                myMessage.Body = sendValue;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                myQueue.Send(myMessage);

                flag = true;

            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            return flag;
        }
    }
}
