using System;
using System.Collections.Generic;

namespace Merrigan0.StringsInternal {
    //// When you've got a string but want to view it as text.
    //[Untested]
    //public class StringText : Merrigan0.Internal.Text {
    //    private String s;
    //    private long iConvertedToLinesSoFar;
    //    private MutableArray<String> linesSoFar = new MutableArray<String>();

    //    public override long Length { get { return linesSoFar.Current.Length; } }

    //    public StringText(String s) {
    //        this.s = s;
    //    }

    //    protected override string GetItem(long i) {
    //        MutableArray<String> linesReadSoFar = new MutableArray<String>();
    //        if (i < linesSoFar.Current.Length) {
    //            return linesSoFar.Current[i];
    //        }

    //        long linesToRead = i - linesSoFar.Current.Length;
    //        while (linesToRead > 0) {
    //            String line;
    //            if (!s.TryReadLine(iConvertedToLinesSoFar, out line, out iConvertedToLinesSoFar)) {
    //                throw new IndexOutOfRangeException();
    //            }
    //            linesReadSoFar.Append(line);
    //            --linesToRead;
    //        }
    //        linesSoFar.Append(linesReadSoFar.Current);
    //        return linesSoFar.Current[i];
    //    }
    //}
}
