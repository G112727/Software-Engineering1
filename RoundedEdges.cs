using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Software_Engineering1
{

    public class RoundedPanel : Panel
    {
        public int BorderRadius { get; set; } = 20;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Set smoothing mode for better rounded edges
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create the rounded rectangle path
            using (GraphicsPath path = new GraphicsPath())
            {
                float radius = BorderRadius;
                RectangleF rect = new RectangleF(0, 0, this.Width, this.Height);

                // Define rounded corners
                path.StartFigure();
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                // Set the region for rounded corners
                this.Region = new Region(path);

                // Optional: Draw border for rounded panel
                using (Pen pen = new Pen(Color.Black, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
