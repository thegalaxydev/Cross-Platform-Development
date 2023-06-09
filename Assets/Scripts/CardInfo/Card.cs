using UnityEngine;

/// <summary>
/// Card class to symbolize a card.
/// </summary>
public class Card
{
    /// <summary>
    /// Takes in a number and suite to describe the card.
    /// </summary>
    /// <param name="number">The number (1-13) of the card.</param>
    /// <param name="suite">The suite (1-4) of the card.</param>
    public Card(int number, int suit, Sprite texture)
    {
        _number = number;
        _suit = suit;
        _texture = texture;
    }

    public int Number { get { return _number;} }
    public int Suit { get { return _suit;} }

    public Sprite Texture { get { return _texture; } }

    private int _number;
    private int _suit;
    private Sprite _texture;
}
