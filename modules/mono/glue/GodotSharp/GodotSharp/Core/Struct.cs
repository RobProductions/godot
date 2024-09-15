using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot.NativeInterop;
using System.Diagnostics;

#nullable enable

namespace Godot.Collections
{
    public sealed class StructInfo
    {

    }
    /// <summary>
    /// Wrapper around Godot's Struct class, a collection of Variant
    /// typed elements allocated in the engine in C++. Useful when
    /// interfacing with the engine or exporting data to the editor.
    /// Otherwise prefer .NET collections or use
    /// built-in C# structs or classes."/>.
    /// </summary>
    [DebuggerTypeProxy(typeof(ArrayDebugView<Variant>))]
    [DebuggerDisplay("Count = {Count}")]
#pragma warning disable CA1710 // Identifiers should have correct suffix
    public sealed class Struct :
#pragma warning restore CA1710
        IList<Variant>,
        IReadOnlyList<Variant>,
        ICollection,
        IDisposable
    {
        internal godot_struct.movable NativeValue;

        private WeakReference<IDisposable>? _weakReferenceToSelf;

        /// <summary>
        /// Constructs a new empty <see cref="Struct"/>.
        /// </summary>
        public Struct()
        {
            NativeValue = (godot_struct.movable)NativeFuncs.godotsharp_struct_new();
            _weakReferenceToSelf = DisposablesTracker.RegisterDisposable(this);
        }

        /// <summary>
        /// Constructs a new <see cref="Struct"/> from the given collection's elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="collection"/> is <see langword="null"/>.
        /// </exception>
        /// <param name="collection">The collection of elements to construct from.</param>
        /// <returns>A new Godot Struct.</returns>
        public Struct(IEnumerable<Variant> collection) : this()
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (Variant element in collection)
                Add(element);
        }

        /// <summary>
        /// Constructs a new <see cref="Struct"/> from the given objects.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <param name="array">The objects to put in the new array.</param>
        /// <returns>A new Godot Struct.</returns>
        public Struct(Variant[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            NativeValue = (godot_struct.movable)NativeFuncs.godotsharp_struct_new();
            _weakReferenceToSelf = DisposablesTracker.RegisterDisposable(this);

            int length = array.Length;

            Resize(length);

            for (int i = 0; i < length; i++)
                this[i] = array[i];
        }

        /// <summary>
        /// Constructs a new <see cref="Struct"/> from the given span's elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A new Godot Struct.</returns>
        public Struct(Span<StringName> array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            NativeValue = (godot_struct.movable)NativeFuncs.godotsharp_struct_new();
            _weakReferenceToSelf = DisposablesTracker.RegisterDisposable(this);

            int length = array.Length;

            Resize(length);

            for (int i = 0; i < length; i++)
                this[i] = array[i];
        }

        /// <summary>
        /// Constructs a new <see cref="Struct"/> from the given span's elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A new Godot Struct.</returns>
        public Struct(Span<NodePath> array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            NativeValue = (godot_struct.movable)NativeFuncs.godotsharp_struct_new();
            _weakReferenceToSelf = DisposablesTracker.RegisterDisposable(this);

            int length = array.Length;

            Resize(length);

            for (int i = 0; i < length; i++)
                this[i] = array[i];
        }

        /// <summary>
        /// Constructs a new <see cref="Struct"/> from the given span's elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A new Godot Struct.</returns>
        public Struct(Span<Rid> array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            NativeValue = (godot_struct.movable)NativeFuncs.godotsharp_struct_new();
            _weakReferenceToSelf = DisposablesTracker.RegisterDisposable(this);

            int length = array.Length;

            Resize(length);

            for (int i = 0; i < length; i++)
                this[i] = array[i];
        }

        // We must use ReadOnlySpan instead of Span here as this can accept implicit conversions
        // from derived types (e.g.: Node[]). Implicit conversion from Derived[] to Base[] are
        // fine as long as the array is not mutated. However, Span does this type checking at
        // instantiation, so it's not possible to use it even when not mutating anything.
        /// <summary>
        /// Constructs a new <see cref="Struct"/> from the given ReadOnlySpan's elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A new Godot Struct.</returns>
        public Struct(ReadOnlySpan<GodotObject> array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            NativeValue = (godot_struct.movable)NativeFuncs.godotsharp_struct_new();
            _weakReferenceToSelf = DisposablesTracker.RegisterDisposable(this);

            int length = array.Length;

            Resize(length);

            for (int i = 0; i < length; i++)
                this[i] = array[i];
        }

        private Struct(godot_struct nativeValueToOwn)
        {
            NativeValue = (godot_struct.movable)(nativeValueToOwn.IsAllocated ?
                nativeValueToOwn :
                NativeFuncs.godotsharp_struct_new());
            _weakReferenceToSelf = DisposablesTracker.RegisterDisposable(this);
        }

        // Explicit name to make it very clear
        internal static Struct CreateTakingOwnershipOfDisposableValue(godot_struct nativeValueToOwn)
            => new Struct(nativeValueToOwn);

        ~Struct()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes of this <see cref="Struct"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            // Always dispose `NativeValue` even if disposing is true
            NativeValue.DangerousSelfRef.Dispose();

            if (_weakReferenceToSelf != null)
            {
                DisposablesTracker.UnregisterDisposable(_weakReferenceToSelf);
            }
        }

        /// <summary>
        /// Returns a copy of the <see cref="Struct"/>.
        /// If <paramref name="deep"/> is <see langword="true"/>, a deep copy if performed:
        /// all nested arrays and dictionaries are duplicated and will not be shared with
        /// the original array. If <see langword="false"/>, a shallow copy is made and
        /// references to the original nested arrays and dictionaries are kept, so that
        /// modifying a sub-array or dictionary in the copy will also impact those
        /// referenced in the source array. Note that any <see cref="GodotObject"/> derived
        /// elements will be shallow copied regardless of the <paramref name="deep"/>
        /// setting.
        /// </summary>
        /// <param name="deep">If <see langword="true"/>, performs a deep copy.</param>
        /// <returns>A new Godot Struct.</returns>
        public Struct Duplicate(bool deep = false)
        {
            godot_struct newStruct;
            var self = (godot_struct)NativeValue;
            NativeFuncs.godotsharp_struct_duplicate(ref self, deep.ToGodotBool(), out newStruct);
            return CreateTakingOwnershipOfDisposableValue(newStruct);
        }

