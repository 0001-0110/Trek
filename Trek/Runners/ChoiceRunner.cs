using Trek.Entities;

namespace Trek.Runners
{
    public class ChoiceRunner : IRunner
    {
        public string GetStatus(Player player)
        {
            return $"{player.CurrentPosition.Description}\nYou can:\n{string.Join('\n', player.CurrentPosition.Edges.Select((choice, index) => $"{index}. {choice.Directive}"))}";
        }

        public string Execute(Player player, string input)
        {
            if (!int.TryParse(input, out int choice))
                return "Please select a choice by it's number";
            if (choice <= 0 || choice > player.CurrentPosition.Edges.Count())
                return "Please select a valid number";

            return player.ExecuteAction(player.CurrentPosition.Edges.ElementAt(choice - 1));
        }
    }
}
