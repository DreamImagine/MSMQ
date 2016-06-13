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

            //for (int i = 0; i < 10; i++)
            //{
            //    SendMessage("11_"+i);
            //    SendMessage2("22_"+i);
            //    SendMessage3("33_"+i);
            //}
            SendMessageBook();
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


        /// <summary>
        /// 连接消息队列并发送消息到队列
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static bool SendMessageBook(string sendValue = "113")
        {
            bool flag = false;
            try
            {
                //连接到本地的队列
                MessageQueue myQueue = new MessageQueue(".\\private$\\myBook");
                Message myMessage = new Message();

                Book book = new Book();
                book.BookId = 1001;
                book.BookName = "ASP.NET";
                book.BookAuthor = "ZhangSan";
                book.BookPrice = 88.88;



                myMessage.Body = book;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(Book) });

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


    public class Book
    {
        private int _BookId;
        public int BookId
        {
            get { return _BookId; }
            set { _BookId = value; }
        }

        private string _BookName;
        public string BookName
        {
            get { return _BookName; }
            set { _BookName = value; }
        }

        private string _BookAuthor;
        public string BookAuthor
        {
            get { return _BookAuthor; }
            set { _BookAuthor = value; }
        }

        private double _BookPrice;
        public double BookPrice
        {
            get { return _BookPrice; }
            set { _BookPrice = value; }
        }
    }
}
