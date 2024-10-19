using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroDFS.Services.Features.Trips.Queries.GetTripDirections
{
    public class GetTripDirectionsQueryDto
    {
        public int StationsCount { get; set; }
        public List<StationDto> Points { get; set; } = [];
        public List<StationsLine> Lines { get; set; } = [];
        public int TicketPrice { get; set; }
    }

    public class StationsLine
    {
        public int StartStationId { get; set; }
        public int TargetStationId { get; set; }
        public string Line { get; set; }
    }

    public class StationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
