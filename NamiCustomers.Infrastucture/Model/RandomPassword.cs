namespace NamiCustomers.Infrastucture.Model
{
    public class RandomPassword
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
