using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Threading;

namespace MSMQ
{
	class Program
	{
		static void Main(string[] args)
		{
			//#region         http://www.cnblogs.com/beniao/archive/2008/06/26/1229934.html

			//var msmqPaht = @".\private$\myQueue";

			////连接到本地的队列
			//var myQueue = MSMQHelper.CreateQueue(msmqPaht);

			//Message myMessage = new Message();
			//myMessage.Body = "423123";

			//myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

			//myQueue.Send(myMessage);

			////myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });


			//Message myMessage2 = new Message();
			//myMessage2.Body = "523123";

			//myMessage2.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

			//myQueue.Send(myMessage2);

			// 清空队列的消息
			//myQueue.Purge();


			//删除现有的消息队列
			//MessageQueue.Delete(@".\private$\myQueue");



			//Message myMessage2 = myQueue.Receive();

			//string context = (string)myMessage2.Body;

			//Console.WriteLine("消息内容为：" + context);



			//GetAllMessage();

			//PublicMSMQ();

			//StartThreads();

			//SeneRemoteMessage();


			NetMQHelper netMqHelper=new NetMQHelper();
			netMqHelper.Init();


			MSMQHelper  msmqHelper=new MSMQHelper();
			msmqHelper.Init();

			#region 升级

			//SendMessage(new Book() {
			//    BookAuthor="ffefe",
			//    BookId=23213,
			//    BookName="name",
			//    BookPrice=12.1

			//});


			//Console.WriteLine(ReceiveMessage());


			// 事务


			//TransactionSendQueue();

			//TransactionReceiveQueue();

			//SendMessageAnysc();
			//ReceiveMessageAsync();
			//#endregion

			//AsyncHelper asyncHlper = new AsyncHelper();

			//asyncHlper.Init();


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


		/// <summary>
		/// 连接消息队列并发送消息到队列
		/// </summary>
		/// <param name="book"></param>
		/// <returns></returns>
		public static bool SendMessage(Book book)
		{
			bool flag = false;
			try
			{
				//连接到本地的队列
				MessageQueue myQueue = new MessageQueue(".\\private$\\myQueue");
				Message myMessage = new Message();

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


		/// <summary>
		/// 接受消息队列并从队列中接受消息
		/// </summary>
		/// <returns></returns>
		public static string ReceiveMessage()
		{
			//连接到本地队列
			MessageQueue myQueue = new MessageQueue(".\\private$\\myQueue");
			myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Book) });

			try
			{
				var myMessage = myQueue.Receive();

				//myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(Book) });

				Book book = (Book)myMessage.Body;

				return string.Format("编号：{0},书名：{1},作者：{2},定价：{3}", book.BookId,
					book.BookName,
					book.BookAuthor,
					book.BookPrice);

			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}

			return "";
		}


		//#region  http://www.cnblogs.com/beniao/archive/2008/06/28/1230311.html
		/// <summary>
		/// 带有事务的发送消息队列
		/// </summary>
		public static void TransactionSendQueue()
		{
			//创建事务性的专用消息队列
			if (!MessageQueue.Exists(@".\private$\myQueueTrans"))
			{
				MessageQueue myTranMessage = MessageQueue.Create(@".\private$\myQueueTrans", true);
			}
			MessageQueue myQueue = new MessageQueue(".\\private$\\myQueueTrans");

			var myMessage = new Message(1231, new XmlMessageFormatter(new Type[] { typeof(int) }));

			MessageQueueTransaction myTransaction = new MessageQueueTransaction();

			// 启动事务
			myTransaction.Begin();

			// 发送加入事务
			myQueue.Send(myMessage, myTransaction);

			myTransaction.Commit();

			Console.WriteLine("消息发送成功！");

		}

		public static void TransactionReceiveQueue()
		{
			MessageQueue myQueue = new MessageQueue(".\\private$\\myQueueTrans");

			myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(int) });

