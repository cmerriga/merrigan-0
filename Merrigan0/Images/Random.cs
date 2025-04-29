using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Merrigan0 {
    [Untested]
    public static class Random {
        private static MutableMap<Type, Func<object>> newFunctionsByTypeSoFar = new MutableMap<Type, Func<object>>();
        private static object newFunctionsByTypeSoFarByTypeLock = new object();

        [ThreadStatic]
        public static RandomGenerator Ambient = new RandomGenerator();

        static Random() {
            // Register all the type-specific functions
        }

        public static int Binomial(int n, double p) { return Ambient.Binomial(n, p); }

        public static bool Bool() { return Ambient.Bool(); }

        public static double Double() { return Ambient.Double(); }

        public static double Double(double around) { return Ambient.Double(around); }

        public static double Double(double low, double high) { return Ambient.Double(low, high); }

        public static double Exponential(double mu) { return Ambient.Exponential(mu); }

        public static int Int() { return Ambient.Int(); }

        public static int Int(int n) { return Ambient.Int(n); }

        public static long Long() { return Ambient.Long(); }

        public static long Long(long n) { return Ambient.Long(n); }

        public static int Poisson(double lambda) { return Ambient.Poisson(lambda); }

        public static void RegisterNewFunction(Type type, Func<object> newFunction) {
            lock (newFunctionsByTypeSoFarByTypeLock) {
                newFunctionsByTypeSoFar.Add(type, newFunction);
            }
        }

        public static void RegisterNewFunctions(params object[] typesAndFunctions) {
            lock (newFunctionsByTypeSoFarByTypeLock) {
                foreach (Tuple<Type, Func<object>> pair in Utilities.ToPairs<Type, Func<object>>(typesAndFunctions)) {
                    newFunctionsByTypeSoFar.Add(pair.Item1, pair.Item2);
                }
            }
        }

        public static bool TryGetNewFunction(Type type, out Func<object> newFunction) {
            return newFunctionsByTypeSoFar.Current.TryGetValue(type, out newFunction);
        }

        //// Use plain constraints instead of ConstraintAttributes
        public static object Value(
            /* [MustNot("IsAbstract")] */ Type type) ////,
            ////Array<Constraint> constraints = null) 
        {
            return Ambient.Value(type); /////, constraints);
        }

        public static T Value<T>() {
            return (T)Ambient.Value(typeof(T));
        }
    }

    [Untested]
    public class RandomGenerator {
        private static Array<Type> descendingFrequencyTypes = Array<Type>.From(
            typeof(String),
            typeof(long),
            typeof(Nullable<bool>));

        private Dictionary<Type, Func<object>> defaultNewFunctionsByType;

        private System.Random dotNetRandomGenerator;
        private MutableMap<Type, Func<object>> newFunctionsByTypeSoFar = new MutableMap<Type, Func<object>>();

        protected Dictionary<Type, Func<object>> DefaultNewFunctionsByType {
            get {
                if (defaultNewFunctionsByType == null) {
                    defaultNewFunctionsByType = GetDefaultNewFunctionsByType();
                }
                return defaultNewFunctionsByType;
            }
        }

        public RandomGenerator() { dotNetRandomGenerator = new System.Random(); }

        public RandomGenerator(int seed) { dotNetRandomGenerator = new System.Random(seed); }

        ////public object Object(

        ////public T Object<T>() {
        ////    return (T)Object(typeof(T));
        ////}

        [return: Less("n")]
        //[return: Average("n * p")]
        public int Binomial([Positive] int n, [Normal] double p) {
            // Could be majorly sped up with pre- or lazily-created ordered Monte Carlo lookups
            // Could also be hybrid so high/improbable values are shunted off to checkpoints/ranges and sequentially probed
            // with fresh calculations from there
            double goal = dotNetRandomGenerator.NextDouble();
            double q = 1.0 - p;
            double downQUpP = p / q;
            double pMix = System.Math.Pow(q, n);
            double total = pMix;
            double k = 0.0;
            while (total < goal) {
                ++k;

                // Increase the combination value
                // Example: going from (10 3) to (10 4) means multiplying by 7 and dividing by 4
                pMix *= (n - k + 1) / k;

                // Shrink one q and embiggen one p
                pMix *= downQUpP;

                total += pMix;
            }
            return (int)k;
        }

        public bool Bool() { return (dotNetRandomGenerator.Next() & 0x00000001) != 0; }

        public byte Byte() { return (byte)Int(System.Byte.MinValue, System.Byte.MaxValue); }

        public /*[Uniform(0, Char.MaxValue)] */char Char() { return (char)(32 + dotNetRandomGenerator.Next(96)); }

        public decimal Decimal() {
            return (decimal)Int();
        }

        public double Double() {
            return Normal(10.0, 10.0);
        }

        public double Double(double around) {
            return Normal(around, around * 0.1);
        }

        public double Double(double low, double high) {
            return low + dotNetRandomGenerator.NextDouble() * (high - low);
        }

        //[Profile()]
        public double Exponential(double mean) {
            // The exponentially distributed probability of an number being less than n is 1 - e^(-n/mu).
            // We choose a uniformly distributed probability p, and find where on the exponential CDF
            // that would be found.
            double p = dotNetRandomGenerator.NextDouble();
            double exponential = -System.Math.Log(1.0 - p) * mean;
            return exponential;
        }

        public float Float() { return (float)Double(); }

        public float Float(float low, float high) { return (float)Double((double)low, (double)high); }

        public float Float(float limit) { return (float)Double((float)limit); }

        public DateTime FutureTime() {
            return FutureTime(TimeSpan.FromDays(5.0));
        }

        public DateTime FutureTime(TimeSpan mean) {
            return DateTime.UtcNow + TimeSpan.FromMilliseconds(Exponential(mean.TotalMilliseconds));
        }

        public Guid Guid() { return System.Guid.NewGuid(); }

        public int Int() { return Poisson(3); }

        public int Int(int n) {
            if (n >= 0) {
                return dotNetRandomGenerator.Next(n);
            }
            return -dotNetRandomGenerator.Next(-n);
        }

        public int Int(int low, /*[Greater("low")][LessThan(0x70000000)] */int high) {
            return low + Int(high - low);
        }

        public T Item<T>(Array<T> items) {
            return items[Long(items.Length)];
        }

        public long Long() { return Poisson(3); }

        // n must be less than 2^62.
        public /*[Uniform(0, 0x4000000000000000)] */long Long(/*[LessThan(0x4000000000000000)] */long n) {
            bool negative = n < 0;
            if (negative) {
                n = -n;
            }

            // We will choose a coarse-grain index of 2^31-sized blocks. The highest-indexed block will
            // likely be incomplete, e.g. n % (2^31) > 0. In that case, the numbers in the highest-indexed
            // block will be overrepresented unless we accommodate the smaller number of values available
            // than the block size.
            //
            // The technique is:
            //      1. Choose a block index.
            //      2. If it is not the highest index, choose a value from within the 2^31 values of the block.
            //          If it is the highest index, continue below.
            //      3. Decide whether to take a value from this block, using probability (v * t) / n, where
            //          n is the exclusive max
            //          v is (n % (the block size))
            //          t is the number of blocks chosen from
            //      4. If that probability is met, choose a value from within the v numbers in that block.
            //          Otherwise continue below.
            //      5. Choose a number from (t - 1) full blocks.
            long nBlocks = ((n - 1) >> 31) + 1;
            long total = 0L;

            long iBlock = (long)((nBlocks == 0x80000000) ? dotNetRandomGenerator.Next() : dotNetRandomGenerator.Next((int)nBlocks));
            long fine;
            if (iBlock == nBlocks - 1) {
                long v = n & 0x7FFFFFFF;
                double pUseThisBlock = (double)(v * nBlocks) / (double)n;
                if (Test(pUseThisBlock)) {
                    fine = (long)dotNetRandomGenerator.Next((int)v);
                } else {
                    iBlock = (long)dotNetRandomGenerator.Next((int)(nBlocks - 1));
                    fine = (long)dotNetRandomGenerator.Next();
                }
            } else {
                fine = (long)dotNetRandomGenerator.Next();
            }
            total = (iBlock << 31) + fine;

            if (negative) {
                total = -total;
            }
            return total;
        }

        public long Long(long low, /*[Greater("low")] */long high) {
            return low + Long(high - low);
        }

        public double Normal(double mean, double standardDeviation) {
            return mean + Normal() * standardDeviation; /////
        }

        public double Normal() {
            double totalSoFar = 0.0;
            for (int c = 0; c < 100; ++c) {
                totalSoFar += dotNetRandomGenerator.NextDouble();
            }
            totalSoFar -= 50.0;
            return totalSoFar / 5.0;
        }

        public object Nullable(Type type) {
            if (Test(0.1)) {
                return null;
            } else {
                Type genericNullable = typeof(Nullable<>);
                Type specificNullable = genericNullable.MakeGenericType(type);
                object nullable = Activator.CreateInstance(specificNullable, Value(type));
                return nullable;
            }
        }

        public Nullable<T> Nullable<T>() where T : struct {
            if (Test(0.1)) {
                return null;
            } else {
                return new Nullable<T>(Value<T>());
            }
        }

        public DateTime PastTime() {
            return PastTime(TimeSpan.FromDays(5.0));
        }

        public DateTime PastTime(TimeSpan mean) {
            return DateTime.UtcNow - TimeSpan.FromMilliseconds(Exponential(mean.TotalMilliseconds));
        }

        public /*[Mode("lambda")] */int Poisson(/*[Positive] */double lambda) {
            // Could be majorly sped up with pre- or lazily-created ordered Monte Carlo lookups
            // Could also be hybrid so high/improbable values are shunted off to checkpoints/ranges and sequentially probed
            // with fresh calculations from there
            double goal = dotNetRandomGenerator.NextDouble();
            double pK = System.Math.Exp(-lambda);
            double total = pK;
            double k = 0.0;
            while (total < goal) {
                ++k;

                // Increase the constant on top
                pK *= lambda;

                // Increase the denominator
                pK /= k;

                total += pK;
            }
            return (int)k;
        }

        public void RegisterNewFunction(Type type, Func<object> newFunction) {
            newFunctionsByTypeSoFar[type] = newFunction;
        }

        public void RegisterNewFunctions(params object[] typesAndFunctions) {
            foreach (Tuple<Type, Func<object>> pair in Utilities.ToPairs<Type, Func<object>>(typesAndFunctions)) {
                newFunctionsByTypeSoFar[pair.Item1] = pair.Item2;
            }
        }

        public sbyte Sbyte() { return (sbyte)Int(System.SByte.MinValue, System.SByte.MaxValue); }

        public short Short() { return (short)Int(System.Int16.MinValue, System.Int16.MaxValue); }

        public String String() {
            int length = Poisson(5);
            char[] chars = new char[length];
            for (int i = 0; i < length; ++i) {
                chars[i] = Char();
            }
            return Merrigan0.String.From(chars);
        }

        public bool Test(/*[Probability] */ double p) {
            return dotNetRandomGenerator.NextDouble() < p;
        }

        public DateTime Time() {
            if (Bool()) {
                return PastTime();
            } else {
                return FutureTime();
            }
        }

        public bool Test(/*[LessThan("denominator")] */int numerator, /*[Positive] */int denominator) {
            return numerator <= dotNetRandomGenerator.Next(denominator);
        }

        public bool TryGetNewFunction(Type type, out Func<object> newFunction) {
            if (newFunctionsByTypeSoFar.Current.TryGetValue(type, out newFunction)) {
                return true;
            }
            return DefaultNewFunctionsByType.TryGetValue(type, out newFunction);
        }

        //public static bool TryGetNewFunction(Type type, out Func<object> newFunction) {
        //    return newFunctionsByTypeSoFar.Current.TryGetValue(type, out newFunction);
        //}

        public Type Type() {
            long i = Zipf(descendingFrequencyTypes.Length);
            return descendingFrequencyTypes[i];
        }

        public uint Uint() { return (uint)Long(System.UInt32.MinValue, System.UInt32.MaxValue); }

        public ulong Ulong() { return (ulong)Long((long)System.UInt64.MinValue, 0x4000000000000000L); }

        public ushort Ushort() { return (ushort)Int(System.UInt16.MinValue, System.UInt16.MaxValue); }

        public object Value(
            /* [MustNot("IsAbstract")] */ Type type///,
            /*Array<Constraint> constraints = null*/) {
            // If this is one with a specific creation method, use that
            // If this is a pre-coded type, use that
            Array<Expression> constraints = null; ////
            Func<object> newFunction;
            if (TryGetNewFunction(type, out newFunction)) {
                if (constraints == null || constraints.Length == 0) {
                    return newFunction();
                }
                int nTries = 0;
                while (nTries < 1000) {
                    bool acceptable = true;
                    Expression firstConstraint = constraints[0];
                    object value = newFunction();
                    Glom valueGlom = Glom.From(value);
                    foreach (Expression constraint in constraints) {
                        if (!(bool)constraint.Evaluate(valueGlom)) {
                            acceptable = false;
                            break;
                        }
                    }
                    if (acceptable) {
                        return value;
                    }
                }
                throw new Exception();
            }

            // If this is a nullable struct, get that
            Type underlyingType;
            if (Reflection.Nullable(type, out underlyingType)) {
                return Nullable(underlyingType);
            }

            // If this is a delegate, fail
            if (Reflection.Implements(type, typeof(MulticastDelegate))) {
                throw new Exception("Cannot create random delegates");
            }

            // If this is an enumerable type, create an array of random size; initialize it; and convert it
            // to the desired type
            Type itemType;
            if (Reflection.ImplementsTypedEnumerable(type, out itemType)) {
                //// problem: an AppendArray is an IEnumerable<T> but can't be converted to from a plain array
                //// solution: only do this for things initializable via an object[]
                int length = Random.Int(8) + 2;
                object[] block = new object[length];
                for (long i = 0; i < block.Length; ++i) {
                    block[i] = Random.Value(itemType);
                }
                object value;
                if (Conversion.TryConvert(block, type, out value)) {
                    return value;
                }
            }

            ////// If this type has a specific generation function, use that
            ////Func<object> generationFunction;
            ////if (DefaultNewFunctionsByType.TryGetValue(type, out generationFunction)) {
            ////    return generationFunction();
            ////}

            // Find public settable properties
            Array<PropertyInfo> settableProperties = Reflection.PublicSettableProperties(type);
            Array<FieldInfo> settableFields = Reflection.PublicSettableFields(type);

            // Set them
            ///// temporary
            ////object instance = Activator.CreateInstance(type);
            ////foreach (PropertyInfo settableProperty in settableProperties) {
            ////    object value = Value(settableProperty.PropertyType);
            ////    settableProperty.SetValue(instance, value, null);
            ////}
            ////foreach (FieldInfo settableField in settableFields) {
            ////    object value = Value(settableField.FieldType);
            ////    settableField.SetValue(instance, value);
            ////}

            // Find the constructor that sets the most properties with names similar to the public properties
            Array<string> settableNames = settableProperties.Transform(p => p.Name) + settableFields.Transform(p => p.Name);
            ConstructorInfo constructorWithMostProperties = null;
            ParameterInfo[] parameters = null;
            int nMostParametersSoFar = -1;
            foreach (ConstructorInfo constructorToTry in type.GetConstructors()) {
                ParameterInfo[] parametersToTry = constructorToTry.GetParameters();

                // We can't create a random function at this point
                bool functionParameterFound = false;
                foreach (ParameterInfo parameter in parametersToTry) {
                    if (typeof(Delegate).IsAssignableFrom(parameter.ParameterType)) {
                        functionParameterFound = true;
                        break;
                    }
                }
                if (functionParameterFound) {
                    continue;
                }

                int nParameters = parametersToTry.Length;

                if (nParameters > nMostParametersSoFar) {
                    constructorWithMostProperties = constructorToTry;
                    parameters = parametersToTry;
                    nMostParametersSoFar = nParameters;
                }
            }
            if (constructorWithMostProperties == null) {
                //// should find a derived class
                throw new NotImplementedException("No public constructor for type.");
            }

            // Cut settable fields by ones that are matchable to parameters in constructor
            settableProperties = settableProperties.Where(p => {
                string name = p.Name;
                foreach (ParameterInfo parameter in parameters) {
                    if (string.Equals(parameter.Name, name, StringComparison.OrdinalIgnoreCase)) {
                        return false;
                    }
                }
                return true;
            });
            settableFields = settableFields.Where(f => {
                string name = f.Name;
                foreach (ParameterInfo parameter in parameters) {
                    if (string.Equals(parameter.Name, name, StringComparison.OrdinalIgnoreCase)) {
                        return false;
                    }
                }
                return true;
            });

            // Call the constructor with random, legal arguments
            object[] constructorParameterValues = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; ++i) {
                constructorParameterValues[i] = Value(parameters[i].ParameterType);
            }
            object instance = Activator.CreateInstance(type, constructorParameterValues);

            // Set the settable fields and properties not probably set by the constructor
            foreach (PropertyInfo property in settableProperties) {
                property.SetValue(instance, Value(property.PropertyType), null);
            }
            foreach (FieldInfo field in settableFields) {
                field.SetValue(instance, Value(field.FieldType));
            }

            //// Get random value with the constraints found on each parameter

            return instance;
        }

        public T Value<T>() { return (T)Value(typeof(T)); }

        // Returns the index of the option chosen by a harmonic probability distribution.
        //// [Complexity("nOptions")]
        [return: NotNegative]
        public long Zipf([Positive] long nOptions) {
            // Figure out the total integer representing the random space
            long initialInterval = Math.Factorial(nOptions);
            long randomRangeSoFar = 0L;
            long iOptionToAdd = 0L;
            long interval = initialInterval;
            while (true) {
                randomRangeSoFar += interval;
                ++iOptionToAdd;
                if (iOptionToAdd >= nOptions) {
                    break;
                }
                interval = interval * iOptionToAdd / (iOptionToAdd + 1);
            }

            // Play the roulette wheel
            long target = Long(randomRangeSoFar);
            interval = initialInterval;
            long limit = interval;
            long iOptionToTry = 0L;
            while (iOptionToTry < nOptions) {
                if (target < limit) {
                    return iOptionToTry;
                }
                ++iOptionToTry;
                interval *= iOptionToTry;
                interval /= (iOptionToTry + 1);
                limit += interval;
            }
            throw new Exception();
        }

        private Dictionary<Type, Func<object>> GetDefaultNewFunctionsByType() {
            Dictionary<Type, Func<object>> newFunctionsByType = new Dictionary<Type, Func<object>>();
            newFunctionsByType.Add(typeof(bool), () => (object)Bool());
            newFunctionsByType.Add(typeof(byte), () => (object)Byte());
            newFunctionsByType.Add(typeof(char), () => (object)Char());
            newFunctionsByType.Add(typeof(decimal), () => (object)Decimal());
            newFunctionsByType.Add(typeof(double), () => (object)Double());
            newFunctionsByType.Add(typeof(float), () => (object)Float());
            newFunctionsByType.Add(typeof(Guid), () => (object)Guid());
            newFunctionsByType.Add(typeof(int), () => (object)Int());
            newFunctionsByType.Add(typeof(long), () => (object)Long());
            newFunctionsByType.Add(typeof(short), () => (object)Short());
            newFunctionsByType.Add(typeof(string), () => (object)String().ToString());
            newFunctionsByType.Add(typeof(String), () => (object)String());
            newFunctionsByType.Add(typeof(ushort), () => (object)Ushort());
            newFunctionsByType.Add(typeof(uint), () => (object)Uint());
            newFunctionsByType.Add(typeof(ulong), () => (object)Ulong());
            newFunctionsByType.Add(typeof(DateTime), () => (object)Time());
            newFunctionsByType.Add(typeof(Type), () => (object)Type());
            return newFunctionsByType;
        }
    }
}
