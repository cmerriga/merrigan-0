using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    public static class Halftoning {
        public static unsafe void BresenhamHalftone(byte* pData, int h, int w, int stride) {
            // Set up start values
            //Random random = new Random();
            int[] startValues = new int[256];
            int current = 24;
            for (int i = 0; i < 256; ++i) {
                startValues[i] = // random.Next(256);
                current;
                current += 157;
                if (current > 255) {
                    current -= 256;
                }
            }

            // Create a tracking array for line above
            int[] lineAbove = new int[w];
            for (int i = 0; i < w; ++i) {
                lineAbove[i] = startValues[i % 256];
            }

            byte* pRow = pData;
            for (int y = 0; y < h; ++y) {
                byte* pPixel = pRow;

                // Okay, new row. Let's get a start value
                int accumulatedWhiteness = startValues[y % 256];

                for (int x = 0; x < w; ++x) {
                    int whiteness = (pPixel[0] + pPixel[1] + pPixel[2]) / 3;
                    accumulatedWhiteness += whiteness;
                    int verticalAccumulatedWhiteness = lineAbove[x] + whiteness;
                    if (accumulatedWhiteness + verticalAccumulatedWhiteness > 511) {
                        //if (accumulatedWhiteness >= 256) {
                        accumulatedWhiteness -= 256;
                        verticalAccumulatedWhiteness -= 256;
                        pPixel[0] = 255; // blue
                        pPixel[1] = 255; // green
                        pPixel[2] = 255; // red
                    } else {
                        pPixel[0] = 0; // blue
                        pPixel[1] = 0; // green
                        pPixel[2] = 0; // red
                    }
                    lineAbove[x] = verticalAccumulatedWhiteness;

                    pPixel[3] = 255; // alpha
                    pPixel += 4;
                }
                pRow += stride;
            }
        }

        public static unsafe void FloydSteinbergHalftone(byte* pData, int h, int w, int stride) {
            // Set up start values
            //Random random = new Random();
            int[] startValues = new int[256];
            int current = 24;
            for (int i = 0; i < 256; ++i) {
                startValues[i] = // random.Next(256);
                current;
                current += 157;
                if (current > 255) {
                    current -= 256;
                }
            }

            // Create a tracking array for line above
            int[] lineAbove = new int[w];
            for (int i = 0; i < w; ++i) {
                lineAbove[i] = startValues[i % 256];
            }

            byte* pRow = pData;
            for (int y = 0; y < h; ++y) {
                byte* pPixel = pRow;

                // Okay, new row. Let's get a start value
                int accumulatedWhiteness = startValues[y % 256];

                for (int x = 0; x < w; ++x) {
                    int whiteness = (pPixel[0] + pPixel[1] + pPixel[2]) / 3;
                    accumulatedWhiteness += whiteness;
                    int verticalAccumulatedWhiteness = lineAbove[x] + whiteness;
                    if (accumulatedWhiteness + verticalAccumulatedWhiteness > 511) {
                        //if (accumulatedWhiteness >= 256) {
                        accumulatedWhiteness -= 256;
                        verticalAccumulatedWhiteness -= 256;
                        pPixel[0] = 255; // blue
                        pPixel[1] = 255; // green
                        pPixel[2] = 255; // red
                    } else {
                        pPixel[0] = 0; // blue
                        pPixel[1] = 0; // green
                        pPixel[2] = 0; // red
                    }
                    lineAbove[x] = verticalAccumulatedWhiteness;

                    pPixel[3] = 255; // alpha
                    pPixel += 4;
                }
                pRow += stride;
            }
        }

        public static unsafe void BresenhamHalftoneWithAccumulation(byte* pData, int h, int w, int stride) {
            // Set up start values
            //Random random = new Random();
            int[] startValues = new int[256];
            int current = 24;
            for (int i = 0; i < 256; ++i) {
                startValues[i] = // random.Next(256);
                current;
                current += 157;
                if (current > 255) {
                    current -= 256;
                }
            }

            // Create a tracking array for line above
            int[] lineAbove = new int[w];
            for (int i = 0; i < w; ++i) {
                lineAbove[w - i - 1] = startValues[i % 256];
            }

            byte* pRow = pData;
            for (int y = 0; y < h; ++y) {
                byte* pPixel = pRow;

                // Okay, new row. Let's get a start value
                int accumulatedWhiteness = startValues[y % 256];

                for (int x = 0; x < w; ++x) {
                    int whiteness = (pPixel[0] + pPixel[1] + pPixel[2]) / 3;
                    accumulatedWhiteness += whiteness;
                    int verticalAccumulatedWhiteness = lineAbove[x] + whiteness;
                    if (accumulatedWhiteness + verticalAccumulatedWhiteness > 511) {
                        accumulatedWhiteness -= 256;
                        verticalAccumulatedWhiteness -= 256;
                        pPixel[0] = 255; // blue
                        pPixel[1] = 255; // green
                        pPixel[2] = 255; // red
                    } else {
                        pPixel[0] = 0; // blue
                        pPixel[1] = 0; // green
                        pPixel[2] = 0; // red

                        int accumulation = accumulatedWhiteness + verticalAccumulatedWhiteness;
                        if (accumulation < 0) {
                            pPixel[2] = (byte)-accumulation;
                        } else {
                            pPixel[1] = (byte)accumulation;
                        }
                    }
                    lineAbove[x] = verticalAccumulatedWhiteness;

                    pPixel[3] = 255; // alpha
                    pPixel += 4;
                }
                pRow += stride;
            }
        }

        public static unsafe void BresenhamHalftoneColor(byte* pData, int h, int w, int stride) {
            // Set up start values
            //Random random = new Random();
            int[] startValues = new int[256];
            int current = 24;
            for (int i = 0; i < 256; ++i) {
                startValues[i] = // random.Next(256);
                current;
                current += 157;
                if (current > 255) {
                    current -= 256;
                }
            }

            // Create start values for each color
            int[] blueStartValues = new int[w];
            for (int i = 0; i < w; ++i) {
                blueStartValues[i] = startValues[i % 256];
            }

            int[] greenStartValues = new int[w];
            for (int i = 0; i < w; ++i) {
                greenStartValues[i] = startValues[(i + 1) % 256];
            }

            int[] redStartValues = new int[w];
            for (int i = 0; i < w; ++i) {
                redStartValues[i] = startValues[(i + 2) % 256];
            }


            // Create a tracking array for line above
            int[] lineAboveBlue = new int[w];
            for (int i = 0; i < w; ++i) {
                lineAboveBlue[i] = startValues[i % 256];
            }

            int[] lineAboveGreen = new int[w];
            for (int i = 0; i < w; ++i) {
                lineAboveGreen[i] = startValues[(i + 1) % 256];
            }

            int[] lineAboveRed = new int[w];
            for (int i = 0; i < w; ++i) {
                lineAboveRed[i] = startValues[(i + 2) % 256];
            }


            byte* pRow = pData;
            for (int y = 0; y < h; ++y) {
                byte* pPixel = pRow;

                // Okay, new row. Let's get a start value
                int accumulatedBlueness = blueStartValues[y % 256];
                int accumulatedGreenness = greenStartValues[y % 256];
                int accumulatedRedness = redStartValues[y % 256];

                for (int x = 0; x < w; ++x) {
                    int blueness = pPixel[0];
                    int greenness = pPixel[1];
                    int redness = pPixel[2];
                    accumulatedBlueness += blueness;
                    accumulatedGreenness += greenness;
                    accumulatedRedness += redness;
                    int verticalAccumulatedBlueness = lineAboveBlue[x] + blueness;
                    int verticalAccumulatedGreenness = lineAboveGreen[x] + greenness;
                    int verticalAccumulatedRedness = lineAboveRed[x] + redness;
                    if (accumulatedBlueness + verticalAccumulatedBlueness >= 512) {
                        accumulatedBlueness -= 256;
                        verticalAccumulatedBlueness -= 256;
                        pPixel[0] = 255; // blue
                    } else {
                        pPixel[0] = 0; // blue
                    }
                    if (accumulatedGreenness + verticalAccumulatedGreenness >= 512) {
                        accumulatedGreenness -= 256;
                        verticalAccumulatedGreenness -= 256;
                        pPixel[1] = 255; // green
                    } else {
                        pPixel[1] = 0; // green
                    }
                    if (accumulatedRedness + verticalAccumulatedRedness >= 512) {
                        accumulatedRedness -= 256;
                        verticalAccumulatedRedness -= 256;
                        pPixel[2] = 255; // red
                    } else {
                        pPixel[2] = 0; // red
                    }
                    lineAboveBlue[x] = verticalAccumulatedBlueness;
                    lineAboveGreen[x] = verticalAccumulatedGreenness;
                    lineAboveRed[x] = verticalAccumulatedRedness;

                    pPixel[3] = 255; // alpha
                    pPixel += 4;
                }
                pRow += stride;
            }
        }

        public static unsafe void BresenhamHalftone45(byte* pData, int h, int w, int stride) {
            // Set up start values
            //Random random = new Random();
            int[] startValues = new int[256];
            int current = 24;
            for (int i = 0; i < 256; ++i) {
                startValues[i] = // random.Next(256);
                current;
                current += 157;
                if (current > 255) {
                    current -= 256;
                }
            }

            // Create a tracking array for line above
            int[] lineAbove = new int[w];
            for (int i = 0; i < w; ++i) {
                lineAbove[i] = startValues[i % 256];
            }

            int[] lineAboveBefore = new int[w];
            for (int i = 0; i < w; ++i) {
                lineAboveBefore[i] = startValues[(i + 1) % 256];
            }


            byte* pRow = pData;
            for (int y = 0; y < h; ++y) {
                byte* pPixel = pRow;

                // Okay, new row. Let's get a start value
                int accumulatedWhiteness = startValues[y % 256];

                for (int x = 0; x < w; ++x) {
                    int whiteness = (pPixel[0] + pPixel[1] + pPixel[2]) / 3;
                    accumulatedWhiteness += whiteness;
                    int verticalAccumulatedWhiteness = lineAbove[x] + whiteness;
                    int aboveLeftAccumulatedWhiteness = lineAboveBefore[x] + whiteness;
                    if (accumulatedWhiteness + verticalAccumulatedWhiteness + aboveLeftAccumulatedWhiteness > 767) {
                        //if (accumulatedWhiteness >= 256) {
                        accumulatedWhiteness -= 256;
                        verticalAccumulatedWhiteness -= 256;
                        aboveLeftAccumulatedWhiteness -= 256;
                        pPixel[0] = 255; // blue
                        pPixel[1] = 255; // green
                        pPixel[2] = 255; // red
                    } else {
                        pPixel[0] = 0; // blue
                        pPixel[1] = 0; // green
                        pPixel[2] = 0; // red
                    }
                    lineAbove[x] = verticalAccumulatedWhiteness;
                    if (x < w - 1) {
                        lineAboveBefore[x + 1] = aboveLeftAccumulatedWhiteness;
                    }

                    pPixel[3] = 255; // alpha
                    pPixel += 4;
                }
                pRow += stride;
            }
        }

        public static unsafe void RandomHalftone(byte* pData, int h, int w, int stride) {
            System.Random random = new System.Random();
            byte* pRow = pData;
            for (int y = 0; y < h; ++y) {
                byte* pPixel = pRow;
                for (int x = 0; x < w; ++x) {
                    int whiteness = (pPixel[0] + pPixel[1] + pPixel[2]) / 3;
                    if (random.Next(256) <= whiteness) {
                        pPixel[0] = 255; // blue
                        pPixel[1] = 255; // green
                        pPixel[2] = 255; // red
                    } else {
                        pPixel[0] = 0; // blue
                        pPixel[1] = 0; // green
                        pPixel[2] = 0; // red
                    }

                    pPixel[3] = 255; // alpha
                    pPixel += 4;
                }
                pRow += stride;
            }
        }
    }
}
