//namespace Merrigan0 {
//    public class ChainAdapter : Adapter {
//        public Adapter BaseAdapter { get; private set; }
//        public Adapter LastAdapter { get; private set; }
//        public int Length { get; private set; }

//        public ChainAdapter(Adapter baseAdapter, Adapter lastAdapter) :
//            base(baseAdapter.FromType, lastAdapter.ToType) {
//            BaseAdapter = baseAdapter;
//            LastAdapter = lastAdapter;

//            ChainAdapter baseChainAdapter = BaseAdapter as ChainAdapter;
//            if (baseChainAdapter != null) {
//                Length = baseChainAdapter.Length + 1;
//            } else {
//                Length = 2;
//            }
//        }

//        public override object To(object value) {
//            return LastAdapter.To(BaseAdapter.To(value));
//        }
//    }
//}
