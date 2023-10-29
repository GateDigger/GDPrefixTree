using System.Collections.Generic;

namespace GDPrefixTree
{
    public partial interface IGDNode<S, T> : IEnumerable<IGDNode<S, T>>
    {
        /// <summary>
        /// Exposes the collection of all immediate descendants of the node
        /// </summary>
        IEnumerable<IGDNode<S, T>> GetChildNodes();
    }
}