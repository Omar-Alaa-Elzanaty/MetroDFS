using MediatR;
using MetroDFS.Models.Comman.Enums;
using MetroDFS.Models.Models;
using MetroDFS.Presistance;
using MetroDFS.Services.Shared;
using System.Xml.XPath;

namespace MetroDFS.Services.Features.Stations.Commands.AddChild
{
    public record AddChildCommand:IRequest<Result<string>>
    {
        public int ParentStationId { get; set; }
        public List<ChildStationDetailDto> ChildStationsIds { get; set; }
    }

    public record ChildStationDetailDto
    {
        public int StationId { get; set; }
        public Lines Line { get; set; }
        public string Direction { get; set; }
    }

    internal class AddChildCommandHandler : IRequestHandler<AddChildCommand, Result<string>>
    {
        private readonly MetroDFSContext _context;

        public AddChildCommandHandler(MetroDFSContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(AddChildCommand command, CancellationToken cancellationToken)
        {
            var station = await _context.Stations.FindAsync(command.ParentStationId);

            if(station is null)
            {
                return Result<string>.Failure("station not found.");
            }

            await _context.ChildrensStations.AddRangeAsync(command.ChildStationsIds.Select(x => new ChildStation()
            {
                ParentStationId = station.Id,
                StationId = x.StationId,
                Line = x.Line,
                LineDirection = x.Direction
            }), cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Relations added.");
        }
    }
}
