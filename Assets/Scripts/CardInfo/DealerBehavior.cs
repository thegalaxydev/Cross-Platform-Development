using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerBehavior : MonoBehaviour
{
    private List<Card> deck = new List<Card>();
    
    private List<Card> _playerCards = new List<Card>();
    private List<Card> _dealerCards = new List<Card>();

    private List<GameObject> _dealerCardImages = new List<GameObject>();
    private List<GameObject> _playerCardImages = new List<GameObject>();

    [SerializeField]
    private Texture2D[] _cardTextures;

    [SerializeField]
    private GameObject _dealerCardBase;
    [SerializeField]
    private GameObject _playerCardBase;

    [SerializeField]
    private GameObject _cardBase;

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _standButton;
    [SerializeField]
    private Button _hitButton;

    private int _dealerCount;
    private int _playerCount;


    private Card _dealerHidden;

    private void Start()
    {
    }

    /// <summary>
    /// Adds a new card to the player and displays it on screen.
    /// </summary>
    private void AddCardToPlayer()
    {
        _playerCount += deck[1].Number;
        _playerCards.Add(deck[1]);
     
        GameObject newCard = Instantiate(_cardBase, _playerCardBase.transform);
        
        newCard.transform.localPosition = Vector3.zero + new Vector3(25 * _playerCardImages.Count, 0, 0);

        newCard.GetComponent<Image>().sprite = deck[1].Texture;

        _playerCardImages.Add(newCard);
        deck.RemoveAt(1);

    }

    /// <summary>
    /// Resets and re-initializes the Deck.
    /// </summary>
    public void InitDeck()
    {
        // clear the list so it can be reinitialized
        deck.Clear();

        // reset the player and dealer total
        _playerCount = 0;
        _dealerHidden = null;
        _dealerCount = 0;

        int cardNumber = 1;
        int suite = 1;

        // create a new instance for each card number from each suite
        for (int i = 0; i < 52; i++)
        {
            if (cardNumber > 13)
            {
                cardNumber = 1;
                suite++;
            }

            if (suite > 4)
                break;

            deck.Add(new Card(cardNumber, suite, _cardTextures[i]));

            cardNumber++;
        }

        Shuffle();
    }

    /// <summary>
    /// Deals out the initial cards and determines the dealer's hidden card.
    /// </summary>
    public void Deal()
    {
        InitDeck();
        _startButton.gameObject.SetActive(false);

        _hitButton.interactable = true;
        _standButton.interactable = true;

        AddCardToPlayer();

        _dealerHidden = deck[1];
        deck.RemoveAt(1);

        AddCardToPlayer();

        _dealerCount += deck[1].Number;
        deck.RemoveAt(1);
    }

    /// <summary>
    /// Deals a card to the player and determines if they've lost or not.
    /// </summary>
    public void Hit()
    {
        AddCardToPlayer();

        // If the player's count is over 21, they lose.
        if (_playerCount > 21)
        {
            _standButton.interactable = false;
            _hitButton.interactable = false;

            _startButton.gameObject.SetActive(true);

            return;
        }
        // If the player's count is equal to 21, they win blackjack.
        else if (_playerCount == 21)
        {
            _standButton.interactable = false;
            _hitButton.interactable = false;

            _startButton.gameObject.SetActive(true);

            return;
        }

        // display the playercount on screen
    }
    /// <summary>
    /// End the game and have the dealer deal to themself until they win or bust.
    /// </summary>
    public void Stand()
    {
        _dealerCount += _dealerHidden.Number;

        // Continuously deal until they reach a win or loss outcome.
        while(_dealerCount < 21 && _dealerCount < _playerCount)
        {
            _dealerCount += deck[1].Number;
            deck.RemoveAt(1);
        }

        // Determine the outcome of the draw.
        if (_dealerCount > 21)
        {

        }
        else if (_dealerCount > _playerCount || _dealerCount == 21)
        {

        }
        else
        {

        }

        // reset the game
        _standButton.interactable = false;
        _hitButton.interactable = false;

        _startButton.gameObject.SetActive(true);

    }

    /// <summary>
    /// Utilizes the Fisher–Yates shuffle algorithm to shuffle the deck.
    /// </summary>
    private void Shuffle()
    {
        System.Random random = new System.Random();

        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            Card temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }
}
