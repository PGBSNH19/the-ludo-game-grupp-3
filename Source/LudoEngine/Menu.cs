using System;
using System.Collections.Generic;
using System.Linq;
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

        

        public static bool WantToMoveTwoPiecesFromYard()
        {
            //fråga vill du flytta 2 gubbar till 1  från bo ja/nej
            return true;
        }

        public static bool WantToMoveInactivePiece()
        {
            //vill du gå ut med gubbar eller vill du gå med en Active gubbe
            return true;
        }
    }
}
