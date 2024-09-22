using UnityEngine;

namespace TK
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Piece : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Collider2D _collider2D;
        private Vector3 _initialPosition;

        public Player Owner { get; private set; }
        public Vector2Int CurrentCoordinates { get; private set; }
        public bool IsPlaced { get; private set; }

        private void OnMouseDown()
        {
            if (Helpers.IsMouseOverUI()) return;
            GameManager.Instance.HandlePieceClick(this);
        }

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _initialPosition = transform.position;
        }

        public void Init(Player owner)
        {
            transform.position = _initialPosition;
            IsPlaced = false;
            Owner = owner;
            CurrentCoordinates = new Vector2Int(-1, -1);
            SetDefaultColor();
        }

        // Place the piece on the board at a specific grid position
        public void Place(BoardCell cell)
        {
            CurrentCoordinates = cell.Coordinates;
            transform.position = cell.transform.position;
            IsPlaced = true;
            SetDefaultColor();
        }

        public void SetDefaultColor()
        {
            spriteRenderer.color = Color.white;
        }

        public void SetHighlightedColor()
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }

        public void EnableInteraction()
        {
            _collider2D.enabled = true;
        }

        public void DisableInteraction()
        {
            _collider2D.enabled = false;
        }

        public void ResetPiece()
        {
            Init(null);
        }

        // Abstract method to validate movement (implemented by derived classes)
        public abstract bool IsMoveValid(Vector2Int targetCoordinates, Board board);
    }
}