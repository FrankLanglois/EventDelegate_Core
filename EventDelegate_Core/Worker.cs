using System;
using System.Collections.Generic;
using System.Text;

namespace EventDelegate_Core
{
    public delegate int OperationPerformedHandler( object sender, WorkPerformedEventArgs e );

    public class Worker
    {
        public event EventHandler<WorkPerformedEventArgs> WorkPerformed;
        public event EventHandler WorkCompleted;

        public void DoWork()
        {
            for( int i = 0 ; i < 5 ; i++ )
            {
                // Raise event Performed               
                OnWorkPerformed( i, i + 2 );  
            }

            // Raise event Completed
            OnWorkCompleted();
        }

        protected virtual void OnWorkPerformed(int value1, int value2 )
        {            
            WorkPerformed?.Invoke( this, new WorkPerformedEventArgs(value1, value2));           
        }

        protected virtual void OnWorkCompleted()
        {            
            WorkCompleted?.Invoke( this, EventArgs.Empty );           
        }
    }
}
