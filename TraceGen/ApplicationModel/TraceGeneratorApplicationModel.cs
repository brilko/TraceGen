using System.Windows.Controls;

namespace TraceGen {
    class TraceGeneratorApplicationModel {
        public readonly PaintingZone AppPaintingZone;
        public TraceGeneratorApplicationModel(Canvas canvas) {
            AppPaintingZone = new PaintingZone(canvas);
        }
    }
}
