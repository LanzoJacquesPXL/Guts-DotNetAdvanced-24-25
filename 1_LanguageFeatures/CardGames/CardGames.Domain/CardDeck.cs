
namespace CardGames.Domain;

public class CardDeck : ICardDeck
{

    public CardDeck()
    {
        IList<ICard> cards = new List<ICard>();
        foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
        {
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                cards.Add(new Card(suit, rank));
            }
        }
        Cards = cards;
    }

    private CardDeck(IList<ICard> cards)
    {
        Cards = cards;
    }

    IList<ICard> Cards { get; }

    private static readonly Random _random = new Random();

    public int RemainingCards => throw new NotImplementedException();

    public ICard DealCard()
    {
        int lengthDeck = Cards.Count;
        if (lengthDeck != 0)
        {
            ICard dealtCard = Cards[lengthDeck-1];
            Cards.Remove(Cards[lengthDeck-1]);
            return dealtCard;
        }
        throw new InvalidOperationException("Deck is empty");
    }

    public void Shuffle()
    {
        for (int n = Cards.Count - 1; n > 0; --n)
        {
            int k = _random.Next(n+1);
            ICard temp = Cards[n];
            Cards[n] = Cards[k];
            Cards[k] = temp;
        }
    }

    public IList<CardDeck> SplitBySuit()
    {
        IList<CardDeck> cardDecks = new List<CardDeck>();
        var heartsQuery = from cards in Cards
                               where cards.Suit == CardSuit.Hearts
                               select cards;
        IList<ICard> heartsCards = heartsQuery.ToList();
        CardDeck heartsDeck = new CardDeck(heartsCards);
        cardDecks.Add(heartsDeck);
        var spadesQuery = from cards in Cards
                          where cards.Suit == CardSuit.Spades
                          select cards;
        IList<ICard> spadesCards = spadesQuery.ToList();
        CardDeck spadesDeck = new CardDeck(spadesCards);
        cardDecks.Add(spadesDeck);
        var diamondsQuery = from cards in Cards
                          where cards.Suit == CardSuit.Diamonds
                          select cards;
        IList<ICard> diamondsCards = diamondsQuery.ToList();
        CardDeck diamondsDeck = new CardDeck(diamondsCards);
        cardDecks.Add(diamondsDeck);
        var clubsQuery = from cards in Cards
                          where cards.Suit == CardSuit.Clubs
                          select cards;
        IList<ICard> clubsCards = clubsQuery.ToList();
        CardDeck clubsDeck = new CardDeck(clubsCards);
        cardDecks.Add(clubsDeck);
        return cardDecks;
    }

    public ICardDeck WithoutCardsRankingLowerThan(CardRank minimumRank)
    {
        var cardsQuery = from cards in Cards
                         where cards.Rank >= minimumRank
                         select cards;
        IList<ICard> setOfCards = cardsQuery.ToList();
        return new CardDeck(setOfCards);
    }
}