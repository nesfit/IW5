# IW5
# Programming in .NET and C# 
# Introduction to the .NET Platform and C# language basics
<div class="right">
[ Jan Pluskal <ipluskal@fit.vutbr.cz> ]
</div>

---
## Agenda

* .NET Framework
* C# language syntax
* Data types
* Variables, parameters
* Statements, expressions, operators
* Namespaces

---
## .NET Framework

Based on Common Language Runtime (CLR) with additional libraries
<div class="left">
<ul>
  <li>Library types</li>
  <ul>
    <li>Core Framework</li>
    <ul>
      <li>System.dll</li>
      <li>System.Xml.dll</li>
      <li>System.Dynamic.dll</li>
    </ul> 
    <li>Application technologies</li>
    <ul>
      <li>WPF - (PresentationCore.dll)</li>
      <li>WCF</li>
    </ul> 
  </ul> 
</ul>    
</div>
<div class="right">
<img src="/Lectures/Lecture_01/assets/image/csh_in_nutshell_framework.png" />
</div>

---
### .NET Framework - Selected Libraries

* **WinForms** 
* **ASP.NET**  
* **WPF**  – *Windows Presentation Foundation*
* **WCF**  – *Windows Communication Foundation*
* **WF**   – *Windows Workflow Foundation*
* **LINQ** – *Language Integrated Query*

---
### .NET Framework - Architecture
![Architecture](/Lectures/Lecture_01/assets/image/dot_net_architecture.jpg)

---
## CLR – Common Language Runtime

* Runtime processing *managed code*
* Similar to Java Virtual Machine
<br />
<div class="left">
<ul>
  <li>Services</li>
  <ul>
    <li>Memory management</li>
    <li>Library loading</li>
    <li>Security services</li>
    <li>Exception handeling</li>
    <li>...</li>
  </ul> 
  <li>Language neutral</li>
  <ul>
    <li>Supports development in multiple languages</li>
    <li>C#, VB, Managed C++, Delphi .NET, F#, ... </li>
  </ul> 
</ul>    
</div>
<div class="right">
<img src="/Lectures/Lecture_01/assets/image/dot_net_clr.png" style="margin-left:30px" />
</div>

---
### Memory Management

* **Garbage collector (GC)**
  * Automated memory management without need of programmer intervention   
  * Provided by CLR
  * Based on reachability from GC roots
* Benefits
  * No need for *manual deallocation*, manual call of destructor
  * No need to use *pointers*
![GC generations](/Lectures/Lecture_01/assets/image/gc_generations.jpg)

+++

![GC roots](/Lectures/Lecture_01/assets/image/gc_roots.jpg)

+++

![GC collection](/Lectures/Lecture_01/assets/image/gc_collecting.png)

---
### CLR Details

* C# is compiled to so called *managed code*
* **Managed code** is stored as *Assembly*:
  * Executable code (*.exe)
  * Library (*.dll)
* **IL - Intermediate Language**
  * Managed code representation
* **JIT - Just-In-Time Compiler**
  * JIT compiles IL into assembler (native machine code - x86, x64, ARM, ...)
  * Code optimizations
  * Dynamic code generation
  
+++

![CLD](/Lectures/Lecture_01/assets/image/clr.gif)

---
## C# Platform Support
* Designed to run on Windows platform, but there are some exceptions..
* **ASP.NET**
  * C# runs on server back-end
  * Transpilation into HTML supported on all platforms
  * AS.NET 5.0 Linux, Mac OS X
* **MONO project**
  * Runs on custom runtime, not on the CLR!
  * Custom compiler
  * Linux, Solaris, Mac OS X, Windows
  * Open source
* **Silverlight**
  * Hosts application in C# on a client side.
  * Alternative to Adobe Flash
  * Windows, Mac OS X
  * **Deprecated**
* **.NET Core**

+++

### .NET Core
* Open source, support *Windows, Linux, Mac OS X*
* Multi-platform implementation of base .NET libraries
![dotNetCore](/Lectures/Lecture_01/assets/image/dotNetCore.png)

---
# C# Language Basics
---?code=/Lectures/Lecture_01/HelloWorld/Program.cs&lang=C#&title=Hello World
@[1]
@[3]
@[5]
@[7]
@[9]
@[11]
@[13]
@[14]

[Code sample](/Lectures/Lecture_01/HelloWorld/Program.cs)

---
## C# Lanage syntax
* Based on C, C++
* **Identifiers**
  * Names of *classes, methods, variables*
* **Key words**
  * Names reserved by compiler
  * E.g., 
    ```C#
    public, static, int
    ```
  * If you need used *key word* as identifier, use prefix **@**
  * E.g., 
    ```C# 
    @public, @static, @int
    ```
  * It's useful, if you use library written in another .NET language

+++

