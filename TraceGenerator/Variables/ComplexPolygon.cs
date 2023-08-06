using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TraceGenerator {
    public class ComplexPolygon {
        public List<TrGenPolygon> FillingPolygones = new List<TrGenPolygon>();
        public List<TrGenPolygon> VoidPolygones = new List<TrGenPolygon>();
        public IEnumerable<Segment> GetAllEdges() {
            foreach (var polyg in FillingPolygones)
                foreach (var edge in polyg.Edges)
                    yield return edge;
            foreach (var polyg in VoidPolygones)
                foreach (var edge in polyg.Edges)
                    yield return edge;
        }

        /// <summary>
        /// Определяет пересекает ли отрезок хотя бы одну грань сложного полигона
        /// </summary>
        public bool IsSegmentIntersectEdjes(Segment seg) {
            return TrGenAlgorithms.IsSegmentIntersectCompPolyg(seg, this);
        }

        /// <summary>
        /// Находит набор вершин сложного полигона, для которых отрезок между заданной точкой и 
        /// вершиной не пересекает ни одной стороны сложного полигона
        /// </summary>
        public IEnumerable<Point> FindVerticesToPoint(Point pnt) {
            return TrGenAlgorithms.FindVerticesToPoint(this, pnt);
        }

    }
}
//TODO: Реализовать возможность объединения включающих и исключающих многоугольников 
//TODO: Сделать так, чтобы ComplexPolygon добавлял вершины на месте точек пересечений сторон многоугольников