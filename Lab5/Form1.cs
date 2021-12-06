using Lab5.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        readonly List<BaseObject> objects = new();
        //лист кругов
        readonly List<MyCircle> circles = new();

        readonly Player player;
        Marker? marker;
        ushort score = 0;

        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            objects.Add(player);

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            //делегат пересечения игрока с кругом
            player.OnCircleOverlap += (circle) =>
            {
                Random random = new();
                circle.X = random.Next(20, 580);
                circle.Y = random.Next(20, 380);
                circle.Timer = random.Next(60, 150);

                score++;
                lblScore.Text = $"Счёт: {score}";
            };

            Random random = new();
            circles.Add(new MyCircle(random.Next(20, 580), random.Next(20, 480), 0));
            circles.Add(new MyCircle(random.Next(20, 580), random.Next(20, 380), 0));

            foreach (var item in circles.ToList())
            {
                //делегат окончания таймера
                item.OnDecreaseTimer += (circle) =>
                {
                    Random random = new();
                    circle.X = random.Next(20, 580);
                    circle.Y = random.Next(20, 380);
                    circle.Timer = random.Next(60, 150);
                };
            }
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            updatePlayer();
            UpdateCircles();

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            foreach (var obj in circles.ToList())
            {
                //проверка пересечения игрока с кругами
                if (player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }

            //отрисовка кругов
            foreach (var circle in circles)
            {
                g.Transform = circle.GetTransform();
                circle.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }

        //метод обновления круга
        private void UpdateCircles()
        {
            foreach (var circle in circles)
            {
                circle.DecreaseTimer();
            }
        }

        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
