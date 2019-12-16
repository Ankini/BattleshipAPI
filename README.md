# Battleship state tracking APIs

This exercise is based on the classic game “Battleship”.  
- Two players 
- Each have a 10x10 board  
- During setup, players can place an arbitrary number of “battleships” on their board. The ships are 1-by-n sized, must fit entirely on the board, and must be aligned either vertically or horizontally. 
- During play, players take turn “attacking” a single position on the opponent’s board, and the opponent must respond by either reporting a “hit” on one of their battleships (if one occupies that position) or a “miss”
- A battleship is sunk if it has been hit on all the squares it occupies 
- A player wins if all of their opponent’s battleships have been sunk.  
 
 
Technology glossary 

WEB APIs
- Visual Studio 2017
- .NET framework 4.7
- Web API 5.2.7
- AutoMapper 8.0
- Unity 5.11
Unit test 
- xUNIT 2.4
- Nsubstitute 4.2
- AutoFixture 4.11


Functionality/Features

3 APIs supporting services for single player 
- Place Ship
- Take an attack 
- Reset Game

Model Validation via annotation 
Global exception handling via IExceptionHandler
Global exception logging via IExceptionLogger
Validation while placing and attacking the Ship on board 
Reset board for resetting game
Extension method demo via Enum description 
xUnit Unit test cases to verify the BL manager functionalities


# Solution briefing

1. Battleship.Services 
  
  StateTrackerController
  API Exception handling 
  GlobalExceptionHandler - this is exception handler to capture all the unhandle excceptions within the web api pipeline
  GlobalExceptionLogger - Logging all the unhandled exception for health monitoring and trouble shooting
  
  API Configs
  
  MapperConfig -Battleship - Automapper – Register
  UnityConfig - Battleship - Dependacy Registry
  WebApiConfig - Battleship  - Web Api config
 
 2. Battleship.BL 
 
  StateTrackingManager – state tracking business logic
  ShipOrientationFactory – Facotry to resolve ShipOrientation for calculating points occupied on board basesd on orientation
  BattleShipFactory – Factory to resolve Ship type base on ShipType enum

3. Battleship.Tests

  StateTrackingManagerTests – unit test for stateTrackingManager
  ConfigureAutoMapperAttribute – automapper config for unit tests
