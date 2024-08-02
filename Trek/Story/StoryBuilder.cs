using Trek.Story;

namespace Trek
{
    internal class StoryBuilder
    {
        private readonly StoryGraph _graph;

        public StoryBuilder()
        {
            _graph = new StoryGraph();
        }

        public StoryGraph Build()
        {
            return _graph;
        }
    }
}
