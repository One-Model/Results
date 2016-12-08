using Xunit;

namespace OneModel.Results.Tests
{
    public class ResultTests
    {
        [Fact]
        public void A_Valid_Result_Is_Considered_True()
        {
            var actual = new Result();
            Assert.True(actual.IsValid);
            Assert.True(actual);
        }

        [Fact]
        public void A_Result_Without_Errors_Is_True()
        {
            var actual = new Result(new Message(Severity.Warning, "a"));
            Assert.True(actual.IsValid);
            Assert.True(actual);
        }

        [Fact]
        public void A_Result_With_Errors_Is_False()
        {
            var actual = new Result(new Message(Severity.Error, "a"));
            Assert.False(actual.IsValid);
            Assert.False(actual);
        }

        [Fact]
        public void A_Result_Can_Be_Initialised_With_Messages()
        {
            var a = new Message(Severity.Warning, "a");
            var b = new Message(Severity.Error, "b");
            var actual = new Result(a, b);
            
            Assert.Collection(actual.Messages,
                i => Assert.Same(a, i),
                i => Assert.Same(b, i));
        }

        [Fact]
        public void Results_Can_Be_Anded_Together()
        {
            var a = new Message(Severity.Error, "a");
            var b = new Message(Severity.Error, "b");

            var resultA = new Result(a);
            var resultB = new Result(b);

            var actual = resultA & resultB;

            Assert.Collection(actual.Messages,
                i => Assert.Same(a, i),
                i => Assert.Same(b, i));
        }

        [Fact]
        public void Results_Can_Be_Short_Circuit_Anded_Together()
        {
            var a = new Message(Severity.Error, "a");
            var b = new Message(Severity.Error, "b");

            var resultA = new Result(a);
            var resultB = new Result(b);

            var actual = resultA && resultB;

            Assert.Collection(actual.Messages,
                i => Assert.Same(a, i));
        }

        [Fact]
        public void Results_Can_Be_Short_Circuit_Anded_Together_2()
        {
            var b = new Message(Severity.Error, "b");

            var resultA = new Result();
            var resultB = new Result(b);

            var actual = resultA && resultB;

            Assert.Collection(actual.Messages,
                i => Assert.Same(b, i));
        }

        [Fact]
        public void Results_Can_Be_Chained_Together_With_Ands()
        {
            var actual =
                new Result(new Message(Severity.Error, "a")) &
                new Result(new Message(Severity.Error, "b")) &
                new Result(new Message(Severity.Error, "c")) &
                new Result(new Message(Severity.Error, "d"));
            
            Assert.False(actual);
            Assert.False(actual.IsValid);
            Assert.Collection(actual.Messages,
                i => Assert.Equal("a", i.Text),
                i => Assert.Equal("b", i.Text),
                i => Assert.Equal("c", i.Text),
                i => Assert.Equal("d", i.Text));
        }

        [Fact]
        public void Ok_Results_Can_Be_Created_Via_Named_Constructor()
        {
            var actual = Result.Empty();
            Assert.True(actual.IsValid);
            Assert.Empty(actual.Messages);
        }

        [Fact]
        public void Error_Results_Can_Be_Created_Via_Named_Constructor()
        {
            var actual = Result.Error("test");
            Assert.False(actual.IsValid);
            Assert.NotEmpty(actual.Messages);
        }

        [Fact]
        public void Warning_Results_Can_Be_Created_Via_Named_Constructor()
        {
            var actual = Result.Warning("test");
            Assert.True(actual.IsValid);
            Assert.NotEmpty(actual.Messages);
        }

        [Fact]
        public void WithValue_Returns_The_Correct_Result()
        {
            var input = Result.Error("test");
            var actual = input.WithValue(4);

            Assert.False(actual.IsValid);
            Assert.Equal(4, actual.Value);
            Assert.Equal(1, actual.Messages.Count);
        }
    }
}