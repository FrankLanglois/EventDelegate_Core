using System;

namespace EventDelegate_Core
{
    class Program
    {
        public delegate int OperationHandler( int value1, int value2 );

        static void Main( string[] args )
        {
            Console.SetWindowSize(Console.LargestWindowWidth-80, Console.LargestWindowHeight-20);
            // Direct Approach with Delegates
            DirectApproachWithoutEvent();

            //Direct Approach Withou tEvent Wit hLambdas
            DirectApproachWithoutEventWithLambdas();

            //  Direct Approach Without Event With Action()
            DirectApproachWithoutEventWithActions();

            // Direct Approac hWithout Event With Functions
            DirectApproachWithoutEventWithFunctions();

            // Approach with Event handler
            ApproachWithEventHandlers();

            // Approach with Event handler with delegate inference
            ApproachWithEventHandlersWithDelegateInference();

            // Approach With EventHandlers With Anonymous Methods
            ApproachWithEventHandlersWithAnonymousMethods();

            // Approach With EventHandlers With Lambdas()
            ApproachWithEventHandlersWithLambdas();



            Console.ReadKey();
        }

        
        static public void DirectApproachWithoutEvent()
        {
            var operationHandlerAdd = new OperationHandler( Add );
            var operationHandlerMultiply = new OperationHandler( Multiply );

            // Bad signature, doesn't match delete
            /// operationHandler += new OperationHandler( Add3 );

            Console.WriteLine( "Direct Approach Without Event:" );
            Console.WriteLine( $"\tAddition result: {DoWork( 2, 3, operationHandlerAdd )}" );
            Console.WriteLine( $"\tMultiplication Result: {DoWork( 2, 3, operationHandlerMultiply ) }" );

            Console.WriteLine( $"\tAdditonWithDirectMethod: { DoWork( 5, 5, Add )}" );
            Console.WriteLine( "\r\n" );
        }

        static public void DirectApproachWithoutEventWithLambdas()
        {
            OperationHandler delAdd = ( a, b ) => a + b;
            OperationHandler delMultiply = ( a, b ) => a * b;

            Console.WriteLine( "Direct Approach Without Event with Lambdas:" );
            Console.WriteLine( $"\tAddition result: {DoWork( 2, 3, delAdd )}" );
            Console.WriteLine( $"\tMultiplication Result: {DoWork( 2, 3, delMultiply ) }" );

            Console.WriteLine( $"\tAdditonWithDirectMethod: { DoWork( 5, 5, Add )}" );
            Console.WriteLine( "\r\n" );
        }

        static public void DirectApproachWithoutEventWithActions()
        {
            Console.WriteLine( "Direct Approach Without Event with Actions:" );
            Action<int,int> actionAdd = (a,b) => Console.WriteLine( $"\tAddition result: {a+b}" );
            Action<int, int> actionMultiply = (a, b) => Console.WriteLine($"\tMultiplication result: {a*b}");
            
            DoWorkWithAction( 2, 3, actionAdd );
            DoWorkWithAction( 2, 3, actionMultiply );
            Console.WriteLine( "\r\n" );
        }

        static public void DirectApproachWithoutEventWithFunctions()
        {
            Func<int, int, int> funcAdd = ( a, b ) => a + b;
            Func<int, int, int> funcMultiply = ( a, b ) => a * b;

            Console.WriteLine( "Direct Approach Without Event with Functions:" );
            Console.WriteLine( $"\tAddition result: {DoWorkWithFunction( 2, 3, funcAdd )}" );
            Console.WriteLine( $"\tMultiplication Result: {DoWorkWithFunction( 2, 3, funcMultiply ) }" );

           
            Console.WriteLine( "\r\n" );
        }

        static public void ApproachWithEventHandlers()
        {
            Console.WriteLine( "Approach With Event Handlers:" );
            var worker = new Worker();
            worker.WorkPerformed += new EventHandler<WorkPerformedEventArgs>( Worker_workPerformed );
            worker.WorkCompleted += new EventHandler( Worker_WorkCompleted );
            worker.DoWork();
            Console.WriteLine( "\r\n" );
        }

        static public void ApproachWithEventHandlersWithDelegateInference()
        {
            Console.WriteLine( "Approach With EventHandlers With Delegate Inference:" );
            var worker = new Worker();
            worker.WorkPerformed += Worker_workPerformed;         
            worker.WorkCompleted += Worker_WorkCompleted;
            worker.DoWork();
            Console.WriteLine( "\r\n" );
        }

        static public void ApproachWithEventHandlersWithAnonymousMethods()
        {
            Console.WriteLine( "Approach With Event Handlers with Anonymous Methods:" );
            var worker = new Worker();
            worker.WorkPerformed += delegate( object sender, WorkPerformedEventArgs e )
            {
                Console.WriteLine( $"\tAddtion: {e.Value1 + e.Value2}" );
            };
            worker.WorkCompleted += delegate( object sender, EventArgs e )
            {
                Console.WriteLine( "\tCalculations done!" );
            };
            worker.DoWork();
            Console.WriteLine( "\r\n" );
        }

        static public void ApproachWithEventHandlersWithLambdas()
        {
            Console.WriteLine( "Approach With Event Handlers with Lambdas:" );
            var worker = new Worker();
            worker.WorkPerformed += ( s, e ) =>
            {
                Console.WriteLine( $"\tAddtion: {e.Value1 + e.Value2}" );
            };
            worker.WorkCompleted += ( s, e ) =>
            {
                Console.WriteLine( "\tCalculations done!" );
            };
            worker.DoWork();
            Console.WriteLine( "\r\n" );
        }
             

        static private int DoWork( int value1, int value2, OperationHandler del )
        {
            return del( value1, value2 );
        }

        // Action<T> is a delegate.
        static private void DoWorkWithAction( int value1, int value2, Action<int,int> del )
        {
            del( value1, value2 );
        }

        // Function<T,TResult> is a delegate.
        static private int DoWorkWithFunction( int value1, int value2, Func<int,int,int> del )
        {
            return del( value1, value2 );
        }

        static private int Add( int a, int b )
        {
            return a + b;
        }

        static private int Add3( int a, int b, int c )
        {
            return a + b + c;
        }

        static private int Multiply( int a, int b )
        {
            return a * b;
        }
                private static void Worker_WorkCompleted( object sender, EventArgs e )
        {
            Console.WriteLine( "\tCalculations done!" );
        }

        private static void Worker_workPerformed( object sender, WorkPerformedEventArgs e )
        {
            Console.WriteLine( $"\tAddtion: {e.Value1 + e.Value2}" );
        }

    }
}
