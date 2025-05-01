using System;

namespace Merrigan0 {
    /*
     * A single-point object that offers caching of common representations of an instance,
     * for example the block representation of an Array.
     * 
     * How to use:
     *      public class Square {
     *          private Extender extender;
     *          
     *          public IPolygon IPolygon {
     *              get {
     *                  IPolygon polygon = (IPolygon)extender[0];
     *                  if (polygon == null) {
     *                      polygon = new Polygon(points);
     *                      extender[0] = polygon;
     *                  }
     *                  return polygon;
     *              }
     *          }
     *      }
     */
    [Untested]
    public struct Extender {
        private Extension extension;

        public object this[int i] {
            get {
                if (extension == null) {
                    extension = new Extension();
                }
                return extension.Get(i);
            }
            set {
                if (extension == null) {
                    extension = new Extension();
                }
                extension.Set(i, value);
            }
        }
    }
}
