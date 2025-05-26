namespace NamiCustomers.Infrastucture.Utilities
{
    public class RandomPasswordUtility
    {
        private readonly Random random = new Random();

        public string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()).Trim();
        }
    }
}
