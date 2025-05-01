### Merrigan-0
*C# library of common utilities for use on personal projects, implementing a few unusual patterns*

#### Lazy, and event-sourced.
All collections (arrays, maps, sets, strings) are representations of a single operation.

For instance, given an array of one billion items, inserting a new item at index 123456789, is an O(1) action of creating a new instance of the `InsertItemArray` class. That new instance represents the whole 1,000,000,001-member collection as an array, using a pointer to the previous array, the new item, and the index where it was inserted, using the previous array to look up most of the items requested and adjusting queries for indexes above 123456789 by subtracting one from them.

Classes like `AppendArray`, `RemoveArray`, `ConcatenateArray`, and `ReverseArray` are created for most mutations of an array that might be reasonably expected. But the long list of these classes are hidden, and only the abstract parent class `Array` is available publicly. All `Array`s are immutable, allowing the opportunity for safe sharing among threads. As with the other abstract classes `Set`, `Map`, and `String`, a mutable version of each is publicly available: `MutableArray`, `MutableSet`, etc. Each allow for all expected methods like `Append()`, `Insert()`, and `Remove()`, which simply update an internal current value to an instance of an operation or event class like `AppendArray`. A `MutableArray` is not an `Array` though. To access items in the current condition of the `MutableArray`, you access the `Current` member which is the state of the `MutableArray` at the time you accessed it.

Representing collections this way allows for fast mutations on large objects, the ability to share data without copying, easy storage and retrieval as log-storage, easy tracing of the history of the entity, easy undo, and easy application of updates via log shipping. It comes at a severe performance penalty as collections get large. It is up to the internal classes to perform optimizations, which they do *not* already do, other than for the length of the collections.

#### Function-level testability and commitments via C# attributes.
Annotating a function like so, both defines the contract of what is expected of each input and output, but also some examples to demonstrate expected behavior for unusual cases.

```csharp
      [Example("(1, 2, 3)", 4, "(1, 2, 3, 4)")]
      [return: Equals("Length", "a.Length + 1")]
      public static Array<T> operator +(Array<T> a, T item) { return new AppendArray<T>(a, item); }
```

The library supplies test functions that automatically use this meta information to test the function. With thorough annotation of each input and output, all meaningful unit tests of such a function are created with zero further effort, and XML Doc comments can be forgone completely in favor of information that is available at runtime. This comes at some cost of string space in the built DLL.

Complex expressions can be made to define these restrictions via a syntax called `NML` (for No Markup Language), and presumes a `Context` of embedded specifications, the nearest of which is the return value, then the call arguments, then the instance the method call is made on (if any). In the above example, the `Equals` function takes NML strings that refer to "Length" but also "a.Length". "Length" would be resolved to the closest specification, namely the return value. "a.Length" would fail to be resolved against the return value, so would be tried against the call arguments, resulting in the Length value of argument `a`.

#### Other niceties.
The NML syntax is just one syntax that can be made easily via C# declaration, such as a list of binary operators defined with numeric precedence below. Other custom syntaxes that resolve to a set of expression objects can be defined quickly.

```csharp
      new HierarchicalRecognizer.Layer("assignment", "=", 1, false, (rt1, rt2) => new ContextAppendExpression((Expression)rt1.Data, (Expression)rt2.Data)),
      new HierarchicalRecognizer.Layer("logical or", "||", 3, true, (rt1, rt2) => new LogicalOrExpression((Expression)rt1.Data, (Expression)rt2.Data)),
      new HierarchicalRecognizer.Layer("logical and", "&&", 4, true, (rt1, rt2) => new LogicalAndExpression((Expression)rt1.Data, (Expression)rt2.Data)),
      new HierarchicalRecognizer.Layer("bitwise or", "|", 5, true, (rt1, rt2) => new BitwiseOrExpression((Expression)rt1.Data, (Expression)rt2.Data)),
      new HierarchicalRecognizer.Layer("bitwise xor", "^", 6, false, (rt1, rt2) => new BitwiseXorExpression((Expression)rt1.Data, (Expression)rt2.Data)),
```

Similarly, text `Recognizers` can be combined to make customer regex-like parsing of text, for example, `Dumbex` which is a regular expression-like syntax "for the rest of us" (under construction in the current iteration).

An `Executions` namespace with facilities to execute everything defined by `Expression`s, synchronously or in parallel, in predictably-short chunks.

`Glom`s, short for "agglomerations", which are catch-all objects for any mish-mash of anything: lists, named values, and/or an entire context created from a lineage of parents such as the one used in testing results of a function call.
