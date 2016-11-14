namespace OneModel.Results
{
    public class Message
    {
        public Severity Severity { get; }

        public string Text { get; }

        public Message(Severity severity, string text)
        {
            Severity = severity;
            Text = text;
        }
    }
}