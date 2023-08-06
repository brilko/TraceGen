using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    public struct Vector {
        System.Windows.Vector winVector;
        public double X { get => winVector.X; set => winVector.X = value; }
        public double Y { get => winVector.Y; set => winVector.Y = value; }
        public Vector(Point firstPnt, Point secondPnt) {
            var x = secondPnt.X - firstPnt.X;
            var y = secondPnt.Y - firstPnt.Y;
            winVector = new System.Windows.Vector(x, y);
        }

        public Vector(double x, double y) {
            winVector = new System.Windows.Vector(x, y);
        }

        public void Recalc(Point firstPnt, Point secondPnt) {
            winVector.X = secondPnt.X - firstPnt.X;
            winVector.Y = secondPnt.Y - firstPnt.Y;
        }

        public double CrossProduct(Vector vect) {
            return System.Windows.Vector.CrossProduct(winVector, vect.winVector);
        }

        public double ScalarProduct(Vector vect)
            => X * vect.X + Y * vect.Y;

        public double AngleBetween(Vector vect) {
            return System.Windows.Vector.AngleBetween(winVector, vect.winVector);
        }

        public void Negate()
            => winVector.Negate();

        public override string ToString() {
            return winVector.ToString();
        }

        public System.Windows.Vector ToWinVector() {
            return winVector;
        }
    }
}
