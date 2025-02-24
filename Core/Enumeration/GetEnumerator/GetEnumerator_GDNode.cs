using System.Collections;
using System.Collections.Generic;

namespace GDPrefixTree
{
    public abstract class GDNode<S, T> : IGDNode<S, T>
    {
        public abstract bool IsPathing
        {
            get;
            set;
        }
        public abstract T Value
        {
            get;
            set;
        }

        /// <summary>
        /// Implements IGDNode.AppendChildNode
        /// </summary>
        public abstract bool AppendChildNode(S address, IGDNode<S, T> node);

        /// <summary>
        /// Implements IGDNode.FindChildNode
        /// </summary>
        public abstract bool FindChildNode(S address, out IGDNode<S, T> node);

        /// <summary>
        /// Implements IGDNode.RemoveChildNode
        /// </summary>
        public abstract bool RemoveChildNode(S address);

        /// <summary>
        /// Implements IGDNode.RemoveChildNode
        /// </summary>
        public abstract bool RemoveChildNode(S address, out IGDNode<S, T> node);

        /// <summary>
        /// Implements IGDNode.GetChildNodes
        /// </summary>
        public abstract IEnumerable<IGDNode<S, T>> GetChildNodes();

        /// <summary>
        /// Yields an enumerator for the entire tree except the root
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<IGDNode<S, T>> GetEnumerator()
        {
            //Thanks to Michael Liu;
            //https://stackoverflow.com/questions/2055927/ienumerable-and-recursion-using-yield-return

            Stack<IEnumerator<IGDNode<S, T>>> enumeratorStack = new Stack<IEnumerator<IGDNode<S, T>>>();
            IEnumerator<IGDNode<S, T>> currentEnumerator = GetChildNodes().GetEnumerator();


        MOVENEXT:
            if (!currentEnumerator.MoveNext())
                goto POP;

            if (currentEnumerator.Current == null)
                goto MOVENEXT;

            yield return currentEnumerator.Current;
            enumeratorStack.Push(currentEnumerator);
            currentEnumerator = currentEnumerator.Current.GetChildNodes().GetEnumerator();
            goto MOVENEXT;


        POP:
            currentEnumerator.Dispose();

            if (enumeratorStack.Count == 0)
                goto BREAK;

            currentEnumerator = enumeratorStack.Pop();
            goto MOVENEXT;


        BREAK:
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}