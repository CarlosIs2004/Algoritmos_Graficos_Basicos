using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Algortino_de_lineas
{
    internal class Relleno
    {
        public PointF startPoint;
        public float radius;
        public float centerX;
        public float centerY;
        public PointF pointPoligon;
        public const float SF = 10 / 2f;
        public Bitmap bufferBitmap;
        public Graphics bufferGraphics;
        public PictureBox canvas;
        public SolidBrush fadeBrush;
        public List<PointF> points;
        public List<PointF> poinstPoligon;
        public PointF point;
        public Color color;
        
        public Relleno(PictureBox picCanvas, PointF startpoint)
        {
            canvas = picCanvas;
            this.startPoint = startpoint; 
            this.points = new List<PointF>();
            this.bufferBitmap = new Bitmap(canvas.Width, canvas.Height);
            
            this.bufferGraphics = Graphics.FromImage(bufferBitmap);
            this.point = new PointF();
            this.centerX = bufferBitmap.Width/2;
            this.centerY = bufferBitmap.Height / 2;
        }

        public void paintPoligon(int vertices) {
            

            fadeBrush = new SolidBrush(Color.FromArgb(120, 0, 0, 255));
            polygon(vertices, 6, fadeBrush);
            mi_floodfill(centerX, centerX, fadeBrush);
            

        }

        public void mi_floodfill(float x, float y, SolidBrush fadeBrush) {
            color = bufferBitmap.GetPixel((int)x, (int)y);
            Color colorComp = Color.FromArgb(0, 0, 0, 0);
            if (color.ToArgb() == colorComp.ToArgb()) {
                bufferGraphics.FillRectangle(fadeBrush, x,(-y), 3, 3);
                //mi_floodfill(x, y+1, fadeBrush);
                //mi_floodfill(x+1, y, fadeBrush);
                //mi_floodfill(x, y-1, fadeBrush);
                //mi_floodfill(x+1, y, fadeBrush);

            }
        
        
        }

        public void polygon( int nVertice, float radius, SolidBrush  fadeBrush) {
            radius = radius * SF;
            pointPoligon = new PointF();
            poinstPoligon = new List<PointF>();
            float angle = 0;
            float radianes;
            PointF[] pointsPoligonArr;
            for (int i = 0; i < nVertice; i++)
            {

                angle += (360 / nVertice);
                radianes = angle * ((float)Math.PI / 180);
                pointPoligon.X = centerX + (radius) * (float)(Math.Cos(radianes));
                pointPoligon.Y = centerY + (radius) * (float)(Math.Sin(radianes));
                poinstPoligon.Add(pointPoligon);
            }
            pointsPoligonArr = new PointF[(int)poinstPoligon.Count];

            for (int i = 0; i < poinstPoligon.Count; i++)
            {
                pointsPoligonArr[i] = poinstPoligon[i];
            }
            Pen mPen = new Pen(fadeBrush.Color, 3);
            bufferGraphics.DrawPolygon(mPen, pointsPoligonArr);


        }
    }
}