### [C# Keywords](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/)  
<table style="color:#2a76dd">
<thead>
<tr>
<th></th>
<th></th>
<th></th>
<th></th>
</tr>
</thead>
<tbody>
<tr>
<td>abstract</td>
<td class="x-hidden-focus">as</td>
<td>base</td>
<td>bool</td>
</tr>
<tr>
<td>break</td>
<td>byte</td>
<td>case</td>
<td>catch</td>
</tr>
<tr>
<td>char</td>
<td>checked</td>
<td>class</td>
<td>const</td>
</tr>
<tr>
<td>continue</td>
<td>decimal</td>
<td>default</td>
<td>delegate</td>
</tr>
<tr>
<td>do</td>
<td>double</td>
<td>else</td>
<td>enum</td>
</tr>
<tr>
<td>event</td>
<td>explicit</td>
<td>extern</td>
<td>false</td>
</tr>
<tr>
<td>finally</td>
<td>fixed</td>
<td>float</td>
<td>for</td>
</tr>
<tr>
<td>foreach</td>
<td>goto</td>
<td>if</td>
<td>implicit</td>
</tr>
<tr>
<td>in</td>
<td>in (generic modifier)</td>
<td>int</td>
<td>interface</td>
</tr>
<tr>
<td>internal</td>
<td>is</td>
<td>lock</td>
<td>long</td>
</tr>
<tr>
<td>namespace</td>
<td>new</td>
<td>null</td>
<td>object</td>
</tr>
<tr>
<td>operator</td>
<td>out</td>
<td>out (generic modifier)</td>
<td>override</td>
</tr>
<tr>
<td>params</td>
<td>private</td>
<td>protected</td>
<td>public</td>
</tr>
<tr>
<td>readonly</td>
<td>ref</td>
<td>return</td>
<td>sbyte</td>
</tr>
<tr>
<td>sealed</td>
<td>short</td>
<td>sizeof</td>
<td>stackalloc</td>
</tr>
<tr>
<td>static</td>
<td>string</td>
<td>struct</td>
<td>switch</td>
</tr>
<tr>
<td>this</td>
<td>throw</td>
<td>true</td>
<td>try</td>
</tr>
<tr>
<td>typeof</td>
<td>uint</td>
<td>ulong</td>
<td>unchecked</td>
</tr>
<tr>
<td>unsafe</td>
<td>ushort</td>
<td>using</td>
<td>using static</td>
</tr>
<tr>
<td>virtual</td>
<td>void</td>
<td>volatile</td>
<td>while</td>
</tr>
</tbody>
</table>

+++ 

### [Contextual Keywords](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/)  
* Can be used only in given *context*, otherwise they are not considered to be keywords
* Can be used without **@**, in case that they are not used in given context*
 
<table style="color:#2a76dd">
<thead>
<tr>
<th></th>
<th></th>
<th></th>
</tr>
</thead>
<tbody>
<tr>
<td>add</td>
<td>alias</td>
<td>ascending</td>
</tr>
<tr>
<td>async</td>
<td>await</td>
<td>descending</td>
</tr>
<tr>
<td>dynamic</td>
<td>from</td>
<td>get</td>
</tr>
<tr>
<td>global</td>
<td>group</td>
<td>into</td>
</tr>
<tr>
<td>join</td>
<td>let</td>
<td>nameof</td>
</tr>
<tr>
<td>orderby</td>
<td>partial (type)</td>
<td>partial (method)</td>
</tr>
<tr>
<td>remove</td>
<td>select</td>
<td>set</td>
</tr>
<tr>
<td>value</td>
<td>var</td>
<td>when (filter condition)</td>
</tr>
<tr>
<td>where (generic type constraint)</td>
<td>where (query clause)</td>
<td>yield</td>
</tr>
</tbody>
</table>

+++

### Literals & Delimiters
* Literals
  * Data inserted in code
  * E.g., `12, 30, 42`
* Delimiters
  * Characters used for code structuralization
  * Curly brackets `{, }`
    * Creates code blocks
    * Used to *impart a scope*
  * Semicolon `;`
    * Delimits statements
    * Statement can be written in multiple lines.
    ```C#
    Console.WriteLine
      (1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10);
    ```
    
+++

### Operators & Comments
* Operators
  * Used to combine multiple expressions
  * E.g., `. () * + -`
* Comments 
  * Line
    ```C#
    // line comment
    ```
  * Block
    ```C#
    /* Comment can be split
    into multiple lines */
    ```
    * **Do not use block comments!**
  * Documenting 
    ```C#
    /// <summary>
    /// Documents class, method, property, ...
    /// </summary>
    public class Test {}
    ```
---
## [Data Types](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/types-and-variables)
* A data type or simply type is a classification of data which tells the compiler or interpreter how the programmer intends to use the data.

* C# recognizes two kinds of data types *value types* and *reference types*.
* **Value type**
  * Variables directly contain their data.
  * Two variables, each have their copy of the data, and it is not possible for operations on one to affect the other.
* **Reference types**
  * Store references to their data, the latter being known as objects. 
  * Two variables can reference the same object, thus it is possible for operations on one variable to affect the object referenced by the other variable.
  
---
## Value Types
* **Simple Types**
  * Signed integral: `sbyte, short, int, long`
  * Unsigned integral: `byte, ushort, uint, ulong`
  * Unicode characters: `char`
  * IEEE floating point: `float, double`
  * High-precision decimal: `decimal`
  * Boolean: `bool`
