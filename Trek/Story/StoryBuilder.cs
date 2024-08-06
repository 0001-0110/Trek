using Trek.Story;
using static Trek.Story.StoryGraph;
using Choice = Trek.Story.StoryGraph.Choice;

namespace Trek
{
    internal class StoryBuilder
    {
        public class LocationBuilder
        {
            private readonly string _description;

            public string Id { get; }

            public LocationBuilder(string id, string description)
            {
                _description = description;
                Id = id;
            }

            public Location Build()
            {
                return new Location(Id, _description);
            }
        }

        public class ChoiceBuilder
        {
            private readonly string _directive;
            private readonly string _description;

            public string SourceId { get; }
            public string TargetId { get; }

            public ChoiceBuilder(string directive, string description, string sourceId, string targetId)
            {
                _directive = directive;
                _description = description;
                SourceId = sourceId;
                TargetId = targetId;
            }

            public Choice Build(Dictionary<string, Location> locations)
            {
                return new Choice(_directive, _description, locations[SourceId], locations[TargetId]);
            }
        }

        private readonly StoryGraph _graph;
        private readonly Dictionary<string, Location> _locations;
        // The choices that were requested but could not be added yet since the linked locations are not yet created
        private readonly List<ChoiceBuilder> _pendingChoices;

        public StoryBuilder()
        {
            _graph = new StoryGraph();
            _locations = new Dictionary<string, Location>();
            _pendingChoices = new List<ChoiceBuilder>();
        }

        public void AddLocation(LocationBuilder builder)
        {
            Location location = builder.Build();
            _locations.Add(builder.Id, location);
            _graph.Vertices.Add(location);
        }

        public void AddChoice(ChoiceBuilder builder)
        {
            if (_locations.ContainsKey(builder.SourceId) && _locations.ContainsKey(builder.TargetId))
                // If the locations needed are already present, add the action
                _graph.Connect(builder.Build(_locations));
            else
                // Stores all the data that is needed to create actions later, since actions can't be created before the
                // locations they are connected to
                _pendingChoices.Add(builder);
        }

        public StoryGraph Build()
        {
            foreach (ChoiceBuilder builder in _pendingChoices)
            {
                _graph.Connect(builder.Build(_locations));
            }
            // So that choices are not added multiple times if the graph is built multiple times
            _pendingChoices.Clear();

            return _graph;
        }
    }
}
