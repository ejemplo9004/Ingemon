using System.Collections.Generic;
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


    public List<Card> ShuffleDeck(List<Card> deck)
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            (deck[k], deck[n]) = (deck[n], deck[k]);
        }

        return deck;
    }

    public void FillDeck()
    {
        while (info.discardDeck.Count > 0)
        {
            info.drawDeck.Add(info.discardDeck[0]);
            info.discardDeck.RemoveAt(0);
        }

        info.drawDeck = ShuffleDeck(info.drawDeck);
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
        info.hand.Add(card);
        CombatSingletonManager.Instance.eventManager.UpdateCardHand(card);
    }

    public void Discard(Card card)
    {
        info.discardDeck.Add(card);
        info.hand.Remove(card);
        CombatSingletonManager.Instance.eventManager.UpdateHand(info.hand);
    }

    public void DiscardHand()
    {
        while (info.hand.Count > 0)
        {
            Discard(info.hand[0]);
        }
    }

    public void DiscardRandom()
    {
        int n = info.hand.Count - 1;
        if(n < 0) return;
        
        int rand = Random.Range(0, n);
        Discard(info.hand[rand]);
    }

    public void DiscardExpensive()
    {
        int n = info.hand.Count - 1;
        if(n < 0) return;

        int mostExpensive = 0;
        for (int i = 1; i <= n; i++)
        {
            int m = info.hand[i].info.cost;
            int x = info.hand[mostExpensive].info.cost;
            if (m >= x)
            {
                if (m > x)
                {
                    mostExpensive = m;
                }
                else
                {
                    if (Random.Range(0, 1) == 1)
                    {
                        mostExpensive = m;
                    }
                }
            }
        }
        
        Discard(info.hand[mostExpensive]);
    }
}
