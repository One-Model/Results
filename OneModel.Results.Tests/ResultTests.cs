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
    }
}