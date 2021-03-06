﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiveMSMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //启用单线程，读取消息队列消息
            new Thread(MSMQListen).Start();
        }

        private static void MSMQListen()
        {
            while (true)
            {

                Console.WriteLine("我在循环了");
                ReceiveMessage();
                ReceiveMessage2();
                ReceiveMessage3();
                //取出队列里的消息
                //MessageBox.Show(MsgQueue.ReceiveMessage());
            }
            // ReSharper disable once FunctionNeverReturns
        }

        /// <summary>
        /// 接受消息队列并从队列中接受消息
        /// </summary>
        /// <returns></returns>
        public static string ReceiveMessage()
        {
            Console.WriteLine("我在执行myQueue");

            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(".\\private$\\myQueue");
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            try
            {
                var myMessage = myQueue.Receive();

                //myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(Book) });

                string book = (string)myMessage.Body;

                Console.WriteLine(book);

                return book;

                //return string.Format("编号：{0},书名：{1},作者：{2},定价：{3}", book.BookId,
                //    book.BookName,
                //    book.BookAuthor,
                //    book.BookPrice);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return "";
        }


        /// <summary>
        /// 接受消息队列并从队列中接受消息
        /// </summary>
        /// <returns></returns>
        public static string ReceiveMessage2()
        {
            Console.WriteLine("我在执行myQueue2");

            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(".\\private$\\myQueue2");
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            try
            {
                var myMessage = myQueue.Receive();

                //myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(Book) });

                string book = (string)myMessage.Body;

                Console.WriteLine(book);

                return book;

                //return string.Format("编号：{0},书名：{1},作者：{2},定价：{3}", book.BookId,
                //    book.BookName,
                //    book.BookAuthor,
                //    book.BookPrice);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return "";
        }



        /// <summary>
        /// 接受消息队列并从队列中接受消息
        /// </summary>
        /// <returns></returns>
        public static string ReceiveMessage3()
        {
            Console.WriteLine("我在执行myQueue3");

            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(".\\private$\\myQueue3");
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            try
            {
                var myMessage = myQueue.Receive();

                //myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(Book) });

                string book = (string)myMessage.Body;

                Console.WriteLine(book);

                return book;

                //return string.Format("编号：{0},书名：{1},作者：{2},定价：{3}", book.BookId,
                //    book.BookName,
                //    book.BookAuthor,
                //    book.BookPrice);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return "";
        }

    }
}
