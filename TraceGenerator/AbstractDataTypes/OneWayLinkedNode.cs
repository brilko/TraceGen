using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TraceGenerator {
    class OneWayLinkedNode<T> {
        public OneWayLinkedNode<T> Previous;
        public T Value;
        public bool IsRoot {
            get => Previous == null;
        }
        public OneWayLinkedNode(T value, OneWayLinkedNode<T> previous = null) {
            Previous = previous;
            Value = value;
        }

        public OneWayLinkedNode<T> BuildNextNode(T value) {
            return new OneWayLinkedNode<T>(value, this);
        }


    }
}
