using Aggregator.InterfaceAdapters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Application.UseCases.User.GetServices;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetServicesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        var result = new object();

        try
        {
            var services = await _unitOfWork.Companies
                .Include(x => x.Services)
                .AsNoTracking()
                .Select(c => new 
                {
                    c.Name,
                    c.Description,
                    c.LogoUrl,
                    Services = c.Services.Select(s => new 
                    {
                        s.Name,
                        s.Description,
                        s.CompanyId,
                        s.ServiceData,
                    }).ToList()
                })
                .ToListAsync();
            
            //TODO use dto
            result = services;
        }
        //TODO use global error interceptor
        catch (Exception ex) 
        { 
            //TODO use log
            Console.WriteLine(ex.ToString());

            result = ex;
        }

        //TODO use response pattern
        return result;
    }
}