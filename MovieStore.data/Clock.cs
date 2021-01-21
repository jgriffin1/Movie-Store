using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.data
{
    public class Clock
    {
        public int Hr { get; set; }
        public int Min { get; set; }
        public int Sec { get; set; }

        //default constructor
        public Clock()
        {

        }
        public Clock(int hr, int min, int sec)
        {
            this.Hr = hr;
            this.Min = min;
            this.Sec = sec;
        }

    }
}
