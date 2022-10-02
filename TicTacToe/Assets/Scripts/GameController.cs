using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private SlotUIComponent[,] GameBoard;
    // Reference to the players
    private Player[] thePlayers;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _ticTacToeBoardPanel;
    [SerializeField] private GameObject _gameStartContainer;
    [SerializeField] private TMPro.TMP_Dropdown _gameStartGridSizeDropdown;
    [SerializeField] private TMPro.TMP_Dropdown _gameStartCharacterDropdown;
    [SerializeField] private GameObject _gameOverContainer;
    [SerializeField] private TMPro.TMP_Dropdown _gameOverGridSizeDropdown;
    [SerializeField] private TMPro.TMP_Text _gameOverText;
    [SerializeField] private TMPro.TMP_Dropdown _gameOverCharacterDropdown;
    [SerializeField] private GameboardUIComponent _gameboardUIComponent;

    private GridLayoutGroup boardLayoutGroup;
    private static GameController _gameController;

    public static GameController Instance => _gameController;

    public Player currentPlayer { get; set; }
    public int NumberOfTurnsTaken { get; set; }
    private int boardSize;

    void Awake()
    {
        _gameController = this;
    }
    
    public void InitializeBoard(int maxSize)
    {
        boardSize = maxSize;
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

    private void ClearBoard()
    {
        foreach (Transform child in _ticTacToeBoardPanel.transform) {
            Destroy(child.gameObject);
        }
    }
    
    private void ChangePlayerTurn()
    {
        this.thePlayers[0].myTurn = !this.thePlayers[0].myTurn;
        this.thePlayers[1].myTurn = !this.thePlayers[1].myTurn;
        
        currentPlayer = this.thePlayers[0].myTurn ? this.thePlayers[0] : this.thePlayers[1];
        
        if(currentPlayer is AIPlayer)
            ProcessMove(null);
    }

    private bool GameHasWinner()
    {
        int xCoord = this.currentPlayer.coords[0];
        int yCoord = this.currentPlayer.coords[1];

        // Check Column
        for (int i = 0; i < this.boardSize; i++)
        {
            if (this.GameBoard[i, yCoord].Slot.character != this.currentPlayer.playerChar)
                break;
            
            if (i == boardSize - 1)
                return true;
        }
        
        // Check Row
        for (int i = 0; i < this.boardSize; i++)
        {
            if (this.GameBoard[xCoord, i].Slot.character != this.currentPlayer.playerChar)
                break;
            if (i == boardSize - 1)
                return true;
        }
        
        // Check Diagonal From Top Left To Bottom Right
        for (int i = 0; i < this.boardSize; i++)
        {
            if (this.GameBoard[i, i].Slot.character != this.currentPlayer.playerChar)
                break;
            if (i == boardSize - 1)
                return true;
        }

        // Check Diagonal From Top Right to Bottom Left
        for (int i = 0; i < this.boardSize; i++)
        {
            if (this.GameBoard[i, (this.boardSize - 1) - i].Slot.character != this.currentPlayer.playerChar)
                break;
            if (i == boardSize - 1)
                return true;
        }
        
        return false;
    }

    private bool IsTieGame()
    {
        return NumberOfTurnsTaken == (this.boardSize * this.boardSize);
    }
    
    public void ProcessMove(Slot slot)
    {
        int[] coordsSlotChosen = currentPlayer.TakeTurn(slot);
        this.GameBoard[coordsSlotChosen[0], coordsSlotChosen[1]].UpdateSlot(currentPlayer.playerChar);
        
        NumberOfTurnsTaken++;

        // If Number of turns is less than turns for there to be a possible winner
        if (NumberOfTurnsTaken < (this.boardSize * 2) - 1)
        {
            this.ChangePlayerTurn();
            return;
        }

        if (GameHasWinner())
        {
            _gameOverText.text = "Player " + currentPlayer.playerChar + " wins!";
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

    public bool SlotIsEmpty(int[] coords)
    {
        return this.GameBoard[coords[0], coords[1]].Slot.character == ' ';
    }
    
    public void NewGame()
    {
        this.InitializeBoard(_gameStartGridSizeDropdown.value + 3);
        thePlayers = new Player[2];
        if (_gameStartCharacterDropdown.value == 0)
        {
            thePlayers[0] = new RealPlayer('X');
            thePlayers[0].myTurn = true;
            currentPlayer = thePlayers[0];
            thePlayers[1] = new AIPlayer('O', this.boardSize);
        }
        else
        {
            thePlayers[0] = new RealPlayer('O');
            thePlayers[1] = new AIPlayer('X', this.boardSize);
            thePlayers[1].myTurn = true;
            currentPlayer = thePlayers[1];
        }
        
        _ticTacToeBoardPanel.SetActive(true);
        _gameStartContainer.SetActive(false);
        
        
        if (currentPlayer is AIPlayer)
            this.ProcessMove(null);
    }

    public void PlayAgain()
    {
        NumberOfTurnsTaken = 0;
        this.ClearBoard();
        this.InitializeBoard(_gameOverGridSizeDropdown.value + 3);
        thePlayers = new Player[2];
        if (_gameOverCharacterDropdown.value == 0)
        {
            thePlayers[0] = new RealPlayer('X');
            thePlayers[0].myTurn = true;
            currentPlayer = thePlayers[0];
            thePlayers[1] = new AIPlayer('O', this.boardSize);
        }
        else
        {
            thePlayers[0] = new RealPlayer('O');
            thePlayers[1] = new AIPlayer('X', this.boardSize);
            thePlayers[1].myTurn = true;
            currentPlayer = thePlayers[1];
        }
        
        _gameOverContainer.SetActive(false);
        
        if (currentPlayer is AIPlayer)
            this.ProcessMove(null);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
