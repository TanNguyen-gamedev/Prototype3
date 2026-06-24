# Prototype 3: Architecture & Lessons Learned

## Lessons Learned
- **Environment Manipulation**: Instead of moving the player through a massive world, the player stays mostly stationary while the world (`RepeatBackground`, `Obstacle`) moves towards them.
- **Decoupled State**: Game state is managed cleanly using a boolean ScriptableObject Event Channel. This allows individual components (like background scrollers and particle emitters) to halt autonomously upon receiving a "Game Over" broadcast without needing a centralized God-class.
- **Mathematical Looping**: Learned to use bounds and math to seamlessly repeat backgrounds rather than constantly instantiating and destroying background prefabs.
- **Animation & Physics**: Synchronizing visual animations (jumping, falling) with physical Rigidbody states. Emphasized using strict `FixedUpdate` for adding jump forces and updating zero-allocation states.