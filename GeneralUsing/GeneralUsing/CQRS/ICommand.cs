using MediatR;

namespace GeneralUsing.CQRS;
public interface ICommand<TResponse> : IRequest<TResponse>
{
}
