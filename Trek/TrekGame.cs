using Trek.Entities;
using Trek.Runners;
using Trek.Story;
using static Trek.Story.StoryGraph;

namespace Trek
{
    public class TrekGame
    {
        public static TrekGame Load(IRunner runner, string path)
        {
            return new TrekGame(runner, new StoryLoader().Load(path));
        }

        private readonly StoryGraph _story;
        private readonly IRunner _runner;

        internal TrekGame(IRunner runner, StoryGraph story)
        {
            _story = story;
            _runner = runner;
        }

        private Location GetRandomSpawnLocation()
        {
            // TODO
            return _story.Vertices.First();
        }

        public string Join(string name, out Player player)
        {
            // If location is null, the player will spawn on any suitable spwan location
            player = new Player(name, GetRandomSpawnLocation());
            return GetStatus(player);
        }

        public Player Join(string name, Location location)
        {
            return new Player(name, location);
        }

        public string GetStatus(Player player)
        {
            return _runner.GetStatus(player);
        }

        public string ExecuteAction(Player player, string input)
        {
            return _runner.Execute(player, input);
        }
    }
}
