using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    [TestFixture]
    class PointExtensionsTests {
        [TestCase(0, 0, 1, 1, 2)]
        [TestCase(1, 2.3, 6, 3, 25.49)]
        public void SquareDistanceTest(double x1, double y1, double x2, double y2, double expected) {
            var pnt1 = new Point(x1, y1);
            var pnt2 = new Point(x2, y2);
            var actual = pnt1.GetSquareOfRange(pnt2);
            Assert.AreEqual(expected, actual, 0.001);
        }
    }
}
