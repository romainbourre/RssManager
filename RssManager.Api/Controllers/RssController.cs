using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using RssManager.Application.Exceptions;
using RssManager.Application.UseCases.GetResourcesForUser;


namespace RssManager.Api.Controllers;

[ApiController]
[Route("rss")]
public class RssController : ControllerBase
{
    private readonly ILogger<RssController> logger;
    public RssController(ILogger<RssController> logger)
    {
        this.logger = logger;
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetFeed([FromServices] GetResourcesForUserUseCase useCase,
        Guid userId
    )
    {
        try
        {
            GetResourcesForUserResponse userProfileAndResources = await useCase.Handle(new GetResourcesForUserRequest(userId));

            var feed = new SyndicationFeed(
                userProfileAndResources.User.Fullname,
                userProfileAndResources.User.Description,
                new Uri(userProfileAndResources.User.Website),
                userProfileAndResources.Resources.Select(resource => new SyndicationItem(resource.Title, resource.Description, new Uri(resource.Url))));

            using var stream = new MemoryStream();

            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true,
                Async = true,
            };

            await using (var xmlWriter = XmlWriter.Create(stream, settings))
            {
                var rssFormatter = new Rss20FeedFormatter(feed, false);
                rssFormatter.WriteTo(xmlWriter);
                await xmlWriter.FlushAsync();
            }

            return this.File(stream.ToArray(), "application/rss+xml; charset=utf-8");

        }
        catch (UserNotFoundException e)
        {
            this.logger.LogTrace(e, "{Message}", e.Message);
            return this.NotFound(e.Message);
        }
        catch(Exception e)
        {
            this.logger.LogError("{Message}", e.Message);
            return this.Problem(e.Message);
        }
    }
}
