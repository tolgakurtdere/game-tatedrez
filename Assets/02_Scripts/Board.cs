using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace TK
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private BoardCell[] cells;
        private const int ROW_COUNT = 3;
        private const int COLUMN_COUNT = 3;
        private readonly BoardCell[,] _grid = new BoardCell[ROW_COUNT, COLUMN_COUNT];
        public int PlacedPieces { get; private set; }
        public bool IsAllPiecesPlaced => PlacedPieces >= ROW_COUNT + COLUMN_COUNT;

        private void Awake()
        {
            var cellCount = cells.Length;
            if (cellCount != _grid.Length)
            {
                Debug.LogError("There is an error!");
                return;
            }

            var index = 0;
            for (var row = 0; row < ROW_COUNT; row++)
            {
                for (var col = 0; col < COLUMN_COUNT; col++)
                {
                    var cell = cells[index];
                    cell.Init(new Vector2Int(row, col));
                    _grid[row, col] = cell;
                    index++;
                }
            }
        }

        [CanBeNull]
        public BoardCell GetCell(int row, int column)
        {
            var insideBounds = row is >= 0 and < ROW_COUNT && column is >= 0 and < COLUMN_COUNT;
            return insideBounds ? _grid[row, column] : null;
        }

        public void HighlightCellsIfValidForPiece(Piece piece)
        {
            foreach (var cell in cells)
            {
                if (IsCellValidForPiece(cell, piece))
                {
                    cell.SetHighlightedColor();
                }
                else
                {
                    cell.SetDefaultColor();
                }
            }
        }

        public void SetDefaultColorCells()
        {
            foreach (var cell in cells)
            {
                cell.SetDefaultColor();
            }
        }

        public bool MovePiece(BoardCell cell, Piece piece)
        {
            if (IsCellValidForPiece(cell, piece))
            {
                GetCell(piece.CurrentCoordinates.x, piece.CurrentCoordinates.y)?.RemovePiece(); // Clear old position
                cell.SetPiece(piece); // Move to new position
                piece.Place(cell);

                SetDefaultColorCells();
                PlacedPieces++;
                return true;
            }

            return false;
        }

        public bool IsCellValidForPiece(BoardCell cell, Piece piece)
        {
            return IsAllPiecesPlaced
                ? cell.IsEmpty && piece.IsMoveValid(cell.Coordinates, this)
                : cell.IsEmpty;
        }

        public bool AnyPieceFromPlayerCanMove(List<Piece> pieces)
        {
            foreach (var piece in pieces)
            {
                if (!piece.IsPlaced)
                {
                    continue;
                }

                if (cells.Any(cell => cell.IsEmpty && piece.IsMoveValid(cell.Coordinates, this)))
                {
                    return true;
                }
            }

            return false;
        }

        // Check for Tic-Tac-Toe win condition
        public bool CheckWin(Player player)
        {
            for (var i = 0; i < 3; i++)
            {
                // Check rows
                if (_grid[i, 0] && _grid[i, 1] && _grid[i, 2] &&
                    _grid[i, 0].Owner == player && _grid[i, 1].Owner == player && _grid[i, 2].Owner == player)
                    return true;

                // Check columns
                if (_grid[0, i] && _grid[1, i] && _grid[2, i] &&
                    _grid[0, i].Owner == player && _grid[1, i].Owner == player && _grid[2, i].Owner == player)
                    return true;
            }

            // Check diagonals
            if (_grid[0, 0] && _grid[1, 1] && _grid[2, 2] &&
                _grid[0, 0].Owner == player && _grid[1, 1].Owner == player && _grid[2, 2].Owner == player)
                return true;

            if (_grid[0, 2] && _grid[1, 1] && _grid[2, 0] &&
                _grid[0, 2].Owner == player && _grid[1, 1].Owner == player && _grid[2, 0].Owner == player)
                return true;

            return false;
        }

        public void ResetBoard()
        {
            PlacedPieces = 0;
            
            foreach (var cell in cells)
            {
                cell.ResetCell();
            }
        }
    }
}