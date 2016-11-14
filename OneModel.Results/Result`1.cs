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

        public static bool operator true(Result<T> a) => a.IsValid;

        public static bool operator false(Result<T> a) => !a.IsValid;

        public static implicit operator bool(Result<T> a) => a.IsValid;

        /// <summary>
        /// Performs a logical AND between two results.
        /// </summary>
        public static Result<T> operator &(Result<T> a, Result b)
        {
            return a.And(b);
        }

        /// <summary>
        /// Performs a logical AND between two results.
        /// </summary>
        public static Result<T> operator &(Result a, Result<T> b)
        {
            return b.And(a);
        }

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
        public Result<T> And(Result b)
        {
            var messages = Messages.Concat(b.Messages);
            return new Result<T>(Value, messages);
        }
    }
}