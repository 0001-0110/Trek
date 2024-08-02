using Graphium;

namespace Trek.Story
{
    public class StoryGraph : DirectedGraph<StoryGraph.Location, StoryGraph.Action>
    {
        public class Location : Vertex
        {
            public string Name { get; }

            public string Description { get; }

            public Location(string name, string description)
            {
                Name = name;
                Description = description;
            }

            public override string ToString()
            {
                return $"{Name}\n{Description}";
            }
        }

        public class Action : Edge
        {
            // The text to select this action
            // TODO Need a better name
            public string Text { get; }

            // The text to display when a player execute this action
            public string Description { get; }

            public Action(string description, Location vertex1, Location vertex2) : base(vertex1, vertex2)
            {
                Description = description;
            }
        }
    }
}
