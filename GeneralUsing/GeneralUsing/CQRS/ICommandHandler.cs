using MediatR;

namespace GeneralUsing.CQRS;
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse> where TResponse : notnull 
{
}
