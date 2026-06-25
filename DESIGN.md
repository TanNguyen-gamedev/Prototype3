# Prototype 3: Architecture & Lessons Learned

## Overview
Prototype 3 is an endless side-scrolling runner. The architectural focus is on creating a sense of forward momentum using background math looping, handling physics-based jumping, and using a decoupled Event Channel to instantly broadcast game state changes (like "Game Over") across multiple independent systems.

## Architecture & Implementation Details

### 1. Environment Illusion & Mathematical Looping (`RepeatBackground.cs`)
- **Stationary Player**: Instead of moving the player forward through a massive world, the player remains stationary on the X-axis while the world moves left towards them.
- **Math-Based Looping**: The background avoids expensive instantiation by calculating its own bounding box size at `Start()`. In `Update()`, once it translates left by exactly half its width (`_repeatWidth`), it seamlessly resets to its original `_startPosition`. 
- **Screen Movement**: Static objects like the background translate smoothly using `transform.Translate()` in `Update()` (`ScreenMovememnt.cs`).

### 2. Player Mechanics & Physics (`PlayerController.cs`)
- **Jumping**: Uses the New Input System. When the `Jump` action is performed, if the player is grounded, an upward impulse force is applied to the `Rigidbody`.
- **Gravity Modification**: In `FixedUpdate()`, if the player's Y velocity is negative (falling), extra gravity is applied using `ForceMode.Acceleration` to create a snappy, arcade-style jump curve.
- **Collision & FX**: `OnCollisionEnter` strictly manages the grounded state and triggers particle systems (dirt when running, explosion when crashing).

### 3. Decoupled State Management (`BoolEventChannelSO`)
- **Game Over Broadcast**: When the player hits an obstacle, `PlayerController` raises a `true` event via `_gameOverChannel`. 
- **Autonomous Systems**: Instead of a God-class finding and stopping everything, objects manage their own response:
  - `ScreenMovememnt.cs` sets its speed to 0.
  - `SpawnManager.cs` stops instantiating new obstacles.
  - `Obstacle.cs` sets its `Rigidbody` velocity to zero.

### 4. Obstacle Physics (`Obstacle.cs`)
- **Framerate Independence**: Obstacles utilize `Rigidbody.AddForce` in `FixedUpdate()` rather than `transform.Translate`. This ensures their movement speed remains consistent across different framerates and physics steps.

## Lessons Learned
- **FixedUpdate vs Update**: Moving the force-based movement in `Obstacle.cs` from `Update` to `FixedUpdate` resolved jitter issues, proving that all Rigidbody physics operations must strictly reside in `FixedUpdate`.
- **Decoupled Architecture**: The `BoolEventChannelSO` is highly effective. It allows particle emitters, background scrollers, and spawners to halt instantly on a Game Over without the `PlayerController` needing any hard references to them.