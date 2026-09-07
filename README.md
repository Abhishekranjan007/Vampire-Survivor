# Wave Survival System

A scalable Vampire Survivors-style survival system built in Unity 6 as a technical assignment.

The project focuses on gameplay architecture, data-driven design, object pooling, event-driven communication, wave progression, upgrade systems, and mobile performance considerations.

## Overview

The player survives progressively harder enemy waves while automatically attacking nearby enemies, collecting XP, leveling up, and selecting upgrades.

The project was developed with Android/mobile as the target platform, while also supporting keyboard input in the Unity Editor for development and testing.

## Core Features

- Character movement
- Keyboard and virtual joystick input
- Automatic player attacks
- Multiple weapon types
- Multiple enemy types
- Enemy spawning and wave progression
- Enemy object pooling
- Projectile object pooling
- XP pickup system
- XP-based leveling
- Upgrade selection system
- Player health and death handling
- Pause system
- Wave timer
- Data-driven enemy configuration
- Data-driven weapon configuration
- Data-driven upgrade configuration
- Mobile-oriented performance considerations

## Architecture

The project separates gameplay responsibilities into focused systems rather than placing all gameplay logic inside a single manager.

Examples include:

- `PlayerMovement` — handles player movement and input
- `PlayerHealth` — manages health, damage and death
- `EnemySpawner` — controls enemy spawning
- `EnemyPool` — manages reusable enemy instances
- `WeaponController` — manages player weapons
- Projectile pools — reuse projectile objects
- `XPSystem` — handles XP collection and level progression
- `UpgradeManager` — presents and applies level-up upgrades
- `WaveManager` — controls wave progression and wave state
- `PauseManager` — controls pause/resume behaviour

## Data-Driven Design

ScriptableObjects are used to separate gameplay configuration from runtime logic.

The project uses data assets for systems such as:

- Enemy configuration
- Weapon configuration
- Upgrade configuration
- Wave configuration

This makes gameplay values easier to tune without modifying the underlying gameplay code.

For example, enemy configuration can define properties such as:

- Health
- Movement speed
- Damage
- XP reward
- Enemy type

This approach makes it easier to add and balance new content.

## Object Pooling

Object pooling is used for frequently spawned and destroyed gameplay objects.

The main pooled objects include:

- Enemies
- Projectiles
- XP pickups

Instead of repeatedly instantiating and destroying these objects during gameplay, existing objects are reused.

This reduces runtime allocations and helps avoid unnecessary garbage collection during high-activity gameplay situations.

### Enemy Lifecycle

The basic enemy lifecycle is:

1. `EnemySpawner` requests an enemy from the pool.
2. The enemy is initialized with its configured data.
3. The enemy participates in gameplay.
4. When defeated, it rewards XP.
5. The enemy is returned to the pool.
6. The pooled instance can later be reused.

## Event-Driven Communication

C# events are used where systems need to react to gameplay changes without creating unnecessary direct dependencies.

Examples include:

- `PlayerHealth.OnHealthChanged`
- `PlayerHealth.OnDeath`
- `XPSystem.OnLevelUp`

For example, when the player levels up, the XP system raises a level-up event and the upgrade system reacts to it.

This keeps systems more independent and makes the flow of gameplay events easier to follow.

## Wave System

Wave progression is controlled by `WaveManager`.

Each wave is configured through `WaveData`, allowing properties such as duration and enemy spawning configuration to be changed without modifying the wave-management code.

The wave manager controls the current wave, timer and progression between waves.

The system is designed so additional waves and configurations can be added through data rather than hardcoding each wave individually.

## Upgrade System

When the player reaches the required XP threshold:

1. The XP system detects the level-up.
2. A level-up event is raised.
3. The upgrade manager pauses gameplay.
4. Upgrade choices are presented to the player.
5. The player selects an upgrade.
6. The selected upgrade is applied to the weapon system.
7. Gameplay resumes.

Current upgrade categories include:

- Damage
- Attack Speed
- Projectile Speed

## Enemy Management

Enemies are spawned separately from their runtime behaviour.

`EnemySpawner` is responsible for deciding when and where enemies should appear, while enemy instances are responsible for their own gameplay behaviour.

An active enemy limit is also used to provide a predictable upper bound on the number of active enemies.

This helps keep runtime load more manageable on mobile hardware.

## Targeting

Active enemies are tracked so that weapon systems can efficiently find valid targets.

For larger-scale versions of the system, the targeting architecture could be extended with spatial partitioning or grid-based lookup to reduce the cost of searching through large numbers of enemies.

## Input

The project uses Unity's Input System.

### Editor / PC Testing

- `W` / `A` / `S` / `D` — movement
- `ESC` — pause

### Mobile

A virtual joystick is used for player movement.

The gameplay systems are shared between the editor and mobile input paths.

## Performance Considerations

The project was designed with mobile performance in mind.

Important considerations include:

- Object pooling for frequently spawned objects
- Limiting the number of active enemies
- Reusing projectiles and XP pickups
- Avoiding unnecessary Instantiate/Destroy operations during gameplay
- Event-driven communication between systems
- Data-driven configuration through ScriptableObjects
- Keeping high-frequency gameplay paths free from unnecessary managed allocations

The main areas requiring additional optimization when scaling to significantly larger enemy counts would be targeting and spatial queries.

## Technology

- Unity 6
- Universal Render Pipeline (URP)
- C#
- Unity Input System
- ScriptableObjects
- Object Pooling
- C# Events
- Android
- Mobile Optimization

## Controls

| Action | Editor / PC | Mobile |
|---|---|---|
| Move | WASD | Virtual Joystick |
| Pause | ESC | Pause Button |
| Attack | Automatic | Automatic |

## Project Structure

The project is organized around gameplay responsibilities and reusable systems.

Major areas include:

- Player
- Enemies
- Weapons
- Projectiles
- XP
- Upgrades
- Waves
- UI
- Pooling
- Game Management
- Data / ScriptableObjects

## Development Notes

This project was created as a short technical assignment with a focus on demonstrating scalable Unity gameplay architecture rather than production-level content volume.

The implementation prioritizes:

- Clear system responsibilities
- Reusable gameplay components
- Data-driven configuration
- Runtime performance
- Easy extension of gameplay content

## Possible Future Improvements

If this system were expanded into a larger production project, possible improvements would include:

- More advanced enemy targeting using spatial partitioning
- Additional weapon behaviours
- More enemy behaviours
- More upgrade types
- More sophisticated wave configurations
- Additional pooling optimizations
- Burst/Jobs-based processing for extremely large enemy counts
- Further mobile profiling and optimization
- More advanced progression and meta-game systems