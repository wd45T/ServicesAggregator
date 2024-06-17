using Aggregator.InterfaceAdapters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Application.UseCases.User.GetServices;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, IEnumerable<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetServicesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<string>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        var services = await _unitOfWork.Services
            .AsNoTracking()
            .ToListAsync();

        return new List<string> { "1", "2" };
    }
}