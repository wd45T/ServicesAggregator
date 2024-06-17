using MediatR;

namespace Aggregator.Application.UseCases.User.GetServices;

public record GetServicesQuery : IRequest<IEnumerable<string>>;