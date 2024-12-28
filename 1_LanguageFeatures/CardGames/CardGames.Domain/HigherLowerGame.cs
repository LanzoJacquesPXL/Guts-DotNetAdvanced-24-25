namespace CardGames.Domain;

public class HigherLowerGame
{

    private ICardDeck _cardDeck;
    private int _requiredAmountOfCorrectGuesses;
    private bool _hasWon;

    public ICard CurrentCard { get; private set; }
    public ICard? PreviousCard { get; private set; }
    public int NumberOfCorrectGuesses { get; private set; }

    public string? Motivation { get; private set; }

    public bool HasWon => _hasWon;

    public HigherLowerGame(ICardDeck standardDeck, int requiredNumberOfCorrectGuesses, CardRank minimumRank = CardRank.Ace)
    {
        _cardDeck = standardDeck.WithoutCardsRankingLowerThan(minimumRank);
        _cardDeck.Shuffle();
        _requiredAmountOfCorrectGuesses = requiredNumberOfCorrectGuesses;
        CurrentCard = _cardDeck.DealCard();
    }

    public void MakeGuess(bool higher)
    {
        ICard nextCard = _cardDeck.DealCard();
        bool isCorrectGuess;
        if (higher)
        {
             isCorrectGuess = CurrentCard.Rank <= nextCard.Rank;
        } else
        {
            isCorrectGuess = CurrentCard.Rank >= nextCard.Rank;
        }
        if (isCorrectGuess)
        {
            NumberOfCorrectGuesses += 1;
            int guessesToGo = _requiredAmountOfCorrectGuesses - NumberOfCorrectGuesses;
            if (guessesToGo == 0)
            {
                _hasWon = true;
            }
            if (guessesToGo <= 3)
            {
                Motivation = "Only " + guessesToGo + " guesses needed";
            }
        } else
        {
            NumberOfCorrectGuesses = 0;
            Motivation = null;
        }
        PreviousCard = CurrentCard;
        CurrentCard = nextCard;
    }
}