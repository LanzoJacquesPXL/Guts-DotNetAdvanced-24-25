namespace CardGames.Domain;

public interface ICardDeck
{
    int RemainingCards { get; }
    void Shuffle();
    ICard DealCard();
    ICardDeck WithoutCardsRankingLowerThan(CardRank minimumRank);

    [Obsolete("This feature was removed")]
    IList<CardDeck> SplitBySuit();
}