namespace WSantosDev.Commons
{
    public abstract class Maybe<T>
    {
        private Maybe()
        {
        }

        public sealed class Some : Maybe<T>
        {
            public T Value { get; }
            public Some(T value) => Value = value;
            
            public static implicit operator T (Some some) =>
                some.Value;
        }

        public sealed class None : Maybe<T>
        {
            public None() { }
        }

        public static implicit operator T(Maybe<T> value)
        {
            return value switch
            {
                Some some => some.Value,
                _ => throw new ArgumentException(default, nameof(value))
            };
        }

        public static implicit operator Maybe<T>(T? value)
        {
            if (value is null)
                return new None();

            return new Maybe<T>.Some(value);
        }

        public void Match(Action<T> some, Action none)
        {
            if (this is Some s)
            {
                some(s.Value);
                return;
            }

            none();
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            if (this is Some s)
                return some(s.Value);

            return none();
        }

        #region Equality
        
        public static bool operator ==(Maybe<T> left, Maybe<T> right) =>
            Equals(left, right);

        public static bool operator !=(Maybe<T> left, Maybe<T> right) =>
            !Equals(left, right);

        public override bool Equals(object? obj)
        {
            if(obj is null)
                return false;
            
            if(obj is Maybe<T> other)
                return Equals(other);

            return false;
        }

        public bool Equals(Maybe<T> other)
        {
            if (this is None && other is None)
                return true;

            if (this is Some left && other is Some right)
                return left.Value!.Equals(right.Value);

            return false;
        }

        public override int GetHashCode()
        {
            if(this is Some some)
                return HashCode.Combine(some.GetHashCode());

            return 0;
        }

        #endregion
    }
}