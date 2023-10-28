# GDPrefixTree
is GateDigger's generic implementation of a prefix tree.

## Overview
### Core
The core part of the project is split up into subfolders in order to be as modular as possible.
- Core/Base - neccessary functionality
  - Provides
    - Get
    - Set
    - Static traversal methods
  - Requires
    - Implementation of IGDKey&lt;S&gt;
    - Implementation of IGDNode&lt;S, T&gt;
      - IsPathing
      - Value
      - AppendChildNode
      - FindChildNode
- Core/Update - smart updates of values
  - Provides
    - Update
    - SetOrUpdate
- Core/Removal - value deletion
  - Provides
    - Remove
    - RemoveBranch
  - Requires
    - Additional implementation of IGDNode&lt;S, T&gt;
      - RemoveChildNode
- Core/Enumeration - systematic enumeration of nodes
  - Provides
    - GDPrefixTree implementation of IEnumerable&lt;S, T&gt;
    - Extension of IGDNode&lt;S, T&gt; by IEnumerable&lt;IGDNode&lt;S, T&gt;&gt;
  - Requires
    - Implementation of IEnumerable&lt;IGDNode&lt;S, T&gt;&gt; through IGDNode&lt;S, T&gt;
- Core/Enumeration/GetEnumerator - absorbtion of interface IGDNode&lt;S, T&gt; into abstract class GDNode&lt;S, T&gt;
  - Provides
    - Implementation of IEnumerable&lt;S, T&gt;
  - Requires
    - Implementation equivalent to standard IGDNode&lt;S, T&gt;
    - Implementation of GetChildNodes

### The example
Core/Example_WordCounting implements IGDKey&lt;char&gt; and GDNode&lt;char, int&gt;. Program.cs contains a script which reads through a text file and builds a prefix tree which counts word occurences. The user can then query the tree.

## License

MIT License

Copyright (c) 2023 GateDigger

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
