# I3E_Asg1_XavierOng_ReadMe

## 1. Gamee Description
This game is a first-person game where player must navigate through to collect cards and coins while avoiding hazardous areas, unlocking doors and escaping through the final exit

## 2. How to run the game
1. Open the project in Unity
2. Open the main scene
3. Press Play in the unity editor

## 3. Controls
### Key   |  Action
W      | Move Forward
A      | Move Left
S      | Move Backward
D      | Move Right
Mouse  | Look Around
E      | Interact / Collect / Scan Card

## 4. Game Objective
- Collect All Access Cards.
- Collect All Coins.
- Avoid Hazardous Areas.
- Use the correct card to unlock secured doors.
- Unlock the Final Door.\
- Reach the Exit to complete the game.

## 5. Gameplay Instructions

### Collecting Cards
Look at the access card aand press "E", and the card will be added into the inventory.

### Collecting Coins
Look at the coin and press "E", the coin count will increase.

### Unlocking Doors
Approach a card scanner and press "E", if the card is in the inventory the door will open, else an "Access Denied" essage will appear

### Health System
At the start player will have 5 hp,
entering hazardous areas reduces the Death Zone HP bar.

When it reaches zero:
- Lose 1 HP
- Respawn at the latest checkpoint

If HP reaches zero:
- Game Over screen appears
- Player respawns at the starting point
- HP is restored

### Final Door 
The Final Door will only open after:
- All Access Cards have been collected
- All Coins have been collected

## 6. Platform Requirements
Operating System
- Windows 10 or Windows 11

Hardware
- Minimum:
 - Intel Core i3 or equivalent
 - 8 GB RAM
 - DirectX 11 compatible graphics card
 - 1 GB free storage

Recommended:
- Intel Core i5 or better
- 16 GB RAM
- Dedicated GPU
- 2 GB free storage

Used in Gameplay Video
- Intel Core i9
- 32 GB RAM
- Nvidia GeForce RTX 4060

## 7. Unity Version
Developed Using: Unity 6000.3.13f1

## 8. Known Limitations / Bugs
- Doors may briefly clip through nearby objects if placed too close to walls.
- Players may need to face cards or scanners directly for interaction to register.
- Hazard damage only applies while inside the designated damage area.
- The Final Door only unlocks after all required collectibles have been obtained.
- Restarting the game resets all progress.

## 9. References and Credits
Models
- Unity Asset Store
 - Coin: https://assetstore.unity.com/packages/3d/props/gold-coins-1810
 - Card: https://assetstore.unity.com/packages/3d/props/retro-psx-horror-puzzle-item-pack-icon-lowpoly-250188
 - Scanner: https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-access-machine-162924
- Probuilder

Textures & Materials
- Custom Materials

Audio 
- Unity Asset Store: https://assetstore.unity.com/packages/audio/sound-fx/collectables-sound-effects-pack-290553
- Background Music: AI-generated: https://suno.com/s/3dIvmq69qJFyhmvB

Software Used
- Unity 6
- Visual Studio Code
- Suno (Background Music)
- ChatGPT (code assistance and debugging, ray casting and Quaternion.Lerp)

## 10. Puzzle Answer Key
### Blue Door
Required: Blue Access Card

### Red Door
Required: Red Access Card

### Yellow Door
Required: Yellow Access Card

### Final Door
Required:
- Blue Access Card
- Red Access Card
- Yellow Access Card
- All Coins Collected
