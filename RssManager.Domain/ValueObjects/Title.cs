using RssManager.Domain.Exceptions;


namespace RssManager.Domain.ValueObjects;

public readonly struct Title
{
    public readonly string Value;
    
    private Title(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new IncorrectResourceTitleException();
        this.Value = text;
    }

    public static Title Of(string text)
    {
        return new Title(text);
    }

    public override string ToString()
    {
        return this.Value;
    }
}
