using Graphium;
using static Trek.Story.StoryGraph;
using Action = Trek.Story.StoryGraph.Action;

namespace Trek.Entities
{
    public class Player : GraphVisitor<Location, Action>
    {
        public string Name { get; }

        //public Inventory Inventory { get; }

        // If the location is null, then the player should spwan on any start node
        internal Player(string name, Location location) : base(location)
        {
            Name = name;
        }

        internal string ExecuteAction(Action action)
        {
            Traverse(action);
            return action.Description;
        }

        public string GetStatus()
        {
            return CurrentPosition.ToString();
        }
    }
}
