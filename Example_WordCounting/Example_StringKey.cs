namespace GDPrefixTree
{
    public class StringKey : IGDKey<char>
    {
        readonly string key;
        int currentIndex;
        public StringKey(string s)
        {
            key = s;
            currentIndex = 0;
        }

        public bool GetCurrentDigit(out char digit)
        {
            if (currentIndex < key.Length)
            {
                digit = key[currentIndex];
                return true;
            }
            else
            {
                digit = default(char);
                return false;
            }
        }

        public void StepForward()
        {
            if (currentIndex < key.Length)
                currentIndex++;
        }

        public override string ToString()
        {
            return "Key{" + key.ToString() + "; " + currentIndex.ToString() + "}";
        }
    }
}