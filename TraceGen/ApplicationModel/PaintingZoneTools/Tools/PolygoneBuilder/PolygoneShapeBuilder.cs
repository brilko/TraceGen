using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TraceGen {
    class PolygoneShapeBuilder {
        private readonly Brush colorOfEdgePolygone;
        private readonly Brush colorOfFillPolygone;

        private readonly int radiusOfEllipse;
        private readonly double strokeThick;

        public PolygoneShapeBuilder(Brush colorOfEdgePolygone, Brush colorOfFillPolygone,
                            int radiusOfEllipse = 3, double strokeThick = 2) {
            this.radiusOfEllipse = radiusOfEllipse;
            this.strokeThick = strokeThick;
            this.colorOfEdgePolygone = colorOfEdgePolygone;
            this.colorOfFillPolygone = colorOfFillPolygone;
        }

        public Polygon Polygone() {
            return new Polygon {
                Fill = colorOfFillPolygone
            };
        }

        public Ellipse Ellipse(Point point) {
            Ellipse ellipse = new Ellipse {
                Height = 2 * radiusOfEllipse,
                Width = 2 * radiusOfEllipse,
                Fill = colorOfEdgePolygone
            };
            ellipse.SetValue(Canvas.LeftProperty, point.X - radiusOfEllipse);
            ellipse.SetValue(Canvas.TopProperty, point.Y - radiusOfEllipse);
            return ellipse;
        }

        public Line Segment(Point point1, Point point2) {
            var line = new Line {
                X1 = point1.X,
                Y1 = point1.Y,
                X2 = point2.X,
                Y2 = point2.Y,
                Stroke = colorOfEdgePolygone,
                StrokeThickness = strokeThick
            };
            return line;
        }

    }
}
