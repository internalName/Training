using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotWorldOfTanks.Model;
using NotWorldOfTanks.View;
using Point = System.Drawing.Point;

namespace NotWorldOfTanks
{

    public partial class Form1 : Form
    {
        private AreaView _areaView = default(AreaView);
        private WallView _wallView = default(WallView);
        private TankView _tankView = default(TankView);
        private Point tankPoint = default(Point);
        private direction _direction = default(direction);
        private bool teleport = false;
        private RotateFlipType _startPos = default(RotateFlipType);


        public Form1(AreaView area)
        {
            _areaView = area;

            InitializeComponent();

            this.Size = new Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2);
            pictureBox1.Left = area.LocationArea(ClientSize.Width, pictureBox1.Width);
            pictureBox1.Top = area.LocationArea(ClientSize.Height, pictureBox1.Height);

            this.pictureBox1.Size = _areaView.AreaSize;
            this.pictureBox1.Size = _areaView.SetScale(this.Size, pictureBox1.Size);

            this.MinimumSize = new Size(pictureBox1.Width + 150, pictureBox1.Height + 150);

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _tankView = new TankView(_areaView.TankNumber);
            _wallView = new WallView(_areaView.AreaSize.Height, _areaView.AreaSize.Width);
            tankPoint = TankStart(1);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            pictureBox1.Left = ClientSize.Width / 2 - pictureBox1.Width / 2;
            pictureBox1.Top = ClientSize.Height / 2 - pictureBox1.Height / 2;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            bool crashed = false;

            int dir = r.Next(1, 5);
            


            for (int i = 0; i < _tankView._tank.Count; i++)
            {

                    if ((_tankView._tank[i].Direction is direction.Up))// && _tankView._tank[i].Y > 30 && !_wallView.FieldLocation.Any(n => n.Equals(new Point(_tankView._tank[i].X, _tankView._tank[i].Y - 30))))
                    {   
                        _tankView._tank[i].Y -= 1;
                        if (teleport)
                        {
                            if (dir == 1) break;
                            else if (dir == 2)
                            {
                                _tankView._tank[i].Direction = direction.Left;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                            else if (dir == 3)
                            {
                                _tankView._tank[i].Direction = direction.Down;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                            else if (dir == 4)
                            {
                                _tankView._tank[i].Direction = direction.Right;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                        }
                        break;

                }


                if ((_tankView._tank[i].Direction is direction.Down))// && _tankView._tank[i].Y < _area.AreaSize.Width - 30 &&!_wallView.FieldLocation.Any(n => n.Equals(new Point(_tankView._tank[i].X, _tankView._tank[i].Y + 30))))
                {
                    _tankView._tank[i].Y += 1;
                    if (teleport)
                    {
                        if (dir == 1) break;
                        else if (dir == 2)
                        {
                            _tankView._tank[i].Direction = direction.Left;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                        else if (dir == 3)
                        {
                            _tankView._tank[i].Direction = direction.Up;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                        else if (dir == 4)
                        {
                            _tankView._tank[i].Direction = direction.Right;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                        teleport = false;
                       
                    }
                    break;
                }


                if ((_tankView._tank[i].Direction is direction.Left) )//&& _tankView._tank[i].X > 30 && !_wallView.FieldLocation.Any(n => n.Equals(new Point(_tankView._tank[i].X - 30, _tankView._tank[i].Y))))
                {
                    _tankView._tank[i].X -= 1;

                    if (teleport)
                    {
                        if (dir == 1) break;
                        else if (dir == 2)
                        {
                            _tankView._tank[i].Direction = direction.Down;
                            _tankView.tank=_tankView.Flip(_tankView._tank[i].Direction);
                        }
                        else if (dir == 3)
                        {
                            _tankView._tank[i].Direction = direction.Up;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                        else if (dir == 4)
                        {
                            _tankView._tank[i].Direction = direction.Right;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                        teleport = false;
                    }
                    break;
                }

                if ((_tankView._tank[i].Direction is direction.Right))// && _tankView._tank[i].X < _area.AreaSize.Height - 30 && !_wallView.FieldLocation.Any(n => n.Equals(new Point(_tankView._tank[i].X + 30, _tankView._tank[i].Y))))
                {
                    _tankView._tank[i].X += 1;

                    if (teleport)
                    {
                        if (dir == 1) break;
                        else if (dir == 2)
                        {
                            _tankView._tank[i].Direction = direction.Left;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                        else if (dir == 3)
                        {
                            _tankView._tank[i].Direction = direction.Up;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                        else if (dir == 4)
                        {
                            _tankView._tank[i].Direction = direction.Down;
                            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
                        }
                        teleport = false;
                    }
                    break;
                }
            }

            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var point in _wallView.FieldLocation)
            {
                g.DrawImage(_wallView.WallImage, new Rectangle(point.StartPosition.X, point.StartPosition.Y, 30, 30));
            }
;
            for (int i = 0; i < _tankView._tank.Count; i++)
            {
                g.DrawImage(_tankView.tank, new Rectangle(_tankView._tank[i].X, _tankView._tank[i].Y, 30, 30));
            }
        }

        private bool MeetAboutTheWall(Tank tank)
        {
            bool result = false;

            //if (tank.Direction == direction.Down)
            //{
            //    if (points.Any(n => n.Y >= tank.Y + 30))
            //    {
            //        for (int i = 0; i < 30; i++)
            //        {
            //            if (points.Any(n => n.X <= tank.X - i) || points.Any(n => n.X >= tank.X + i))
            //            {
            //                result = true;
            //                break;
            //            }
            //        }
            //    }
            //}

            //if (tank.Direction == direction.Up)
            //{
            //    if (points.Any(n => n.Y >= tank.Y - 30))
            //    {
            //        for (int i = 0; i < 30; i++)
            //        {
            //            if (points.Any(n => n.X <= tank.X - i) || points.Any(n => n.X >= tank.X + i))
            //            {
            //                result = true;
            //                break;
            //            }
            //        }
            //    }
            //}

            //if (tank.Direction == direction.Left)
            //{
            //    if (points.Any(n => n.X >= tank.X - 30))
            //    {
            //        for (int i = 0; i < 30; i++)
            //        {
            //            if (points.Any(n => n.Y <= tank.Y - i) || points.Any(n => n.Y >= tank.Y + i))
            //            {
            //                result = true;
            //                break;
            //            }
            //        }
            //    }
            //}



            return result;
        }

        private Point TankStart(int numTank)
        {
            Point point = default(Point);

            Random r_h = new Random();

            int count = _areaView.AreaSize.Width * _areaView.AreaSize.Height - _areaView.AreaSize.Width / 10 -
                        (_areaView.AreaSize.Width * 2 + _areaView.AreaSize.Height * 2) + 4;

            int a = 0, b = 0;
            for (int j = 0; j < numTank; j++)
            {
                for (int i = 0; i < count; i++)
                {
                    a = r_h.Next(30, _areaView.AreaSize.Height - 30) / 30 * 30;
                    b = r_h.Next(30, _areaView.AreaSize.Width - 30) / 30 * 30;

                    if (!_wallView.FieldLocation.Any(n => new Point(a, b).Equals(n)))
                    {
                        point = new Point(a, b);
                        _tankView.AddTank(point.X, point.Y);
                        break;

                    }
                    else i -= 1;

                }
            }


            return point;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            timer2.Enabled = !timer2.Enabled;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random r=new Random();
            if(r.Next(0,2)==1) teleport = true;

        }
    }
}