			if (myQueue.Transactional)
			{
				MessageQueueTransaction myTransaction = new MessageQueueTransaction();
				myTransaction.Begin();

				var myMessage = myQueue.Receive();
				int context = (int)myMessage.Body;

				myTransaction.Commit();

				Console.WriteLine(context);

			}
		}


		// 异步
		public static void SendMessageAsync()
		{
			if (!MessageQueue.Exists(".\\private$\\myAsyncQueue"))
			{
				MessageQueue.Create(".\\private$\\myAsyncQueue", true);
			}

			MessageQueue myQueue = new MessageQueue(".\\private$\\myAsyncQueue");
			if (myQueue.Transactional)
			{
				Book book = new Book();
				book.BookId = 1001;
				book.BookName = "ASP.NET";
				book.BookAuthor = "ZhangSan";
				book.BookPrice = 88.88;

				var myMessage = new Message(book, new XmlMessageFormatter(new Type[] { typeof(Book) }));

				var myTranaaction = new MessageQueueTransaction();

				myTranaaction.Begin();

				myQueue.Send(myMessage, myTranaaction);

				myTranaaction.Commit();

				Console.WriteLine("发送消息成功");
			}


		}

		/// <summary>
		/// 异步接收信息
		/// </summary>

		public static void ReceiveMessageAsync()
		{
			MessageQueue myQueue = new MessageQueue(".\\private$\\myAsyncQueue");

			if (myQueue.Transactional)
			{
				MessageQueueTransaction myTransaction = new MessageQueueTransaction();

				// 这里使用委托，当接受消息完成的时候就执行 MyReceiveCompleted 方法
				//这里使用了委托,当接收消息完成的时候就执行MyReceiveCompleted方法
				myQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(MyReceiveCompleted);
				myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Book) });
				myTransaction.Begin();
				myQueue.BeginReceive();//启动一个没有超时时限的异步操作
				//signal.WaitOne();
				myTransaction.Commit();

				Console.WriteLine("执行完了");

			}
		}


		private static void MyReceiveCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
		{
			try
			{
				MessageQueue myQueue = (MessageQueue)source;
				//完成指定的异步接收操作
				Message message = myQueue.EndReceive(asyncResult.AsyncResult);
				//signal.Set();
				Book book = message.Body as Book;
				Console.WriteLine("图书编号：{0}--图书名称：{1}--图书作者：{2}--图书定价：{3}",
				   book.BookId.ToString(),
				   book.BookName,
				   book.BookAuthor,
				   book.BookPrice.ToString());
				myQueue.BeginReceive();

				Console.WriteLine("处理book");
			}
			catch (MessageQueueException me)
			{
				Console.WriteLine("异步接收出错,原因：" + me.Message);

			}

		}



		private static int ThreadNumber = 5;
		private static Thread[] ThreadArray = new Thread[ThreadNumber];

		private static void StartThreads()
		{
			int counter; //线程计数

			for (counter = 0; counter < ThreadNumber; counter++)
			{
				ThreadArray[counter] = new Thread(new ThreadStart(MSMQListen));
				ThreadArray[counter].Start();
				Console.WriteLine((counter + 1) + "号线程开始！");
			}

		}



		private static void MSMQListen()
		{
			while (true)
			{
				Console.WriteLine(ReceiveMessage());
			}
		}



		public static void SeneRemoteMessage()
		{
			try
			{
				//if (MessageQueue.Exists(@"FormatName:Direct=TCP:192.168.31.185\\private$\\queue"))
				//{
				//	MessageQueue.Create(@"FormatName:Direct=TCP:192.168.31.185\\private$\\queue");
				//}

				MessageQueue rmQ = new MessageQueue(@"FormatName:Direct=TCP:192.168.31.39\\myQueue");
				//,Direct=TCP:192.168.1.2\\private$\\queue


				var myMessage = new Message(1231, new XmlMessageFormatter(new Type[] { typeof(int) }));

				//rmQ.Send("sent to regular queue - Atul");

				rmQ.Send(myMessage);
				
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}



		//#endregion  
	}
}
