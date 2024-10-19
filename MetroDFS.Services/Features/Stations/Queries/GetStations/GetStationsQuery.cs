using Mapster;
using MediatR;
using MetroDFS.Presistance;
using MetroDFS.Services.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroDFS.Services.Features.Stations.Queries.GetStations
{
    public record GetStationsQuery:IRequest<Result<List<GetStationsQueryDto>>>;

    internal class GetStationsQueryHandler : IRequestHandler<GetStationsQuery, Result<List<GetStationsQueryDto>>>
    {
        private readonly MetroDFSContext _context;

        public GetStationsQueryHandler(MetroDFSContext context)
        {
            _context = context;
        }

        public async Task<Result<List<GetStationsQueryDto>>> Handle(GetStationsQuery request, CancellationToken cancellationToken)
        {
            var stations = await _context.Stations
                         .ProjectToType<GetStationsQueryDto>()
                         .ToListAsync(cancellationToken);

            return Result<List<GetStationsQueryDto>>.Success(stations);
        }
    }
}
