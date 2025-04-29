using System;
using System.Collections.Generic;

namespace Merrigan0 {
    // A group that can be read destructively in order, lowest to highest.
    [Untested]
    public class Heap<T> {
        private long iNextBlankInLowestLayer = 0;

        // Array of layers, with counts 1, 2, 4, etc.
        // If iNextBlankInLowestLayer is 0, the lowest layer is NOT instantiated yet
        private List<T[]> layers = new List<T[]>();

        // Optimization
        private long length = 0;

        private Func<T, T, int> compare;

        public long Length { get { return length; } }

        public Heap(IEnumerable<T> items, Func<T, T, int> compare) :
            this(compare) {
            foreach (T item in items) {
                Add(item);
            }
        }

        public Heap(Func<T, T, int> compare) { this.compare = compare; }

        [DiagnosticOnly]
        [Test]
        public static void Test() {
            Testing.Test(typeof(Heap<int>).Name, () => {
                TestSort(new int[] { });
                TestSort(new int[] { 0 });
                TestSort(new int[] { 0, 1 });
                TestSort(new int[] { 1, 0 });
                TestSort(new int[] { 0, 1, 1 });
                TestSort(new int[] { 1, 0, 0 });
                TestSort(new int[] { 1, 4, 2, 1, 3, 1, 5 });
                TestSort(new int[] { 5, 4, 3, 2, 1, 6, 5, 4, 3, 2 });
            });
        }

        // Adds an item to an appropriate spot in the heap
        public void Add(T item) {
            // If it's time to add another layer, do so
            if (iNextBlankInLowestLayer == 0) {
                // First layer
                if (layers.Count == 0) {
                    layers.Add(new T[1]);
                } else {
                    // Subsequent layers
                    layers.Add(new T[layers[layers.Count - 1].Length * 2]);
                }
            }

            // Add the item at the end
            layers[layers.Count - 1][iNextBlankInLowestLayer] = item;

            // Bubble the item up as needed
            int iLayer = layers.Count - 1;
            long iInLayer = iNextBlankInLowestLayer;
            int iLayerAbove = iLayer - 1;
            long iInLayerAbove = iInLayer / 2;
            while (iLayerAbove >= 0) {
                T itemAbove = layers[iLayerAbove][iInLayerAbove];

                // If the s are in the right order, we are done
                bool topIsLower = compare(itemAbove, item) <= 0;
                if (topIsLower) {
                    break;
                }

                // Swap with the one above
                layers[iLayerAbove][iInLayerAbove] = item;
                layers[iLayer][iInLayer] = itemAbove;

                // Move focus up to the one we just switched
                iLayer = iLayerAbove;
                iInLayer = iInLayerAbove;

                iLayerAbove = iLayer - 1;
                iInLayerAbove = iInLayer / 2;
            }

            // Move the writing point ahead
            ++iNextBlankInLowestLayer;
            if (iNextBlankInLowestLayer >= layers[layers.Count - 1].Length)
                iNextBlankInLowestLayer = 0;
            ++length;

            Check();
        }

        [DiagnosticOnly]
        public bool Check() {
            if (length == 0) {
                return true;
            }
            return CheckTree(0, 0);
        }

        // Returns the lowest-ranked item, if any
        public bool TryGetTop(out T item) {
            if (layers.Count == 0) {
                item = default(T);
                return false;
            }
            item = layers[0][0];
            return true;
        }

