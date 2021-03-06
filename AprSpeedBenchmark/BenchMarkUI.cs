﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using NativeWIN32API;
namespace GBA_effect
{
    unsafe public partial class BenchMarkUI : Form
    {
        static uint* ScreenBg, Sprite, result;

        static int* sin_t, cos_t;

        public BenchMarkUI()
        {
            InitializeComponent();
            init();
        }

        private void BenchMarkUI_Shown(object sender, EventArgs e)
        {
            for (int y = 0; y < 512; y++)
                for (int x = 0; x < 512; x++)
                    result[x + y * 512] = ScreenBg[x + (y << 9)];

            for (int y = 0; y < 256; y++)
                for (int x = 0; x < 256; x++)
                {
                    uint color = Sprite[x + y * 256];
                    if ((color & 0xff000000) != 0) result[(x + 128) + ((y + 128) << 9)] = color;
                }
            NativeGDI.DrawImageHighSpeedtoDevice();
        }

        void init()
        {
            {
                Bitmap bg = new Bitmap(Application.StartupPath + "/bg.png");
                ScreenBg = (uint*)Marshal.AllocHGlobal(sizeof(uint) * 512 * 512);
                BitmapData srcData = bg.LockBits(new Rectangle(Point.Empty, bg.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                for (int y = 0; y < 512; y++)
                    for (int x = 0; x < 512; x++)
                        ScreenBg[x + y * 512] = ((uint*)srcData.Scan0.ToPointer())[x + y * 512];
                bg.UnlockBits(srcData);
            }

            {
                Bitmap sp = new Bitmap(Application.StartupPath + "/wheel.png");
                Sprite = (uint*)Marshal.AllocHGlobal(sizeof(uint) * 256 * 256);
                BitmapData srcData = sp.LockBits(new Rectangle(Point.Empty, sp.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                for (int y = 0; y < 256; y++)
                    for (int x = 0; x < 256; x++)
                        Sprite[x + y * 256] = ((uint*)srcData.Scan0.ToPointer())[x + y * 256];
                sp.UnlockBits(srcData);
            }
            result = (uint*)Marshal.AllocHGlobal(sizeof(uint) * 512 * 512);

            sin_t = (int*)Marshal.AllocHGlobal(sizeof(int) * 360);
            cos_t = (int*)Marshal.AllocHGlobal(sizeof(int) * 360);

            for (int i = 0; i < 360; i++)
            {
                double _angle = i * 0.01745329252;

                double cos = Math.Cos(_angle);
                double sin = Math.Sin(_angle);


                sin_t[i] = (int)Math.Round(sin * 1024);
                cos_t[i] = (int)Math.Round(cos * 1024);

            }

            NativeGDI.initHighSpeed(panel1.CreateGraphics(), 512, 512, result, 0, 0);

        }

        bool runnging = false;

        private void button2_Click(object sender, EventArgs e)
        {
            if (runnging)
            {
                MessageBox.Show("testing....");
                Console.WriteLine("running...wait!");
                return;
            }

            progressBar1.Value = 0;
            label_2.Text = "平均時速(整數) : ";

            new Thread(() =>
            {
                runnging = true;
                run2();
                runnging = false;
            }).Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (runnging)
            {
                MessageBox.Show("testing....");
                Console.WriteLine("running...wait!");
                return;
            }

            progressBar1.Value = 0;
            label_3.Text = "平均時速(綜合,無輸出) : ";

            new Thread(() =>
            {
                runnging = true;
                _run();
                runnging = false;
            }).Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (runnging)
            {
                MessageBox.Show("testing....");
                Console.WriteLine("running...wait!");
                return;
            }

            progressBar1.Value = 0;
            label_4.Text = "平均時速(整數,無輸出) : ";

            new Thread(() =>
            {
                runnging = true;
                _run2();
                runnging = false;
            }).Start();

        }

        private void benchmark_Click(object sender, EventArgs e)
        {
            if (runnging)
            {
                MessageBox.Show("testing....");
                Console.WriteLine("running...wait!");
                return;
            }

            progressBar1.Value = 0;
            label_1.Text = "平均時速(綜合) : ";

            new Thread(() =>
            {
                runnging = true;
                run();
                runnging = false;
            }).Start();
        }

        void run2()
        {
            Stopwatch st = new Stopwatch();
            st.Restart();

            const int ref_loc_x = 128, ref_loc_y = 128;


            int counts = 36000, dist = 0, w = 0, angle = 0;

            for (int i = 0; i < counts; i++)
            {

                int cos = cos_t[angle];
                int sin = sin_t[angle];

                for (int y = 0; y < 512; y++)
                    for (int x = 0; x < 512; x++)
                        result[x + (y << 9)] = ScreenBg[x + (y << 9)];

                Parallel.For(0, (256), y =>
                {
                    for (int x = 0; x < 256; x++)
                    {

                        uint color = Sprite[x + (y << 8)];

                        int index_dst = ((((x - ref_loc_x) * cos + (y - ref_loc_y) * sin) >> 10) + ref_loc_x + 128 )|
                        (((-1 * (x - ref_loc_x) * sin + (y - ref_loc_y) * cos)>>10) + ref_loc_y + 128 << 9);

                        if ((color & 0xff000000) != 0) result[index_dst] = result[index_dst - 1] = color;

                    }
                });
                NativeGDI.DrawImageHighSpeedtoDevice();

                angle += 6;
                dist += 1914;
                w += 6; // 0.0166666666666667;

                if (angle >= 360)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        progressBar1.Value = dist;
                    }));
                    angle -= 360;
                }
            }

            st.Stop();

            w /= 360;

            int ws = (int)(w / ((double)st.ElapsedMilliseconds / (1000.0 * 60)));
            int speed = (int)((ws * 60 * 1.914) / 1000.0);

            Console.WriteLine("Cost : " + st.ElapsedMilliseconds + " ms");
            Console.WriteLine("RPM : " + ws);
            Console.WriteLine("Avg Speed : " + speed + " Km/H");
            Console.WriteLine("dist : " + dist / 1000.0 + " m");

            this.Invoke(new MethodInvoker(() =>
            {
                label_2.Text = "平均時速(整數) : " + speed + "km/h";
            }));

        }

