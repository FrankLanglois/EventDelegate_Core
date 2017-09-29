using System;
using System.Collections.Generic;
using System.Text;

namespace EventDelegate_Core
{
    public class WorkPerformedEventArgs : EventArgs
    {
        public WorkPerformedEventArgs(int value1, int value2)
        {
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }
}