        // Returns the lowest-ranked item, if any, removes it, and restores
        // the heap consistency
        public bool TryPop(out T item) {
            // If no item exists, return failed
            if (length == 0) {
                item = default(T);
                return false;
            }

            // Remember the item at the top of the heap because we're going to overwrite it
            // with the current last item
            T[] upperLayer = layers[0];
            item = upperLayer[0];

            // Retarget the last item and shrink the layers list if necessary
            T itemToInsert;
            if (iNextBlankInLowestLayer == 0) {
                iNextBlankInLowestLayer = layers[layers.Count - 1].Length - 1;
                itemToInsert = layers[layers.Count - 1][iNextBlankInLowestLayer];
            } else {
                // Target the previous
                --iNextBlankInLowestLayer;
                itemToInsert = layers[layers.Count - 1][iNextBlankInLowestLayer];
                if (iNextBlankInLowestLayer == 0) {
                    layers.RemoveAt(layers.Count - 1);
                }
            }
            --length;

            // Put the item at the top
            upperLayer[0] = itemToInsert;
            long iInUpperLayer = 0;
            int iLowerLayer = 1;
            while (iLowerLayer < layers.Count) {
                T[] lowerLayer = layers[iLowerLayer];
                long iLeftInLowerLayer = iInUpperLayer * 2;

                // If we're comparing to the lower layer, we have to obey the length limit
                bool lowerLayerIsLowest = (iLowerLayer == layers.Count - 1);

                // If there are no children, we are done
                if (lowerLayerIsLowest && (iNextBlankInLowestLayer != 0) && iLeftInLowerLayer >= iNextBlankInLowestLayer) {
                    break;
                }

                // If there is one child, the left is it
                T leftItem = lowerLayer[iLeftInLowerLayer];
                long iRightInLowerLayer = iLeftInLowerLayer + 1;
                if (lowerLayerIsLowest && (iNextBlankInLowestLayer != 0) && iRightInLowerLayer >= iNextBlankInLowestLayer) {
                    // Switch if they are out of order
                    if (compare(itemToInsert, leftItem) > 0) {
                        upperLayer[iInUpperLayer] = leftItem;
                        lowerLayer[iLeftInLowerLayer] = itemToInsert;
                    }
                    goto finish;
                }

                // Find which item is lower
                T rightItem = lowerLayer[iRightInLowerLayer];
                bool leftIsLower = compare(leftItem, rightItem) <= 0;
                long iToChangeInLowerLayer;
                T itemToPromoteFromLowerLayer;
                if (leftIsLower) {
                    // If they are already ordered, we are done
                    if (compare(itemToInsert, leftItem) <= 0) {
                        goto finish;
                    }
                    iToChangeInLowerLayer = iLeftInLowerLayer;
                    itemToPromoteFromLowerLayer = leftItem;
                } else {
                    // If they are already ordered, we are done
                    if (compare(itemToInsert, rightItem) <= 0) {
                        goto finish;
                    }
                    iToChangeInLowerLayer = iRightInLowerLayer;
                    itemToPromoteFromLowerLayer = rightItem;
                }

                // Switch
                upperLayer[iInUpperLayer] = itemToPromoteFromLowerLayer;
                lowerLayer[iToChangeInLowerLayer] = itemToInsert;

                // Move down to the next level
                upperLayer = lowerLayer;
                iInUpperLayer = iToChangeInLowerLayer;
                ++iLowerLayer;
            }

        finish:
            Check();
            return true;
        }

        protected static void TestSort(params int[] sequence) {
            Testing.Test(String.Join((Array<int>)sequence, " "), () => {
                Heap<int> heap = new Heap<int>(sequence, Comparers.CompareFunction<int>());
                Testing.TestEquals("Length", heap.Length, sequence.Length);
                int item;
                int previousItem;
                MutableArray<int> sortedSoFar = new MutableArray<int>();
                if (!heap.TryPop(out item)) {
                    return;
                }
                sortedSoFar.Append(item);
                previousItem = item;
                bool succeeded = true;
                while (heap.TryPop(out item)) {
                    sortedSoFar.Append(item);
                    if (item < previousItem) {
                        succeeded = false;
                    }
                    previousItem = item;
                }
                Testing.Test("in order", succeeded, () => sortedSoFar.Current.ToString());
            });
        }

        protected bool CheckTree(int iLayer, long iInLayer) {
            if (iLayer < layers.Count - 1) {
                T topItem = layers[iLayer][iInLayer];
                long iInLowerLayer = iInLayer * 2;
                T[] lowerLayer = layers[iLayer + 1];

                // If we're checking the bottom layer, don't go as far as iNextBlankInLowestLayer
                int nToCheck = 2;
                if (iLayer == layers.Count - 2) {
                    if (iInLowerLayer >= iNextBlankInLowestLayer) {
                        return true;
                    } else if (iInLowerLayer + 1 >= iNextBlankInLowestLayer) {
                        nToCheck = 1;
                    }
                }

                if (nToCheck >= 1) {
                    if (compare(lowerLayer[iInLowerLayer], topItem) < 0) {
                        return false;
                    }
                    if (!CheckTree(iLayer + 1, iInLowerLayer)) {
                        return false;
                    }
                }

                if (nToCheck >= 2) {
                    if (compare(lowerLayer[iInLowerLayer + 1], topItem) < 0) {
                        return false;
                    }
                    if (!CheckTree(iLayer + 1, iInLowerLayer + 1)) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