* **Enum types**
  * User-defined types of the form `enum E {...}`
* **Struct types**
  * User-defined types of the form `struct S {...}`
* **Nullable value types**
  * Extensions of all other value types with a `null` value
  
+++

### Signed Integral
| Type   |    Size | Range                                                              |
| ------ | ------- | ------------------------------------------------------------------ |
| `sbyte`|  8 bits | range from -128 - 127                                              |
| `short`| 16 bits | range from -32,768 - 32,767                                        |
| `int`  | 32 bits | range from -2,147,483,648 - 2,147,483,647                          |
| `long` | 64 bits | range from –9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 |

+++

### Unsigned integral
|  Type   |    Size | Range                                      |
| ------- | ------- | ------------------------------------------ |
| `byte`  |  8 bits | range from 0 - 255                         |
| `ushort`| 16 bits | range from 0 - 65,535                      |
| `uint`  | 32 bits | range from 0 - 4,294,967,295               |
| `ulong` | 64 bits | range from 0 - 18,446,744,073,709,551,615  |

+++

### Floating point
|   Type   |    Size | Range                                                               |
| -------- | ------- | ------------------------------------------------------------------- |
| `float`  | 32 bits | range from $$1.5 × 10^{−45} - 3.4 × 10^{38}$$  7-digit precision    |
| `double` | 64 bits | range from $$5.0 × 10^{−324} - 1.7 × 10^{308}$$  15-digit precision |

+++

### Decimal
|    Type   |    Size  | Range                                                                                         |
| --------- | -------- | --------------------------------------------------------------------------------------------- |
| `decimal` | 128 bits | range is at least $$–7.9 × 10^{−28} - 7.9 × 10^{28}$$ with at least 28-digit precision range |

+++

### Literals notation
* Classical
  * E.g., `127`, `42`, etc...
* Hexadecimal
  * E.g., `0x7F`, `0x2A` etc... 
* Decimal 
  * `'.'` character as a delimiter
  * `'e'` character as an exponent
* Rules
  * If literal contains `'.'`, or `'e'` than data type is decimal
  * Else data type is the smallest one that fits `int, uint, long, ulong`.

+++

### Numerical data types specification
* Using specific character as a suffix

```C#
 Console.WriteLine(1f.GetType());  // Float   (float)
 Console.WriteLine(1d.GetType());  // Double  (doulbe)
 Console.WriteLine(1m.GetType());  // decimal (decimal)
 Console.WriteLine(1u.GetType());  // UInt32  (uint)
 Console.WriteLine(1L.GetType());  // Int64   (long)
 Console.WriteLine(1ul.GetType()); // UInt64  (ulong) 
```
+++?code=/Lectures/Lecture_01/DataTypes/NumericalNotationSample.cs&lang=C#&title=Numerical Notation Sample

[Code sample](/Lectures/Lecture_01/DataTypes/NumericalNotationSample.cs)

+++

### Numerical data types casting
* Transformation of **integral type** to **integral type**:
  * *implicit* in case that *target type* can accommodate the whole range of *source type*
  * *explicit* otherwise
* Transformation of **decimal type** to **decimal type**:
  * `float` can be *implicitly* casted to `double`
  * `double` has to be casted *explicitly* to `float`
* Transformation of **integral type** to **decimal type**:
  * Casting is *implicit* to `float`, `double`, and `decimal`
* Transformation of **decimal type** to **integral type**:
  * Casting has to be *explicit* 
    * *Truncation* can occur
    * *Lost precision*
    
+++

### Arithmetic operations
* `+` addition
* `-` subtraction
* `*` multiplication
* `/` division
* `++` incrementation
* `--` decrementation

+++

#### Byte, sbyte, short, ushort
* 8 and 16 bits types do not have arithmetical operations
  * E.g., `byte, sbyte, short, ushort`
  * Compiler does implicit cast to large type `int, uint`
  ```C#
  short x = 1, y = 1;
  short z = x + y;    // Compile-time error
  ```
  * Solution is to do explicit cast
  ```C#
  short x = 1, y = 1;
  short z = (short)(x + y); // OK
  ```

+++

#### Numerical Overflow
* Overflow of integral types

```C#
int a = int.MinValue;
a--;
Console.WriteLine(a == int.MaxValue); // True
```

* Usage of `checked` keyword or compiler option **/checked+**

```C#
int a = int.MinValue;
var i = checked(a--); // throw OverflowException
Console.WriteLine(i == int.MaxValue);  
```
+++?code=/Lectures/Lecture_01/DataTypes/IntegralOverflowSample.cs&lang=C#&title=Numerical Notation Sample

[Code sample](/Lectures/Lecture_01/DataTypes/IntegralOverflowSample.cs)
  
+++

#### Truncation and precision loss
* Types `float` and `double` are stored in binary form. Therefore, only multiples of 2 are stored precisely.

