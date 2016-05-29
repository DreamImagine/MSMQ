using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMQ
{
    public class AsyncHelper
    {

        public void Init()
        {

             TestIntAsync();
        }

        public async Task<int> TestIntAsync()
        {

            return 1;
   
        }


    }
}
