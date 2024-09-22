using UnityEngine;

namespace TK
{
    public class Rook : Piece
    {
        public override bool IsMoveValid(Vector2Int targetCoordinates, Board board)
        {
            // Check if the move is in a straight line (either horizontally or vertically)
            if (targetCoordinates.x != CurrentCoordinates.x && targetCoordinates.y != CurrentCoordinates.y)
            {
                return false;
            }

            // Check if there are no pieces blocking the path
            if (!IsPathClear(targetCoordinates, board))
            {
                return false;
            }

            return true;
        }

        private bool IsPathClear(Vector2Int targetCellCoordinates, Board board)
        {
            if (targetCellCoordinates.x == CurrentCoordinates.x)
            {
                // Vertical move
                var startY = Mathf.Min(targetCellCoordinates.y, CurrentCoordinates.y) + 1;
                var endY = Mathf.Max(targetCellCoordinates.y, CurrentCoordinates.y);

                for (var y = startY; y < endY; y++)
                {
                    if (board.GetCell(targetCellCoordinates.x, y) is not { IsEmpty: true })
                    {
                        return false;
                    }
                }
            }
            else
            {
                // Horizontal move
                var startX = Mathf.Min(targetCellCoordinates.x, CurrentCoordinates.x) + 1;
                var endX = Mathf.Max(targetCellCoordinates.x, CurrentCoordinates.x);

                for (var x = startX; x < endX; x++)
                {
                    if (board.GetCell(x, targetCellCoordinates.y) is not { IsEmpty: true })
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}