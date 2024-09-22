using JetBrains.Annotations;
using UnityEngine;

namespace TK
{
    public class BoardCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public Vector2Int Coordinates { get; private set; }
        private Piece Piece { get; set; }
        [CanBeNull] public Player Owner => Piece?.Owner;
        public bool IsEmpty => !Piece;

        private void OnMouseDown()
        {
            if (Helpers.IsMouseOverUI()) return;
            GameManager.Instance.HandleBoardCellClick(this);
        }

        public void Init(Vector2Int coordinates)
        {
            Coordinates = coordinates;
            SetDefaultColor();
        }

        public void SetDefaultColor()
        {
            var color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }

        public void SetHighlightedColor()
        {
            var color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;
        }

        public void SetPiece(Piece piece)
        {
            Piece = piece;
        }

        public void RemovePiece()
        {
            Piece = null;
        }

        public void ResetCell()
        {
            SetDefaultColor();
            Piece?.ResetPiece();
            RemovePiece();
        }
    }
}