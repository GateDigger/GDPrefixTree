using System.Collections.Generic;

namespace GDPrefixTree
{
    public class WordCountingNode : GDNode<char, int>
    {
        public const string CHARSET = "abcdefghijklmnopqrstuvwxyz{";
        public const int SHIFTCONST = 'a';

        int value;
        IGDNode<char, int>[] childNodes;
        public WordCountingNode()
        {
            value = -1;
            childNodes = new IGDNode<char, int>[CHARSET.Length];
        }

        public override bool IsPathing
        {
            get
            {
                return value == -1;
            }
            set
            {
                if (value)
                    this.value = -1;
            }
        }

        public override int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        public override bool AppendChildNode(char keyDigit, IGDNode<char, int> node)
        {
            //keydigit is assumed to be normalized already
            childNodes[keyDigit - SHIFTCONST] = node;
            return true;
        }

        public override bool FindChildNode(char keyDigit, out IGDNode<char, int> node)
        {
            //keydigit is assumed to be normalized already
            node = childNodes[keyDigit - SHIFTCONST];
            return node != null;
        }

        public override bool RemoveChildNode(char keyDigit)
        {
            //keydigit is assumed to be normalized already
            childNodes[keyDigit - SHIFTCONST] = null;
            return true;
        }

        public override bool RemoveChildNode(char keyDigit, out IGDNode<char, int> node)
        {
            //keydigit is assumed to be normalized already
            node = childNodes[keyDigit - SHIFTCONST];
            childNodes[keyDigit - SHIFTCONST] = null;
            return true;
        }

        public override IEnumerable<IGDNode<char, int>> GetChildNodes()
        {
            return childNodes;
        }

        public override string ToString()
        {
            return IsPathing ? "Node{}" : ("Node{" + value.ToString() + "}");
        }
    }
}