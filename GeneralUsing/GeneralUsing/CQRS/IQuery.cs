using MediatR;

namespace GeneralUsing.CQRS;
public interface IQuery<TResponse> : IRequest<TResponse>
{
}
