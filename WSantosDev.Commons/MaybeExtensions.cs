namespace WSantosDev.Commons
{
    public static class MaybeExtensions
    {
        public static Maybe<T> AsMaybe<T>(ref this T? value) where T : struct
        {
            if (value is null)
                return new Maybe<T>.None();

            return new Maybe<T>.Some(value.Value);
        }
    }
}