```C#
float f1 = 0.09f * 100f;
float f2 = 0.09f * 99.999999f;
Assert.False(f1>f2);
```
* `decimal` is stored in decimal form, but it has still a limitted precision

```C#
decimal m = 1M  /  6M;                          // 0.1666666666666666666666666667M
double  d = 1.0 / 6.0;                          // 0.16666666666666666
decimal notQuiteWholeM = m + m + m + m + m + m; // 1.0000000000000000000000000002M
double  notQuiteWholeD = d + d + d + d + d + d; // 0.99999999999999989      
Console.WriteLine(notQuiteWholeM == 1M);        // False
Console.WriteLine(notQuiteWholeD < 1.0);        // True
```
+++?code=/Lectures/Lecture_01/DataTypes/TruncationSample.cs&lang=C#&title=Truncation Sample

[Code sample](/Lectures/Lecture_01/DataTypes/TruncationSample.cs)

+++

#### Decimal Type Equality
* What contains the variable areSame?

```C#
double x = 49.0;
double y = 1 / x;

double calculatedResult = x * y;
double expectedResult = 1.0;

bool areSame = calculatedResult == expectedResult;
```

```C#
public static bool AlmostEqual(double a, double b)
{
  const double tolerance = 0.000000000000001;
  if (a == b)
  {
    return true;
  }
  return Math.Abs(a - b) < tolerance;
}
```

+++?code=/Lectures/Lecture_01/DataTypes/DecimalEqualitySample.cs&lang=C#&title=Decimal Equality Sample

[Code sample](/Lectures/Lecture_01/DataTypes/DecimalEqualitySample.cs)

+++

#### Bitwise operations
| Operator |   Meaning   |      Example     |   Result    |
| -------- | ----------- | ---------------- | ----------- |
|    `~`   |     Not     |         `~0xfU`  | 0xfffffffOU |
|    `&`   |     And     |   `0xf0 & 0x33`  | 0x30        |
|  &#x7c;  |      Or     |`0xf0`&#x7c;`0x33`| 0xf3        |
|    `^`   |     Xor     | `0xff00 ^ 0x00ff`| 0xffff      |
|   `<<`   |  Left shift |  `0x20 << 2`     | 0x80        |
|   `>>`   | Right shift |  `0x20 >> 1`     | 0x10        |
  
+++

### Nullable value types
* Do not have to be declared before they can be used. 
* For each non-nullable value type `T` there is a corresponding nullable value type `T?`, which can hold an additional value, `null`. 
* For instance, `int?` is a type that can hold any 32-bit integer or the value `null`.

+++

### Boolean type
* `System.Boolean`/`bool`
* Used to store logical values - `true` or `false`
* `sizeof(bool) == sizeof(uint8) == sizeof(sbyte)`
* Nothing can be casted to `bool`
* Operators:
  * Equality `==, !=`
  * Conditional operators `&&, ||`
    ```C#
    public bool UseUmbrela(bool rainy, bool sunny, bool windy) {
      return !windy && (rainy || sunny);
    }
    ```
* Often used for *Lazy evaluation* 

+++

### Char
* `System.Char`/`char`
* Literal is denoted by a single-quote, e.g., `'a'`
* Can be cast to integral type
  * *Implicit* cast to `ushort`
  * *Explicit* cast to others

+++

#### Escape Sequences
|   Char  | Meaning                     |   Value  |  
| ------- | --------------------------- | -------- | 
|    `\'` | Apostrophe                  | `0x0027` |
|    `\"` | Quotaion                    | `0x0022` |
|    `\\` | Backslash                   | `0x005C` |
|    `\o` | Null                        | `0x0000` |
|    `\a` | Alert                       | `0x0007` |
|    `\b` | Backspace                   | `0x0008` |
|    `\n` | New line                    | `0x000A` |
|    `\r` | Carriage return             | `0x000D` |
|    `\t` | Horizontal tab              | `0x0009` |
|    `\v` | Vertical tab                | `0x000B` |
| `\u \x` | Unicode char, e.g., \u00a9  | `0x000B` |

+++

###  Reference types
* **Class types**
  * Ultimate base class of all other types: `object`
  * Unicode strings: `string`
  * User-defined types of the form `class C {...}`
* **Interface types**
  * User-defined types of the form `interface I {...}`
* **Array types**
  * Single- and multi-dimensional, for example, `int[]` and `int[,]`
* **Delegate types**
  *User-defined types of the form `delegate int D(...)`
* Supports generics, whereby they can be parameterized with other types.
  
+++

#### Class
* Defines a data structure that contains:
  * Data members (*fields*)
  * Function members (*methods*, *properties*, and others). 
* Class types support single inheritance and polymorphism, mechanisms whereby derived classes can extend and specialize base classes.

+++

#### Struct
* Similar to a class type in that it represents a structure with data members and function members.
* **Unlike classes**, *structs* are value types and do not typically require heap allocation. 
* Struct types do not support user-specified inheritance, and all struct types implicitly inherit from type object.

+++

