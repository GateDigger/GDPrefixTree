namespace GDPrefixTree
{
    public partial interface IGDNode<S, T> : IEnumerable<IGDNode<S, T>>
    {
        public abstract IEnumerable<IGDNode<S, T>> GetChildNodes();
    }
}