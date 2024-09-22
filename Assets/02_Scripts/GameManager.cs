using System;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<Player> OnGameEnd;
        [SerializeField] private Board board;
        [SerializeField] private List<Piece> player1Pieces;
        [SerializeField] private List<Piece> player2Pieces;
        private Player _player1;
        private Player _player2;
        private Player _currentPlayer;
        private Piece _selectedPiece;
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (!Instance) Instance = this;
            else Destroy(gameObject);

            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void Start()
        {
            InitializeGame();
        }

        // Initializes the game and sets up players
        private void InitializeGame()
        {
            _player1 = new Player("Player 1", player1Pieces);
            _player2 = new Player("Player 2", player2Pieces);
            _currentPlayer = _player1;
            SetPlayerTurn();
        }

        // Switches the turn between players
        private void SwitchTurn()
        {
            _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
            if (!CanPlayerMove(_currentPlayer))
            {
                Debug.Log($"{_currentPlayer.Name} cannot move!");
                SwitchTurn();
                return;
            }

            SetPlayerTurn();
        }

        private bool CanPlayerMove(Player player)
        {
            return !board.IsAllPiecesPlaced || board.AnyPieceFromPlayerCanMove(player.Pieces);
        }

        private void SetPlayerTurn()
        {
            var currentPlayer = _currentPlayer;
            var opponentPlayer = currentPlayer == _player1 ? _player2 : _player1;

            if (board.IsAllPiecesPlaced)
            {
                currentPlayer.EnableAllPiecesInteraction();
            }
            else
            {
                currentPlayer.EnableNonPlacedPiecesInteraction();
            }

            opponentPlayer.DisableAllPiecesInteraction();
            Debug.Log($"{_currentPlayer.Name}'s turn.");
        }

        // Handles clicking on a piece
        public void HandlePieceClick(Piece piece)
        {
            _selectedPiece?.SetDefaultColor();
            piece.SetHighlightedColor();
            board.HighlightCellsIfValidForPiece(piece);
            _selectedPiece = piece;
        }

        // Handles clicking on a board cell
        public void HandleBoardCellClick(BoardCell cell)
        {
            if (!_selectedPiece)
            {
                return;
            }

            if (board.MovePiece(cell, _selectedPiece))
            {
                _selectedPiece = null;
                if (board.CheckWin(_currentPlayer))
                {
                    OnGameEnd?.Invoke(_currentPlayer);
                }
                else
                {
                    SwitchTurn();
                }
            }
        }

        public void RestartGame()
        {
            board.ResetBoard();
            InitializeGame();
        }
    }
}