using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceGenerator {
    #region PriorityQueueSuplay
    interface IPriorited {
        int TakePriority();
    }

    public enum BestPriority {
        Minimal,
        Maximal
    }
    #endregion
    class PriorityQueue<T> where T : IPriorited {
        private readonly Dictionary<int, Queue<T>> dictOfPriorities = new Dictionary<int, Queue<T>>();
        private readonly SortedSet<int> sortedKeys = new SortedSet<int>();

        private int count = 0;
        public int Count {
            get {
                return count;
            }
            private set {
                if (value < 0)
                    throw new InvalidOperationException("No one element in priority queue");
                count = value;
            }
        }

        BestPriority BestPriorityOfQueue { get; }

        public PriorityQueue(BestPriority bestPriorityOfQueue) {
            BestPriorityOfQueue = bestPriorityOfQueue;
        }

        #region Enqueue
        /// <summary>
        /// Добавление элемента в очередь с приоритетом
        /// </summary>
        /// <param name="element"></param>
        public void PrEnqueue(T element) {
            int priority = element.TakePriority();
            sortedKeys.Add(priority);
            InsertToDict(priority, element);
            Count++;
        }

        private void InsertToDict(int priority, T element) {
            if (dictOfPriorities.ContainsKey(priority)) {
                dictOfPriorities[priority].Enqueue(element);
            } else {
                var newQueueWithElement = new Queue<T>();
                newQueueWithElement.Enqueue(element);
                dictOfPriorities.Add(priority, newQueueWithElement);
            }
        }
        #endregion

        #region Dequeue
        /// <summary>
        /// Получить следующий элемент из очереди с приоритетом
        /// </summary>
        /// <returns></returns>
        public T PrDequeue() {
            int bestPriority = TakeBestPriority();
            var bestQueue = dictOfPriorities[bestPriority];
            var bestEl = bestQueue.Dequeue();
            CleanQueueIfVoid(bestPriority, bestQueue);
            Count--;
            return bestEl;
        }

        private int TakeBestPriority() {
            if (BestPriorityOfQueue == BestPriority.Maximal)
                return sortedKeys.Max;
            else
                return sortedKeys.Min;
        }

        private void CleanQueueIfVoid(int priority, Queue<T> queue) {
            if (queue.Count > 0)
                return;
            sortedKeys.Remove(priority);
            dictOfPriorities.Remove(priority);
        }
        #endregion
    }
}
