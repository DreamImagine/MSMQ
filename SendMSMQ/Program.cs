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
                SendMessage();
            }

        }



        /// <summary>
        /// 连接消息队列并发送消息到队列
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static bool SendMessage()
        {
            bool flag = false;
            try
            {
                //连接到本地的队列
                MessageQueue myQueue = new MessageQueue(".\\private$\\myQueue");
                Message myMessage = new Message();

                myMessage.Body = "11";
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
