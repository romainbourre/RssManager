using RssManager.Domain.Exceptions;


namespace RssManager.Domain.ValueObjects;

public readonly struct Url
{
    public readonly string Value;
    
    private Url(string url)
    {
        bool isCorrectUrl = Uri.TryCreate(url, UriKind.Absolute, out Uri? uri);

        if (!isCorrectUrl || uri == null || uri.Scheme != Uri.UriSchemeHttps)
            throw new IncorrectResourceUrlException();

        this.Value = url;
    }
    
    public static Url Of(string url)
    {
        return new Url(url);
    }

    public override string ToString()
    {
        return this.Value;
    }
}
