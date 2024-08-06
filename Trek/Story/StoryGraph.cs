using Graphium;

namespace Trek.Story
{
    public class StoryGraph : DirectedGraph<StoryGraph.Location, StoryGraph.Choice>
    {
        public class Location : Vertex
        {
            public string Id { get; }

            public string Description { get; }

            public Location(string id, string description)
            {
                Id = id;
                Description = description;
            }
        }

        public class Choice : Edge
        {
            // The text to select this action
            public string Directive { get; }

            // The text to display when a player execute this action
            public string Description { get; }

            public Choice(string directive, string description, Location vertex1, Location vertex2) : base(vertex1, vertex2)
            {
                Directive = directive;
                Description = description;
            }
        }
    }
}