        /// <summary>
        /// Assigns the given value to all elements in the struct. This can typically be
        /// used together with <see cref="Resize(int)"/> to create a struct with a given
        /// size and initialized elements.
        /// Note: If <paramref name="value"/> is of a reference type (<see cref="GodotObject"/>
        /// derived, <see cref="Struct"/> or <see cref="Dictionary"/>, etc.) then the array
        /// is filled with the references to the same object, i.e. no duplicates are
        /// created.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <param name="value">The value to fill the array with.</param>
        public void Fill(Variant value)
        {
            ThrowIfReadOnly();

            godot_variant variantValue = (godot_variant)value.NativeVar;
            var self = (godot_struct)NativeValue;
            NativeFuncs.godotsharp_struct_fill(ref self, variantValue);
        }

        /// <summary>
        /// Returns the maximum value contained in the struct if all elements are of
        /// comparable types. If the elements can't be compared, <see langword="null"/>
        /// is returned.
        /// </summary>
        /// <returns>The maximum value contained in the array.</returns>
        public Variant Max()
        {
            godot_variant resVariant;
            var self = (godot_struct)NativeValue;
            NativeFuncs.godotsharp_struct_max(ref self, out resVariant);
            return Variant.CreateTakingOwnershipOfDisposableValue(resVariant);
        }

        /// <summary>
        /// Returns the minimum value contained in the array if all elements are of
        /// comparable types. If the elements can't be compared, <see langword="null"/>
        /// is returned.
        /// </summary>
        /// <returns>The minimum value contained in the array.</returns>
        public Variant Min()
        {
            godot_variant resVariant;
            var self = (godot_struct)NativeValue;
            NativeFuncs.godotsharp_struct_min(ref self, out resVariant);
            return Variant.CreateTakingOwnershipOfDisposableValue(resVariant);
        }

        /// <summary>
        /// Compares this <see cref="Struct"/> against the <paramref name="other"/>
        /// <see cref="Struct"/> recursively. Returns <see langword="true"/> if the
        /// sizes and contents of the arrays are equal, <see langword="false"/>
        /// otherwise.
        /// </summary>
        /// <param name="other">The other array to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if the sizes and contents of the arrays are equal,
        /// <see langword="false"/> otherwise.
        /// </returns>
        public bool RecursiveEqual(Struct other)
        {
            var self = (godot_struct)NativeValue;
            var otherVariant = (godot_struct)other.NativeValue;
            return NativeFuncs.godotsharp_struct_recursive_equal(ref self, otherVariant).ToBool();
        }

        /// <summary>
        /// Resizes the array to contain a different number of elements. If the array
        /// size is smaller, elements are cleared, if bigger, new elements are
        /// <see langword="null"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <param name="newSize">The new size of the array.</param>
        /// <returns><see cref="Error.Ok"/> if successful, or an error code.</returns>
        public Error Resize(int newSize)
        {
            ThrowIfReadOnly();

            var self = (godot_struct)NativeValue;
            return NativeFuncs.godotsharp_struct_resize(ref self, newSize);
        }

        /// <summary>
        /// Creates a shallow copy of a range of elements in the source <see cref="Struct"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="start"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="start">The zero-based index at which the range starts.</param>
        /// <returns>A new array that contains the elements inside the slice range.</returns>
        public Struct Slice(int start)
        {
            if (start < 0 || start > Count)
                throw new ArgumentOutOfRangeException(nameof(start));

            return GetSliceRange(start, Count, step: 1, deep: false);
        }

        /// <summary>
        /// Creates a shallow copy of a range of elements in the source <see cref="Struct"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="start"/> is less than 0 or greater than the array's size.
        /// -or-
        /// <paramref name="length"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="start">The zero-based index at which the range starts.</param>
        /// <param name="length">The length of the range.</param>
        /// <returns>A new array that contains the elements inside the slice range.</returns>
        // The Slice method must have this signature to get implicit Range support.
        public Struct Slice(int start, int length)
        {
            if (start < 0 || start > Count)
                throw new ArgumentOutOfRangeException(nameof(start));

            if (length < 0 || length > Count)
                throw new ArgumentOutOfRangeException(nameof(start));

            return GetSliceRange(start, start + length, step: 1, deep: false);
        }

        /// <summary>
        /// Returns the slice of the <see cref="Struct"/>, from <paramref name="start"/>
        /// (inclusive) to <paramref name="end"/> (exclusive), as a new <see cref="Struct"/>.
        /// The absolute value of <paramref name="start"/> and <paramref name="end"/>
        /// will be clamped to the array size.
        /// If either <paramref name="start"/> or <paramref name="end"/> are negative, they
        /// will be relative to the end of the array (i.e. <c>arr.GetSliceRange(0, -2)</c>
        /// is a shorthand for <c>arr.GetSliceRange(0, arr.Count - 2)</c>).
        /// If specified, <paramref name="step"/> is the relative index between source
        /// elements. It can be negative, then <paramref name="start"/> must be higher than
        /// <paramref name="end"/>. For example, <c>[0, 1, 2, 3, 4, 5].GetSliceRange(5, 1, -2)</c>
        /// returns <c>[5, 3]</c>.
        /// If <paramref name="deep"/> is true, each element will be copied by value
        /// rather than by reference.
        /// </summary>
        /// <param name="start">The zero-based index at which the range starts.</param>
        /// <param name="end">The zero-based index at which the range ends.</param>
        /// <param name="step">The relative index between source elements to take.</param>
        /// <param name="deep">If <see langword="true"/>, performs a deep copy.</param>
        /// <returns>A new array that contains the elements inside the slice range.</returns>
        public Struct GetSliceRange(int start, int end, int step = 1, bool deep = false)
        {
            godot_struct newStruct;
            var self = (godot_struct)NativeValue;
            NativeFuncs.godotsharp_struct_slice(ref self, start, end, step, deep.ToGodotBool(), out newStruct);
            return CreateTakingOwnershipOfDisposableValue(newStruct);
        }

