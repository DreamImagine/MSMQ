using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMQ
{
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
