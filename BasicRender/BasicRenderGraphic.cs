using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRender
{
    public abstract class BasicRenderGraphic {
        protected String[] Contents;
        protected String Name;

        public abstract void draw(int LeftPos, int TopPos);
        public string getName() { return Name; }
    }

    public abstract class BasicGraphic : BasicRenderGraphic{
        public override void draw(int LeftPos, int TopPos) {
            foreach (String Line in Contents){
                Render.SetPos(LeftPos, TopPos++);
                Render.Draw(Line);
            }
        }
    }

    public abstract class HiColorGraphic : BasicRenderGraphic{
        public override void draw(int LeftPos, int TopPos){
            foreach (String Line in Contents){
                Render.SetPos(LeftPos, TopPos++);
                Render.HiColorDraw(Line);
            }
        }
    }

    public class BasicGraphicFromFile : BasicGraphic {

        public BasicGraphicFromFile(String Filename) {

            if (!File.Exists(Filename)) { throw new FileNotFoundException(); }
            Name = Filename;
            Contents = File.ReadAllLines(Filename);

        }

    }
    public class HiColorGraphicFromFile : HiColorGraphic{

        public HiColorGraphicFromFile(String Filename){

            if (!File.Exists(Filename)) { throw new FileNotFoundException(); }
            Name = Filename;
            Contents = File.ReadAllLines(Filename);
        }
    }

}
