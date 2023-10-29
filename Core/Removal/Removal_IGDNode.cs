namespace GDPrefixTree
{
    public partial interface IGDNode<S, T>
    {
        /// <summary>
        /// Attempts to remove the node from a specified address
        /// </summary>
        /// <param name="address">The address of the node to remove</param>
        /// <returns>Whether the attempt is successful</returns>
        bool RemoveChildNode(S address);

        /// <summary>
        /// Attempts to remove the node from a specified address
        /// </summary>
        /// <param name="address">The address of the node to remove</param>
        /// <param name="node">The removed node, if the attempt is successful</param>
        /// <returns>Whether the attempt is successful</returns>
        bool RemoveChildNode(S address, out IGDNode<S, T> node);
    }
}