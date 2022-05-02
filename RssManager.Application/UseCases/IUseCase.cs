namespace RssManager.Application.UseCases;

public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request);
}
