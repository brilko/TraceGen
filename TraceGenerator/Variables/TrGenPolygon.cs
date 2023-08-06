using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TraceGenerator {
    public class TrGenPolygon {
        public readonly Point[] Points;
        public readonly Segment[] Edges;
        public TrGenPolygon(Point[] polygon) {
            Points = polygon;
            Edges = FormEdges().ToArray();
        }

        private IEnumerable<Segment> FormEdges() {
            if (Points.Length <= 1) 
                yield break;
            if (Points.Length == 2) {
                yield return new Segment(Points[0], Points[1]);
                yield break;
            }
            for (int i = 1; i < Points.Length; i++) {
                yield return new Segment(Points[i - 1], Points[i]);
            }
            yield return new Segment(Points[Points.Length-1], Points[0]);
        }

        public static TrGenPolygon BuildPolygonByVerticesAndPoint(Point[] vertices, Point interPnt)
            => Geometry.FormTrGenPolygon(vertices, interPnt);
    }
}
