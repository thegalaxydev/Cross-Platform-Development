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
    private Sprite[] _cardTextures;
    [SerializeField]
    private Sprite _cardBackImage;

    [SerializeField]
    private GameObject _dealerCardBase;
    [SerializeField]
    private GameObject _playerCardBase;
    [SerializeField]
    private GameObject _dealerHiddenBase;

    [SerializeField]
    private GameObject _cardBase;

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _standButton;
    [SerializeField]
    private Button _hitButton;

    [SerializeField]
    private MessageBehavior _messageHolder;

    private int _dealerCount;
    private int _playerCount;

    private GameObject _dealerHiddenCard;


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
    /// Adds a new card to the dealer and displays it on screen.
    /// </summary>
    private void AddCardToDealer()
    {
        _dealerCount += deck[1].Number;
        _dealerCards.Add(deck[1]);

        GameObject newCard = Instantiate(_cardBase, _dealerCardBase.transform);

        newCard.transform.localPosition = Vector3.zero + new Vector3(25 * _dealerCardImages.Count, 0, 0);

        newCard.GetComponent<Image>().sprite = deck[1].Texture;

        _dealerCardImages.Add(newCard);
        deck.RemoveAt(1);
    }

    /// <summary>
    /// Sets the hidden card of the dealer and displays it face down.
    /// </summary>
    private void SetDealerHidden()
    {
        _dealerHidden = deck[1];

        GameObject newCard = Instantiate(_cardBase, _dealerHiddenBase.transform);

        newCard.transform.localPosition = Vector3.zero;

        newCard.GetComponent<Image>().sprite = _cardBackImage;

        _dealerHiddenCard = newCard;
        deck.RemoveAt(1);
    }

    private void DisplayMessage(string message)
    {
        
    }

    /// <summary>
    /// Resets and re-initializes the Deck.
    /// </summary>
    public void InitDeck()
    {
        // clear the list so it can be reinitialized
        deck.Clear();
        

        // clear the player's cards
        for (int i = 0; i < _playerCardImages.Count; i++)
        {
            GameObject card = _playerCardImages[i];
            
            Destroy(card);
        }
        _playerCardImages.Clear();

        // clear the dealer's cards
        for (int i = 0; i < _dealerCardImages.Count; i++)
        {
            GameObject card = _dealerCardImages[i]; 
            Destroy(card);
        }
        _dealerCardImages.Clear();

        // reset the player and dealer total
        _playerCount = 0;
        _dealerHidden = null;
        _dealerCount = 0;

        int cardNumber = 1;
        int suit = 1;

        // create a new instance for each card number from each suit
        for (int i = 0; i < 52; i++)
        {
            if (cardNumber > 13)
            {
                cardNumber = 1;
                suit++;
            }

            if (suit > 4)
                break;

            deck.Add(new Card(cardNumber <= 10 ? cardNumber : 10, suit, _cardTextures[i]));

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
        _messageHolder.HideMessage();
        _startButton.gameObject.SetActive(false);

        _hitButton.interactable = true;
        _standButton.interactable = true;

        AddCardToPlayer();

        SetDealerHidden();

        AddCardToPlayer();

        AddCardToDealer();
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
            _messageHolder.ShowMessage();
            _messageHolder.SetMessage("PLAYER BUST - DEALER WIN");
            _standButton.interactable = false;
            _hitButton.interactable = false;

            _startButton.gameObject.SetActive(true);

            return;
        }
        // If the player's count is equal to 21, they win blackjack.
        else if (_playerCount == 21)
        {
            _messageHolder.ShowMessage();
            _messageHolder.SetMessage("BLACKJACK - PLAYER WIN");
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
        _dealerHiddenCard.GetComponent<Image>().sprite = _dealerHidden.Texture;

        // Continuously deal until they reach a win or loss outcome.
        while(_dealerCount < 21 && _dealerCount < _playerCount)
        {
            AddCardToDealer();
        }

        _messageHolder.ShowMessage();

        // Determine the outcome of the draw.
        if (_dealerCount > 21)
        {
            _messageHolder.SetMessage("DEALER BUST - PLAYER WIN");
        }
        else if (_dealerCount > _playerCount || _dealerCount == 21)
        {
            _messageHolder.SetMessage("DEALER WIN");
        }
        else
        {
            _messageHolder.SetMessage("PLAYER WIN");
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
