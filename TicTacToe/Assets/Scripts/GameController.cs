using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Reference to the slot ui components in the game board
    /// </summary>
    private SlotUIComponent[,] GameBoard;
    
    /// <summary>
    /// Reference to the players
    /// </summary>
    private Player[] _thePlayers;
    
    /// <summary>
    /// Reference to the slot prefab
    /// </summary>
    [SerializeField] private GameObject _slotPrefab;
    
    /// <summary>
    /// Reference to the tic tac toe board panel
    /// </summary>
    [SerializeField] private GameObject _ticTacToeBoardPanel;
    
    /// <summary>
    /// Reference to the game start container
    /// </summary>
    [SerializeField] private GameObject _gameStartContainer;
    
    /// <summary>
    /// Reference to the game start grid size drop down
    /// </summary>
    [SerializeField] private TMPro.TMP_Dropdown _gameStartGridSizeDropdown;
    
    /// <summary>
    /// Reference to the game start character size drop down
    /// </summary>
    [SerializeField] private TMPro.TMP_Dropdown _gameStartCharacterDropdown;
    
    /// <summary>
    /// Reference to the game over container
    /// </summary>
    [SerializeField] private GameObject _gameOverContainer;
    
    /// <summary>
    /// Reference to the game over grid size drop down
    /// </summary>
    [SerializeField] private TMPro.TMP_Dropdown _gameOverGridSizeDropdown;
    
    /// <summary>
    /// Reference to the game over text
    /// </summary>
    [SerializeField] private TMPro.TMP_Text _gameOverText;
    
    /// <summary>
    /// Reference to the game over character size drop down
    /// </summary>
    [SerializeField] private TMPro.TMP_Dropdown _gameOverCharacterDropdown;
    
    /// <summary>
    /// Reference to the gameboardui component
    /// </summary>
    [SerializeField] private GameboardUIComponent _gameboardUIComponent;

    /// <summary>
    /// Reference to the audio source
    /// </summary>
    [SerializeField] private AudioSource _audioSource;
    
    /// <summary>
    /// Reference to the boardlayoutgroup
    /// </summary>
    private GridLayoutGroup _boardLayoutGroup;
    
    /// <summary>
    /// Reference to the game controller for singleton pattern
    /// </summary>
    private static GameController _gameController;
    
    /// <summary>
    /// Reference to the game controller for singleton pattern
    /// </summary>
    public static GameController Instance => _gameController;
    
    /// <summary>
    /// Reference to the current player
    /// </summary>
    private Player _currentPlayer;
    
    /// <summary>
    /// Reference to the number of turns that have taken place
    /// </summary>
    private int _numberOfTurnsTaken;
    
    /// <summary>
    /// Reference to the size of the board chosen by the user
    /// </summary>
    private int _boardSize;

    void Awake()
    {
        _gameController = this;
    }
    
    /// <summary>
    /// Function used to initialize and set up the board
    /// </summary>
    /// <param name="maxSize">the max size of the board chosen by the user</param>
    private void InitializeBoard(int maxSize)
    {
        _boardSize = maxSize;
        _gameboardUIComponent.Initialize(maxSize, 32);
        GameBoard = new SlotUIComponent[maxSize, maxSize];

        for (int i = 0; i < maxSize; i++)
        {
            for (int j = 0; j < maxSize; j++)
            {
                GameObject slot = Instantiate(_slotPrefab, _ticTacToeBoardPanel.transform);
                SlotUIComponent slotUIComponent = slot.GetComponent<SlotUIComponent>();
                slotUIComponent.Initialize(new Slot(i, j), null);
                GameBoard[i, j] = slotUIComponent;
            }
        }
    }

    /// <summary>
    /// Function used to clear the board
    /// </summary>
    private void ClearBoard()
    {
        foreach (Transform child in _ticTacToeBoardPanel.transform) {
            Destroy(child.gameObject);
        }
    }
    
    /// <summary>
    /// Function used to change player turn
    /// </summary>
    private void ChangePlayerTurn()
    {
        this._thePlayers[0].myTurn = !this._thePlayers[0].myTurn;
        this._thePlayers[1].myTurn = !this._thePlayers[1].myTurn;
        
        _currentPlayer = this._thePlayers[0].myTurn ? this._thePlayers[0] : this._thePlayers[1];
        
        if(_currentPlayer is AIPlayer)
            ProcessMove(null);
    }

    /// <summary>
    /// Function to check if the game has a winner
    /// </summary>
    /// <returns>Returns true or false whether a player has won</returns>
    private bool GameHasWinner()
    {
        int xCoord = this._currentPlayer.coords[0];
        int yCoord = this._currentPlayer.coords[1];

        // Check Column
        for (int i = 0; i < this._boardSize; i++)
        {
            if (this.GameBoard[i, yCoord].Slot.character != this._currentPlayer.playerChar)
                break;
            
            if (i == _boardSize - 1)
                return true;
        }
        
        // Check Row
        for (int i = 0; i < this._boardSize; i++)
        {
            if (this.GameBoard[xCoord, i].Slot.character != this._currentPlayer.playerChar)
                break;
            if (i == _boardSize - 1)
                return true;
        }
        
        // Check Diagonal From Top Left To Bottom Right
        for (int i = 0; i < this._boardSize; i++)
        {
            if (this.GameBoard[i, i].Slot.character != this._currentPlayer.playerChar)
                break;
            if (i == _boardSize - 1)
                return true;
        }

        // Check Diagonal From Top Right to Bottom Left
        for (int i = 0; i < this._boardSize; i++)
        {
            if (this.GameBoard[i, (this._boardSize - 1) - i].Slot.character != this._currentPlayer.playerChar)
                break;
            if (i == _boardSize - 1)
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Function used to check if the game is a tie
    /// </summary>
    /// <returns>Returns true or false whether their is a tie</returns>
    private bool IsTieGame()
    {
        return _numberOfTurnsTaken == (this._boardSize * this._boardSize);
    }
    
    /// <summary>
    /// Function used to process the most recent move that has taken place
    /// </summary>
    /// <param name="slot">the slot of the most recent move</param>
    public void ProcessMove(Slot slot)
    {
        int[] coordsSlotChosen = _currentPlayer.TakeTurn(slot);
        this.GameBoard[coordsSlotChosen[0], coordsSlotChosen[1]].UpdateSlot(_currentPlayer.playerChar);
        
        _numberOfTurnsTaken++;
        _audioSource.Play();
        // If Number of turns is less than turns for there to be a possible winner
        if (_numberOfTurnsTaken < (this._boardSize * 2) - 1)
        {
            this.ChangePlayerTurn();
            return;
        }

        if (GameHasWinner())
        {
            _gameOverText.text = "Player " + _currentPlayer.playerChar + " wins!";
            _gameOverContainer.SetActive(true);
            return;
        }
        
        if (IsTieGame())
        {
            _gameOverText.text = "Tie Game!";
            _gameOverContainer.SetActive(true);
            return;
        }
        
        this.ChangePlayerTurn();
    }

    /// <summary>
    /// Function to check if a slot is empty based on passed in coords
    /// </summary>
    /// <param name="coords">the coords to check in the board</param>
    /// <returns>Returns true or false if the slot at the coords passed in is empty</returns>
    public bool SlotIsEmpty(int[] coords)
    {
        return this.GameBoard[coords[0], coords[1]].Slot.character == ' ';
    }
    
    /// <summary>
    /// Function called to start a new game
    /// </summary>
    public void NewGame()
    {
        _audioSource.Play();
        this.InitializeBoard(_gameStartGridSizeDropdown.value + 3);
        _thePlayers = new Player[2];
        if (_gameStartCharacterDropdown.value == 0)
        {
            _thePlayers[0] = new RealPlayer('X');
            _thePlayers[0].myTurn = true;
            _currentPlayer = _thePlayers[0];
            _thePlayers[1] = new AIPlayer('O', this._boardSize);
        }
        else
        {
            _thePlayers[0] = new RealPlayer('O');
            _thePlayers[1] = new AIPlayer('X', this._boardSize);
            _thePlayers[1].myTurn = true;
            _currentPlayer = _thePlayers[1];
        }
        
        _ticTacToeBoardPanel.SetActive(true);
        _gameStartContainer.SetActive(false);
        
        
        if (_currentPlayer is AIPlayer)
            this.ProcessMove(null);
    }

    /// <summary>
    /// Function used to play the game again
    /// </summary>
    public void PlayAgain()
    {
        _audioSource.Play();
        _numberOfTurnsTaken = 0;
        this.ClearBoard();
        this.InitializeBoard(_gameOverGridSizeDropdown.value + 3);
        _thePlayers = new Player[2];
        if (_gameOverCharacterDropdown.value == 0)
        {
            _thePlayers[0] = new RealPlayer('X');
            _thePlayers[0].myTurn = true;
            _currentPlayer = _thePlayers[0];
            _thePlayers[1] = new AIPlayer('O', this._boardSize);
        }
        else
        {
            _thePlayers[0] = new RealPlayer('O');
            _thePlayers[1] = new AIPlayer('X', this._boardSize);
            _thePlayers[1].myTurn = true;
            _currentPlayer = _thePlayers[1];
        }
        
        _gameOverContainer.SetActive(false);
        
        if (_currentPlayer is AIPlayer)
            this.ProcessMove(null);
    }

    /// <summary>
    /// Function used to quit the game
    /// </summary>
    public void QuitGame()
    {
        _audioSource.Play();
        Application.Quit();
    }
}
