namespace TeaNotes.Email
{
    public class ConfirmationCodeGenerator
    {
        private readonly Random _random = new();
        private readonly int _length = 6;

        public string Generate()
        {
            return string.Concat(Enumerable.Range(0, _length).Select(_ => _random.Next(10)));
        }
    }
}
