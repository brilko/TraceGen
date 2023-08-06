using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    [TestFixture]
    class VariablesTests {
        public class TrGenPolygonTest {
            [Test]
            public void EdgeTest() {
                var polyg = new TrGenPolygon(new Point[] {
                    new Point(0, 0),
                    new Point(0, 1),
                    new Point(1, 1),
                    new Point(1, 0)
                });
                var segs = new Segment[] {
                    new Segment(new Point(0, 0), new Point(0, 1)),
                    new Segment(new Point(0, 1), new Point(1, 1)),
                    new Segment(new Point(1, 1), new Point(1, 0)),
                    new Segment(new Point(1, 0), new Point(0, 0)),
                };

                Assert.AreEqual(polyg.Edges.Length, segs.Length);
                for (int i = 0; i < segs.Length; i++) {
                    Assert.AreEqual(polyg.Edges[i], segs[i]);
                }

                polyg = new TrGenPolygon(new Point[] {
                    new Point(0, 0),
                    new Point(0, 1)
                });

                segs = new Segment[] {
                    new Segment(new Point(0, 0), new Point(0, 1))
                };

                Assert.AreEqual(polyg.Edges.Length, segs.Length);
                for (int i = 0; i < segs.Length; i++) {
                    Assert.AreEqual(polyg.Edges[i], segs[i]);
                }

                polyg = new TrGenPolygon(new Point[] {
                    new Point(0, 0)
                });

                segs = new Segment[] { };

                Assert.AreEqual(polyg.Edges.Length, segs.Length);
                for (int i = 0; i < segs.Length; i++) {
                    Assert.AreEqual(polyg.Edges[i], segs[i]);
                }
            }
        }
    }
}
