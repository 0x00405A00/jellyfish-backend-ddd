using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Windows.Input;

/* Nicht gemergte Änderung aus Projekt "MobileApp (net7.0-ios)"
Vor:
using Microsoft.Maui.Controls.Shapes;
Nach:
using Microsoft.Maui.Controls.Shapes;
using MobileApp;
using MobileApp.ControlExtension;
*/
using Microsoft.Maui.Controls.Shapes;

namespace MobileApp.ControlExtension
{
    public class GraphicsDrawableExtension : BindableObject, IDrawable
    {
        public Microsoft.Maui.Controls.Shapes.Path Path
        {
            get
            {
                var val = (Microsoft.Maui.Controls.Shapes.Path)GetValue(PathProperty);
                return val;
            }
            set
            {
                SetValue(PathProperty, value);
            }
        }


        public static readonly BindableProperty PathProperty =
                BindableProperty.Create("Path", typeof(Microsoft.Maui.Controls.Shapes.Path), typeof(GraphicsDrawableExtension), null, BindingMode.TwoWay, null);

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 6;
            PathF pathF = new PathF();
            /*Microsoft.Maui.Controls.Shapes.PathGeometry pathGeometry = new PathGeometry();
            pathGeometry. = Microsoft.Maui.Controls.Shapes.PathGeometry("M 10 10 L 90 10 L 50 90 Z");

            // Erstellen eines PathF-Objekts aus dem PathGeometry-Objekt
            
            foreach (var figure in pathGeometry.Figures)
            {
                PathFigureF figureF = new PathFigureF();
                foreach (var segment in figure.Segments)
                {
                    if (segment is LineSegment lineSegment)
                    {
                        figureF.Segments.Add(new LineSegmentF(new PointF(lineSegment.Point.X, lineSegment.Point.Y)));
                    }
                    // Weitere Segmente wie CubicBezierSegment oder QuadraticBezierSegment können hier hinzugefügt werden
                }
                figureF.IsClosed = figure.IsClosed;
                figureF.StartPoint = new PointF(figure.StartPoint.X, figure.StartPoint.Y);
                pathF.Figures.Add(figureF);
            }*/
            canvas.DrawPath(pathF);
        }
    }
}
