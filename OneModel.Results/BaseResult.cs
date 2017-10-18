using System.Collections.Generic;

namespace OneModel.Results
{
    public abstract class BaseResult
    {
        private readonly List<Message> _messages;

        protected BaseResult()
        {
            _messages = new List<Message>();
        }

        protected BaseResult(IEnumerable<Message> messages) : this()
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
        /// Adds a warning to this result set.
        /// </summary>
        public void AddWarning(string message)
        {
            Add(new Message(Severity.Warning, message));
        }

        /// <summary>
        /// Adds an error to this result set.
        /// </summary>
        public void AddError(string message)
        {
            Add(new Message(Severity.Error, message));
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

        public static bool operator true(BaseResult a) => a.IsValid;

        public static bool operator false(BaseResult a) => !a.IsValid;

        public static implicit operator bool(BaseResult a) => a.IsValid;

    }
}
