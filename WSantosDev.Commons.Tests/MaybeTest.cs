namespace WSantosDev.Commons.Tests
{
    public class MaybeTest
    {
        [Fact]
        public void ConvertFromNullableToSome()
        {
            //Arrange
            string? nullableString = "string";
            
            //Act
            Maybe<string> maybe = nullableString;
            
            //Assert
            Assert.Equal(nullableString, maybe);
        }

        [Fact]
        public void ConvertFromNullToNone()
        {
            //Arrange
            string? nullableString = null;

            //Act
            Maybe<string> maybe = nullableString;

            //Assert
            Assert.True(maybe is Maybe<string>.None);
        }

        [Fact]
        public void ThrowExceptionWhenNoneTriesToGetValue()
        {
            //Arrange
            string? nullableString = null;

            //Act
            Maybe<string> maybe = nullableString;

            //Assert
            Assert.Throws<ArgumentException>(() => string.IsNullOrWhiteSpace(maybe));
        }

        [Theory]
        [InlineData(null, -1)]
        [InlineData(3, 13)]
        public void MatchSuccessfully(int? input, int expected)
        {
            //Arrange
            int result = 10;
            
            //Act
            Maybe<int> maybe = input.AsMaybe();
            maybe.Match((value) => result += value, () => result = -1);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 10, true)]
        [InlineData(20, 0, false)]
        [InlineData(null, null, true)]
        public void CompareEqualSuccessfully(int? left, int? right, bool expected)
        {
            //Act
            var leftMaybe = left.AsMaybe();
            var rightMaybe = right.AsMaybe();

            //Assert
            Assert.Equal(expected, leftMaybe == rightMaybe);
        }
    }
}