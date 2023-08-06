using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace TraceGen {
    interface IPaintZoneTool {
        IEnumerable<Shape> NextPoint(Point newPnt, TaskTraseGenBuilder task);
        IEnumerable<Shape> EndShape(Point newPnt, TaskTraseGenBuilder task);
        //IEnumerable<Shape> Repaint(TaskTraseGenBuilder task);
    }
}
