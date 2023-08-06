using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using TraceGenerator;

namespace TraceGen {
    class PolygoneBuilder : IPaintZoneTool {
        public static readonly PolygoneBuilder FillBuilder = 
            new PolygoneBuilder(Brushes.Aquamarine, Brushes.LightSteelBlue);

        public static readonly PolygoneBuilder VoidBuilder =
            new PolygoneBuilder(Brushes.Aquamarine, Brushes.Gray);

        private Polygon Polygone;
        private Point lastPoint;

        private readonly PolygoneShapeBuilder shapeBuilder;

        private PolygoneBuilder(Brush colorOfEdgePolygones, Brush colorOfFillPolygones) {
            shapeBuilder = new PolygoneShapeBuilder(colorOfEdgePolygones, colorOfFillPolygones);
        }

        private Ellipse StartPolygone(Point newPoint) {
            Polygone = shapeBuilder.Polygone();
            Polygone.Points.Add(newPoint);
            lastPoint = newPoint;
            return shapeBuilder.Ellipse(newPoint);
        }

        private IEnumerable<Shape> NextLineOfPolygone(Point newPoint) {
            yield return shapeBuilder.Ellipse(newPoint);
            yield return shapeBuilder.Segment(lastPoint, newPoint);
            lastPoint = newPoint;
            Polygone.Points.Add(newPoint);
        }

        private IEnumerable<Shape> EndPolygone() {
            yield return shapeBuilder.Segment(lastPoint, Polygone.Points.First());
            yield return Polygone;
            Polygone = null;
        }

        private bool isFirstPoint = true;
        public IEnumerable<Shape> NextPoint(Point newPnt, TaskTraseGenBuilder task = null) {
            if (isFirstPoint) {
                isFirstPoint = false;
                yield return StartPolygone(newPnt);
            } else {
                foreach (var shape in NextLineOfPolygone(newPnt)) {
                    yield return shape;
                }
            }
        }

        public IEnumerable<Shape> EndShape(Point newPnt, TaskTraseGenBuilder task = null) {
            if (!isFirstPoint) {
                if (this == FillBuilder) {
                    task?.AddNewFillPolygon(Polygone.Points);
                } else if (this == VoidBuilder) {
                    task?.AddNewVoidPolygon(Polygone.Points);
                } else {
                    throw new NotImplementedException();
                }
                isFirstPoint = true;
                return EndPolygone();
            }
            return Enumerable.Empty<Shape>();
        }

        /*public IEnumerable<Shape> Repaint(TaskTraceGen task) {
            isFirstPoint = true;
            Polygone = new Polygon();
            if (this == FillBuilder) {
                return ReformPolygones(task.CompPolygon.FillingPolygones);
            } else if (this == VoidBuilder) {
                return ReformPolygones(task.CompPolygon.VoidPolygones);
            } else {
                throw new NotImplementedException();
            }
        }

        private IEnumerable<Shape> ReformPolygones(List<TrGenPolygon> tracePolygones) {
            foreach (var trPolyg in tracePolygones) {
                foreach (var polygon in ReformPolygon(trPolyg)) {
                    yield return polygon;
                }    
            }
        }

        private IEnumerable<Shape> ReformPolygon(TrGenPolygon trPolygon) {
            foreach (var pnt in trPolygon.Points) {
                foreach (var shape in NextPoint(pnt)) { 
                    
                }
            }

        }*/
    }
}
