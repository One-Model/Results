using System.Collections.Generic;
using System.Linq;

namespace OneModel.Results
{
    /// <summary>
    /// Represents the result of running an algorithm, where
    /// there is no return result other than some messages.
    /// </summary>
    public class Result : BaseResult
    {
        public Result()
        {
        }

        public Result(IEnumerable<Message> messages) : base(messages)
        {
        }

        public Result(params Message[] messages) : base(messages)
        {
        }

        /// <summary>
        /// Returns a new Result&lt;T&gt; that includes all of the messages
        /// from this Result, but with the specified value.
        /// </summary>
        public Result<T> WithValue<T>(T value)
        {
            return new Result<T>(value, Messages);
        }

        /// <summary>
        /// Performs a logical AND between two results.
        /// </summary>
        public static Result operator &(Result a, Result b)
        {
            return new Result(a.Messages.Concat(b.Messages));
        }

        #region Named constructors
        public static Result Error(string message)
        {
            return new Result(new Message(Severity.Error, message));
        }

        public static Result Empty()
        {
            return new Result();
        }

        public static Result Warning(string message)
        {
            return new Result(new Message(Severity.Warning, message));
        }
        #endregion
    }
}