#### Interface
* Defines a contract as a named set of public function members. 
* A *class* or *struct* that implements an interface must provide implementations of the interface’s function members. 
* An interface may inherit from multiple base interfaces, and a class or struct may implement multiple interfaces.

+++

#### Delegate
* Represents references to methods with a particular parameter list and return type. 
* Makes it possible to treat methods as entities that can be assigned to variables and passed as parameters. 
* Are analogous to function types provided by functional languages. 
  * They are also similar to the concept of function pointers found in some other languages.
  * Unlike function pointers, delegates are object-oriented and **type-safe**.

+++

### Boxing/Unboxing
* C#'s type system is unified such that a value of any type can be treated as an `object`. 
* Every type in C# directly or indirectly derives from the `object` class type, and `object` is the ultimate *base class* of all types. 
* Values of reference types are treated as objects simply by viewing the values as type object. 
* Values of value types are treated as objects by performing **boxing** and **unboxing** operations. 

+++

In the following example, an int value is converted to object and back again to `int`.

```C#
using System;
class BoxingExample
{
    static void Main()
    {
        int i = 123;
        object o = i;    // Boxing
        int j = (int)o;  // Unboxing
    }
}
```

+++?code=/Lectures/Lecture_01/DataTypes/BoxingSample.cs&lang=C#&title=Boxing Sample

[Code sample](/Lectures/Lecture_01/DataTypes/BoxingSample.cs)

+++

### String
* `System.String` / `string`
* Represents sequence of characters
* Reference data type
* Literal is denote by double-quotes. e.g., `"string value"`
* Verbatim string is denote by `@` prefix, e.g., 
```C#
@"Multi-line
string"
```
* Use `string.Empty` to assigned empty strings, never `""`

+++

* String concatenation by `+` operator
  * `string s = "a" + "b"  // ab`
  * Not all operands needs to be strings themselves.
  * Non string operands get called `ToString()` method no them.
  ```C#
  string s = "a" + 5; // a5
  ```
  * For multiple string concatenation operations avoid usage of `+`
    * Use `System.Text.StringBuilder`
    * `s = System.String.Format("{0} times {1} = {2}", i, j, (i*j));`
    * `s = $"{i} times {j} = {i*j}";`

+++

### Array
* Represents fixed length data structure of homogeneous items
* Stored in sequential block of memory
* Declaration:

  ```C#
  char[] characters = new char[5];
  char[] characters = new char[] {'a','b','c'};
  char[] characters = {'a','b','c'};
  ```
* Access to array items:

  ```C#
  characters[0] = 'a';
  characters[1] = 'b';
  for (int I = 0; I < characters.Length; i++)
  {
    Console.WriteLine(characters[i]);
  }
  ```
* Initialization
  * Value types - default value
  * Reference types - `null`
* Array range checked
  * Access out of array range throws `IndexOutOfRangeException`.
  
+++

### Single- and multi-dimensional arrays
* Unlike the types listed above, array types do not have to be declared before they can be used.
* Instead, array types are constructed by following a type name with square brackets. 
* For example:
  * `int[]` is a single-dimensional array of int,
  * `int[,]` is a two-dimensional array of int,
  * `int[][]` is a single-dimensional array of single-dimensional array of int.
  
+++

#### Matrix
* Declared by `[,]`

```C#
int[,] matrix = new int[3,3];
int[,] matrix = new int[,] {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
```
* Item access:

```C#
for (int i = 0; i < matrix.GetLength(0); i++)
  for (int j = 0; j < matrix.GetLength(1); j++)
    matrix[i, j] = i * 3 + j;
```

+++

#### Array of arrays
* Declared by `[][]`

```C#
int[][] matrix = new int[3][];
```
* Item access:

```C#
matrix[i][j] = 5;
```
  
--- 
### Variables
* Variables in C#, including fields, array elements, local variables, and parameters represents storage locations, and every variable has a type that determines what values can be stored in the variable.
* **Non-nullable** value type
  * A value of that exact type
* **Nullable** value type
  * A null value or a value of that exact type
* **Object**
  * A null reference, a reference to an object of any reference type, or a reference to a boxed value of any value type
* **Class** type
  * A null reference, a reference to an instance of that class type, or a reference to an instance of a class derived from that class type

+++

* **Interface** type
  * A null reference, a reference to an instance of a class type that implements that interface type, or a reference to a boxed value of a value type that implements that interface type
* **Array** type
  * A null reference, a reference to an instance of that array type, or a reference to an instance of a compatible array type
* **Delegate** type
  * A null reference or a reference to an instance of a compatible delegate type

+++  

### Stack vs Heap
* **Stack**
  * Allocated block of memory for *local variables, parameters*
* **Heap**
  * Storage for *reference data types*
  * Managed by *Garbage Collector*
* **Rules** for variable assignment:
  * Local variable has to be assigned before reading
  * Method has to be called with all arguments
  * All other values are initialized automatically

+++

#### Default values
|    Type   | Default value  |  
| --------- | -------------- | 
| Reference | `null`         |
| Numerical | `0`            |
| Enums     | `0`            |
| Char      | `'\0'`         |
| Boolean   | `false`        |

