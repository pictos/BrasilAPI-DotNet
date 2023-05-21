namespace BrasilAPI;

public class BrasilAPIException : Exception
{
    public BrasilAPIException(string message) : base(message) { }

    public object ContentData { get; internal set; }

    public int Code { get; internal set; }

    public string Url { get; internal set; }
}
