using System;

namespace GDPrefixTree
{
    /// <summary>
    /// GDPrefixTree is GateDigger's implementation of a generic prefix tree structure
    /// </summary>
    /// <typeparam name="S">The type of digits/atoms in keys</typeparam>
    /// <typeparam name="T">The type of stored values</typeparam>
    public partial class GDPrefixTree<S, T>
    {
        /// <summary>
        /// Instantiates PrefixTree<S, T> (with a mandatory root node)
        /// </summary>
        /// <param name="root">A root node</param>
        /// <exception cref="ArgumentNullException">Don't pass null as root</exception>
        public GDPrefixTree(IGDNode<S, T> root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));
            Root = root;
        }

        /// <summary>
        /// The root node of the structure
        /// </summary>
        public IGDNode<S, T> Root
        {
            get;
            private set;
        }

        bool TraverseReadOnly(IGDKey<S> key, out IGDNode<S, T> value)
        {
            return TraverseReadOnly(Root, key, out value);
        }

        /// <summary>
        /// Attempts to traverse a tree from currentNode along the supplied key
        /// </summary>
        /// <param name="currentNode">The starting node</param>
        /// <param name="key">The traversal key object</param>
        /// <param name="resultNode">The resulting node, if the attempt is successful</param>
        /// <returns>Whether the attempt is successful</returns>
        /// <exception cref="ArgumentNullException">Don't pass null as currentNode</exception>
        public static bool TraverseReadOnly(IGDNode<S, T> currentNode, IGDKey<S> key, out IGDNode<S, T> resultNode)
        {
            IGDNode<S, T> nextNode;

            S keyDigit;

            if (currentNode == null)
                throw new ArgumentNullException(nameof(currentNode));

            while (true)
            {
                if (!key.GetCurrentDigit(out keyDigit))
                {
                    resultNode = currentNode;
                    return true;
                }

                if (!currentNode.FindChildNode(keyDigit, out nextNode))
                {
                    //If this goes through, TraverseReadWrite can finish the traversal process based on the result node and the current state of the key
                    resultNode = currentNode;
                    return false;
                }

                currentNode = nextNode;
                key.StepForward();
            }
        }

        bool TraverseReadWrite(IGDKey<S> key, Func<IGDNode<S, T>> nodeFactory, out IGDNode<S, T> resultNode)
        {
            return TraverseReadWrite(Root, key, nodeFactory, out resultNode);
        }

        /// <summary>
        /// Attempts to traverse a tree from currentNode along the supplied key; Creates missing nodes
        /// </summary>
        /// <param name="currentNode">The starting node</param>
        /// <param name="key">The traversal key object</param>
        /// <param name="nodeFactory">A factory for missing nodes</param>
        /// <param name="resultNode">The resulting node, if the attempt is successful</param>
        /// <returns>Whether the attempt is successful</returns>
        /// <exception cref="ArgumentNullException">Don't pass null as currentNode</exception>
        public static bool TraverseReadWrite(IGDNode<S, T> currentNode, IGDKey<S> key, Func<IGDNode<S, T>> nodeFactory, out IGDNode<S, T> resultNode)
        {
            IGDNode<S, T> nextNode;

            S keyDigit;

            if (currentNode == null)
                throw new ArgumentNullException(nameof(currentNode));

            while (true)
            {
                if (!key.GetCurrentDigit(out keyDigit))
                {
                    resultNode = currentNode;
                    return true;
                }

                if (!currentNode.FindChildNode(keyDigit, out nextNode))
                {
                    break;
                }

                currentNode = nextNode;
                key.StepForward();
            }

            while (true)
            {
                nextNode = nodeFactory();

                if (!currentNode.AppendChildNode(keyDigit, nextNode))
                {
                    resultNode = currentNode;
                    return false;
                }

                currentNode = nextNode;
                key.StepForward();

                if (!key.GetCurrentDigit(out keyDigit))
                {
                    resultNode = currentNode;
                    return true;
                }
            }
        }

        /// <summary>
        /// Attempts to retrieve the value stored under a specified key
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="value">The value, if the attempt is successful</param>
        /// <returns>Whether the attempt is successful</returns>
        public bool Get(IGDKey<S> key, out T value)
        {
            IGDNode<S, T> node;
            if (TraverseReadOnly(key, out node))
            {
                value = node.Value;
                return !node.IsPathing;
            }
            else
            {
                value = default(T);
                return false;
            }
        }

        /// <summary>
        /// Attempts to set a value to a specified key; if the key does not exist in the tree, it will be created
        /// </summary>
        /// <param name="key">The key object</param>
        /// <param name="nodeFactory">A factory for missing nodes</param>
        /// <param name="value">The value to set</param>
        /// <returns>Whether the operation is successful</returns>
        public bool Set(IGDKey<S> key, Func<IGDNode<S, T>> nodeFactory, T value)
        {
            IGDNode<S, T> node;
            if (TraverseReadWrite(key, nodeFactory, out node))
            {
                node.Value = value;
                node.IsPathing = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}