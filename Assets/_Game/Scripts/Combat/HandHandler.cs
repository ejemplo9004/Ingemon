using UnityEngine;

public class HandHandler
{
    private CombatInfo info;

    public HandHandler(CombatInfo info)
    {
        this.info = info;
    }
    
    public void Draw(int draws)
    {
        for (int i = 0; i < draws; i++)
        {
            if (info.drawDeck.Count > 0)
            {
                if (info.hand.Count >= 10)
                {
                    info.discardDeck.Add(info.drawDeck[0]);
                    info.drawDeck.RemoveAt(0);
                }
                else
                {
                    AddToHand(info.drawDeck[0]);
                    info.drawDeck.RemoveAt(0);
                }
            }
            else
            {
                FillDeck();
                i--;
                if (info.drawDeck.Count == 0)
                {
                    Debug.Log("La deck esta empty");
                    return;
                }
            }
        }
    }


    public void ShuffleDeck()
    {
        int n = info.drawDeck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            (info.drawDeck[k], info.drawDeck[n]) = (info.drawDeck[n], info.drawDeck[k]);
        }
    }

    public void FillDeck()
    {
        while (info.discardDeck.Count > 0)
        {
            info.drawDeck.Add(info.discardDeck[0]);
            info.discardDeck.RemoveAt(0);
        }

        ShuffleDeck();
    }

    public void LogDeck()
    {
        foreach (var card in info.drawDeck)
        {
            Debug.Log($"{card}");
        }
    }

    public void AddToHand(Card card)
    {
        Debug.Log($"Spawning Card {card.id} : {card.info.cardName}");
        info.hand.Add(card);
        CombatSingletonManager.Instance.eventManager.UpdateCardHand(card);
    }

    public void Discard(Card card)
    {
        info.discardDeck.Add(card);
        info.hand.Remove(card);
    }

    public void DiscardHand()
    {
        while (info.hand.Count > 0)
        {
            Discard(info.hand[0]);
        }

        CombatSingletonManager.Instance.eventManager.UpdateHand(info.hand);
    }
}