        /// <summary>
        /// Concatenates two <see cref="Struct"/>s together, with the <paramref name="right"/>
        /// being added to the end of the <see cref="Struct"/> specified in <paramref name="left"/>.
        /// For example, <c>[1, 2] + [3, 4]</c> results in <c>[1, 2, 3, 4]</c>.
        /// </summary>
        /// <param name="left">The first array.</param>
        /// <param name="right">The second array.</param>
        /// <returns>A new Godot Struct with the contents of both arrays.</returns>
        public static Struct operator +(Struct left, Struct right)
        {
            if (left == null)
            {
                if (right == null)
                    return new Struct();

                return right.Duplicate(deep: false);
            }

            if (right == null)
                return left.Duplicate(deep: false);

            int leftCount = left.Count;
            int rightCount = right.Count;

            Struct newStruct = left.Duplicate(deep: false);
            newStruct.Resize(leftCount + rightCount);

            for (int i = 0; i < rightCount; i++)
                newStruct[i + leftCount] = right[i];

            return newStruct;
        }

        /// <summary>
        /// Returns the item at the given <paramref name="index"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The property is assigned and the array is read-only.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <value>The <see cref="Variant"/> item at the given <paramref name="index"/>.</value>
        public unsafe Variant this[int index]
        {
            get
            {
                GetVariantBorrowElementAt(index, out godot_variant borrowElem);
                return Variant.CreateCopyingBorrowed(borrowElem);
            }
            set
            {
                ThrowIfReadOnly();

                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                var self = (godot_struct)NativeValue;
                godot_variant* ptrw = NativeFuncs.godotsharp_struct_ptrw(ref self);
                godot_variant* itemPtr = &ptrw[index];
                (*itemPtr).Dispose();
                *itemPtr = value.CopyNativeVariant();
            }
        }

