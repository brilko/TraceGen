using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    [TestFixture]
    class GeneratorOfTracesTest {
        private static readonly ComplexPolygon cmpPolyg =
            new ComplexPolygon() {
                FillingPolygones = new List<TrGenPolygon>() {
                    new TrGenPolygon(new Point[]{
                        new Point(-6, 7),
                        new Point(-6, -6),
                        new Point(5, -6),
                        new Point(5, -3),
                        new Point(-3, -3),
                        new Point(-3, 4),
                        new Point(5, 4),
                        new Point(5, 7),
                    }),
                    new TrGenPolygon(new Point[]{
                        new Point(-1, 3),
                        new Point(2, 2),
                        new Point(2, -1),
                        new Point(-1, 1),
                    })
                },
                VoidPolygones = new List<TrGenPolygon>() {
                    new TrGenPolygon(new Point[]{
                        new Point(-1, 6),
                        new Point(-2, 5),
                        new Point(-1, 3),
                    })
                }
            };

        private static readonly Point begin = new Point(3, 5);

        private static readonly Tuple<Point, bool>[] ends =
            new Tuple<Point, bool>[] { 
                Tuple.Create(new Point(3, -4), false),
                Tuple.Create(new Point(-5, 5), false),
                Tuple.Create(new Point(1, 5), true)
            };

        private static readonly CaseForTest[] cases = 
            ends
            .Select(e => new CaseForTest() {
                Task = new TaskTraceGen() {
                    CompPolygon = cmpPolyg,
                    Begin = begin,
                    End = e.Item1
                },
                Success = e.Item2
            })
            .ToArray();

        private class CaseForTest {
            public TaskTraceGen Task;
            public bool Success;
        }

        private static readonly Point[] NoWayToPointTestPoint = new[] {
            new Point(1, 1)
        };

        private static readonly TaskTraceGen[] NoWayTasks =
            NoWayToPointTestPoint
            .Select(pnt => new TaskTraceGen() { 
                Begin = begin,
                End = pnt,
                CompPolygon = cmpPolyg
            })
            .ToArray(); 

        [Test]
        public void TestPrimitiveUnit() {
            GeneratorOfTraces trGen = new GeneratorOfTraces();
            foreach (var cas in cases) {
                var actual = trGen.GenerateTrace(cas.Task).ToArray();
                if (cas.Success) {
                    Assert.AreEqual(2, actual.Length);
                    Assert.AreEqual(cas.Task.Begin, actual[0]);
                    Assert.AreEqual(cas.Task.End, actual[1]);
                } else {
                    Assert.AreEqual(cas.Task.Begin, actual[0]);
                    Assert.AreEqual(cas.Task.End, actual[actual.Length - 1]);
                    TestTrekInCmpl(cas.Task.CompPolygon, actual);
                }
            }
            foreach (var noWayCase in NoWayTasks) {
                var actual = trGen.GenerateTrace(noWayCase).ToArray();
                Assert.IsTrue(actual.Length == 0);
            }    

        }

        private void TestTrekInCmpl(ComplexPolygon cmpl, Point[] trek) {
            for (int i = 1; i < trek.Length; i++) {
                var seg = new Segment(trek[i - 1], trek[i]);
                Assert.IsFalse(cmpl.IsSegmentIntersectEdjes(seg));
            }
        }




    }
}
