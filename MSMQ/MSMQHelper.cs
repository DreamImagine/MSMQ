using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;


namespace MSMQ
{
    public class MSMQHelper
    {

        /// <summary>
        /// 通过 Create 方法创建使用指定路径的新消息队列
        /// </summary>
        /// <param name="queuePaht"></param>
        public static MessageQueue CreateQueue(string queuePaht)
        {

            if (!MessageQueue.Exists(queuePaht))
            {
                return MessageQueue.Create(queuePaht);
            }
            else
            {
                return new MessageQueue(queuePaht);
            }
        }



	    public void Init()
	    {
		    SendYuanChen();
	    }


		public void SendYuanChen()
		{
			try
			{
				string s = "sssssssss";
				var msg = new Message();
				msg.Body = s;
				msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
				var mq = new MessageQueue(@"FormatName:Direct=TCP:112.74.14.58\private$\queuedemo");
				mq.Send(msg);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}


		 
    }
}
