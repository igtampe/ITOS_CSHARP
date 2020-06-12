using System;
using BasicRender;
using BasicWindows;

namespace ITOS_CSHARP {
    public class Program{
        
        public static void Main(string[] args)  {


            Console.Title="ITOS EMULATOR [Verison 4.0]";
            Console.BackgroundColor=ConsoleColor.Black;
            Console.ForegroundColor=ConsoleColor.White;

            Console.Clear();



            Console.Clear();




            Window Hello = new HelloWorldWindow();
            Hello.Execute();

            

            Render.Color(ConsoleColor.Black,ConsoleColor.Yellow);
            Console.Clear();
            Render.CenterText("ITOS has shut down. Press a key to turn off your computer.",Console.WindowHeight/2,ConsoleColor.Black,ConsoleColor.DarkYellow);
            Render.Pause();

        }
    }
}
