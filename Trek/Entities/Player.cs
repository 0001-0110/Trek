using Graphium;
using static Trek.Story.StoryGraph;
using Choice = Trek.Story.StoryGraph.Choice;

namespace Trek.Entities
{
    public class Player : GraphVisitor<Location, Choice>
    {
        public string Name { get; }

        //public Inventory Inventory { get; }

        // If the location is null, then the player should spwan on any start node
        internal Player(string name, Location location) : base(location)
        {
            Name = name;
        }

        internal string ExecuteAction(Choice action)
        {
            Traverse(action);
            return action.Description;
        }
    }
}