```C#
static int y;
static void Main()
{
  int x;
  Console.WriteLine (x);        // Compile-time error
  int[] ints = new int[2];
  Console.WriteLine (ints[0]);  // 0
  Console.WriteLine (y);        // 0
}
```

+++?code=/Lectures/Lecture_01/DataTypes/DefaultValuesSample.cs&lang=C#&title=Default Values Sample

[Code sample](/Lectures/Lecture_01/DataTypes/DefaultValuesSample.cs)
  
---
### Parameters
* Parameters can be passed to a method as:

| Modifier  | Passed as |  Variable definition |
| --------- | --------- |  --------------------|
| (none)    | Value     |  Going in            |
|  ref      | Reference |  Going in            |
|  out      | Reference |  Going out           |

+++

#### Parameter by Value

```C#
static void Foo(StringBuilder fooSB)
{
  fooSB.Append("test");
  fooSB = null;
}
...
StringBuilder sb = new StringBuilder();
Foo (sb);
Console.WriteLine (sb.ToString());      // test
```

+++

#### Parameter by Reference
* Parameter is passed by reference, `ref` keyword

```C#
private static void Foo(ref int p)
{
  p = p + 1;          // Increment p by 1
}
...
int x = 8;
Foo(ref x);           // Ask Foo to deal directly with x
Console.WriteLine(x); // x is now 9
```

+++

#### Parameter as Out
* Similar passing as a reference, `out` keyword, except:
  * Variable does not need to be initialized before method call.
  * Variable needs to be assigned before return from a method.
  
```C#
private static void Split(string name, out string firstNames, out string lastName)
{
  int i = name.LastIndexOf(' ');
  firstNames = name.Substring(0, i);
  lastName = name.Substring(i + 1);
}
...
Split("Stevie Ray Vaughan", out var a, out var b);
Console.WriteLine(a); // Stevie Ray
Console.WriteLine(b); // Vaughan
```

+++

#### Parameter with `params[]`
* Can be used only with the last parameter in a method signature.
* Has to be declared as an array.
* Used to pass multiple variables of the same type.
* Used to create more universal methods with a variable count of parameters.

```C#
private int Sum(params int[] ints)
{
  int sum = 0;
  for (int i = 0; i < ints.Length; i++)
    sum += ints[i]; // Increase sum by ints[i]
    return sum;
}
...
int total = Sum(1, 2, 3, 4);
Console.WriteLine(total); // 10
```

+++

#### Optional parameters
* Do not need to be declared.
* Reduce the number of method overrides. 

```C#
void Foo(int x = 2) { … }
...   
Foo();
```

+++

#### Named parameters
* Usually used with method calls on methods with multiple optional parameters
* Reduce the number of method overrides.

```C#
void Foo(int x = 2, int y = 3) { … }
...
Foo(y:4, x:4);
Foo(y: ++a, x: --a); 
Foo(y: 1);
```

+++?code=/Lectures/Lecture_01/DataTypes/ParametersSample.cs&lang=C#&title= Parameters Sample
@[9-19]
@[22-32]
@[35-47]
@[50-62]
@[65-71]
@[74-83]

[Code sample](/Lectures/Lecture_01/DataTypes/ParametersSample.cs)

---
### Expressions & Operators

#### Expressions
* Returns some value after computation
* The simplest expression is *constant* or *variable*, e.g., `5`
* Expression can be combined using operators
  * `5*4`
  * `(5*4)+1`

+++

#### Operators
* Operators are *unary, binary, ternary*
* *Binary* operators use **infix** notation, operator is in between operands
* **Primary expression**
  * Used to build the language
  * `Math.Log(1)` contains two primary operators `.` and `()`

+++  

#### Void expression
* Do not have a value
* Cannot be combined with other operators
* E.g., `{}, return, etc...`

+++

#### Assigning expression
* E.g., `x=x+5`
* Can be part of another expression
  * `y = 5 * (x = 2);`
* Can be used to initialize multiple variables:
  * `a = b = c = d = e = 0;`
* Combination of operators
  * `x+=5`, the same meaning as `x=x+5`
  
+++

#### Priority and assignment
* Priority is evaluated by the priority of operators
* Operators with the same priority are evaluated starting with the most left one
* Left-associative operators
  * `8/4/2` equals `(8/4)/2`
* Right-associative operators
  * `x = y = 3;`
  
+++

#### Table of operators

| Category        | Operator symbol | Operator name               | Example        | User overloadable |
|-----------------|-----------------|-----------------------------|----------------|-------------------|
| Primary         | .               | Member access               | x.y            | No                |
|                 | -> (unsafe)     | Pointer to struct           | x->y           | No                |
|                 | ()              | Function cal                | x()            | No                |
|                 | []              | Array/Index                 | a[x]           | via indexer       |
|                 | ++              | Post-increment              | x++            | No                |
|                 | --              | Post-decrement              | x--            | No                |
|                 | new             | Create instance             | new x()        | No                |
|                 | stackalloc      | Unsafe stack allocation     | stackalloc(10) | No                |
|                 | typeof          | Get type from identifier    | typeof(int)    | No                |
|                 | checked         | Integral overflow check on  | checked(x)     | No                |
|                 | unchecked       | Integral overflow check off | unchecked(x)   | No                |
|                 | default         | Default value               | default(int)   | No                |
|                 | await           | Await                       | await myTask   | No                |

