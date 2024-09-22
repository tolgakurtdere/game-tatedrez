using System.Collections.Generic;

namespace TK
{
    public class Player
    {
        public string Name { get; private set; }
        public List<Piece> Pieces { get; private set; }

        public Player(string name, List<Piece> pieces)
        {
            Name = name;
            Pieces = pieces;

            foreach (var piece in pieces)
            {
                piece.Init(this);
            }
        }

        public void EnableAllPiecesInteraction()
        {
            foreach (var piece in Pieces)
            {
                piece.EnableInteraction();
            }
        }

        public void DisableAllPiecesInteraction()
        {
            foreach (var piece in Pieces)
            {
                piece.DisableInteraction();
            }
        }

        public void EnableNonPlacedPiecesInteraction()
        {
            foreach (var piece in Pieces)
            {
                if (!piece.IsPlaced)
                {
                    piece.EnableInteraction();
                }
                else
                {
                    piece.DisableInteraction();
                }
            }
        }
    }
}