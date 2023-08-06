using System.Collections.Generic;
using System.Windows;

namespace TraceGenerator {
    /// <summary>
    /// Сборник элементарных алгоритмов по вычислительной геометрии
    /// </summary>
    internal static class Geometry {
        public static bool IsIntersect(Segment segA, Segment segB)
            => IsSegmentIntersectAlgorithm.IsIntersect(segA, segB);
        
        /// <summary>
        /// Создаёт из набора вершин и точки внутри выпуклого полигона полигон.
        /// Вершины могут идти в любом порядке
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static TrGenPolygon FormTrGenPolygon(Point[] vertices, Point interPnt)
            => FormTrGenPolygonAlgorithm.FormPolygon(vertices, interPnt);
    }
}
