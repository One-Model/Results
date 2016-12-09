using System;
using System.Collections.Generic;
using System.Linq;

namespace OneModel.Results
{
    /// <summary>
    /// Represents the result of running an algorithm, where
    /// there is a result of type T, and some messages.
    /// </summary>
    public class Result<T> : Result
    {
        public Result()
        {
            Value = default(T);
        }

        public Result(T value)
        {
            Value = value;
        }

        public Result(IEnumerable<Message> messages) : base(messages)
        {
            Value = default(T);
        }

        public Result(T value, IEnumerable<Message> messages) : base(messages)
        {
            Value = value;
        }

        public Result(params Message[] messages) : base(messages)
        {
        }

        public Result(T value, params Message[] messages) : base(messages)
        {
            Value = value;
        }

        /// <summary>
        /// The value produced by the algorithm.
        /// </summary>
        public T Value { get; }
        
        /// <summary>
        /// Performs a logical AND between two results.
        /// </summary>
        public Result<TOut> And<TOther,TOut> (Result<TOther> b, Func<T, TOther, TOut> aggregator)
        {
            var @out = aggregator(Value, b.Value);
            var messages = Messages.Concat(b.Messages);
            return new Result<TOut>(@out, messages);
        }

        /// <summary>
        /// Performs a logical AND between two results.
        /// </summary>
        public Result<T> And(BaseResult b)
        {
            var messages = Messages.Concat(b.Messages);
            return new Result<T>(Value, messages);
        }

        #region Named constructors
        public static Result<T> Error(string message)
        {
            return new Result<T>(default(T), new Message(Severity.Error, message));
        }
        
        public static Result<T> Empty()
        {
            return new Result<T>(default(T));
        }

        public static Result<T> Warning(string message)
        {
            return new Result<T>(default(T), new Message(Severity.Warning, message));
        }
        #endregion
    }
}