using System;
using System.Collections;
using BasicRender;

namespace BasicWindows {

    public enum HeaderPosition { LEFT, CENTER, RIGHT }

    public abstract class Window {

        public const ConsoleColor WindowClearColor = ConsoleColor.DarkCyan;

        private Boolean Animated;
        private Boolean Shadowed;

        protected int LeftPos;
        protected int TopPos;
        protected int Length;
        protected int Height;

        protected String Title;

        protected ConsoleColor MainBG;
        protected ConsoleColor HeaderBG;
        protected ConsoleColor HeaderFG;
        protected HeaderPosition HeadPos;

        protected WindowElement HighlightedElement;

        protected ArrayList AllElements;

        /// <summary>Creates a window.</summary>
        /// <param name="Animated"></param>
        /// <param name="Shadowed"></param>
        /// <param name="Length"></param>
        /// <param name="Height"></param>
        /// <param name="LeftPos">Left Position (Specify -1 for centered)</param>
        /// <param name="TopPos">Top Position (Specify -1 for centered</param>
        public Window(Boolean Animated,Boolean Shadowed,ConsoleColor MainBG, ConsoleColor HeaderBG, ConsoleColor HeaderFG, HeaderPosition HeadPos, String Title, int Length,int Height,int LeftPos,int TopPos) {

            this.Animated=Animated;
            this.Shadowed=Shadowed;

            this.MainBG=MainBG;
            this.HeaderBG=HeaderBG;
            this.HeaderFG=HeaderFG;
            this.HeadPos=HeadPos;

            this.Title=Title;
            this.Length=Length;
            this.Height=Height;

            if(LeftPos==-1) { this.LeftPos=((Console.WindowWidth)-Length)/2; } else { this.LeftPos=LeftPos; }
            if(TopPos==-1) { this.TopPos=((Console.WindowHeight)-Height)/2; } else { this.TopPos=TopPos; }
            
            AllElements=new ArrayList();
        }

        public void Execute() { 
            Draw(Animated);

            //OnKeyPress returns true if we should continue execution.
            while(onKeyPress(Console.ReadKey(true))) {}


        }
        private void Draw(Boolean Animated) {

            //Render the shadow
            if(Shadowed) { Render.Box(ConsoleColor.Black,Length,Height-1,LeftPos+2,TopPos+2); }

            //Do the animation
            if(Animated) {
                int SmallHeight = 1;

                for(int SmallLength = 0; SmallLength<Length; SmallLength++) {
                    if(SmallHeight!=Height) { SmallHeight++; }
                    Render.Box(MainBG,SmallLength,SmallHeight,LeftPos,TopPos);
                }
            }

            //draw the main box
            Render.Box(MainBG,Length,Height,LeftPos,TopPos);

            //draw the header
            Render.Row(HeaderBG,Length,LeftPos,TopPos);

            Render.SetPos(LeftPos,TopPos);

            Render.Color(HeaderBG,HeaderFG);

            //Draw the header text.
            switch(HeadPos) {
                case HeaderPosition.LEFT:
                    Render.Echo("═ "+Title+" ");
                    for(int i = 3+Title.Length; i<Length; i++) { Render.Echo("═"); }
                    break;
                case HeaderPosition.CENTER:
                    //calculate lengths of the two side bars.
                    int SidesLength = (Length-(Title.Length+2))/2;

                    //Do the first side
                    for(int i = 0; i<SidesLength; i++) { Render.Echo("="); }

                    //Echo the title
                    Render.Echo(" "+Title+" ");

                    //Do the other side
                    for(int i = 0; i<SidesLength; i++) { Render.Echo("="); }

                    break;
                case HeaderPosition.RIGHT:
                    for(int i = 0; i<Length-3+Title.Length; i++) { Render.Echo("═"); }
                    Render.Echo(" "+Title+" =");
                    break;
                default:
                    break;
            }

            //Draw the footer
            Render.Color(MainBG,HeaderFG);
            Render.SetPos(LeftPos,TopPos+Height-1);
            for(int i = 0; i<Length; i++) { Render.Echo("═"); }

            //Draw each subelement.
            foreach(WindowElement element in AllElements) { element.Draw(LeftPos,TopPos); }
        }

        public bool onKeyPress(ConsoleKeyInfo PressedKey) {

            if(PressedKey.Modifiers==ConsoleModifiers.Control&&PressedKey.Key==ConsoleKey.W) { Close(); return false; }
            switch(HighlightedElement.OnKeyPress(PressedKey)) {
                case KeyPressReturn.NOTHING:
                    break;
                case KeyPressReturn.NEXT_ELEMENT:
                    if(HighlightedElement.GetNextElement()!=null) {
                        HighlightedElement.setHighlighted(false);
                        HighlightedElement=HighlightedElement.GetNextElement();
                        HighlightedElement.setHighlighted(true);
                    }
                    break;
                case KeyPressReturn.PREV_ELEMENT:
                    if(HighlightedElement.GetPrevElement()!=null) {
                        HighlightedElement.setHighlighted(false);
                        HighlightedElement=HighlightedElement.GetPrevElement();
                        HighlightedElement.setHighlighted(true);
                    }
                    break;
                case KeyPressReturn.CLOSE:
                    Close(); return false;
                default:
                    break;
            }
            return true;

        }

        /// <summary>Redraws the window without animations</summary>
        public void Redraw() { Draw(true); }

        /// <summary>Closes the window</summary>
        public void Close() {
            //Do the animation
            if(Animated) {
                int SmallHeight = 1;

                for(int SmallLength = 0; SmallLength<Length; SmallLength++) {
                    if(SmallHeight!=Height) { SmallHeight++; }
                    Render.Box(WindowClearColor,SmallLength,SmallHeight,LeftPos,TopPos);
                }
            }

            //Delete the mainbox.
            Render.Box(WindowClearColor,Length,Height,LeftPos,TopPos);

            //if shadowed, delete the main shadow
            if(Shadowed) {Render.Box(WindowClearColor,Length,Height-1,LeftPos+2,TopPos+2); }

        }
    }

    public class HelloWorldWindow:Window {
        public HelloWorldWindow() : base(true,true,ConsoleColor.Gray,ConsoleColor.DarkBlue,ConsoleColor.White,HeaderPosition.CENTER,"Hello World!",20,6,-1,-1) {

            AllElements.Add(new Icon(this,Icon.IconType.INFORMATION,1,2));
            AllElements.Add(new Label(this,"Hello World!",ConsoleColor.Gray,ConsoleColor.Black,6,2));

            Button OKButton = new CloseButton(this,"[    OK    ]",ConsoleColor.DarkGray,ConsoleColor.White,ConsoleColor.DarkBlue,6,4);
            AllElements.Add(OKButton);

            HighlightedElement=OKButton;
            OKButton.setHighlighted(true);

        }

    }

}
