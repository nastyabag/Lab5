﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Objects
{
    public class Player : BaseObject
    {
        public Action<Marker> OnMarkerOverlap;
        public Action<MyCircle> OnCircleOverlap;

        public float vX;
        public float vY;
        public Player(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(
                new SolidBrush(Color.DeepSkyBlue),
                -15, -15, 30, 30
                );

            g.DrawEllipse(
                new Pen(Color.Black, 2),
                -15, -15,
                30, 30
                );

            g.DrawEllipse(new Pen(Color.Black, 2), 0, 0, 25, 0);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            if (obj is Marker)
                OnMarkerOverlap(obj as Marker);
            else if (obj is MyCircle)
                OnCircleOverlap(obj as MyCircle);
        }
    }
}
