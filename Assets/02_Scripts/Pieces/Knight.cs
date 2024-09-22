using UnityEngine;

namespace TK
{
    public class Knight : Piece
    {
        public override bool IsMoveValid(Vector2Int targetCoordinates, Board board)
        {
            var deltaX = Mathf.Abs(targetCoordinates.x - CurrentCoordinates.x);
            var deltaY = Mathf.Abs(targetCoordinates.y - CurrentCoordinates.y);

            // The move is valid if it forms an L-shape: (2, 1) or (1, 2)
            return (deltaX == 2 && deltaY == 1) || (deltaX == 1 && deltaY == 2);
        }
    }
}