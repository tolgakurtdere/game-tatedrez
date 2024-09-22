using UnityEngine;

namespace TK
{
    public class Bishop : Piece
    {
        public override bool IsMoveValid(Vector2Int targetCoordinates, Board board)
        {
            // Check if the move is along a diagonal
            if (Mathf.Abs(targetCoordinates.x - CurrentCoordinates.x) !=
                Mathf.Abs(targetCoordinates.y - CurrentCoordinates.y))
            {
                return false;
            }

            // Check if the path is clear
            if (!IsPathClear(targetCoordinates, board))
            {
                return false;
            }

            return true;
        }

        private bool IsPathClear(Vector2Int targetCellCoordinates, Board board)
        {
            var xDirection = targetCellCoordinates.x > CurrentCoordinates.x ? 1 : -1;
            var yDirection = targetCellCoordinates.y > CurrentCoordinates.y ? 1 : -1;

            var x = CurrentCoordinates.x + xDirection;
            var y = CurrentCoordinates.y + yDirection;

            while (x != targetCellCoordinates.x && y != targetCellCoordinates.y)
            {
                if (board.GetCell(x, y) is not { IsEmpty: true })
                {
                    return false;
                }

                x += xDirection;
                y += yDirection;
            }

            return true;
        }
    }
}