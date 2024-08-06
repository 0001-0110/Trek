using Trek.Entities;

namespace Trek.Runners
{
    public interface IRunner
    {
        string GetStatus(Player player);

        string Execute(Player player, string input);
    }
}
