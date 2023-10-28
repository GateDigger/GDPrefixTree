namespace GDPrefixTree
{
    public partial class GDPrefixTree<S, T>
    {
        /// <summary>
        /// Attempts to remove the value stored under a specified key
        /// </summary>
        /// <param name="key">The key object</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool Remove(IGDKey<S> key)
        {
            IGDNode<S, T> node;
            if (TraverseReadOnly(key, out node) && !node.IsPathing)
            {
                node.IsPathing = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to remove the value stored under a specified key
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="value">The removed value</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool Remove(IGDKey<S> key, out T value)
        {
            IGDNode<S, T> node;
            if (TraverseReadOnly(key, out node) && !node.IsPathing)
            {
                value = node.Value;
                node.IsPathing = true;
                return true;
            }
            else
            {
                value = default(T);
                return false;
            }
        }

        /// <summary>
        /// Attempts to remove a branch of a node specified by a key
        /// </summary>
        /// <param name="key">The key object leading to a node</param>
        /// <param name="digit">The address digit of the branch to remove</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool RemoveBranch(IGDKey<S> key, S digit)
        {
            IGDNode<S, T> node;
            if (TraverseReadOnly(key, out node))
            {
                return node.RemoveChildNode(digit);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to remove a branch of a node specified by a key
        /// </summary>
        /// <param name="key">The key object leading to a node</param>
        /// <param name="digit">The address digit of the branch to remove</param>
        /// <param name="removedNode">The node beyond the removed branch</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool RemoveBranch(IGDKey<S> key, S digit, out IGDNode<S, T> removedNode)
        {
            IGDNode<S, T> node;
            if (TraverseReadOnly(key, out node))
            {
                return node.RemoveChildNode(digit, out removedNode);
            }
            else
            {
                removedNode = null;
                return false;
            }
        }
    }
}