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

        public Action<MyCircle> OnDecreaseTimer;

        public MyCircle(float x, float y, float angle) : base(x, y, angle)
        {
            Random random = new();
            Timer = random.Next(60, 150);
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(
                new SolidBrush(Color.Green),
                -20, -20, 40, 40
                );
            g.DrawString(
                $"{Timer}",
                new Font("Verdana", 8),
                new SolidBrush(Color.Green),
                15, 15
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
                OnDecreaseTimer(this as MyCircle);
            }
        }
    }
}
