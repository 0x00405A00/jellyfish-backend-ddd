using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataFilter.Infrastructure;
using Shared.DataFilter.Presentation;

namespace Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
    public interface ISearchQuery<TResponse> : IQuery<TResponse>
    {
    }
    public class SearchQuery<TResponse> : ISearchQuery<TResponse>
    {
        public SearchQuery(SearchParams? searchParams)
        {
            SearchParams = searchParams;
        }

        public SearchParams? SearchParams { get; }
    }
}
