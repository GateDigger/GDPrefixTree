namespace GDPrefixTree
{
    public partial class GDPrefixTree<S, T>
    {
        /// <summary>
        /// Attempts to apply a transformation to the value stored under a supplied key
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="phi">The transformation</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool Update(IGDKey<S> key, Func<T, T> phi)
        {
            IGDNode<S, T> node;
            if (TraverseReadOnly(key, out node) && !node.IsPathing)
            {
                node.Value = phi(node.Value);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to apply a transformation to the value stored under a supplied key
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="oldValue">The original value</param>
        /// <param name="phi">The transformation</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool Update(IGDKey<S> key, out T oldValue, Func<T, T> phi)
        {
            IGDNode<S, T> node;
            if (TraverseReadOnly(key, out node) && !node.IsPathing)
            {
                node.Value = phi(oldValue = node.Value);
                return true;
            }
            else
            {
                oldValue = default(T);
                return false;
            }
        }

        /// <summary>
        /// Attempts to apply a transformation to the value stored under a supplied key
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="phi">The transformation</param>
        /// <param name="newValue">The new value</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool Update(IGDKey<S> key, Func<T, T> phi, out T newValue)
        {
            IGDNode<S, T> node;
            if (TraverseReadOnly(key, out node) && !node.IsPathing)
            {
                newValue = node.Value = phi(node.Value);
                return true;
            }
            else
            {
                newValue = default(T);
                return false;
            }
        }

        /// <summary>
        /// Attempts to apply a transformation to the value stored under a supplied key
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="oldValue">The original value</param>
        /// <param name="phi">The transformation</param>
        /// <param name="newValue">The new value</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool Update(IGDKey<S> key, out T oldValue, Func<T, T> phi, out T newValue)
        {
            IGDNode<S, T> node;
            if (TraverseReadOnly(key, out node) && !node.IsPathing)
            {
                newValue = node.Value = phi(oldValue = node.Value);
                return true;
            }
            else
            {
                newValue = oldValue = default(T);
                return false;
            }
        }

        /// <summary>
        /// Attempts to apply a transformation to the value stored under a supplied key; Uses a default value if no stored value is found
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="nodeFactory">A factory for missing nodes</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="phi">The transformation</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool SetOrUpdate(IGDKey<S> key, Func<IGDNode<S, T>> nodeFactory, T defaultValue, Func<T, T> phi)
        {
            IGDNode<S, T> node;
            if (TraverseReadWrite(key, nodeFactory, out node))
            {
                node.Value = phi(node.IsPathing ? defaultValue : node.Value);
                node.IsPathing = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to apply a transformation to the value stored under a supplied key; Uses a default value if no stored value is found
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="nodeFactory">A factory for missing nodes</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="oldValue">The original value</param>
        /// <param name="phi">The transformation</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool SetOrUpdate(IGDKey<S> key, Func<IGDNode<S, T>> nodeFactory, T defaultValue, out T oldValue, Func<T, T> phi)
        {
            IGDNode<S, T> node;
            if (TraverseReadWrite(key, nodeFactory, out node))
            {
                node.Value = phi(oldValue = node.IsPathing ? defaultValue : node.Value);
                node.IsPathing = false;
                return true;
            }
            else
            {
                oldValue = default(T);
                return false;
            }
        }

        /// <summary>
        /// Attempts to apply a transformation to the value stored under a supplied key; Uses a default value if no stored value is found
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="nodeFactory">A factory for missing nodes</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="phi">The transformation</param>
        /// <param name="newValue">The new value</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool SetOrUpdate(IGDKey<S> key, Func<IGDNode<S, T>> nodeFactory, T defaultValue, Func<T, T> phi, out T newValue)
        {
            IGDNode<S, T> node;
            if (TraverseReadWrite(key, nodeFactory, out node))
            {
                newValue = node.Value = phi(node.IsPathing ? defaultValue : node.Value);
                node.IsPathing = false;
                return true;
            }
            else
            {
                newValue = default(T);
                return false;
            }
        }

        /// <summary>
        /// Attempts to apply a transformation to the value stored under a supplied key; Uses a default value if no stored value is found
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="nodeFactory">A factory for missing nodes</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="oldValue">The original value</param>
        /// <param name="phi">The transformation</param>
        /// <param name="newValue">The new value</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool SetOrUpdate(IGDKey<S> key, Func<IGDNode<S, T>> nodeFactory, T defaultValue, out T oldValue, Func<T, T> phi, out T newValue)
        {
            IGDNode<S, T> node;
            if (TraverseReadWrite(key, nodeFactory, out node))
            {
                newValue = node.Value = phi(oldValue = node.IsPathing ? defaultValue : node.Value);
                node.IsPathing = false;
                return true;
            }
            else
            {
                newValue = oldValue = default(T);
                return false;
            }
        }
    }
}