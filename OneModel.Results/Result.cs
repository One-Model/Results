using System.Collections.Generic;
using System.Linq;

namespace OneModel.Results
{
    /// <summary>
    /// Represents the result of running an algorithm, where
    /// there is no return result other than some messages.
    /// </summary>
    public class Result
    {
        private readonly List<Message> _messages;

        public Result()
        {
            _messages = new List<Message>();
        }

        public Result(IEnumerable<Message> messages) : this()
        {
            AddRange(messages);
        }

        public Result(params Message[] messages) : this()
        {
            AddRange(messages);
        }

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

        /// <summary>
        /// Performs a logical AND between two results.
        /// </summary>
        public static Result operator &(Result a, Result b)
        {
            return new Result(a.Messages.Concat(b.Messages));
        }

        public static bool operator true(Result a) => a.IsValid;

        public static bool operator false(Result a) => !a.IsValid;

        public static implicit operator bool(Result a) => a.IsValid;
    }
}