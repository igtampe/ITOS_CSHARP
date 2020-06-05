using System;
using System.IO;

namespace BasicRender
{
    /// <summary>Holds a Drawable BasicRenderGraphic</summary>
    public abstract class BasicRenderGraphic {
        protected String[] Contents;
        protected String Name;

        public abstract void draw(int LeftPos, int TopPos);
        public string getName() { return Name; }
    }

    /// <summary>Holds a BasicGraphic</summary>
    public abstract class BasicGraphic : BasicRenderGraphic{
        public override void draw(int LeftPos, int TopPos) {
            foreach (String Line in Contents){
                Render.SetPos(LeftPos, TopPos++);
                Render.Draw(Line);
            }
        }
    }

    /// <summary>Holds a HiColorGraphic</summary>
    public abstract class HiColorGraphic : BasicRenderGraphic{
        public override void draw(int LeftPos, int TopPos){
            foreach (String Line in Contents){
                Render.SetPos(LeftPos, TopPos++);
                Render.HiColorDraw(Line);
            }
        }
    }

    /// <summary>Holds a BasicGraphic from a file</summary>
    public class BasicGraphicFromFile : BasicGraphic {

        /// <summary>Generates a BasicGraphic item from a file</summary>
        public BasicGraphicFromFile(String Filename) {

            if (!File.Exists(Filename)) { throw new FileNotFoundException(); }
            Name = Filename;
            Contents = File.ReadAllLines(Filename);

        }

    }

    /// <summary>Holds a HiColorGraphic from a file</summary>
    public class HiColorGraphicFromFile : HiColorGraphic{

        /// <summary>Generates a HiColorGraphic item from a file</summary>
        public HiColorGraphicFromFile(String Filename){

            if (!File.Exists(Filename)) { throw new FileNotFoundException(); }
            Name = Filename;
            Contents = File.ReadAllLines(Filename);
        }
    }

    /// <summary>A Cloud graphic, used for testing.</summary>
    public class Cloud : BasicGraphic {

        public Cloud() {

            String[] Cloud = {
                "111111111111111111111111",
                "111111111111111111111111",
                "111111111111111111111111",
                "111111111111111111111111",
                "111111177771111111111111",
                "111117777777711111111111",
                "111177777777777777111111",
                "111777777777777777771111",
                "117777777777777777777711",
                "111111111111111111111111",
                "111111111111111111111111",
                "111111111111111111111111",
                "111111111111111111111111", 
            };
            
            Contents = Cloud;
            Name = "Cloud Graphic";
        }


    }

}
