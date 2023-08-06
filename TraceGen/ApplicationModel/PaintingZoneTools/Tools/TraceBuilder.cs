using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TraceGen {
    class TraceBuilder {
        public static readonly TraceBuilder StdTraceBuilder = new TraceBuilder(Brushes.Beige);

        private readonly PolygoneShapeBuilder shapeBuilder;

        private TraceBuilder(Brush colorOfWay) {
            shapeBuilder = new PolygoneShapeBuilder(colorOfWay, colorOfWay, 2);
        }

        public IEnumerable<Shape> BuildWay(IEnumerable<Point> wayPnt) {
            Point lastPoint = wayPnt.FirstOrDefault();
            foreach (var pnt in wayPnt) {
                yield return shapeBuilder.Ellipse(pnt);
                yield return shapeBuilder.Segment(pnt, lastPoint);
                lastPoint = pnt;
            }
            yield return shapeBuilder.Ellipse(lastPoint);
        }
    }
}
