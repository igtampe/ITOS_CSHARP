using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOS_CSHARP{
    /// <summary></summary>
    public class BasicRenderC {

        //===============================================================
        //                  BASIC RENDER C#, VERSION 1.0
        //===============================================================
        //(C)2020 Igtampe No rights Reserved.
        //
        //This class is designed to help and quickly render items
        //onscreen in a commandline display. Simply set a few parameters
        //and you should be good to go!

        //It also provides some basic utilities to make coding on C#
        //just a tiny bit more like coding on batch.

        //Configuration of BasicRender:

        public const ConsoleColor WindowBG = ConsoleColor.Black;
        public const ConsoleColor WindowFG = ConsoleColor.White;
        public const ConsoleColor WindowClearColor = ConsoleColor.Black;

        /// <summary>Renders a sprite at the current cursor position</summary>
        /// <param name="sprite"></param>
        /// <param name="BG"></param>
        /// <param name="FG"></param>
        public static void Sprite(string sprite, ConsoleColor BG, ConsoleColor FG) {Sprite(sprite, BG, FG, -1, -1);}

        /// <summary>renders a sprite at the specified cursor position</summary>
        /// <param name="sprite"></param>
        /// <param name="BG"></param>
        /// <param name="FG"></param>
        /// <param name="LeftPos"></param>
        /// <param name="TopPos"></param>
        public static void Sprite(string sprite, ConsoleColor BG, ConsoleColor FG, int LeftPos, int TopPos) {

            if (LeftPos != -1 && TopPos != -1) { SetPos(LeftPos, TopPos); }


        }

        /// <summary>Sets the cursor position to the specified one</summary>
        /// <param name="LeftPos"></param>
        /// <param name="TopPos"></param>
        public static void SetPos(int LeftPos, int TopPos) { Console.SetCursorPosition(LeftPos, TopPos); }















    }



}
