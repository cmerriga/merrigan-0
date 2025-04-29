using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Merrigan0;

namespace PixelWindowTest {
    public partial class Form1 : Form {
        private Window pixelWindow;
        private PixelGridDrawer gridDrawer;
        private System.Random random;

        public Form1() {
            random = new System.Random();
            BackColor = System.Drawing.Color.Maroon;
            InitializeComponent();
            //DoubleBuffered = true;
            gridDrawer = new CustomPixelGridDrawer(Draw);
            pixelWindow = new Window(gridDrawer);
            pixelWindow.Bounds = ClientRectangle;
            Controls.Add(pixelWindow);
            Resize += HandleResize;
        }

        private void Draw(Argb[,] pixels) {
            int h = pixels.GetLength(0);
            int w = pixels.GetLength(1);
            int hCenter = h / 2;
            int wCenter = w / 2;
            float xBrightnessStep;
            //for (int y = 0; y < h; ++y) {
            //    xBrightnessStep = -(510f / w);
            //    int maxBrightnessY = ((y - hCenter) * 510) / h;
            //    if (maxBrightnessY < 0) {
            //        maxBrightnessY = -maxBrightnessY;
            //    }
            //    float xBrightness = 255f;
            //    for (int x = 0; x < w; ++x) {
            //        if (xBrightness < 0f) {
            //            xBrightnessStep = -xBrightnessStep;
            //        }
            //        xBrightness += xBrightnessStep;
            //        // int maxBrightness = System.Math.Max((System.Math.Abs(y - hCenter) * 256 / h), (System.Math.Abs(x - wCenter) * 256 / w));
            //        int maxBrightnessX = (int)(xBrightness + .5f);
            //        byte gray = (byte)random.Next(System.Math.Max(maxBrightnessY, maxBrightnessX));
            //        //System.Drawing.Color color = System.Drawing.Color.FromArgb(gray, 255 - gray, gray);
            //        Argb a = Argb.FromRgb(gray, 0, gray);
            //        pixels[y, x] = a;
            //    }
            //}

            byte gray = (byte)random.Next(256);
            Argb a = Argb.FromRgb(gray, gray, gray);
            for (int y = 0; y < h; ++y) {
                for (int x = 0; x < w; ++x) {
                    pixels[y, x] = a;
                }
            }
        }

        // Do no background painting
        protected override void OnPaintBackground(PaintEventArgs e) { }

        private void HandleResize(object sender, EventArgs ea) {
            pixelWindow.Bounds = ClientRectangle;
        }
    }
}
