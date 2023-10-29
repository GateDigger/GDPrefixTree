using System;
using System.IO;
using System.Text;
using System.Linq;

namespace GDPrefixTree
{
    internal class Program
    {
        static readonly Func<WordCountingNode> Factory = () => new WordCountingNode();
        static void Main(string[] args)
        {
            string file = "Insert full file path here";
            GDPrefixTree<char, int> trie = BuildIndex(file);
            if (trie != null)
                QueryWordCount(trie);
        }
        
        static int increment(int x) => x + 1;
        static GDPrefixTree<char, int> BuildIndex(string file)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine("Invalid path; aborting.");
                return null;
            }


            GDPrefixTree<char, int> result = new GDPrefixTree<char, int>(new WordCountingNode());


            byte[] buffer = new byte[1];
            char current;

            StringBuilder sb = new StringBuilder(128);

            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                while (fs.Read(buffer, 0, 1) > 0)
                {
                    current = NormalizeCharacter((char)buffer[0]);
                    if (!char.IsLetter(current))
                    {
                        result.SetOrUpdate(new StringKey(sb.ToString()), Factory, 0, increment);
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(current);
                    }
                }
                if (sb.Length > 0)
                    result.SetOrUpdate(new StringKey(sb.ToString()), Factory, 0, increment);
            }

            Console.WriteLine("Total word count in subtree: " + result.Aggregate(0, (sum, value) => value.IsPathing ? sum : sum + value.Value));
            Console.WriteLine("Unique word count in subtree: " + result.Aggregate(0, (sum, value) => value.IsPathing ? sum : sum + 1));
            Console.WriteLine("Total node count in subtree: " + result.Aggregate(0, (sum, value) => sum + 1));

            return result;
        }

        static void QueryWordCount(GDPrefixTree<char, int> trie)
        {
            Console.WriteLine();
            Console.WriteLine("Input keys to retrieve subtree statistics...");

            string input;
            IGDNode<char, int> node;

            while (true)
            {
                Console.WriteLine();
                input = Console.ReadLine();
                input = NormalizeString(input);
                if (GDPrefixTree<char, int>.TraverseReadOnly(trie.Root, new StringKey(input), out node))
                {
                    if (!node.IsPathing)
                        Console.WriteLine("\"" + input + "\" was found " + node.Value + (node.Value == 1 ? " time." : " times."));
                    else
                        Console.WriteLine("\"" + input + "\" was not found.");

                    Console.WriteLine("Subtree statistics for \"" + input + "\":");
                    Console.WriteLine("Total words: " + node.Aggregate(0, (sum, value) => value.IsPathing ? sum : sum + value.Value));
                    Console.WriteLine("Unique words: " + node.Aggregate(0, (sum, value) => value.IsPathing ? sum : sum + 1));
                    Console.WriteLine("Subtree size: " + node.Aggregate(0, (sum, value) => sum + 1));
                }
                else
                {
                    Console.WriteLine("Prefix tree readonly traversal failed");
                }
            }
        }



        static char NormalizeCharacter(char c)
        {
            if ('A' <= c && c <= 'Z')
            {
                c = char.ToLower(c);
            }
            else if ('a' <= c && c <= 'z')
            {
                ;
            }
            else
            {
                c = '{';
            }
            return c;
        }

        static string NormalizeString(string s)
        {
            int len = s.Length;
            char[] resultArray = new char[len];
            for (len--; len > -1; len--)
                resultArray[len] = NormalizeCharacter(s[len]);
            return new string(resultArray);
        }
    }
}