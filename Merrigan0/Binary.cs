using System;

namespace Merrigan0 {
    [Untested]
    public static class Binary {
        static Binary() {
            int bitsInUint = sizeof(uint) * 8;

            // Put together the bits table
            SingleBits = new uint[bitsInUint];
            for (int i = 0; i < bitsInUint; ++i) {
                SingleBits[i] = 0x00000001u << i;
            }

            // Put together the masks tables
            LowBitMasks = new uint[bitsInUint];
            for (int i = 1; i <= bitsInUint; ++i) {
                if (i == bitsInUint) {
                    LowBitMasks[i - 1] = uint.MaxValue;
                    continue;
                }

                uint u = SingleBits[i + 1];
                LowBitMasks[i - 1] = u - 1;
            }
            HighBitMasks = new uint[bitsInUint];
            for (int i = 0; i < bitsInUint; ++i) {
                HighBitMasks[i] = ~(LowBitMasks[31 - i]);
            }
        }

        public static uint[] LowBitMasks;
        public static uint[] HighBitMasks;
        public static uint[] SingleBits;

        public static int BitsSet(uint u) {
            u -= u >> 1 & 0x55555555;
            u = (u >> 2 & 0x33333333) + (u & 0x33333333);
            u = (u >> 4) + u & 0x0f0f0f0f;
            u += u >> 8;
            u += u >> 16;
            return (int)(u & 0x0000003f);
        }

        public static bool HasSingleBit(uint u) {
            return (u & (u - 1)) == 0;
        }


        [Example(0x0, 0x0)]
        [Example(0x1, 0x1)]
        [Example(0x100, 0x100)]
        [Example(0x101, 0x101)]
        [Example(0x1FF, 0x1FF)]
        [Example(0xFFFFFFFF, 0x80000000)]
        public static uint MostSignificantBit(uint u) {
            return SingleBits[Log2Floor(u)];
        }

        // Alternates available at http://aggregate.org/MAGIC. This one is probably best
        // for realistically distributed inputs
        public static int Log2Floor(uint u) {
            // Knock off easy ones before making decisions
            if (u == 0) {
                return -1;
            }
            if (u == 1) {
                return 0;
            }
            if (u < 4) {
                return 1;
            }

            // Loop through until all bits gone
            u >>= 2;
            int log2SoFar = 1;
            while (u > 0) {
                ++log2SoFar;
                u >>= 1;
            }
            return log2SoFar;
        }

        // From http://aggregate.org/MAGIC
        public static uint ReverseBits(uint u) {
            u = (((u & 0xaaaaaaaa) >> 1) | ((u & 0x55555555) << 1));
            u = (((u & 0xcccccccc) >> 2) | ((u & 0x33333333) << 2));
            u = (((u & 0xf0f0f0f0) >> 4) | ((u & 0x0f0f0f0f) << 4));
            u = (((u & 0xff00ff00) >> 8) | ((u & 0x00ff00ff) << 8));
            return((u >> 16) | (u << 16));
        }

        public static uint Smear(uint u) {
            u |= u >> 1;
            u |= u >> 2;
            u |= u >> 4;
            u |= u >> 8;
            u |= u >> 16;
            return u;
        }
    }
}
