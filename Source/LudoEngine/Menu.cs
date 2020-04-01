using System;
using System.Collections.Generic;
using System.Text;

namespace LudoEngine
{
    public class Menu
    { 



        public static void PickActivePieceToMove(List<Piece> activePieces)
        {
            //skriv menu för aktiva pjäser
        }

        public static void PickInactivePieceToMove()
        {
            //skriv menu för inaktiva pjäser
        }

        public static void PickPieceToMove(List<Piece> activePieces, int roll)
        {
            if (roll == 6)
            {
                // Gör val av PickInactivePieceToMove eller PickActivePieceToMove
            }
            else if (roll == 1)
            {
                // Gör val av PickInactivePieceToMove eller PickActivePieceToMove
            }


            //skriv menu för inaktiva pjäser
        }
        

    }
}
