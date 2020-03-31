| Player-                                            | Interaktion |
| -------------------------------------------------- | ----------- |
| Enum Color (red,green,blue,yellow) Tas kanske bort | Piece       |
| Array[4]<Piece> Pieces tas kanske bort             | GameEngie   |
| List<Game> ActiveGames                             | Color       |
| int ID-                                            |             |
| string name-                                       |             |
| Bool HasFinished -                                 |             |
| override ToString                                  |             |
|                                                    |             |
|                                                    |             |

| Piece-                    | Interaktion |
| ------------------------- | ----------- |
| Bool isActive             | Spelare     |
| int ID                    | GameEngie   |
| Bool HasFinished          | Color       |
| override ToString         |             |
| int steps(tärningsslaget) |             |
| int position = -1         |             |
|                           |             |
|                           |             |
|                           |             |

| Enum Color (Eventuellt i player) | Interaktion |
| -------------------------------- | ----------- |
| string Red                       | Spelare     |
| string blue                      |             |
| string yellow                    |             |
| string green                     |             |
|                                  |             |
|                                  |             |
|                                  |             |
|                                  |             |
|                                  |             |

| Dice-                 | Interaktion |
| --------------------- | ----------- |
| int GetRandom(1-6){ } | GameEngine  |
|                       | Piece       |
|                       |             |
|                       |             |

| GameState                               | Interaktion |
| --------------------------------------- | ----------- |
| int id                                  | player      |
| List<player> players                    | gameEngine  |
| ïnt NextPlayer (vem ska slå nästa slag) |             |
| Bool HasFinished                        |             |
| Dictionary<Player, Pice>?????           |             |
|                                         |             |
|                                         |             |
|                                         |             |
|                                         |             |

| GameEngine                    | Interaktion |
| ----------------------------- | ----------- |
| int NumberOfplayers           | menu        |
| const int piecesPerPlayer = 4 | gameState   |
| StartNewGame()                | player      |
| LoadGame()                    |             |
| SaveGame(GameState)           |             |
| CreateNewPlayer(Color, Name)  |             |
|                               |             |
|                               |             |
|                               |             |

| Menu                               | Interaktion |
| ---------------------------------- | ----------- |
| printStartMeny                     | GameEngine  |
| MenuSwitch(start, log, load, save) |             |
| navigation(pilar upp ned osv)      |             |
|                                    |             |
|                                    |             |
|                                    |             |
|                                    |             |
|                                    |             |
|                                    |             |

| Log                     | Interaktion |
| ----------------------- | ----------- |
| PrintActiveGames        | menu        |
| PrintFinishedGames      | gameEngine  |
| SaveFinishedGamesTo CSV |             |
|                         |             |
|                         |             |
|                         |             |
|                         |             |
|                         |             |
|                         |             |