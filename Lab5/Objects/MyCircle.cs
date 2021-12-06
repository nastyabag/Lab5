using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Objects
{
    public class MyCircle : BaseObject
    {
        public int Timer;

        public Action<MyCircle> ;

        public MyCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(
                new SolidBrush(Color.Green),
                -20, -20, 40, 40
                );
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-20, -20, 40, 40);
            return path;
        }

        public void DecreaseTimer()
        {
            Timer--;
            if (Timer < 0)
            {

            }
        }
    }
}
