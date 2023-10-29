namespace GDPrefixTree
{
    public partial interface IGDNode<S, T>
    {
        /// <summary>
        /// Signifies whether the object serves as a pathing node or whether it is meant to store a value
        /// </summary>
        bool IsPathing
        {
            get;
            set;
        }

        /// <summary>
        /// The value stored within the object, if !IsPathing
        /// </summary>
        T Value
        {
            get;
            set;
        }

        /// <summary>
        /// Appends a node behind a specified address digit
        /// </summary>
        /// <param name="address">Digit of a key</param>
        /// <param name="node">The node for appendage</param>
        /// <returns>Whether the operation is successful</returns>
        bool AppendChildNode(S address, IGDNode<S, T> node);

        /// <summary>
        /// Searches for a node behind a specified address digit
        /// </summary>
        /// <param name="address">Digit of a key</param>
        /// <param name="node">The found node, if the search succeeds</param>
        /// <returns>Whether the search is successful</returns>
        bool FindChildNode(S address, out IGDNode<S, T> node);
    }
}