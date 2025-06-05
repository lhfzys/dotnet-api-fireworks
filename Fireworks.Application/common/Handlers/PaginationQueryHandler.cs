using Ardalis.Result;
using Fireworks.Application.common.Interfaces;
using Fireworks.Application.common.Models;
using Fireworks.Application.common.Requests;
using Mapster;
using MediatR;
using X.PagedList.EF;

namespace Fireworks.Application.common.Handlers;

public abstract class PaginationQueryHandler<TRequest, TEntity, TDto>(IApplicationDbContext context)
    : IRequestHandler<TRequest, Result<PaginatedResponse<TDto>>>
    where TRequest : PaginationRequest, IRequest<Result<PaginatedResponse<TDto>>>
    where TEntity : class
{

    protected abstract IQueryable<TEntity> BuildQuery(TRequest request);

    public async Task<Result<PaginatedResponse<TDto>>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var query = BuildQuery(request);

        var paged = await query
            .ProjectToType<TDto>()
            .ToPagedListAsync(request.PageNumber, request.PageSize, null, cancellationToken);

        var result = new PaginatedResponse<TDto>
        {
            PageNumber = paged.PageNumber,
            PageSize = paged.PageSize,
            TotalItems = paged.TotalItemCount,
            Items = paged.ToList()
        };

        return Result.Success(result);
    }
}