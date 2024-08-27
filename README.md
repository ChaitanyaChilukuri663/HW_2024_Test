# Endless Runner Game: Doofus Adventure

## Overview

Pulpit Adventure is an endless runner game developed in Unity. The player controls a cube character (Doofus) that moves from one pulpit (platform) to another, avoiding falling off. The game features random spawning of platforms with a countdown timer on each, creating a challenging gameplay experience where quick reflexes and precise movements are essential.

## Game Features

- **Endless Runner Gameplay:** The game continues indefinitely, with the difficulty gradually increasing as more pulpits appear.
- **Dynamic Pulpit Generation:** Pulpits spawn randomly adjacent to the current one, creating a unique path for each playthrough.
- **Countdown Timer:** Each pulpit displays a timer that indicates when it will be destroyed, adding to the challenge.
- **Score Tracking:** Players earn points for each successful move from one pulpit to another, and the score is displayed on the screen.
- **Game Over:** The game ends if the player falls off a pulpit or if no valid pulpits are available to jump to.

## How to Play

1. **Start the Game:** Launch the game from Unity Editor or build and run it on your preferred platform.
2. **Control the Character:** Use the arrow keys to move Doofus to adjacent pulpits.
3. **Stay on the Pulpits:** Move to a new pulpit before the current one’s timer reaches zero.
4. **Earn Points:** Successfully moving to a new pulpit increments your score.
5. **Avoid Falling:** Game over occurs if Doofus falls off a pulpit or no pulpit is available to move.

## Setup and Installation

To set up the game on your local machine, follow these steps:

1. **Clone the Repository:**
    ```bash
    git clone https://github.com/yourusername/pulpit-adventure.git
    ```

2. **Open the Project in Unity:**
   - Open Unity Hub.
   - Click on "Open Project" and select the cloned repository's folder.

3. **Play the Game:**
   - Once the project is open in Unity, click the Play button in the Unity Editor to start the game.

## Unity Project Structure

- **Assets/**: Contains all game assets, including scripts, prefabs, models, and scenes.
- **Scripts/**: Folder containing C# scripts for gameplay logic (e.g., player movement, pulpit spawning, score management).
- **Scenes/**: Main game scene, including game objects, UI elements, and lighting settings.

## Key Scripts

1. **GameManager.cs**: Handles game state, score updates, and game-over conditions.
2. **PlayerMovement.cs**: Manages player movement and interactions with pulpits.
3. **PulpitSpawner.cs**: Controls the spawning and destruction of pulpits with random timing.

## Screenshots

![Game Start](https://github.com/ChaitanyaChilukuri663/HW_2024_Test/blob/master/Images/Gamestart.png)
*Screenshot 1: Initial game screen with Doofus on a pulpit.*

![In-Game](https://github.com/ChaitanyaChilukuri663/HW_2024_Test/blob/master/Images/game.png)
*Screenshot 2: Gameplay showing the countdown timer on a pulpit.*

![Game Over](https://github.com/ChaitanyaChilukuri663/HW_2024_Test/blob/master/Images/Gamesover.png)
*Screenshot 3: Game over screen displaying the final score.*




