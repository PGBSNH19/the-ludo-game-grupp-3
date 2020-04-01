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

        public static void PickPieceToMove(List<Piece> activePieces, int roll, List<Piece> inactivePieces, GameEngine game)
        {

            if (MoveFromYard())
            {
                if (roll == 6)
                {
                    // Gör val av PickInactivePieceToMove eller PickActivePieceToMove
                    if (inactivePieces.Count>1 && MoveTwoFromYard())
                    {
                        
                    }
                    else
                    {
                        
                    }
                    
                }
                else if (roll == 1)
                {
                    // Gör val av PickInactivePieceToMove eller PickActivePieceToMove
                    
                }


            }
            else
            {

            }


            //skriv menu för inaktiva pjäser
        }

        private static bool MoveTwoFromYard()
        {
            return true;
        }

        private static bool MoveFromYard()
        {
            return true;
        }
    }
}
