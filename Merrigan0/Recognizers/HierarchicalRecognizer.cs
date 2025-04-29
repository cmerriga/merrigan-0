using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.RecognizersInternal {
    /*
     * <island:string> := <begin-string><sequence><end-string>
     * <island:embedded> := (<sequence>)
     * <island:bracketed> := [<sequence>]
     * <sequence> := <unit>(<separator><unit>)*
     * <unit> := <prefix><unit>|<number>|<string-literal>|<known-value>|<identifier>
     **/
    public class HierarchicalRecognizer : Recognizer {
        protected Layer sequenceLayer;
        protected Array<IslandRecognizer> islandRecognizers;
        protected Array<Layer> layers;
        protected Array<Recognizer> greedyUnitRecognizers;
        protected Array<Recognizer> minimalUnitRecognizers;
        protected Array<Prefix> prefixes;
        protected Array<Island> islands;
        protected Recognizer sequenceRecognizer;

        public HierarchicalRecognizer(
            String name,
            Array<Layer> layers,

            [Note("Should not be in the layers array.")]
            Layer whiteSpaceSeparatedLayer,

            [WhatItIs("Recognizers of things that are treated as units, regardless of any separators embedded in them, such as numbers with decimals, in order of decreasing specificity, for instance keyword recognizer before identifier recognizer.")]
            Array<Recognizer> greedyUnitRecognizers,

            [WhatItIs("Value, and identifier recognizers, which stop as soon as a separator is found, in order of decreasing specificity, for instance keyword recognizer before identifier recognizer.")]
            Array<Recognizer> minimalUnitRecognizers,

            [WhatItIs("For example, unary operators like - or !.")]
            Array<Prefix> prefixes,

            [Note("Recognizers for these islands should not be included in the mostSpecificTokenRecognizers array.")]
            [Note("A island to represent the begin and end string should be included if it is warranted.")]
            Array<Island> islands) :
            base(name)
        {
            islandRecognizers = islands.Transform(i => new IslandRecognizer(i, this));
            this.layers = layers;
            this.greedyUnitRecognizers = greedyUnitRecognizers;
            this.minimalUnitRecognizers = minimalUnitRecognizers;
            this.prefixes = prefixes;
            this.islands = islands;
            this.sequenceLayer = whiteSpaceSeparatedLayer;

            RegisterNamedRecognizers(islandRecognizers.Cast<Recognizer>());
        }

        public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
            IslandRecognizer topRecognizer = new IslandRecognizer(
                new Island(
                    "top",
                    BeginRecognizer.Only,
                    new SequenceWithEndRecognizer(
                        "sequence-with-string-end", 
                        this,
                        EndRecognizer.Only),
                    EndRecognizer.Only,
                    rt => rt.Children[1].Data),
                this);
            bool recognized = topRecognizer.TryRead(s, ref i, out tree);
            if (!recognized) {
                tree = null;
                return false;
            }
            return true;
        }

        public class Island {
            public Recognizer BeginRecognizer { get; private set; }
            public Recognizer ContentWithEndRecognizer { get; private set; }
            public Recognizer EndRecognizer { get; private set; }
            public String Name { get; private set; }
            public Func<RecognitionTree, object> TransformDataFunction { get; private set; }

            public Island(String name, char chBegin, String recognizerName, char chEnd, Func<RecognitionTree, object> transformDataFunction) :
                this(
                    name,
                    new SpecificCharacterRecognizer(chBegin),
                    new NamedRecognizer(recognizerName),
                    new SpecificCharacterRecognizer(chEnd),
                    transformDataFunction)
            {
            }

            public Island(String name, Recognizer beginRecognizer, Recognizer contentWithEndRecognizer, Recognizer endRecognizer, Func<RecognitionTree, object> transformDataFunction) {
                BeginRecognizer = beginRecognizer;
                ContentWithEndRecognizer = contentWithEndRecognizer;
                EndRecognizer = endRecognizer;
                Name = name;
                TransformDataFunction = transformDataFunction;
            }
        }

        public class Layer {
            [MayBeNull(When = "There is no special recognizer for the first instance encountered in a layer.")]
            public Recognizer InitialRecognizer { get; private set; }

            [WhatItIs("whether the layer collects an expression a la a comma-separated list or series of pluses")]
            public bool Multiterm { get; private set; }

            public String Name { get; private set; }

            [WhatItIs("a relative value that powers a place in the order of operations: a higher value means a tighter adhesion")]
            public int OrderOfOperationsRank { get; private set; }

            public Recognizer Recognizer { get; private set; }
            public Recognizer SeparatorRecognizer { get; private set; }

            ////[WhatItIs("a function that takes two RecognitionTrees and creates an object")]
            ////public Func<RecognitionTree, RecognitionTree, object> TransformFunction { get; private set; }

            public Layer(
                String name,
                String separator,
                int orderOfOperationsRank,
                bool multiterm,
                Func<RecognitionTree, RecognitionTree, object> transformDataFunction,
                Func<RecognitionTree, RecognitionTree, object> initialTransformFunction = null)
                : this(
                    name,
                    new SpecificStringRecognizer(separator),
                    //new Recognizers.SequenceRecognizer(Recognizer.WellKnownRecognizer("optional-white-space"), new StringRecognizer(separator), Recognizer.WellKnownRecognizer("optional-white-space")),
                    orderOfOperationsRank,
                    multiterm,
                    transformDataFunction,
                    initialTransformFunction) 
            {
            }

            public Layer(
                String name,
                Recognizer separatorRecognizer,
                int orderOfOperationsRank,
                bool multiterm,
                Func<RecognitionTree, RecognitionTree, object> transformFunction)
                : this(
                    name,
                    separatorRecognizer,
                    orderOfOperationsRank,
                    multiterm,
                    transformFunction,
                    null) {
            }

            public Layer(
                String name,
                Recognizer separatorRecognizer,
                int orderOfOperationsRank,
                bool multiterm,
                Func<RecognitionTree, RecognitionTree, object> transformFunction,
                Func<RecognitionTree, RecognitionTree, object> initialTransformFunction)
            {
                Multiterm = multiterm;
                Name = name;
                OrderOfOperationsRank = orderOfOperationsRank;
                Recognizer = new LayerRecognizer(Name, transformFunction);
                SeparatorRecognizer = separatorRecognizer;
                if (initialTransformFunction != null) {
                    InitialRecognizer = new LayerRecognizer(Name + " (initial)", initialTransformFunction);
                }
                ////TransformFunction = transformFunction;
            }

            public override string ToString() {
                return Name;
            }
        }

        public class Prefix {
            public String Name { get; private set; }
            public Recognizer Recognizer { get; private set; }
            public Func<object, object> TransformFunction { get; private set; }

            public Prefix(String name, String prefix, Func<object, object> transformFunction) : this(name, new SpecificStringRecognizer(prefix), transformFunction) { }

            public Prefix(String name, Recognizer recognizer, Func<object, object> transformFunction) {
                Name = name;
                Recognizer = recognizer;
                TransformFunction = transformFunction;
            }
        }

        [WhatItIs("A dummy recognizer just to host a GetData function.")]
        protected class LayerRecognizer : Recognizer {
            public LayerRecognizer(String name, Func<RecognitionTree, RecognitionTree, object> transformTreesFunction) :
                base(name, Array<Recognizer>.Empty, rt => transformTreesFunction(rt.Children[0], rt.Children[2])) {
            }

            public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
                throw new NotSupportedException();
            }
        }

        // Basically the entire NML recognizer but able to seek a custom end
        protected class SequenceWithEndRecognizer : Recognizer {
            private HierarchicalRecognizer hierarchicalRecognizer;
            private UnitRecognizer unitRecognizer;

            public SequenceWithEndRecognizer(String name, HierarchicalRecognizer hierarchicalRecognizer, Recognizer endRecognizer) :
                base(name)
            {
                this.hierarchicalRecognizer = hierarchicalRecognizer;
                //Recognizer endWithSpaceRecognizer = new SequenceRecognizer(Recognizer.WellKnownRecognizer("optional-white-space"), endRecognizer);
                this.unitRecognizer = new UnitRecognizer(hierarchicalRecognizer, endRecognizer); //// endWithSpaceRecognizer);
                parent = hierarchicalRecognizer;
            }

            public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
                long iToTry = i;
                String.SkipIntralineWhiteSpace(s, ref iToTry);

                // Find left tree. This is always the first in the sequence
                RecognitionTree leftTree;
                RecognitionTree separatorTree;
                Layer nextLayerSeen;
                if (!unitRecognizer.TryRecognize(s, ref iToTry, out leftTree, out separatorTree, out nextLayerSeen)) {
                    //leftTree = new RecognitionTree(null, s, iToTry, 0, CurrentContextExpression.Only);
                    //separatorTree = new RecognitionTree(null, s, iToTry, 0); // dummy
                    goto fail;
                }

                // If no separator tree, it's done
                if (separatorTree == null) {
                    tree = leftTree;
                    i = iToTry;
                    return true;
                }

                // The layer may have a special procedure to be done on the initial unit
                ////// actually probably not done here

                // Use the left tree and the layer from the separator to get the rest. There should be
                // no separator returned, since it should be the end of the sequence
                RecognitionTree dummySeparatorTree;
                Layer dummyLayer;
                if (!TryRecognizeLayer(s, ref iToTry, nextLayerSeen, leftTree, out tree, out dummySeparatorTree, out dummyLayer)) {
                    goto fail;
                }

                // tree should already contain everything we promise to return
                i = iToTry;
                return true;

            fail:
                tree = null;
                return false;
            }

            /*
             * Tries to read all the units it can that are at parent layer.
             * After succeeding, returns the already parsed next separator seen, and the layer it indicates.
             */
            protected bool TryRecognizeLayer(
                String s,
                ref long i,
                [WhatItIs("the already-known layer whose left tree and separator have already been parsed")] Layer layer,
                RecognitionTree leftTree,
                [WhatItIs("")] out RecognitionTree tree,
                [MayBeNull(When = "end of island is reached instead of a separator")] out RecognitionTree separatorTreeSeen,
                [MayBeNull(When = "end of island is reached instead of a separator")] out Layer nextLayerSeen) {
                // Big meat
                long iToTry = i;

                // The current layer is the one after whose separator we expect the next right tree to be the right term of. It
                // changes as this function loops through 
                Layer currentLayer = layer;

                Recognizer recognizerForCurrentLayer = currentLayer.Recognizer;
                if (currentLayer.Multiterm && currentLayer.InitialRecognizer != null) {
                    recognizerForCurrentLayer = currentLayer.InitialRecognizer;
                }

                // All white space after or before a prefixed unit must be glossed over
                String.SkipIntralineWhiteSpace(s, ref iToTry);

                while (true) {
                    // Find out if the left tree was the first in its layer, or if it itself was a binary expression created via the 
                    // same layer
                    ////bool firstInLayer = false;
                    ////LayerRecognizer leftTreeLayerRecognizer = leftTree.Recognizer as LayerRecognizer;
                    ////if (leftTreeLayerRecognizer != null) {
                    ////    if (!object.ReferenceEquals(leftTreeLayerRecognizer, currentLayer)) {
                    ////        firstInLayer = true;
                    ////    }
                    ////}
                    ////Recognizer recognizerForCurrentLayer = currentLayer.Recognizer;
                    ////if (firstInLayer && currentLayer.InitialRecognizer != null) {
                    ////    recognizerForCurrentLayer = currentLayer.InitialRecognizer;
                    ////}

                    // First thing should be the right unit of the layer that was started already with leftTree
                    RecognitionTree rightTree;
                    if (!unitRecognizer.TryRecognize(s, ref iToTry, out rightTree, out separatorTreeSeen, out nextLayerSeen)) {
                        // There needs to have been a right unit because we had already seen the separator
                        goto fail;
                    }

                    // If there was a right tree but no subsequent separator tree found, that means it was the end of the current island
                    if (separatorTreeSeen == null) {
                        // Combine it with the left tree we were given, or last constructed, and return that with no
                        // new separator tree or layer
                        tree = new RecognitionTree(
                            recognizerForCurrentLayer, // with GetData that supplies the binary expression that combines the two
                            s,
                            leftTree.Index,
                            iToTry - leftTree.Index,
                            new RecognitionTree[] {
                                leftTree,
                                null,
                                rightTree
                            });
                        i = iToTry;
                        return true;
                    }

                    //// Don't we need to check if this layer is now TOO high?
                    ////if (nextLayerSeen.OrderOfOperationsRank <= layer.OrderOfOperationsRank) {
                    ////    tree = new RecognitionTree(
                    ////        recognizerForCurrentLayer, //// currentLayer.Recognizer, // with GetData that supplies the binary expression that combines the two
                    ////        s,
                    ////        leftTree.Index,
                    ////        iToTry - leftTree.Index,
                    ////        leftTree,
                    ////        separatorTreeSeen,
                    ////        rightTree);
                    ////    return true;
                    ////}

                    // A separator was found. That means either:
                    //      - we have found a higher layer, and therefore the returned tree will become the new left tree, or
                    //      - we have found an equal layer, and therefore the returned tree will become the new left tree, or
                    //      - we have found a lower layer, and therefore we should find a new right tree wherein the left child
                    //        of the right tree will be this unit we just found
                    bool nextLayerSeenHigherOrEqual = nextLayerSeen.OrderOfOperationsRank <= currentLayer.OrderOfOperationsRank;
                    if (nextLayerSeenHigherOrEqual) {
                        ////// If the layer seen is the same as the current one, and it is multi-term, just extend the current tree
                        ////if (object.ReferenceEquals(nextLayerSeen, currentLayer) && currentLayer.Multiterm) {
                        ////    LayerRecognizer leftLayerRecognizer
                        ////    if (leftTree.Recognizer
                        ////}

                        ////// If the layer seen is the same as the current one, see if the left one is itself the first one. If so, use
                        ////// the first transform of the layer if one exists
                        ////if (object.ReferenceEquals(nextLayerSeen, currentLayer)) {
                        ////    LayerRecognizer leftLayerRecognizer
                        ////    if (leftTree.Recognizer
                        ////}

                        // Like we just ran into + but had been on * or +. Combine left and right trees and move on using the new
                        // layer
                        leftTree = new RecognitionTree(
                            recognizerForCurrentLayer, //// currentLayer.Recognizer, // with GetData that supplies the binary expression that combines the two
                            s,
                            leftTree.Index,
                            iToTry - leftTree.Index,
                            leftTree,
                            separatorTreeSeen,
                            rightTree);
                        i = iToTry;
                        //currentLayer = nextLayerSeen;
                    } else {
                        // Like we just ran into * but had been on +. Recurse back into this to find a better right tree, which
                        // begins with using the currently-found right tree as its left tree
                        if (!TryRecognizeLayer(s, ref iToTry, nextLayerSeen, rightTree, out rightTree, out separatorTreeSeen, out nextLayerSeen)) {
                            goto fail;
                        }

                        if (separatorTreeSeen == null) {
                            tree = new RecognitionTree(
                                recognizerForCurrentLayer, ////currentLayer.Recognizer, // with GetData that supplies the binary expression that combines the two
                                s,
                                leftTree.Index,
                                iToTry - leftTree.Index,
                                new RecognitionTree[] {
                                leftTree,
                                null,
                                rightTree
                            });
                            i = iToTry;
                            return true;
                        }
                    }

                    recognizerForCurrentLayer = nextLayerSeen.Recognizer;
                    if (!object.ReferenceEquals(nextLayerSeen, currentLayer)) {
                        if (nextLayerSeen.Multiterm && nextLayerSeen.InitialRecognizer != null) {
                            recognizerForCurrentLayer = nextLayerSeen.InitialRecognizer;
                        }
                    }
                    currentLayer = nextLayerSeen;
                }

            fail:
                tree = null;
                separatorTreeSeen = null;
                nextLayerSeen = null;
                return false;
            }
        }

        [WhatItIs("A recognizer that allows reading of any existing recognizer, but terminating where an " +
            "end is recognized.")]
        [Note("Scans the string twice. Not good for long strings.")]
        protected class QuickEndSeekingRecognizer : Recognizer {
            private Recognizer endRecognizer;
            private Recognizer contentRecognizer;

            public QuickEndSeekingRecognizer(Recognizer contentRecognizer, Recognizer endRecognizer) {
                this.contentRecognizer = contentRecognizer;
                this.endRecognizer = endRecognizer;
            }

            public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
                long iToTry = i;
                long length = s.Length;
                long iEnd;
                while (iToTry < length) {
                    iEnd = iToTry;
                    if (endRecognizer.TryRead(s, ref iEnd)) {
                        iToTry = i;
                        if (!contentRecognizer.TryRead(s, ref iToTry, out tree)) {
                            goto fail;
                        }
                        i = iToTry;
                        return true;
                    }
                    ++iToTry;
                }

            fail:
                tree = null;
                return false;
            }
        }

        //// should be SequenceRecognizer? EmbeddedRecognizer?
        protected class IslandRecognizer : Recognizer {
            private Island island;
            private HierarchicalRecognizer hierarchicalRecognizer;
            ////private UnitRecognizer unitRecognizer;

            [Note("The Name property will be set to the name of the island.")]
            public IslandRecognizer(Island island, HierarchicalRecognizer hierarchicalRecognizer)
                : base(island.Name) {
                this.island = island;
                this.hierarchicalRecognizer = hierarchicalRecognizer;
                ////this.unitRecognizer = new UnitRecognizer(hierarchicalRecognizer, island.EndRecognizer);
                parent = hierarchicalRecognizer;
                //// SetParent(hierarchicalRecognizer);
            }

            public override object GetData(RecognitionTree tree) {
                // Return the data from the contents recognition
                return tree.Children[1].Data;
            }

            [WhatItDoes("Recognizes the beginning, a full sequence, and the end.")]
            [Note("Does not recognize a separator afterward.")]
            [Note("Will not skip leading white space.")]
            public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
                long iToTry = i;

                RecognitionTree beginTree;
                if (!island.BeginRecognizer.TryRead(s, ref iToTry, out beginTree)) { goto fail; }

                RecognitionTree sequenceTree;
                String.SkipIntralineWhiteSpace(s, ref iToTry);
                if (!island.ContentWithEndRecognizer.TryRead(s, ref iToTry, out sequenceTree)) { goto fail; }

                String.SkipIntralineWhiteSpace(s, ref iToTry);
                RecognitionTree endTree;
                if (!island.EndRecognizer.TryRead(s, ref iToTry, out endTree)) { goto fail; }

                tree = new RecognitionTree(this, s, i, iToTry - i, beginTree, sequenceTree, endTree);
                i = iToTry;
                return true;

            fail:
                tree = null;
                return false;
            }
        }

        protected abstract class UnitAndSeparatorRecognizer : Recognizer {
            public UnitAndSeparatorRecognizer(String name) : this(name, null) { }
            public UnitAndSeparatorRecognizer(String name, Func<RecognitionTree, object> getDataFunction) : base(name, null, getDataFunction) { }

            public abstract bool TryRecognize(String s, ref long i, out RecognitionTree tree, out RecognitionTree separatorTree, out Layer layer);

            public override bool TryRead(String s, ref long i, out RecognitionTree tree) {
                throw new NotSupportedException();
            }
        }

        [WhatItIs("Any prefixed value or value: <prefixed-value>, <island>, <number>, <string>, <bool>, <identifier>")]
        [Note("Not meant to be a base class for all units, but instead, a recognizer that recognizes any possible units.")]
        [Note("Recognizes the next unit that comes before either a separator or the end of the current island.")]
        protected class UnitRecognizer : UnitAndSeparatorRecognizer {
            private Recognizer islandEndRecognizer;
            private HierarchicalRecognizer hierarchicalRecognizer;
            //private Array<Recognizer> mostSpecificUnitRecognizers;
            private Array<PrefixedUnitRecognizer> prefixedUnitRecognizers;

            public UnitRecognizer(HierarchicalRecognizer hierarchicalRecognizer, Recognizer islandEndRecognizer) :
                base("unit") 
            {
                this.islandEndRecognizer = islandEndRecognizer;
                this.hierarchicalRecognizer = hierarchicalRecognizer;
                this.prefixedUnitRecognizers = hierarchicalRecognizer.prefixes.Transform(p => new PrefixedUnitRecognizer(p, hierarchicalRecognizer, this));
                parent = hierarchicalRecognizer;
                ///// SetParent(hierarchicalRecognizer);
            }

            // If separatorTree is null, that means it was the end of this island
            // Separator tree's data will be a Layer
            public override bool TryRecognize(
                String s, 
                [Note("Updated to be just after the separator tree (without trailing white space")] ref long i,
                out RecognitionTree tree, 
                out RecognitionTree separatorTree, 
                out Layer layer)
            {
                long iToTry = i;
                String.SkipIntralineWhiteSpace(s, ref iToTry);

                // Try to recognize an island
                foreach (IslandRecognizer islandRecognizer in hierarchicalRecognizer.islandRecognizers) {
                    // The island is recognized as a unit and not with a separator //// change this
                    if (islandRecognizer.TryRead(s, ref iToTry, out tree)) {
                        TryRecognizeSeparator(s, ref iToTry, out separatorTree, out layer); //// maybe it should always be accepted?
                        i = iToTry;
                        return true;
                    }
                }

                // Try to recognize a prefixed unit
                foreach (PrefixedUnitRecognizer prefixedUnitRecognizer in prefixedUnitRecognizers) {
                    if (prefixedUnitRecognizer.TryRecognize(s, ref iToTry, out tree, out separatorTree, out layer)) {
                        i = iToTry;
                        return true;
                    }
                }

                // Try all the unit recognizers
                foreach (Recognizer unitRecognizer in hierarchicalRecognizer.greedyUnitRecognizers) {
                    if (unitRecognizer.TryRead(s, ref iToTry, out tree)) {
                        TryRecognizeSeparator(s, ref iToTry, out separatorTree, out layer);
                        i = iToTry;
                        return true;
                    }
                }

                // Before going to the whole-string recognizers, get the characters up to the next separator
                String token;
                if (!TryReadToken(s, ref iToTry, out token, out separatorTree, out layer)) {
                    tree = null;
                    return false;
                }

                // Try all the token recognizers
                foreach (Recognizer tokenRecognizer in hierarchicalRecognizer.minimalUnitRecognizers) {
                    if (tokenRecognizer.TryRecognize(token, out tree)) {
                        i = iToTry;
                        if (separatorTree != null) {
                            i += separatorTree.Length;
                        }
                        return true;
                    }
                }

                tree = null;
                return false;
            }

            [return: Note("false when no characters are found before a separator or end of string")]
            protected bool TryReadToken(String s, ref long i, out String token, out RecognitionTree separatorTree, out Layer layerFound) {
                long iToTry = i;
                long length = s.Length;
                separatorTree = null;
                layerFound = null;
                while (true) {
                    if (iToTry >= length) {
                        break;
                    }
                    long iSeparator = iToTry;
                    if (TryRecognizeSeparator(s, ref iSeparator, out separatorTree, out layerFound)) { break; }
                    ++iToTry;
                }

                // String was 0 length
                if (iToTry == i) {
                    token = null;
                    separatorTree = null;
                    layerFound = null;
                    return false;
                }

                // Regular case
                token = s.Substring(i, iToTry - i);
                i = iToTry;
                return true;
            }

            [Note("Will skip any white space at the beginning. Separator tree will include the leading white space plus the separator, " +
                "but not the trailing white space.")]
            protected bool TryRecognizeSeparator(String s, ref long i, out RecognitionTree separatorTree, out Layer layerFound) {
                // If there is any white space here, then keep track of where to look
                long iFirstNonwhiteSpace = i;
                String.SkipIntralineWhiteSpace(s, ref iFirstNonwhiteSpace);
                bool foundWhiteSpace = iFirstNonwhiteSpace > i;

                // See about island end. No separator or layer will be returned
                long iToTry = iFirstNonwhiteSpace;
                if (islandEndRecognizer.TryRead(s, ref iToTry)) {
                    separatorTree = null;
                    layerFound = null;
                    ////i = iToTry;
                    return true;
                }

                // See about island begins that left no white space
                foreach (Island island in hierarchicalRecognizer.islands) {
                    RecognitionTree islandBeginTree;
                    if (island.BeginRecognizer.TryRead(s, ref iToTry, out islandBeginTree)) {
                        separatorTree = new RecognitionTree(this, s, iToTry, 0, null);
                        layerFound = hierarchicalRecognizer.sequenceLayer;
                        return true;
                    }
                }

                // See about regular separators. Longest one is taken
                Layer longestSeparatorLayer = null;
                long longestSeparatorLength = -1;
                RecognitionTree longestSeparatorTree = null;
                foreach (Layer layer in hierarchicalRecognizer.layers) {
                    RecognitionTree layerSeparatorTree;
                    long iSeparatorBegin = iToTry;
                    if (layer.SeparatorRecognizer.TryRead(s, ref iSeparatorBegin, out layerSeparatorTree)) {
                        // Separator tree's length should be the length of only the separator--no white-space
                        if (layerSeparatorTree.Length > longestSeparatorLength) {
                            longestSeparatorTree = layerSeparatorTree;
                            longestSeparatorLayer = layer;
                            longestSeparatorLength = layerSeparatorTree.Length;
                        }
                    }
                }
                if (longestSeparatorLayer != null) {
                    separatorTree = new RecognitionTree(longestSeparatorLayer.SeparatorRecognizer, s, i, iFirstNonwhiteSpace - i + longestSeparatorTree.Length);
                    //separatorTree = longestSeparatorTree;
                    layerFound = longestSeparatorLayer;
                    i = iToTry + longestSeparatorLength;
                    return true;
                } else if (foundWhiteSpace) {
                    // Separator was the white space we skipped at the beginning
                    separatorTree = new RecognitionTree(hierarchicalRecognizer.sequenceLayer.SeparatorRecognizer, s, i, iFirstNonwhiteSpace - i);
                    layerFound = hierarchicalRecognizer.sequenceLayer;
                    i = iFirstNonwhiteSpace;
                    return true;
                }

                separatorTree = null;
                layerFound = null;
                return false;
            }
        }

        protected class PrefixedUnitRecognizer : UnitAndSeparatorRecognizer {
            private Prefix prefix;

            [Note("Contains any info about separators and island ends to terminate recognition at.")]
            private UnitRecognizer unitRecognizer;

            public PrefixedUnitRecognizer(Prefix prefix, HierarchicalRecognizer hierarchicalRecognizer, UnitRecognizer unitRecognizer) : 
                base(prefix.Name) 
            {
                this.prefix = prefix;
                this.unitRecognizer = unitRecognizer;
                parent = hierarchicalRecognizer;
            }

            public override object GetData(RecognitionTree tree) {
                // Return the transformed version of the data in the unit tree (2nd child)
                return prefix.TransformFunction(tree.Children[1].Data);
            }

            // Checks for prefix and unit.
            public override bool TryRecognize(
                String s,
                ref long i,
                [Note("Data will be an Expression with the unit value transformed according to the prefix.")]
                [Note("The tree will have two children: the prefix tree and the unit tree. The unit tree will contain the following separator tree if any.")]
                out RecognitionTree tree,
                out RecognitionTree separatorTree,
                out Layer layer) 
            {
                long iToTry = i;
                RecognitionTree prefixTree;
                if (!prefix.Recognizer.TryRead(s, ref iToTry, out prefixTree)) { goto fail; }
                RecognitionTree unitTree;
                if (!unitRecognizer.TryRecognize(s, ref iToTry, out unitTree, out separatorTree, out layer)) { goto fail; }
                long lengthWithSeparator = iToTry - i;
                if (separatorTree != null) {
                    lengthWithSeparator += separatorTree.Length;
                }
                tree = new RecognitionTree(this, s, i, iToTry - i, prefixTree, unitTree);
                i = iToTry;
                return true;

            fail:
                tree = null;
                separatorTree = null;
                layer = null;
                return false;
            }
        }
    }
}