        /// <summary>
        /// Adds an item to the end of this <see cref="Struct"/>.
        /// This is the same as <c>append</c> or <c>push_back</c> in GDScript.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <param name="item">The <see cref="Variant"/> item to add.</param>
        public void Add(Variant item)
        {
            ThrowIfReadOnly();

            godot_variant variantValue = (godot_variant)item.NativeVar;
            var self = (godot_struct)NativeValue;
            _ = NativeFuncs.godotsharp_struct_add(ref self, variantValue);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of this <see cref="Struct"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="collection"/> is <see langword="null"/>.
        /// </exception>
        /// <param name="collection">Collection of <see cref="Variant"/> items to add.</param>
        public void AddRange<[MustBeVariant] T>(IEnumerable<T> collection)
        {
            ThrowIfReadOnly();

            if (collection == null)
                throw new ArgumentNullException(nameof(collection), "Value cannot be null.");

            // If the collection is another Godot Struct, we can add the items
            // with a single interop call.
            if (collection is Struct array)
            {
                var self = (godot_struct)NativeValue;
                var collectionNative = (godot_struct)array.NativeValue;
                _ = NativeFuncs.godotsharp_struct_add_range(ref self, collectionNative);
                return;
            }
            /*
            if (collection is Struct<T> typedStruct)
            {
                var self = (godot_struct)NativeValue;
                var collectionNative = (godot_struct)typedStruct.NativeValue;
                _ = NativeFuncs.godotsharp_struct_add_range(ref self, collectionNative);
                return;
            }
            */

            // If we can retrieve the count of the collection without enumerating it
            // (e.g.: the collections is a List<T>), use it to resize the array once
            // instead of growing it as we add items.
            if (collection.TryGetNonEnumeratedCount(out int count))
            {
                int oldCount = Count;
                Resize(Count + count);

                using var enumerator = collection.GetEnumerator();

                for (int i = 0; i < count; i++)
                {
                    enumerator.MoveNext();
                    this[oldCount + i] = Variant.From(enumerator.Current);
                }

                return;
            }

            foreach (var item in collection)
            {
                Add(Variant.From(item));
            }
        }

        /// <summary>
        /// Finds the index of an existing value using binary search.
        /// If the value is not present in the array, it returns the bitwise
        /// complement of the insertion index that maintains sorting order.
        /// Note: Calling <see cref="BinarySearch(int, int, Variant)"/> on an
        /// unsorted array results in unexpected behavior.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0.
        /// -or-
        /// <paramref name="count"/> is less than 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="index"/> and <paramref name="count"/> do not denote
        /// a valid range in the <see cref="Struct"/>.
        /// </exception>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="count">The length of the range to search.</param>
        /// <param name="item">The object to locate.</param>
        /// <returns>
        /// The index of the item in the array, if <paramref name="item"/> is found;
        /// otherwise, a negative number that is the bitwise complement of the index
        /// of the next element that is larger than <paramref name="item"/> or, if
        /// there is no larger element, the bitwise complement of <see cref="Count"/>.
        /// </returns>
        public int BinarySearch(int index, int count, Variant item)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "index cannot be negative.");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "count cannot be negative.");
            if (Count - index < count)
                throw new ArgumentException("length is out of bounds or count is greater than the number of elements.");

            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            godot_variant variantValue = (godot_variant)item.NativeVar;
            var self = (godot_struct)NativeValue;
            return NativeFuncs.godotsharp_struct_binary_search(ref self, index, count, variantValue);
        }

        /// <summary>
        /// Finds the index of an existing value using binary search.
        /// If the value is not present in the array, it returns the bitwise
        /// complement of the insertion index that maintains sorting order.
        /// Note: Calling <see cref="BinarySearch(Variant)"/> on an unsorted
        /// array results in unexpected behavior.
        /// </summary>
        /// <param name="item">The object to locate.</param>
        /// <returns>
        /// The index of the item in the array, if <paramref name="item"/> is found;
        /// otherwise, a negative number that is the bitwise complement of the index
        /// of the next element that is larger than <paramref name="item"/> or, if
        /// there is no larger element, the bitwise complement of <see cref="Count"/>.
        /// </returns>
        public int BinarySearch(Variant item)
        {
            return BinarySearch(0, Count, item);
        }

        /// <summary>
        /// Returns <see langword="true"/> if the array contains the given value.
        /// </summary>
        /// <example>
        /// <code>
        /// var arr = new Godot.Collections.Struct { "inside", 7 };
        /// GD.Print(arr.Contains("inside")); // True
        /// GD.Print(arr.Contains("outside")); // False
        /// GD.Print(arr.Contains(7)); // True
        /// GD.Print(arr.Contains("7")); // False
        /// </code>
        /// </example>
        /// <param name="item">The <see cref="Variant"/> item to look for.</param>
        /// <returns>Whether or not this array contains the given item.</returns>
        public bool Contains(Variant item) => IndexOf(item) != -1;

        /// <summary>
        /// Clears the array. This is the equivalent to using <see cref="Resize(int)"/>
        /// with a size of <c>0</c>
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        public void Clear() => Resize(0);

        /// <summary>
        /// Searches the array for a value and returns its index or <c>-1</c> if not found.
        /// </summary>
        /// <param name="item">The <see cref="Variant"/> item to search for.</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        public int IndexOf(Variant item)
        {
            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            godot_variant variantValue = (godot_variant)item.NativeVar;
            var self = (godot_struct)NativeValue;
            return NativeFuncs.godotsharp_struct_index_of(ref self, variantValue);
        }

        /// <summary>
        /// Searches the array for a value and returns its index or <c>-1</c> if not found.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="item">The <see cref="Variant"/> item to search for.</param>
        /// <param name="index">The initial search index to start from.</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        public int IndexOf(Variant item, int index)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            godot_variant variantValue = (godot_variant)item.NativeVar;
            var self = (godot_struct)NativeValue;
            return NativeFuncs.godotsharp_struct_index_of(ref self, variantValue, index);
        }

        /// <summary>
        /// Searches the array for a value in reverse order and returns its index
        /// or <c>-1</c> if not found.
        /// </summary>
        /// <param name="item">The <see cref="Variant"/> item to search for.</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        public int LastIndexOf(Variant item)
        {
            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            godot_variant variantValue = (godot_variant)item.NativeVar;
            var self = (godot_struct)NativeValue;
            return NativeFuncs.godotsharp_struct_last_index_of(ref self, variantValue, Count - 1);
        }

        /// <summary>
        /// Searches the array for a value in reverse order and returns its index
        /// or <c>-1</c> if not found.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="item">The <see cref="Variant"/> item to search for.</param>
        /// <param name="index">The initial search index to start from.</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        public int LastIndexOf(Variant item, int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            godot_variant variantValue = (godot_variant)item.NativeVar;
            var self = (godot_struct)NativeValue;
            return NativeFuncs.godotsharp_struct_last_index_of(ref self, variantValue, index);
        }

        /// <summary>
        /// Inserts a new element at a given position in the array. The position
        /// must be valid, or at the end of the array (<c>pos == Count - 1</c>).
        /// Existing items will be moved to the right.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="index">The index to insert at.</param>
        /// <param name="item">The <see cref="Variant"/> item to insert.</param>
        public void Insert(int index, Variant item)
        {
            ThrowIfReadOnly();

            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            godot_variant variantValue = (godot_variant)item.NativeVar;
            var self = (godot_struct)NativeValue;
            NativeFuncs.godotsharp_struct_insert(ref self, index, variantValue);
        }

        /// <summary>
        /// Removes the first occurrence of the specified <paramref name="item"/>
        /// from this <see cref="Struct"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <param name="item">The value to remove.</param>
        public bool Remove(Variant item)
        {
            ThrowIfReadOnly();

            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes an element from the array by index.
        /// To remove an element by searching for its value, use
        /// <see cref="Remove(Variant)"/> instead.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="index">The index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            ThrowIfReadOnly();

            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            var self = (godot_struct)NativeValue;
            NativeFuncs.godotsharp_struct_remove_at(ref self, index);
        }

        // ICollection

        /// <summary>
        /// Returns the number of elements in this <see cref="Struct"/>.
        /// This is also known as the size or length of the array.
        /// </summary>
        /// <returns>The number of elements.</returns>
        public int Count => NativeValue.DangerousSelfRef.Size;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => false;

        /// <summary>
        /// Returns <see langword="true"/> if the array is read-only.
        /// See <see cref="MakeReadOnly"/>.
        /// </summary>
        public bool IsReadOnly => NativeValue.DangerousSelfRef.IsReadOnly;

        /// <summary>
        /// Makes the <see cref="Struct"/> read-only, i.e. disabled modying of the
        /// array's elements. Does not apply to nested content, e.g. content of
        /// nested arrays.
        /// </summary>
        public void MakeReadOnly()
        {
            if (IsReadOnly)
            {
                // Avoid interop call when the array is already read-only.
                return;
            }

            var self = (godot_struct)NativeValue;
            NativeFuncs.godotsharp_struct_make_read_only(ref self);
        }

        /// <summary>
        /// Copies the elements of this <see cref="Struct"/> to the given
        /// <see cref="Variant"/> C# array, starting at the given index.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The destination array was not long enough.
        /// </exception>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The index to start at.</param>
        public void CopyTo(Variant[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), "Value cannot be null.");

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex),
                    "Number was less than the array's lower bound in the first dimension.");
            }

            int count = Count;

            if (array.Length < (arrayIndex + count))
            {
                throw new ArgumentException(
                    "Destination array was not long enough. Check destIndex and length, and the array's lower bounds.");
            }

            unsafe
            {
                for (int i = 0; i < count; i++)
                {
                    array[arrayIndex] = Variant.CreateCopyingBorrowed(NativeValue.DangerousSelfRef.Elements[i]);
                    arrayIndex++;
                }
            }
        }

        void ICollection.CopyTo(System.Array copyArray, int index)
        {
            if (copyArray == null)
                throw new ArgumentNullException(nameof(copyArray), "Value cannot be null.");

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Number was less than the struct's lower bound in the first dimension.");
            }

            int count = Count;

            if (copyArray.Length < (index + count))
            {
                throw new ArgumentException(
                    "Destination struct was not long enough. Check destIndex and length, and the struct's lower bounds.");
            }

            unsafe
            {
                for (int i = 0; i < count; i++)
                {
                    object boxedVariant = Variant.CreateCopyingBorrowed(NativeValue.DangerousSelfRef.Elements[i]);
                    copyArray.SetValue(boxedVariant, index);
                    index++;
                }
            }
        }

        // IEnumerable

        /// <summary>
        /// Gets an enumerator for this <see cref="Struct"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<Variant> GetEnumerator()
        {
            int count = Count;

            for (int i = 0; i < count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Converts this <see cref="Struct"/> to a string.
        /// </summary>
        /// <returns>A string representation of this struct.</returns>
        public override string ToString()
        {
            var self = (godot_struct)NativeValue;
            NativeFuncs.godotsharp_struct_to_string(ref self, out godot_string str);
            using (str)
                return Marshaling.ConvertStringToManaged(str);
        }

        /// <summary>
        /// The variant returned via the <paramref name="elem"/> parameter is owned by the Array and must not be disposed.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        internal void GetVariantBorrowElementAt(int index, out godot_variant elem)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            GetVariantBorrowElementAtUnchecked(index, out elem);
        }

        /// <summary>
        /// The variant returned via the <paramref name="elem"/> parameter is owned by the Array and must not be disposed.
        /// </summary>
        internal unsafe void GetVariantBorrowElementAtUnchecked(int index, out godot_variant elem)
        {
            elem = NativeValue.DangerousSelfRef.Elements[index];
        }

        private void ThrowIfReadOnly()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException("Struct instance is read-only.");
            }
        }
    }

    internal interface IGenericGodotStruct
    {
        public Struct UnderlyingStruct { get; }
    }

    /// <summary>
    /// Typed wrapper around Godot's Struct class, a collection of Variant
    /// typed elements allocated in the engine in C++. Useful when
    /// interfacing with the engine. Otherwise prefer .NET collections
    /// or use built-in structs or classes."/>.
    /// </summary>
    /// <typeparam name="T">The type of the array.</typeparam>
    [DebuggerTypeProxy(typeof(ArrayDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
    [SuppressMessage("Naming", "CA1710", MessageId = "Identifiers should have correct suffix")]
    public sealed class Struct<[MustBeVariant] T> :
        IList<T>,
        IReadOnlyList<T>,
        ICollection<T>,
        IEnumerable<T>,
        IGenericGodotStruct
    {
        private static godot_variant ToVariantFunc(in Array<T> godotStruct) =>
            VariantUtils.CreateFromArray(godotStruct);

        private static Array<T> FromVariantFunc(in godot_variant variant) =>
            VariantUtils.ConvertToArray<T>(variant);

        static unsafe Struct()
        {
            VariantUtils.GenericConversion<Array<T>>.ToVariantCb = &ToVariantFunc;
            VariantUtils.GenericConversion<Array<T>>.FromVariantCb = &FromVariantFunc;
        }

        private readonly Struct _underlyingStruct;

        Struct IGenericGodotStruct.UnderlyingStruct => _underlyingStruct;

        internal ref godot_struct.movable NativeValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _underlyingStruct.NativeValue;
        }

        /// <summary>
        /// Constructs a new empty <see cref="Array{T}"/>.
        /// </summary>
        /// <returns>A new Godot Array.</returns>
        public Struct()
        {
            _underlyingStruct = new Struct();
        }

        /// <summary>
        /// Constructs a new <see cref="Struct{T}"/> from the given collection's elements.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="collection"/> is <see langword="null"/>.
        /// </exception>
        /// <param name="collection">The collection of elements to construct from.</param>
        /// <returns>A new Godot Array.</returns>
        public Struct(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            _underlyingStruct = new Struct();

            foreach (T element in collection)
                Add(element);
        }

        /// <summary>
        /// Constructs a new <see cref="Array{T}"/> from the given items.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <param name="array">The items to put in the new array.</param>
        /// <returns>A new Godot Array.</returns>
        public Struct(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            _underlyingStruct = new Struct();

            foreach (T element in array)
                Add(element);
        }

        /// <summary>
        /// Constructs a typed <see cref="Struct{T}"/> from an untyped <see cref="Struct"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="fromStruct"/> is <see langword="null"/>.
        /// </exception>
        /// <param name="fromStruct">The untyped array to construct from.</param>
        /// <returns>A new Godot Array.</returns>
        public Struct(Struct fromStruct)
        {
            if (fromStruct == null)
                throw new ArgumentNullException(nameof(fromStruct));

            _underlyingStruct = fromStruct;
        }

        // Explicit name to make it very clear
        internal static Struct<T> CreateTakingOwnershipOfDisposableValue(godot_struct nativeValueToOwn)
            => new Struct<T>(Struct.CreateTakingOwnershipOfDisposableValue(nativeValueToOwn));

        /// <summary>
        /// Converts this typed <see cref="Struct{T}"/> to an untyped <see cref="Struct"/>.
        /// </summary>
        /// <param name="from">The typed array to convert.</param>
        /// <returns>A new Godot Array, or <see langword="null"/> if <see paramref="from"/> was null.</returns>
        [return: NotNullIfNotNull("from")]
        public static explicit operator Struct?(Struct<T>? from)
        {
            return from?._underlyingStruct;
        }

        /// <summary>
        /// Duplicates this <see cref="Array{T}"/>.
        /// </summary>
        /// <param name="deep">If <see langword="true"/>, performs a deep copy.</param>
        /// <returns>A new Godot Array.</returns>
        public Struct<T> Duplicate(bool deep = false)
        {
            return new Struct<T>(_underlyingStruct.Duplicate(deep));
        }

        /// <summary>
        /// Assigns the given value to all elements in the array. This can typically be
        /// used together with <see cref="Resize(int)"/> to create an array with a given
        /// size and initialized elements.
        /// Note: If <paramref name="value"/> is of a reference type (<see cref="GodotObject"/>
        /// derived, <see cref="Struct"/> or <see cref="Dictionary"/>, etc.) then the array
        /// is filled with the references to the same object, i.e. no duplicates are
        /// created.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <param name="value">The value to fill the array with.</param>
        public void Fill(T value)
        {
            ThrowIfReadOnly();

            godot_variant variantValue = VariantUtils.CreateFrom(value);
            var self = (godot_struct)_underlyingStruct.NativeValue;
            NativeFuncs.godotsharp_struct_fill(ref self, variantValue);
        }

        /// <summary>
        /// Returns the maximum value contained in the array if all elements are of
        /// comparable types. If the elements can't be compared, <see langword="default"/>
        /// is returned.
        /// </summary>
        /// <returns>The maximum value contained in the array.</returns>
        public T Max()
        {
            godot_variant resVariant;
            var self = (godot_struct)_underlyingStruct.NativeValue;
            NativeFuncs.godotsharp_struct_max(ref self, out resVariant);
            return VariantUtils.ConvertTo<T>(resVariant);
        }

        /// <summary>
        /// Returns the minimum value contained in the array if all elements are of
        /// comparable types. If the elements can't be compared, <see langword="default"/>
        /// is returned.
        /// </summary>
        /// <returns>The minimum value contained in the array.</returns>
        public T Min()
        {
            godot_variant resVariant;
            var self = (godot_struct)_underlyingStruct.NativeValue;
            NativeFuncs.godotsharp_struct_min(ref self, out resVariant);
            return VariantUtils.ConvertTo<T>(resVariant);
        }

        /// <summary>
        /// Compares this <see cref="Struct{T}"/> against the <paramref name="other"/>
        /// <see cref="Struct{T}"/> recursively. Returns <see langword="true"/> if the
        /// sizes and contents of the arrays are equal, <see langword="false"/>
        /// otherwise.
        /// </summary>
        /// <param name="other">The other array to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if the sizes and contents of the arrays are equal,
        /// <see langword="false"/> otherwise.
        /// </returns>
        public bool RecursiveEqual(Struct<T> other)
        {
            return _underlyingStruct.RecursiveEqual(other._underlyingStruct);
        }

        /// <summary>
        /// Resizes this <see cref="Struct{T}"/> to the given size.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <param name="newSize">The new size of the array.</param>
        /// <returns><see cref="Error.Ok"/> if successful, or an error code.</returns>
        public Error Resize(int newSize)
        {
            return _underlyingStruct.Resize(newSize);
        }

        /// <summary>
        /// Creates a shallow copy of a range of elements in the source <see cref="Struct{T}"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="start"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="start">The zero-based index at which the range starts.</param>
        /// <returns>A new array that contains the elements inside the slice range.</returns>
        public Struct<T> Slice(int start)
        {
            return GetSliceRange(start, Count, step: 1, deep: false);
        }

        /// <summary>
        /// Creates a shallow copy of a range of elements in the source <see cref="Struct{T}"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="start"/> is less than 0 or greater than the array's size.
        /// -or-
        /// <paramref name="length"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="start">The zero-based index at which the range starts.</param>
        /// <param name="length">The length of the range.</param>
        /// <returns>A new array that contains the elements inside the slice range.</returns>
        // The Slice method must have this signature to get implicit Range support.
        public Struct<T> Slice(int start, int length)
        {
            return GetSliceRange(start, start + length, step: 1, deep: false);
        }

        /// <summary>
        /// Returns the slice of the <see cref="Array{T}"/>, from <paramref name="start"/>
        /// (inclusive) to <paramref name="end"/> (exclusive), as a new <see cref="Array{T}"/>.
        /// The absolute value of <paramref name="start"/> and <paramref name="end"/>
        /// will be clamped to the array size.
        /// If either <paramref name="start"/> or <paramref name="end"/> are negative, they
        /// will be relative to the end of the array (i.e. <c>arr.GetSliceRange(0, -2)</c>
        /// is a shorthand for <c>arr.GetSliceRange(0, arr.Count - 2)</c>).
        /// If specified, <paramref name="step"/> is the relative index between source
        /// elements. It can be negative, then <paramref name="start"/> must be higher than
        /// <paramref name="end"/>. For example, <c>[0, 1, 2, 3, 4, 5].GetSliceRange(5, 1, -2)</c>
        /// returns <c>[5, 3]</c>.
        /// If <paramref name="deep"/> is true, each element will be copied by value
        /// rather than by reference.
        /// </summary>
        /// <param name="start">The zero-based index at which the range starts.</param>
        /// <param name="end">The zero-based index at which the range ends.</param>
        /// <param name="step">The relative index between source elements to take.</param>
        /// <param name="deep">If <see langword="true"/>, performs a deep copy.</param>
        /// <returns>A new array that contains the elements inside the slice range.</returns>
        public Struct<T> GetSliceRange(int start, int end, int step = 1, bool deep = false)
        {
            return new Struct<T>(_underlyingStruct.GetSliceRange(start, end, step, deep));
        }

        /// <summary>
        /// Concatenates two <see cref="Array{T}"/>s together, with the <paramref name="right"/>
        /// being added to the end of the <see cref="Array{T}"/> specified in <paramref name="left"/>.
        /// For example, <c>[1, 2] + [3, 4]</c> results in <c>[1, 2, 3, 4]</c>.
        /// </summary>
        /// <param name="left">The first array.</param>
        /// <param name="right">The second array.</param>
        /// <returns>A new Godot Array with the contents of both arrays.</returns>
        public static Struct<T> operator +(Struct<T> left, Struct<T> right)
        {
            if (left == null)
            {
                if (right == null)
                    return new Struct<T>();

                return right.Duplicate(deep: false);
            }

            if (right == null)
                return left.Duplicate(deep: false);

            return new Struct<T>(left._underlyingStruct + right._underlyingStruct);
        }

        // IList<T>

        /// <summary>
        /// Returns the item at the given <paramref name="index"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The property is assigned and the array is read-only.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <value>The <see cref="Variant"/> item at the given <paramref name="index"/>.</value>
        public unsafe T this[int index]
        {
            get
            {
                _underlyingStruct.GetVariantBorrowElementAt(index, out godot_variant borrowElem);
                return VariantUtils.ConvertTo<T>(borrowElem);
            }
            set
            {
                ThrowIfReadOnly();

                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                var self = (godot_struct)_underlyingStruct.NativeValue;
                godot_variant* ptrw = NativeFuncs.godotsharp_struct_ptrw(ref self);
                godot_variant* itemPtr = &ptrw[index];
                (*itemPtr).Dispose();
                *itemPtr = VariantUtils.CreateFrom(value);
            }
        }

        /// <summary>
        /// Searches the array for a value and returns its index or <c>-1</c> if not found.
        /// </summary>
        /// <param name="item">The <see cref="Variant"/> item to search for.</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        public int IndexOf(T item)
        {
            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            using var variantValue = VariantUtils.CreateFrom(item);
            var self = (godot_struct)_underlyingStruct.NativeValue;
            return NativeFuncs.godotsharp_struct_index_of(ref self, variantValue);
        }

        /// <summary>
        /// Searches the array for a value and returns its index or <c>-1</c> if not found.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="item">The <see cref="Variant"/> item to search for.</param>
        /// <param name="index">The initial search index to start from.</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        public int IndexOf(T item, int index)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            godot_variant variantValue = VariantUtils.CreateFrom(item);
            var self = (godot_struct)_underlyingStruct.NativeValue;
            return NativeFuncs.godotsharp_struct_index_of(ref self, variantValue, index);
        }

        /// <summary>
        /// Searches the array for a value in reverse order and returns its index
        /// or <c>-1</c> if not found.
        /// </summary>
        /// <param name="item">The <see cref="Variant"/> item to search for.</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        public int LastIndexOf(Variant item)
        {
            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            godot_variant variantValue = VariantUtils.CreateFrom(item);
            var self = (godot_struct)_underlyingStruct.NativeValue;
            return NativeFuncs.godotsharp_struct_last_index_of(ref self, variantValue, Count - 1);
        }

        /// <summary>
        /// Searches the array for a value in reverse order and returns its index
        /// or <c>-1</c> if not found.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="item">The <see cref="Variant"/> item to search for.</param>
        /// <param name="index">The initial search index to start from.</param>
        /// <returns>The index of the item, or -1 if not found.</returns>
        public int LastIndexOf(Variant item, int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            godot_variant variantValue = VariantUtils.CreateFrom(item);
            var self = (godot_struct)_underlyingStruct.NativeValue;
            return NativeFuncs.godotsharp_struct_last_index_of(ref self, variantValue, index);
        }

        /// <summary>
        /// Inserts a new element at a given position in the array. The position
        /// must be valid, or at the end of the array (<c>pos == Count - 1</c>).
        /// Existing items will be moved to the right.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="index">The index to insert at.</param>
        /// <param name="item">The <see cref="Variant"/> item to insert.</param>
        public void Insert(int index, T item)
        {
            ThrowIfReadOnly();

            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            using var variantValue = VariantUtils.CreateFrom(item);
            var self = (godot_struct)_underlyingStruct.NativeValue;
            NativeFuncs.godotsharp_struct_insert(ref self, index, variantValue);
        }

        /// <summary>
        /// Removes an element from the array by index.
        /// To remove an element by searching for its value, use
        /// <see cref="Remove(T)"/> instead.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <param name="index">The index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            _underlyingStruct.RemoveAt(index);
        }

        // ICollection<T>

        /// <summary>
        /// Returns the number of elements in this <see cref="Array{T}"/>.
        /// This is also known as the size or length of the array.
        /// </summary>
        /// <returns>The number of elements.</returns>
        public int Count => _underlyingStruct.Count;

        /// <summary>
        /// Returns <see langword="true"/> if the array is read-only.
        /// See <see cref="MakeReadOnly"/>.
        /// </summary>
        public bool IsReadOnly => _underlyingStruct.IsReadOnly;

        /// <summary>
        /// Makes the <see cref="Array{T}"/> read-only, i.e. disabled modying of the
        /// array's elements. Does not apply to nested content, e.g. content of
        /// nested arrays.
        /// </summary>
        public void MakeReadOnly()
        {
            _underlyingStruct.MakeReadOnly();
        }

        /// <summary>
        /// Adds an item to the end of this <see cref="Array{T}"/>.
        /// This is the same as <c>append</c> or <c>push_back</c> in GDScript.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <param name="item">The <see cref="Variant"/> item to add.</param>
        public void Add(T item)
        {
            ThrowIfReadOnly();

            using var variantValue = VariantUtils.CreateFrom(item);
            var self = (godot_struct)_underlyingStruct.NativeValue;
            _ = NativeFuncs.godotsharp_struct_add(ref self, variantValue);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of this <see cref="Array{T}"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="collection"/> is <see langword="null"/>.
        /// </exception>
        /// <param name="collection">Collection of <see cref="Variant"/> items to add.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            ThrowIfReadOnly();

            if (collection == null)
                throw new ArgumentNullException(nameof(collection), "Value cannot be null.");

            // If the collection is another Godot Array, we can add the items
            // with a single interop call.
            if (collection is Struct array)
            {
                var self = (godot_struct)_underlyingStruct.NativeValue;
                var collectionNative = (godot_struct)array.NativeValue;
                _ = NativeFuncs.godotsharp_struct_add_range(ref self, collectionNative);
                return;
            }
            /*
            if (collection is Array<T> typedArray)
            {
                var self = (godot_array)_underlyingStruct.NativeValue;
                var collectionNative = (godot_array)typedArray._underlyingArray.NativeValue;
                _ = NativeFuncs.godotsharp_array_add_range(ref self, collectionNative);
                return;
            }
            */

            // If we can retrieve the count of the collection without enumerating it
            // (e.g.: the collections is a List<T>), use it to resize the array once
            // instead of growing it as we add items.
            if (collection.TryGetNonEnumeratedCount(out int count))
            {
                int oldCount = Count;
                Resize(Count + count);

                using var enumerator = collection.GetEnumerator();

                for (int i = 0; i < count; i++)
                {
                    enumerator.MoveNext();
                    this[oldCount + i] = enumerator.Current;
                }

                return;
            }

            foreach (var item in collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Finds the index of an existing value using binary search.
        /// If the value is not present in the array, it returns the bitwise
        /// complement of the insertion index that maintains sorting order.
        /// Note: Calling <see cref="BinarySearch(int, int, T)"/> on an unsorted
        /// array results in unexpected behavior.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0.
        /// -or-
        /// <paramref name="count"/> is less than 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="index"/> and <paramref name="count"/> do not denote
        /// a valid range in the <see cref="Array{T}"/>.
        /// </exception>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="count">The length of the range to search.</param>
        /// <param name="item">The object to locate.</param>
        /// <returns>
        /// The index of the item in the array, if <paramref name="item"/> is found;
        /// otherwise, a negative number that is the bitwise complement of the index
        /// of the next element that is larger than <paramref name="item"/> or, if
        /// there is no larger element, the bitwise complement of <see cref="Count"/>.
        /// </returns>
        public int BinarySearch(int index, int count, T item)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "index cannot be negative.");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "count cannot be negative.");
            if (Count - index < count)
                throw new ArgumentException("length is out of bounds or count is greater than the number of elements.");

            if (Count == 0)
            {
                // Special case for empty array to avoid an interop call.
                return -1;
            }

            using var variantValue = VariantUtils.CreateFrom(item);
            var self = (godot_struct)_underlyingStruct.NativeValue;
            return NativeFuncs.godotsharp_struct_binary_search(ref self, index, count, variantValue);
        }

        /// <summary>
        /// Finds the index of an existing value using binary search.
        /// If the value is not present in the array, it returns the bitwise
        /// complement of the insertion index that maintains sorting order.
        /// Note: Calling <see cref="BinarySearch(T)"/> on an unsorted
        /// array results in unexpected behavior.
        /// </summary>
        /// <param name="item">The object to locate.</param>
        /// <returns>
        /// The index of the item in the array, if <paramref name="item"/> is found;
        /// otherwise, a negative number that is the bitwise complement of the index
        /// of the next element that is larger than <paramref name="item"/> or, if
        /// there is no larger element, the bitwise complement of <see cref="Count"/>.
        /// </returns>
        public int BinarySearch(T item)
        {
            return BinarySearch(0, Count, item);
        }

        /// <summary>
        /// Clears the array. This is the equivalent to using <see cref="Resize(int)"/>
        /// with a size of <c>0</c>
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        public void Clear()
        {
            _underlyingStruct.Clear();
        }

        /// <summary>
        /// Returns <see langword="true"/> if the array contains the given value.
        /// </summary>
        /// <example>
        /// <code>
        /// var arr = new Godot.Collections.Array&lt;string&gt; { "inside", "7" };
        /// GD.Print(arr.Contains("inside")); // True
        /// GD.Print(arr.Contains("outside")); // False
        /// GD.Print(arr.Contains(7)); // False
        /// GD.Print(arr.Contains("7")); // True
        /// </code>
        /// </example>
        /// <param name="item">The item to look for.</param>
        /// <returns>Whether or not this array contains the given item.</returns>
        public bool Contains(T item) => IndexOf(item) != -1;

        /// <summary>
        /// Copies the elements of this <see cref="Array{T}"/> to the given
        /// C# array, starting at the given index.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0 or greater than the array's size.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The destination array was not long enough.
        /// </exception>
        /// <param name="array">The C# array to copy to.</param>
        /// <param name="arrayIndex">The index to start at.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), "Value cannot be null.");

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex),
                    "Number was less than the array's lower bound in the first dimension.");
            }

            int count = Count;

            if (array.Length < (arrayIndex + count))
            {
                throw new ArgumentException(
                    "Destination array was not long enough. Check destIndex and length, and the array's lower bounds.");
            }

            for (int i = 0; i < count; i++)
            {
                array[arrayIndex] = this[i];
                arrayIndex++;
            }
        }

        /// <summary>
        /// Removes the first occurrence of the specified <paramref name="item"/>
        /// from this <see cref="Array{T}"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The array is read-only.
        /// </exception>
        /// <param name="item">The value to remove.</param>
        /// <returns>A <see langword="bool"/> indicating success or failure.</returns>
        public bool Remove(T item)
        {
            ThrowIfReadOnly();

            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        // IEnumerable<T>

        /// <summary>
        /// Gets an enumerator for this <see cref="Array{T}"/>.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int count = _underlyingStruct.Count;

            for (int i = 0; i < count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Converts this <see cref="Array{T}"/> to a string.
        /// </summary>
        /// <returns>A string representation of this array.</returns>
        public override string ToString() => _underlyingStruct.ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Variant(Struct<T> from) => Variant.CreateFrom(from);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Struct<T>(Variant from) => from.AsGodotStruct<T>();

        private void ThrowIfReadOnly()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException("Array instance is read-only.");
            }
        }
    }
}
