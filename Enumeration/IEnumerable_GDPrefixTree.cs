using System.Collections;
using System.Collections.Generic;

namespace GDPrefixTree
{
    public partial class GDPrefixTree<S, T> : IEnumerable<IGDNode<S, T>>
    {
        IEnumerator<IGDNode<S, T>> IEnumerable<IGDNode<S, T>>.GetEnumerator()
        {
            return Root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Root.GetEnumerator();
        }
    }
}