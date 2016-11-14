using Xunit;

namespace OneModel.Results.Tests
{
    public class GenericResultTests
    {
        [Fact]
        public void A_Valid_Result_Is_Considered_True()
        {
            var actual = new Result<int>();
            Assert.True(actual.IsValid);
            Assert.True(actual);
        }

        [Fact]
        public void A_Result_Without_Errors_Is_True()
        {
            var actual = new Result<int>(new Message(Severity.Warning, "a"));
            Assert.True(actual.IsValid);
            Assert.True(actual);
        }

        [Fact]
        public void A_Result_With_Errors_Is_False()
        {
            var actual = new Result<int>(new Message(Severity.Error, "a"));
            Assert.False(actual.IsValid);
            Assert.False(actual);
        }

        [Fact]
        public void A_Result_Can_Be_Initialised_With_Messages()
        {
            var a = new Message(Severity.Warning, "a");
            var b = new Message(Severity.Error, "b");
            var actual = new Result<int>(a, b);
            
            Assert.Collection(actual.Messages,
                i => Assert.Same(a, i),
                i => Assert.Same(b, i));
        }
    }
}