+++

| Category        | Operator symbol | Operator name               | Example        | User overloadable |
|-----------------|-----------------|-----------------------------|----------------|-------------------|
| Unary           | sizeof          | Get size of struct          | sizeof(int)    | No                |
|                 | +               | Positive value of           | +x             | Yes               |
|                 | -               | Negative value of           | -x             | Yes               |
|                 | !               | Not                         | !x             | Yes               |
|                 | ++              | Pre-increment               | ++x            | Yes               |
|                 | --              | Pre-decrement               | --x            | Yes               |
|                 | ()              | Cast                        | (int)x         | No                |
|                 | * (unsafe)      | Value at address            | *x             | No                |
|                 | &(unsafe)       | Address of value            | &x             | No                |

+++

| Category        | Operator symbol | Operator name               | Example        | User overloadable |
|-----------------|-----------------|-----------------------------|----------------|-------------------|
| Multi-privative | *               | Multiply                    | x * y          | Yes               |
|                 | /               | Divide                      | x / y          | Yes               |
|                 | %               | Remainder                   | x % y          | Yes               |
| Additive        | +               | Add                         | x+y            | Yes               |
|                 | -               | Subtract                    | x-y            | Yes               |
| Shift           | <<              | Shift left                  | x<<y           | Yes               |
|                 | >>              | Shift right                 | x>>y           | Yes               |
| Relational      | <               | Less than                   | x<y            | Yes               |
|                 | >               | Greater than                | x>y            | Yes               |
|                 | <=              | Less than or equals to      | x<=y           | Yes               |
|                 | >=              | Greater than or exuals to   | x>=y           | Yes               |

+++

| Category        | Operator symbol | Operator name               | Example        | User overloadable |
|-----------------|-----------------|-----------------------------|----------------|-------------------|
| Relational      | is              | Type is or is subclass of   | x is y         | No                |
|                 | as              | Type conversion             | x as y         | No                |
| Logical And     | &               | And                         | x & y          | Yes               |
| Logical Xor     | ^               | Exclusive Or                | x ^ y          | Yes               |
| Logical Or      | &#x7c;          | Or                          | x &#x7c; y     | Yes               |
| Conditional And | &&              | Conditional And             | x && y         | Via &             |
| Conditional Or  | &#x7c;&#x7c;    | Conditional or              |x &#x7c;&#x7c; y| Via &             |
| Null coalescing | ??              | Null coalescing             | x ??           | No                |
| Conditional     | ?:              | Conditional                 | isTrue? x : y  | No                |
| Assignment      | =               | Assign                      | x = y          | No                |
|                 | *=              | Multiply self by            | x*=2           | Via *             |

+++

| Category        | Operator symbol | Operator name               | Example        | User overloadable |
|-----------------|-----------------|-----------------------------|----------------|-------------------|
| Assignment      | /=              | Divide self by              | x/=2           | Via /             |
|                 | +=              | Add self by                 | x+=2           | Via +             |
|                 | -=              | Substract from self         | x-=2           | Via -             |
|                 | <<=             | Shift self left by          | x<<=2          | Via <<            |
|                 | >>=             | Shift self right by         | x>>=2          | Via >>            |
|                 | &=              | Add self by                 | x&=2           | Via &             |
|                 | ^=              | Exclusive-Or self by        | x^=2           | Via ^             |
|                 | &#x7c;=         | Or self by                  | x &#x7c;=2     | Via &#x7c;        |
| Lambda          | =>              | Lambda                      | x => x+1       | No                |

---
### Statements 
#### Statements- Expressions

```C#
 // Declare variables with declaration statements:
 string s;
 int x, y;
 System.Text.StringBuilder sb;

 x = 1 + 2;                // Assignment expression
 x++;                      // Increment expression
 y = Math.Max(x, 5);       // Assignment expression
 Console.WriteLine(y);     // Method call expression
 sb = new StringBuilder(); // Assignment expression

 new StringBuilder();      // Object instantiation expression
 new StringBuilder();      // Legal, but useless
 new string('c', 3);       // Legal, but useless
 x.Equals(y);              // Legal, but useless
```

+++

#### Statements - Selection
* Used to define a program control flow
* `if`, `switch` keywords
* Conditional (ternary) operand `?:`

* `if`

```C#
if (5 < 2 * 3)
{
  Console.WriteLine("true");
  Console.WriteLine("Let's move on!");
}
else
{
  Console.WriteLine("False"); // False        
}
```

+++

* `switch`

```C#
switch (cardNumber)
{
  case 10:
  case 13:
    Console.WriteLine("King");
    break;
  case 12:
    Console.WriteLine("Queen");
    break;
  case 11:
    Console.WriteLine("Jack");
    break;
  case -1 :                         // Joker is −1
    goto case 12;                   // In this game joker counts as queen
  default:                          // Executes for any other cardNumber
    Console.WriteLine(cardNumber);
    break;
}
```

