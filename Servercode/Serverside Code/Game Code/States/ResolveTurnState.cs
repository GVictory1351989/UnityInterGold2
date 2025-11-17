using System;

public class ResolveTurnState : IState
{
    public void Enter(GameRoom game)
    {
        var player = game.CurrentPlayer;
        var opponent = game.GetOpponent(player);
        foreach (var cardId in player.PlayedThisTurn)
        {
            if (player.CardDB.Cards.TryGetValue(cardId, out CardData card))
            {
                ResolveCard(game, player, opponent, card);
            }
        }
        player.PlayedThisTurn.Clear();
        game.Broadcast("TurnResolved", player.ConnectUserId);
        game.ChangeState(new CheckEndGameState());
    }

    private void ResolveCard(GameRoom game, Player player, Player opponent, CardData card)
    {
        switch (card.Ability)
        {
            case "Damage:2":
                if (opponent != null)
                {
                    opponent.Score -= 2;
                    if (opponent.Score < 0) opponent.Score = 0;
                }
                break;

            case "Heal:2":
                player.Score += 2;
                break;

            case "Shield:1":
                break;

            case "Draw:1":
                player.DrawCards(1);
                game.SendHandToPlayer(player,true);
                break;

            case "Wildcard":
                break;
        }

        player.Score += card.Power;
    }
    public void Execute(GameRoom game) { }

    public void Exit(GameRoom game) { }
}
