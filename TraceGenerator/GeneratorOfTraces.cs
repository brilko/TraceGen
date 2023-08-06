using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TraceGenerator {
    public class GeneratorOfTraces {
        public IEnumerable<Point> GenerateTrace(TaskTraceGen task) {
            if (!TryBuildTreeWays(task, out OneWayLinkedNode<Point> lastNode))
                return Enumerable.Empty<Point>();

            return BuildTraceByTree(lastNode);
        }

        private bool TryBuildTreeWays(TaskTraceGen task, out OneWayLinkedNode<Point> lastNode) {
            if (!task.CompPolygon.IsSegmentIntersectEdjes(new Segment(task.Begin, task.End))) {
                lastNode = new OneWayLinkedNode<Point>(task.Begin);
                lastNode = lastNode.BuildNextNode(task.End);
                return true;
            }

            var dotSegFactory = new DotAndSegmentFactory(task.End);
            var head = new OneWayLinkedNode<Point>(task.Begin);
            var accessableVertices = task.CompPolygon.FindVerticesToPoint(task.Begin).ToArray();
            var polygD = Geometry.FormTrGenPolygon(accessableVertices, task.Begin);
            var deleted = new HashSet<Point>(polygD.Points);
            var edges = TrGenAlgorithms.FindFreeEdges(task.CompPolygon, polygD);
            var dots = new PriorityQueue<PrioritedDotAndSegment>(BestPriority.Minimal);
            var leafs = new Dictionary<Point, OneWayLinkedNode<Point>>();

            foreach (var seg in edges) {
                var pntOnSeg = seg.GetSquareRelationPoint(task.End);
                var dotSeg = dotSegFactory.Build(pntOnSeg, seg);
                var nextNode = head.BuildNextNode(dotSeg.Dot);
                leafs.Add(dotSeg.Dot, nextNode);
                dots.PrEnqueue(dotSeg);
            }

            while (dots.Count > 0) {
                var dotAndSeg = dots.PrDequeue();
                head = leafs[dotAndSeg.Dot];
                leafs.Remove(head.Value);

                if (!task.CompPolygon.IsSegmentIntersectEdjes(new Segment(head.Value, task.End))) {
                    lastNode = head.BuildNextNode(task.End);
                    return true;
                }
                accessableVertices = task.CompPolygon.FindVerticesToPoint(head.Value)
                    .Where((pnt) => !deleted.Contains(pnt))
                    .Append(dotAndSeg.Segment.Pnt1)
                    .Append(dotAndSeg.Segment.Pnt2)
                    .ToArray();
                if (accessableVertices.Length < 3) 
                    continue;

                polygD = Geometry.FormTrGenPolygon(accessableVertices, head.Value);
                foreach (var pnt in accessableVertices) {
                    deleted.Add(pnt);
                }
                edges = TrGenAlgorithms.FindFreeEdges(task.CompPolygon, polygD);
                foreach (var seg in edges) {
                    var pntOnSeg = seg.GetSquareRelationPoint(task.End);
                    var dotSeg = dotSegFactory.Build(pntOnSeg, seg);
                    var nextNode = head.BuildNextNode(dotSeg.Dot);
                    leafs.Add(dotSeg.Dot, nextNode);
                    dots.PrEnqueue(dotSeg);
                }
            }
            lastNode = null;
            return false;
        }

        private IEnumerable<Point> BuildTraceByTree(OneWayLinkedNode<Point> lastNode) {
            var stack = new Stack<Point>();
            stack.Push(lastNode.Value);
            while (!lastNode.IsRoot) {
                lastNode = lastNode.Previous;
                stack.Push(lastNode.Value);
            }
            return stack;
        }
    }
}
