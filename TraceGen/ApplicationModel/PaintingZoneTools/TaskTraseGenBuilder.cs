using System.Linq;
using System.Windows;
using System.Windows.Media;
using TraceGenerator;

namespace TraceGen {
    class TaskTraseGenBuilder {
        private readonly TaskTraceGen task = new TaskTraceGen();

        public void AddNewVoidPolygon(PointCollection pointsOfPolygon) {
            if(TryBuildPolygon(pointsOfPolygon, out TrGenPolygon polygon))
                task.CompPolygon.VoidPolygones.Add(polygon);
        }

        public void AddNewFillPolygon(PointCollection pointsOfPolygon) {
            if (TryBuildPolygon(pointsOfPolygon, out TrGenPolygon polygon))
                task.CompPolygon.FillingPolygones.Add(polygon);
        }

        private bool TryBuildPolygon(PointCollection pointsOfPolygon, out TrGenPolygon polygon) {
            polygon = new TrGenPolygon(pointsOfPolygon.ToArray());
            return polygon.Points.Length >= 2;
        }

        public void RepositionBegin(Point newPosit) {
            task.Begin = newPosit;
        }

        public void RepositionEnd(Point newPosit) {
            task.End = newPosit;
        }

        public TaskTraceGen Form()
            => task;
    }
}
