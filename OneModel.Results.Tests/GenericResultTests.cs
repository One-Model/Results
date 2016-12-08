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

        [Fact]
        public void Value_Results_Can_Be_Anded_With_Valueless_Results()
        {
            var a = new Result<int>(1);
            var b = new Result();

            var actual = a & b;
            
            Assert.Equal(1, actual.Value);
        }

        [Fact]
        public void Valueless_Results_Can_Be_Anded_With_Value_Results()
        {
            var a = new Result();
            var b = new Result<int>(1);

            var actual = a & b;

            Assert.Equal(1, actual.Value);
        }

        [Fact]
        public void Value_Results_Can_Be_Anded_With_Value_Results()
        {
            var a = new Result<int>(0);
            var b = new Result<int>(1);

            var actual = a.And(b);

            Assert.Equal(0, actual.Value);
        }

        [Fact]
        public void Value_Results_Can_Be_Anded_With_Value_Results_2()
        {
            var a = new Result<int>(1);
            var b = new Result<int>(2);

            var actual = a.And(b, (x,y) => x + y);

            Assert.Equal(3, actual.Value);
        }
        
        [Fact]
        public void Ok_Results_Can_Be_Created_Via_Named_Constructor()
        {
            var actual = Result<int>.Empty();
            Assert.True(actual.IsValid);
            Assert.Empty(actual.Messages);
        }

        [Fact]
        public void Error_Results_Can_Be_Created_Via_Named_Constructor()
        {
            var actual = Result<int>.Error("test");
            Assert.False(actual.IsValid);
            Assert.NotEmpty(actual.Messages);
        }

        [Fact]
        public void Warning_Results_Can_Be_Created_Via_Named_Constructor()
        {
            var actual = Result<int>.Warning("test");
            Assert.True(actual.IsValid);
            Assert.NotEmpty(actual.Messages);
        }
    }
}