using RssManager.AdministrationConsole.Interfaces;
using RssManager.Application.UseCases.GetResourcesForUser;


namespace RssManager.AdministrationConsole.Views;

public class ResourceListView : IPresenter<GetResourcesForUserResponse>
{

    public void Present(GetResourcesForUserResponse data)
    {
        Console.WriteLine($"\n\n* Resource of {data.User.Fullname} ({data.User.Website})");
        
        foreach (GetResourcesForUserResponse.UserResource resource in data.Resources)
        {
            Console.WriteLine(GetSeparator());
            Console.WriteLine($"Title: {resource.Title}");
            Console.WriteLine($"Url: {resource.Url}");
            Console.WriteLine($"Description: {resource.Description}");
        }
        Console.WriteLine(GetSeparator());
    }

    private static string GetSeparator()
    {
        return "---------------------------------------------------------";
    }
}
