namespace Trek
{
    public static class TrekLoader
    {
        public static TrekGame Load(string path)
        {
            StoryBuilder builder = new StoryBuilder();

            return new TrekGame(builder.Build());
        }
    }
}
