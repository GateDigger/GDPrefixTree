using System.Collections.Generic;

namespace GDPrefixTree
{
    public partial interface IGDNode<S, T> : IEnumerable<IGDNode<S, T>>
    {
        IEnumerable<IGDNode<S, T>> GetChildNodes();
    }
}