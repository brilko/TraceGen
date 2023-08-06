using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TraceGen {
    class PointBuilder : IPaintZoneTool {
        public static readonly PointBuilder BeginBuilder =
            new PointBuilder(Brushes.White);

        public static readonly PointBuilder EndBuilder =
            new PointBuilder(Brushes.Black);

        private readonly Brush colorOfDot;
        private readonly int radiusOfDot;

        private PointBuilder(Brush colorOfDot, int radiusOfDot = 6) {
            this.colorOfDot = colorOfDot;
            this.radiusOfDot = radiusOfDot;
        }

        private Ellipse ellipse;
        private Ellipse MakeEllipse(Point point, TaskTraseGenBuilder task) {
            ellipse = new Ellipse {
                Height = 2 * radiusOfDot,
                Width = 2 * radiusOfDot,
                Fill = colorOfDot
            };
            ChangePosition(point, task);
            return ellipse;
        }

        private void ChangePosition(Point newPosition, TaskTraseGenBuilder task) {
            if (ellipse != null) {
                ellipse.SetValue(Canvas.LeftProperty, newPosition.X - radiusOfDot);
                ellipse.SetValue(Canvas.TopProperty, newPosition.Y - radiusOfDot);
                if (this == BeginBuilder) {
                    task.RepositionBegin(newPosition);
                } else if (this == EndBuilder) {
                    task.RepositionEnd(newPosition);
                } else {
                    throw new NotImplementedException();
                }
            }
        }

        public IEnumerable<Shape> EndShape(Point newPnt, TaskTraseGenBuilder task) {
            return Enumerable.Empty<Shape>();
        }

        public IEnumerable<Shape> NextPoint(Point newPnt, TaskTraseGenBuilder task) {
            if (ellipse == null) {
                ellipse = MakeEllipse(newPnt, task);
                yield return ellipse;
            } else {
                ChangePosition(newPnt, task);
            }
        }
    }
}
