namespace Payoneer.Payoneer.Hotels.Model
{
    public static class AspectPriorities
    {
        public const int Correlation = 5;
        public const int Log = 10;
        public const int Cache = 15;
        public const int Retry = 20;
    }
}
