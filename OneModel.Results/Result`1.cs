using System.Collections.Generic;

namespace OneModel.Results
{
    /// <summary>
    /// Represents the result of running an algorithm, where
    /// there is a result of type T, and some messages.
    /// </summary>
    public class Result<T>
    {
        private readonly List<Message> _messages = new List<Message>();

        public Result()
        {
            Value = default(T);
        }

        public Result(T value)
        {
            Value = value;
        }

        public Result(IEnumerable<Message> messages)
        {
            AddRange(messages);
        }

        public Result(T value, IEnumerable<Message> messages)
        {
            Value = value;
            AddRange(messages);
        }

        public Result(params Message[] messages)
        {
            AddRange(messages);
        }

        public Result(T value, params Message[] messages)
        {
            Value = value;
            AddRange(messages);
        }

        /// <summary>
        /// The value produced by the algorithm.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Whether this result is valid. True if there
        /// are no Errors. False if there are 1 or more
        /// Errors.
        /// </summary>
        public bool IsValid { get; private set; } = true;

        /// <summary>
        /// The messages encountered during validation.
        /// </summary>
        public IReadOnlyList<Message> Messages => _messages;

        /// <summary>
        /// Adds a message to this result set.
        /// </summary>
        public void Add(Message message)
        {
            if (message.Severity == Severity.Error)
            {
                IsValid = false;
            }

            _messages.Add(message);
        }

        /// <summary>
        /// Adds a sequence of messages to this result set.
        /// </summary>
        public void AddRange(params Message[] messages)
        {
            AddRange((IEnumerable<Message>)messages);
        }

        /// <summary>
        /// Adds a sequence of messages to this result set.
        /// </summary>
        public void AddRange(IEnumerable<Message> messages)
        {
            foreach (var message in messages)
            {
                Add(message);
            }
        }
        
        public static bool operator true(Result<T> a) => a.IsValid;

        public static bool operator false(Result<T> a) => !a.IsValid;

        public static implicit operator bool(Result<T> a) => a.IsValid;
    }
}