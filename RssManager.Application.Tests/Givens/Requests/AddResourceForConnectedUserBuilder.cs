using RssManager.Application.UseCases.AddResourceForConnectedUser;


namespace RssManager.Application.Tests.Givens.Requests;

public class AddResourceForConnectedUserBuilder
{
    private string? title;
    private string? url;
    private string? description;

    public AddResourceForConnectedUserBuilder WithTitle(string resourceTitle)
    {
        this.title = resourceTitle;
        return this;
    }
    
    public AddResourceForConnectedUserBuilder WithUrl(string resourceUrl)
    {
        this.url = resourceUrl;
        return this;
    }
    
    public AddResourceForConnectedUserBuilder WithDescription(string resourceDescription)
    {
        this.description = resourceDescription;
        return this;
    }

    public AddResourceForConnectedUserBuilder WithDefaultProperties()
    {
        this.title = "The title of my article";
        this.url = "https://my-article.com";
        this.description = "The description of my article";
        return this;
    }

    public AddResourceForConnectedUserRequest Build()
    {
        return new AddResourceForConnectedUserRequest(
            this.title ?? string.Empty,
            this.url ?? string.Empty,
            this.description ?? string.Empty
        );
    }

    public static AddResourceForConnectedUserBuilder GivenAddResourceRequest()
    {
        return new AddResourceForConnectedUserBuilder();
    }
}
