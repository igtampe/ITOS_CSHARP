using System;
using BasicRender;

namespace BasicWindows {

    public enum KeyPressReturn {NOTHING, NEXT_ELEMENT, PREV_ELEMENT, CLOSE,PROCEED}

    /// <summary>Generic WindowElement</summary>
    public abstract class WindowElement {

        protected WindowElement NextElement;
        protected WindowElement PreviousElement;
        protected Window Parent;

        protected int LeftPos;
        protected int TopPos;
        protected Boolean Highlighted;

        public WindowElement(Window Parent) { this.Parent = Parent;}

        public WindowElement GetNextElement() { return NextElement; }
        public WindowElement GetPrevElement() { return PreviousElement; }
        public void SetNextElement(WindowElement NextElement) { this.NextElement = NextElement; }
        public void SetPrevElement(WindowElement PrevElement) { this.PreviousElement = PrevElement; }

        public virtual KeyPressReturn OnKeyPress(ConsoleKeyInfo Key) { return KeyPressReturn.NOTHING; }
        public void setHighlighted(Boolean Highlighted) { this.Highlighted = Highlighted; }
        public abstract void Draw(int WindowLeft, int WindowTop);

    }

    /// <summary>Label that holds text</summary>
    public class Label : WindowElement{
        private String Text;
        private ConsoleColor BG;
        private ConsoleColor FG;

        public Label(Window Parent, string Text, ConsoleColor BG, ConsoleColor FG, int LeftPos, int TopPos):base(Parent){
            this.Text = Text;
            this.BG = BG;
            this.FG = FG;
            this.LeftPos = LeftPos;
            this.TopPos = TopPos;
        }

        public override void Draw(int WindowLeft, int WindowTop) { Render.Sprite(Text, BG, FG, WindowLeft + LeftPos, WindowTop + TopPos); }
    }

    /// <summary>Image that holds a BasicRenderGraphic</summary>
    public class Image : WindowElement{

        private BasicRenderGraphic Graphic;

        public Image(Window Parent, BasicRenderGraphic Graphic, int LeftPos, int TopPos): base(Parent){
            this.Graphic = Graphic;
            this.LeftPos = LeftPos;
            this.TopPos = TopPos;
        }

        public override void Draw(int WindowLeft, int WindowTop) { Graphic.draw(WindowLeft + LeftPos, WindowTop + TopPos); }

    }

    /// <summary>Image that holds a BasicRenderGraphic</summary>
    public class Box:WindowElement {

        private int Length;
        private int Height;
        private ConsoleColor Color; 
            

        public Box(Window Parent, ConsoleColor Color, int Length, int Height,int LeftPos,int TopPos) : base(Parent) {
            this.Length=Length;
            this.Color=Color;
            this.Height=Height;
            this.LeftPos=LeftPos;
            this.TopPos=TopPos;
        }

        public override void Draw(int WindowLeft,int WindowTop) { Render.Box(Color,Length,Height,WindowLeft+LeftPos,WindowTop+TopPos); }

    }

    public class Icon:WindowElement {

        public enum IconType {ERROR,EXCLAMATION,INFORMATION,QUESTION}

        private IconType Type;

        public Icon(Window Parent,IconType Type,int LeftPos, int TopPos) : base(Parent) {
            this.Type=Type;

            this.LeftPos=LeftPos;
            this.TopPos=TopPos;
        }

        public override void Draw(int WindowLeft,int WindowTop) {
            switch(Type) {
                case IconType.ERROR:
                    Render.Box(ConsoleColor.Red,3,3,WindowLeft+LeftPos,WindowTop+TopPos);
                    Render.Sprite("X",ConsoleColor.Red,ConsoleColor.White,WindowLeft+LeftPos+1,WindowTop+TopPos+1);
                    break;
                case IconType.EXCLAMATION:
                    Render.Box(ConsoleColor.Yellow,3,3,WindowLeft+LeftPos,WindowTop+TopPos);
                    Render.Sprite("!",ConsoleColor.Yellow,ConsoleColor.Black,WindowLeft+LeftPos+1,WindowTop+TopPos+1);
                    break;
                case IconType.INFORMATION:
                    Render.Box(ConsoleColor.DarkBlue,3,3,WindowLeft+LeftPos,WindowTop+TopPos);
                    Render.Sprite("i",ConsoleColor.DarkBlue,ConsoleColor.White,WindowLeft+LeftPos+1,WindowTop+TopPos+1);
                    break;
                default:
                    Render.Box(ConsoleColor.DarkBlue,3,3,WindowLeft+LeftPos,WindowTop+TopPos);
                    Render.Sprite("?",ConsoleColor.DarkBlue,ConsoleColor.White,WindowLeft+LeftPos+1,WindowTop+TopPos+1);
                    break;
            }

        }
    }


    /// <summary>Button, though it may as well be a clickable link.</summary>
    public abstract class Button : WindowElement{
        protected ConsoleColor HighlightedBG;
        protected ConsoleColor BG;
        protected ConsoleColor FG;
        protected String Text;

        public Button(Window Parent, String Text, ConsoleColor BG, ConsoleColor FG, ConsoleColor HighlightedBG, int LeftPos, int TopPos):base(Parent){
            this.Text = Text;
            this.BG = BG;
            this.FG = FG;
            this.HighlightedBG = HighlightedBG;
            this.LeftPos = LeftPos;
            this.TopPos = TopPos;
        }

        public override void Draw(int WindowLeft, int WindowTop){
            if (Highlighted) { Render.Sprite(Text, HighlightedBG, FG, WindowLeft + LeftPos, WindowTop + TopPos); }
            else { Render.Sprite(Text, BG, FG, WindowLeft + LeftPos, WindowTop + TopPos); }
        }

        public override KeyPressReturn OnKeyPress(ConsoleKeyInfo Key) { if(Key.Key==ConsoleKey.Enter) { return Action(); } else { return KeyPressReturn.NOTHING; } }

        public abstract KeyPressReturn Action();

    }

    public class CloseButton:Button {
        public CloseButton(Window Parent,string Text,ConsoleColor BG,ConsoleColor FG,ConsoleColor HighlightedBG,int LeftPos,int TopPos) : base(Parent,Text,BG,FG,HighlightedBG,LeftPos,TopPos) {}

        public override KeyPressReturn Action() { return KeyPressReturn.CLOSE; }
    }

}
