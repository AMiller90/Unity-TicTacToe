using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private SlotUIComponent[,] GameBoard;
    // Reference to the players
    public Player[] thePlayers;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _ticTacToeBoardPanel;
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
        InitializeBoard(4);
        thePlayers = new Player[2];
        thePlayers[0] = new RealPlayer('X');
        thePlayers[0].myTurn = true;
        thePlayers[1] = new RealPlayer('O');
        currentPlayer = thePlayers[0];
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

    [ContextMenu("Test")]
    public void Test()
    {
        InitializeBoard(4);
    }

    private void ChangePlayerTurn()
    {
        this.thePlayers[0].myTurn = !this.thePlayers[0].myTurn;
        this.thePlayers[1].myTurn = !this.thePlayers[1].myTurn;
        
        currentPlayer = this.thePlayers[0].myTurn ? this.thePlayers[0] : this.thePlayers[1];
    }

    private bool GameHasWinner()
    {
        int xCoord = this.currentPlayer.coords[0];
        int yCoord = this.currentPlayer.coords[1];

        // Check Column
        for (int i = 0; i < this.boardSize; i++)
        {
            if (this.GameBoard[i, yCoord].Slot.character != this.currentPlayer.playerChar)
                return false;
        }
        
        return true;
    }

    private bool IsTieGame()
    {
        if (NumberOfTurnsTaken == (this.boardSize * this.boardSize))
        {
            return true;
        }

        return false;
    }
    
    public void ProcessMove()
    {
        NumberOfTurnsTaken++;

        if (NumberOfTurnsTaken < (this.boardSize * 2) - 1)
        {
            this.ChangePlayerTurn();
            return;
        }

        if (GameHasWinner())
        {
            Debug.Log("Player " + currentPlayer.playerChar + " has won!");
            return;
        }
        else if (IsTieGame())
        {
            Debug.Log("GAME OVER TIE!!!");
        }
        
        this.ChangePlayerTurn();
    }
}
