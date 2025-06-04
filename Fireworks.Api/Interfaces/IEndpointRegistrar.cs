namespace Fireworks.Api.Interfaces;

public interface IEndpointRegistrar
{
    void MapEndpoints(IEndpointRouteBuilder endpoints);
}