+++

#### Statements - Cycles
* `while`

```C#
int i = 0;
while (i < 3)
{
  Console.WriteLine(i);
  i++;
}
```

* `do {} while();` runs once at minimal

```C#
i = 0;
do
{
  Console.WriteLine(i);
  i++; 
}
while (i < 3);
```

+++

* `for`

```C#
for (int i = 0, prevFib = 1, curFib = 1; i < 10; i++)
{
  Console.WriteLine(prevFib);
  int newFib = prevFib + curFib;
  prevFib = curFib; curFib = newFib;
}
```

* `foreach`
  * block is evaluated for each item in a given `IEnumerable` sequence
  
```C#
foreach (char c in "beer") // c is the iteration variable
{
  Console.WriteLine(c);
}
```

+++

#### Statements - Jump statements
* `break`,`continue`,`goto`,`return`,`throw`
* `break` 
  * ends an iteration of a cycle
  
```C#
int x = 0;
while (true)
{
  if (x++ > 5)
  {  
    break; // break from the loop
  } 
}
// execution continues here after break
```

+++

* `continue`
  * end one cycle iteration
  
```C#
for (int i = 0; i < 10; i++)
{
  if ((i % 2) == 0) // If i is even,
  {
    continue; // continue with next iteration
  }
  Console.Write(i + " ");
}
```

+++

* `goto`
  * switch program flow to selected label
  
```C#
int i = 1;
startLoop:
if (i <= 5)
{
  Console.Write(i + " ");
  i++;
  goto startLoop;
}
```

+++

* `return`
  * return from a method call
  * return value based on a method return type
  
```C#
public decimal Return(decimal d)
{
  decimal p = d * 100m;
  return p; // Return to the calling method with value
}
```

+++

* `throw`
  * handles for exceptional situations
  
```C#
private static void Throw(object obj)
{
  if (obj == null)
  {
    throw new ArgumentNullException("obj");
  }
}
```
+++

#### Statements - Others
* `using`
  * Encapsulated usage of disposable resource
  
```C#
using (var file = File.Open(@"c:\Filepath.txt", FileMode.OpenOrCreate))
{
  file.Write(buffer, offset, count);
} // file.Dispose() is called here
```
+++

#### Statements - Lock
* `lock`
  * For safe access to resource from concurrent context
  * Simplification of Monitor synchronization primitive

```C#
lock(@lock)
{
  i = i++;
}
```

---
### Namespaces
* Groups classes and interfaces to named groups
* Namespace `System.Security.Cryptography` contains class e.g., RSA.
* Usage of types from given namespace

```C#
System.Security.Cryptography.RSA rsa = System.Security.Cryptography.RSA.Create();
```
* Directive `using`

```C#
using System.Security.Cryptography;
public class Namespaces
{
  public void Method()
  {
    RSA rsa = RSA.Create(); // Don't need fully qualified name
  }
}
```

+++

#### Namespaces - Definition
* Keyword `namespace`

```C#
namespace Outer.Middle.Inner
{
  class Class1 { ... }
  class Class2 { ... }
}
```

* Same as:

```C#
namespace Outer
{  
  namespace Middle
  {
    namespace Inner
    {
      class Class1 { ... }
      class Class2 { ... }
    }
  }
}
```

+++

#### Namespaces - Rules
* Names declared in an outer scope are implicitly imported into inner one.

```C#
namespace Outer
{
  namespace Middle
  {
    internal class Class1 { ... }
  
    namespace Inner
    {
      internal class Class2 : Class1 { ... }
    }
  }
}
```

+++

* Namespace hiding
  * In case that in outer namespace is a type with the same name as in the inner one, the inner "wins".
  
```C#
namespace Outer
{
  internal class Foo { ... }
  namespace Inner
  {
    internal class Foo { ... }
    internal class Test
    {
      private Foo f1; // = Outer.Inner.Foo
      private Outer.Foo f2; // = Outer.Foo
    }
  }
}
``` 

+++ 

* Repetition of namespaces
  * Namespace name can be repeated until a collision of names of inner types occurs. 
  * The same namespace can be declared in multiple places.
  
```C#
namespace Outer.Middle.Inner
{
  class Class1 {}
}
...
namespace Outer.Middle.Inner
{
  class Class2 { }
}
```

+++

* Inner `using` directives
  * `using` can be used in inner namespace to limit its scope
  
```C#
namespace N1
{
  class Class1 { }
}
namespace N2
{
  using N1;
  class Class2 : Class1 { }
}
namespace N2
{
  class Class3 : Class1 { } // Compile-time error
}
```  

---
## References
[C# 7.0 in a Nutshell: The Definitive Reference](https://www.amazon.com/C-7-0-Nutshell-Definitive-Reference/dp/1491987650)

[C# Keywords](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/) 

[Data Types](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/types-and-variables)

And ... a lot of Google index images...
