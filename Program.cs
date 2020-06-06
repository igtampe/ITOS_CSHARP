using System;
using BasicRender;
using BasicWindows;

namespace ITOS_CSHARP {
    public class Program{
        
        public static void Main(string[] args)  {

            Render.Color(ConsoleColor.DarkCyan,ConsoleColor.Black);
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