        void _run2()
        {
            Stopwatch st = new Stopwatch();
            st.Restart();

            const int ref_loc_x = 128, ref_loc_y = 128;


            int counts = 36000, dist = 0, w = 0, angle = 0;

            for (int i = 0; i < counts; i++)
            {

                int cos = cos_t[angle];
                int sin = sin_t[angle];

                for (int y = 0; y < 512; y++)
                    for (int x = 0; x < 512; x++)
                        result[x + (y << 9)] = ScreenBg[x + (y << 9)];

                Parallel.For(0, (256), y =>
                {
                    for (int x = 0; x < 256; x++)
                    {

                        uint color = Sprite[x + (y << 8)];

                        int index_dst = ((((x - ref_loc_x) * cos + (y - ref_loc_y) * sin) >> 10) + ref_loc_x + 128) |
                        (((-1 * (x - ref_loc_x) * sin + (y - ref_loc_y) * cos) >> 10) + ref_loc_y + 128 << 9);

                        if ((color & 0xff000000) != 0) result[index_dst] = result[index_dst - 1] = color;

                    }
                });
               // NativeGDI.DrawImageHighSpeedtoDevice();

                angle += 6;
                dist += 1914;
                w += 6; // 0.0166666666666667;

                if (angle >= 360)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        progressBar1.Value = dist;
                    }));
                    angle -= 360;
                }
            }

            st.Stop();

            w /= 360;

            int ws = (int)(w / ((double)st.ElapsedMilliseconds / (1000.0 * 60)));
            int speed = (int)((ws * 60 * 1.914) / 1000.0);

            Console.WriteLine("Cost : " + st.ElapsedMilliseconds + " ms");
            Console.WriteLine("RPM : " + ws);
            Console.WriteLine("Avg Speed : " + speed + " Km/H");
            Console.WriteLine("dist : " + dist / 1000.0 + " m");

            this.Invoke(new MethodInvoker(() =>
            {
                label_4.Text = "平均時速(整數,無輸出) : " + speed + "km/h";
            }));

        }


        void run()
        {
            Stopwatch st = new Stopwatch();
            st.Restart();

            const int ref_loc_x = 128, ref_loc_y = 128;

            double angle = 0, _angle, w = 0;
            int counts = 36000, dist = 0;

            for (int i = 0; i < counts; i++)
            {

                _angle = angle * 0.01745329252;

                double cos = Math.Cos(_angle);
                double sin = Math.Sin(_angle);

                for (int y = 0; y < 512; y++)
                    for (int x = 0; x < 512; x++)
                        result[x + (y << 9)] = ScreenBg[x + (y << 9)];

                Parallel.For(0, (256), y =>
                {
                    for (int x = 0; x < 256; x++)
                    {

                        uint color = Sprite[x + (y << 8)];

                        int index_dst = ((int)((x - ref_loc_x) * cos + (y - ref_loc_y) * sin + ref_loc_x + 128)) |
                        (((int)(-1.0 * (x - ref_loc_x) * sin + (y - ref_loc_y) * cos + ref_loc_y + 128)) << 9);

                        if ((color & 0xff000000) != 0) result[index_dst] = result[index_dst - 1] = color;

                    }
                });
                NativeGDI.DrawImageHighSpeedtoDevice();

                angle += 6;
                dist += 1914;
                w += 0.0166666666666667;

                if (angle >= 360)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        progressBar1.Value = dist;
                    }));
                    angle -= 360;
                }
            }

            st.Stop();

            int ws = (int)(w / ((double)st.ElapsedMilliseconds / (1000.0 * 60)));
            int speed = (int)((ws * 60 * 1.914) / 1000.0);

            Console.WriteLine("Cost : " + st.ElapsedMilliseconds + " ms");
            Console.WriteLine("RPM : " + ws);
            Console.WriteLine("Avg Speed : " + speed + " Km/H");
            Console.WriteLine("dist : " + dist / 1000.0 + " m");

            this.Invoke(new MethodInvoker(() =>
            {
                label_1.Text = "平均時速(綜合) : " + speed + "km/h";
            }));

        }

        void _run()
        {
            Stopwatch st = new Stopwatch();
            st.Restart();

            const int ref_loc_x = 128, ref_loc_y = 128;

            double angle = 0, _angle, w = 0;
            int counts = 36000, dist = 0;

            for (int i = 0; i < counts; i++)
            {

                _angle = angle * 0.01745329252;

                double cos = Math.Cos(_angle);
                double sin = Math.Sin(_angle);

                for (int y = 0; y < 512; y++)
                    for (int x = 0; x < 512; x++)
                        result[x + (y << 9)] = ScreenBg[x + (y << 9)];

                Parallel.For(0, (256), y =>
                {
                    for (int x = 0; x < 256; x++)
                    {

                        uint color = Sprite[x + (y << 8)];

                        int index_dst = ((int)((x - ref_loc_x) * cos + (y - ref_loc_y) * sin + ref_loc_x + 128)) |
                        (((int)(-1.0 * (x - ref_loc_x) * sin + (y - ref_loc_y) * cos + ref_loc_y + 128)) << 9);

                        if ((color & 0xff000000) != 0) result[index_dst] = result[index_dst - 1] = color;

                    }
                });
                //NativeGDI.DrawImageHighSpeedtoDevice();

                angle += 6;
                dist += 1914;
                w += 0.0166666666666667;

                if (angle >= 360)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        progressBar1.Value = dist;
                    }));
                    angle -= 360;
                }
            }

            st.Stop();

            int ws = (int)(w / ((double)st.ElapsedMilliseconds / (1000.0 * 60)));
            int speed = (int)((ws * 60 * 1.914) / 1000.0);

            Console.WriteLine("Cost : " + st.ElapsedMilliseconds + " ms");
            Console.WriteLine("RPM : " + ws);
            Console.WriteLine("Avg Speed : " + speed + " Km/H");
            Console.WriteLine("dist : " + dist / 1000.0 + " m");

            this.Invoke(new MethodInvoker(() =>
            {
                label_3.Text = "平均時速(綜合,無輸出) : " + speed + "km/h";
            }));

        }

    }
}
