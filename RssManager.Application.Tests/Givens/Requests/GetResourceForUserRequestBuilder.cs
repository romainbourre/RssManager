using System;
using RssManager.Application.UseCases.GetResourcesForUser;


namespace RssManager.Application.Tests.Givens.Requests;

public class GetResourceForUserRequestBuilder
{
    private Guid userId;
    public GetResourceForUserRequestBuilder WithUserId(Guid userId)
    {
        this.userId = userId;
        return this;
    }
    
    public GetResourcesForUserRequest Build()
    {
        return new GetResourcesForUserRequest(
            userId
        );
    }
    
    public static GetResourceForUserRequestBuilder GivenGetProfileAndResourcesRequest() => new();
}
