using PlayerIOClient;
using UnityEngine;
public class HandState : GameState
{
    public override void HandleMessage(GamePlayPage game, Message message)
    {
        Debug.Log($"Mesage {message}");
        uint index = 0;
        while (index < message.Count)
        {
            string playerId = message.GetString(index++);
            string cardId = message.GetString(index++);
            string cardName = message.GetString(index++);
            int cost = int.Parse(message.GetString(index++));
            int health = int.Parse(message.GetString(index++));
            string ability = message.GetString(index++);
            int energy = int.Parse(message.GetString(index++));
            int finalScore = int.Parse(message.GetString(index++));

            CardUnit cardData = new CardUnit
            {
                CardName = cardName,
                OwnerId = playerId,
                Id = cardId,
                Cost = cost,
                Health = health,
                Ability = ability,
                EnergyValue = energy,
                FinalScore = finalScore,
                CardDisplay = game.CardDatabase.GetSpriteByStandardCard(Utils.getCardbyName(cardName, out bool check))
            };

            PlayerUI targetPlayer = (playerId == game.MainPlayer.playerId) ? game.MainPlayer : game.OpponentPlayer;

            if (!targetPlayer.hand.Exists(c => c.Id == cardId))
            {
                targetPlayer.hand.Add(cardData);
                GameObject cardGO = GameObject.Instantiate(game.CardPrefab, targetPlayer.PlayerHandArea);
                cardGO.name = cardData.CardName;
                cardGO.GetComponent<CardInteract>().PassData(cardData, cardData.CardDisplay);
                targetPlayer.PlayerHandArea.GetComponent<HandLayout>().ArrangeHand();
            }
        }
    }
}
