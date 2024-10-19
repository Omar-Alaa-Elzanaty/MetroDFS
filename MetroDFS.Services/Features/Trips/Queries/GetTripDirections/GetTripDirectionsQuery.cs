using Mapster;
using MediatR;
using MetroDFS.Presistance;
using MetroDFS.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MetroDFS.Services.Features.Trips.Queries.GetTripDirections
{
    public class GetTripDirectionsQuery : IRequest<Result<GetTripDirectionsQueryDto>>
    {
        public int StartStationId { get; set; }
        public int EndStationId { get; set; }
    }

    internal class GetTripDirectionsQueryHandler : IRequestHandler<GetTripDirectionsQuery, Result<GetTripDirectionsQueryDto>>
    {
        private readonly MetroDFSContext _context;
        private Dictionary<int, List<(int,float)>> _graph;
        private Dictionary<ValueTuple<int, int>, (float distance, List<int> points)> _tripDirections;
        private HashSet<ValueTuple<int, int>> _isVisited;
        public GetTripDirectionsQueryHandler(MetroDFSContext context)
        {
            _context = context;
            _graph = [];
            _tripDirections = [];
            _isVisited = [];
        }

        public async Task<Result<GetTripDirectionsQueryDto>> Handle(GetTripDirectionsQuery query, CancellationToken cancellationToken)
        {
            if (await _context.Stations.CountAsync(x => x.Id == query.StartStationId || x.Id == query.EndStationId) != 2)
            {
                return Result<GetTripDirectionsQueryDto>.Failure("Invalid Stations.");
            }

            var nodes = _context.ChildrensStations.ToList();

            if (nodes.Count > 0)
            {
                foreach (var node in nodes)
                {
                    if (!_graph.TryGetValue(node.ParentStationId, out _))
                        _graph.Add(node.ParentStationId, []);

                    _graph[node.ParentStationId].Add((node.StationId,node.Distance));
                }
            }

            var tripRoad = await GetDirections(query.StartStationId, query.StartStationId, query.EndStationId, query.StartStationId);

            var response = new GetTripDirectionsQueryDto();

            var roadStations = _context.Stations.Where(s => tripRoad.points.Contains(s.Id))
                               .ProjectToType<StationDto>()
                               .ToHashSet();

            for (int index = 0; index < tripRoad.points.Count; index++)
            {
                response.Points.Add(roadStations.First(x => x.Id == tripRoad.points[index]));
                if (index < tripRoad.points.Count - 1)
                {
                    response.Lines.Add(new()
                    {
                        StartStationId = tripRoad.points[index],
                        TargetStationId = tripRoad.points[index + 1],
                        Line = nodes.First(x => x.ParentStationId == tripRoad.points[index] && x.StationId == tripRoad.points[index + 1]).LineDirection
                    });
                }
            }

            response.StationsCount = tripRoad.points.Count;
            response.TicketPrice = GetTicketPrice(response.StationsCount);

            return Result<GetTripDirectionsQueryDto>.Success(response);
        }

        private async Task<(float distance, List<int> points)> GetDirections(int fromId,int toId, int endStationId, int startStation)
        {
            if (toId == endStationId)
            {
                return (0, [toId]);
            }

            if (_tripDirections.TryGetValue((fromId, toId), out var road))
            {
                return road;
            }
            var roads = new HashSet<(float distance, List<int> points)>();

            foreach (var child in _graph[toId])
            {
                if (child.Item1 == fromId || child.Item1 == startStation)
                    continue;

                if (_isVisited.TryGetValue((toId, child.Item1), out _))
                    continue;

                _isVisited.Add((toId, child.Item1));
                var returnRoad = await GetDirections(toId, child.Item1, endStationId, startStation);
                if (!returnRoad.points.IsNullOrEmpty() && (roads.IsNullOrEmpty() || roads.OrderBy(x => x.distance).First().distance > returnRoad.distance))
                {
                    returnRoad.distance += child.Item2;
                    roads.Add(returnRoad);
                }
                _isVisited.Remove((toId, child.Item1)); 
            }

            (float, List<int>) result = new();
            var path = new List<int>();

            if (roads.Count > 0)
            {
                var shortestPath = roads.OrderBy(x => x.distance).First();
                path.Add(toId);
                path.AddRange(shortestPath.points);
                result = (shortestPath.distance, path);
            }

            _tripDirections.Add((fromId, toId), result);

            return result;
        }

        private int GetTicketPrice(int stationsCount)
        {
            return stationsCount switch
            {
                <= 9 => 8,
                >= 10 and <= 16 => 10,
                >= 17 and <= 23 => 15,
                _ => 20
            };
        }
    }
}
