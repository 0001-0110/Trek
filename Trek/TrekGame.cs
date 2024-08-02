using Trek.Entities;
using Trek.Story;
using static Trek.Story.StoryGraph;

namespace Trek
{
    public class TrekGame
    {
        public static TrekGame Load(string path)
        {
            return TrekLoader.Load(path);
        }

        private readonly StoryGraph _story;

        internal TrekGame(StoryGraph story)
        {
            _story = story;
        }

        private Location GetRandomSpawnLocation()
        {
            // TODO
            return new Location("Spawn", "This is a test");
        }

        public string Join(string name, out Player player)
        {
            // If location is null, the player will spawn on any suitable spwan location
            player = new Player(name, GetRandomSpawnLocation());
            return player.CurrentPosition.ToString();
        }

        public Player Join(string name, Location location)
        {
            return new Player(name, location);
        }

        public string ExecuteAction(Player player, string input)
        {
            throw new NotImplementedException();
        }
    }
}
