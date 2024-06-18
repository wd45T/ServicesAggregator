using Aggregator.Application.UseCases.User.GetServices;
using FastEndpoints;
using MediatR;

namespace Aggregator.API.Endpoints.User;

public class GetServicesEndpoint : EndpointWithoutRequest<object>
{
    public override void Configure()
    {
        Get("user/company/services");
        Summary(s => s.Summary = "Get all company services");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var sender = Resolve<ISender>();

        var result = await sender.Send(new GetServicesQuery(), cancellationToken);

        await SendAsync(result);
    }
}