using Mapster;
using MediatR;
using MetroDFS.Presistance;
using MetroDFS.Services.Models;
using MetroDFS.Services.Shared;
using Microsoft.EntityFrameworkCore;

namespace MetroDFS.Services.Features.Stations.Commands.Add
{
    public record AddStationCommand:IRequest<Result<int>>
    {
        public string Name { get; set; }
    }

    internal class AddStationCommandHandler : IRequestHandler<AddStationCommand, Result<int>>
    {
        private readonly MetroDFSContext _context;

        public AddStationCommandHandler(MetroDFSContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(AddStationCommand command, CancellationToken cancellationToken)
        {
            var station = await _context.Stations.FirstOrDefaultAsync(x => x.Name == command.Name, cancellationToken);

            if (station != null)
            {
                return Result<int>.Failure("Station already exist.");
            }

            station = command.Adapt<Station>();

            await _context.AddAsync(station,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(station.Id);
        }
    }
}