using System.Collections.Generic;

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
    }
}