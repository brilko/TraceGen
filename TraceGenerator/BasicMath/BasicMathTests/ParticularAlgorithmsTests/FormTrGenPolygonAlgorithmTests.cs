using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    [TestFixture]
    class FormTrGenPolygonAlgorithmTests {
        private static readonly Point interim = new Point(2, 2);
        private static readonly Point[] vertices =
            new Point[] {
                new Point(2, -4),
                new Point(7, 3),
                new Point(-4, -1),
                new Point(4, 6),
                new Point(5, -7),
                new Point(7, -6),
                new Point(8, -2),
                new Point(1, -8),
                new Point(-1, -4),
                new Point(-3, 4)
            };
        private static readonly TrGenPolygon expectedPolyg =
            new TrGenPolygon(new Point[] {
                new Point(7, 3),
                new Point(4, 6),
                new Point(-3, 4),
                new Point(-4, -1),
                new Point(-1, -4),
                new Point(1, -8),
                new Point(2, -4),
                new Point(5, -7),
                new Point(7, -6),
                new Point(8, -2)
            });

        [Test]
        public void TestForming() {
            var actual = Geometry.FormTrGenPolygon(vertices, interim);
            Assert.AreEqual(expectedPolyg.Points.Length, actual.Points.Length);
            for (int i = 0; i < actual.Points.Length; i++) {
                Assert.AreEqual(actual.Points[i], expectedPolyg.Points[i]);
            }
        }
    }
}
