using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    /// <summary>
    /// Сборник специфичных для программы алгоритмов по вычислительной геометрии
    /// </summary>
    static class TrGenAlgorithms {
        /// <summary>
        /// Находит набор вершин сложного полигона, для которых отрезок между заданной точкой и 
        /// вершиной не пересекает ни одной стороны сложного полигона
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="pnt"></param>
        /// <returns></returns>
        public static IEnumerable<Point> FindVerticesToPoint(ComplexPolygon polygon, Point pnt)
            => VerticesToPointAlgorithm.FindVerticesToPoint(polygon, pnt);

        /// <summary>
        /// Находит грани polygon, которые не совпадают с гранями cmplPolyg
        /// </summary>
        /// <param name="cmplPolyg"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static IEnumerable<Segment> FindFreeEdges(
            ComplexPolygon cmplPolyg, TrGenPolygon polygon)
            => FindFreeEdgesAlgorithms.FindFreeEdges(cmplPolyg, polygon);

        /// <summary>
        /// Определяет пересекает ли отрезок хотя бы одну грань сложного полигона
        /// </summary>
        public static bool IsSegmentIntersectCompPolyg(Segment seg, ComplexPolygon cmpPolyg)
            => IsSegmentIntersectComplPolygAlgorithm.IsSegmentIntersectCompPolyg(seg, cmpPolyg);
    }
}
