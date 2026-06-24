# Prototype 3: Endless Runner

**Play the Game:** [Junior Programmer on Unity Play](https://play.unity.com/en/games/cc52519c-3a41-4a3b-a2c8-e76e0d26d5a2/junior-programmer)

## Gameplay Mechanic
An endless side-scrolling runner where the player must time their jumps to hurdle over incoming obstacles.

## Core Game Loop
1. **Run**: The background continuously loops to simulate forward momentum.
2. **Jump**: Player inputs jump commands to clear obstacles.
3. **Game Over**: Colliding with an obstacle triggers a failure state, stopping the background and spawning systems.

## Dataflow
- **Input**: Jump action triggers an upward impulse in `PlayerController`.
- **Environment**: `ScreenMovement` and `RepeatBackground` translate objects to the left. When off-screen, they reset their positions mathematically.
- **State Management**: A `BoolEventChannelSO` broadcasts the "Game Over" state to halt all moving components instantly.