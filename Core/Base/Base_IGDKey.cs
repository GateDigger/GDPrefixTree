namespace GDPrefixTree
{
    public interface IGDKey<S>
    {
        /// <summary>
        /// Retrieves the current digit of the key
        /// </summary>
        /// <param name="digit">The current digit, if the retrieval is successful</param>
        /// <returns>Whether the retrieval is successful; false after StepForward() passes the last digit of the key</returns>
        public bool GetCurrentDigit(out S digit);

        /// <summary>
        /// Moves past the current digit of the key
        /// </summary>
        public void StepForward();
    }
}