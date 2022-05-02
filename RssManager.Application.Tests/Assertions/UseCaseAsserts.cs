using System;
using System.Threading.Tasks;
using FluentAssertions;
using RssManager.Application.UseCases;


namespace RssManager.Application.Tests.Assertions;

public class UseCaseAsserts<TRequest, TResponse>
{
    private readonly IUseCase<TRequest, TResponse> useCase;
    private readonly TRequest request;

    public UseCaseAsserts(IUseCase<TRequest, TResponse> useCase, TRequest request)
    {
        this.useCase = useCase;
        this.request = request;
    }

    public async Task ReturnResponse(TResponse response)
    {
        TResponse getResourcesForUserResponse = await this.ExecuteHandler();
        getResourcesForUserResponse.Should().BeEquivalentTo(response);
    }
    
    public async Task ThrowException<T>(string message) where T : Exception
    {
        Func<Task<TResponse>> action = async () => await this.ExecuteHandler();
        await action.Should().ThrowAsync<T>().WithMessage(message);
    }

    private async Task<TResponse> ExecuteHandler()
    {
        return await this.useCase.Handle(request);
    }
}

public static class UseCaseAssertions
{
    public static UseCaseAsserts<TRequest, TResponse> AssertThatForRequest<TRequest, TResponse>(this IUseCase<TRequest, TResponse> useCase, TRequest request)
    {
        return new UseCaseAsserts<TRequest, TResponse>(useCase, request);
    }
}
