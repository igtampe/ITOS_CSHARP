using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicRender;

namespace ITOS_CSHARP {
    public class Program{
        
        public static void Main(string[] args)  {

            BasicRenderGraphic Clouds = new BasicGraphicFromFile("Cloudy.df");
            Clouds.draw(1, 1);

            Render.Pause();

        }
    }
}
