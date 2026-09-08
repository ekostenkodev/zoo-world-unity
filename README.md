# Zoo World

## Run

1. Open the project in Unity **6000.3.21f1**.
2. Open [ZooWorld.unity](Assets/Client/Scenes/ZooWorld.unity).
3. Press **Play**. Animals spawn every 1–2 seconds.

## Entry point

The **Startup** object in the scene has a `ZooWorldLifetimeScope` component.

- [ZooWorldLifetimeScope.cs](Assets/Client/Scripts/Bootstrap/ZooWorldLifetimeScope.cs) registers dependencies with VContainer and sets `ZooSimulation` as the entry point.
- [ZooSimulation.cs](Assets/Client/Scripts/Bootstrap/ZooSimulation.cs): `Start()` initializes the presenters and spawner, `Tick()` updates spawning, and `FixedTick()` updates movement.
- `AnimalSpawner` picks an animal type and position, spawns it through `AnimalLifecycle`, and attaches collision handling.

## Files

Under `Assets/Client/`:

- `Scripts/Bootstrap/` — dependency registration and startup.
- `Scripts/Zoo/` — spawning, animal selection, and world bounds.
- `Scripts/Animals/` — animal model, creation, pooling, population, and collisions.
- `Scripts/Animals/Movement/` — movement settings in `Data/`, algorithms in `Strategies/`.
- `Scripts/Animals/Feeding/` — feeding rules and predator/prey roles.
- `Scripts/Presentation/` — death counter display and eating feedback.
- `Scripts/Statistics/` — death counts.
- `Configuration/` — zoo and animal settings.
- `Prefabs/` — animals and eating feedback.
- `Materials/` — materials.

## Settings

- [DefaultZoo.asset](Assets/Client/Configuration/Zoos/DefaultZoo.asset) lists the animal types available for spawning. Assigned to **Zoo Data** on **Startup**.
- [Frog.asset](Assets/Client/Configuration/Animals/Frog.asset) and [Snake.asset](Assets/Client/Configuration/Animals/Snake.asset) define each animal's prefab, food chain role, and movement settings. Select role and movement types in the Inspector.
- To add an animal type, use **Create / Zoo World / Animal**, fill in its settings, and add the asset to `DefaultZoo`.
- Spawn timing is in `AnimalSpawner.cs`. Movement at world boundaries is in `AnimalMovement.cs`.

`Data` classes hold settings. `View` classes hold component references. Services and presenters handle behavior. R3 handles subscriptions, including the `Fed` stream used by the statistics and eating feedback presenters.
