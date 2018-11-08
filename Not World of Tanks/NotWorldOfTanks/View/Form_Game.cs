using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotWorldOfTanks.View;

namespace NotWorldOfTanks
{
    public partial class Form1 : Form
    {
        private AreaView _area = default(AreaView);
        private Size _size = default(Size);
        private Point _targetPosition = default(Point);
        public Bitmap tank = default(Bitmap);
        public Bitmap wall = default(Bitmap);
        private TankView _tankView = default(TankView);
        public PictureBox picBox = default(PictureBox);

        public Form1(AreaView area)
        {
            _area = area;

            InitializeComponent();

            this.Size = new Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2);
            pictureBox1.Left = area.LocationArea(ClientSize.Width, pictureBox1.Width);
            pictureBox1.Top = area.LocationArea(ClientSize.Height, pictureBox1.Height);

            this.pictureBox1.Size = _area.AreaSize;
            this.pictureBox1.Size = _area.SetScale(this.Size,pictureBox1.Size);

            this.MinimumSize = new Size(pictureBox1.Width + 150, pictureBox1.Height + 150);

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _targetPosition.X = pictureBox1.Height / 2;// Испраить!!
            _targetPosition.Y = pictureBox1.Width - 60;
            tank = Resource_NWoT.Tank;
            wall = Resource_NWoT.wall;

        }

        protected override void OnResize(EventArgs e)
           {
             base.OnResize(e);
             pictureBox1.Left = ClientSize.Width / 2 - pictureBox1.Width / 2;
             pictureBox1.Top = ClientSize.Height / 2 - pictureBox1.Height / 2;
           }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_targetPosition.Y > 30) _targetPosition.Y -= 1;
            //if (_tankView._targetPosition.Y > 30) _tankView._targetPosition.Y = -1;
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(tank, new Rectangle(_targetPosition.X, _targetPosition.Y, 30, 30));

            for (int height = 0, width = 0;
                (height < pictureBox1.Width) && (width < pictureBox1.Height);
                height += 30, width += 30)
            {
                g.DrawImage(wall, new Rectangle(height, 0, 30, 30));
                g.DrawImage(wall, new Rectangle(0, width, 30, 30));
                g.DrawImage(wall, new Rectangle(height, pictureBox1.Width - 30, 30, 30));
                g.DrawImage(wall, new Rectangle(pictureBox1.Height - 30, width, 30, 30));

            }

            //_tankView.Draw(e, pictureBox1);
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;

        }
    }
}
