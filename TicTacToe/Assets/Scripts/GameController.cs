using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private SlotUIComponent[,] GameBoard;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _ticTacToeBoardPanel;
    [SerializeField] private GameboardUIComponent _gameboardUIComponent;
    private GridLayoutGroup boardLayoutGroup;
    private Player _currentPlayer;
    private static GameController _gameController;

    public static GameController Instance => _gameController;

    public Player currentPlayer
    {
        get => _currentPlayer;
        set => _currentPlayer = value;
    }

    void Awake()
    {
        _gameController = this;
        InitializeBoard(4);
    }
    
    public void InitializeBoard(int maxSize)
    {
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
    
}
