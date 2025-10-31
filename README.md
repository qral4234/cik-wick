# Cik-Wick

A 3D adventure game developed in Unity where you play as a character navigating a kitchen environment, collecting eggs while avoiding a chasing cat and various hazards.

## 🎮 Game Overview

In Cik-Wick, players must collect eggs while navigating through a challenging kitchen environment. The game features:

- **Dynamic Chase Mechanics**: A cat that actively pursues the player when they're on the ground
- **Collectible System**: Different types of wheat with unique power-ups
- **Health System**: Players must avoid fire hazards and manage their health
- **Movement Mechanics**: Running, jumping, and sliding capabilities

## 🎯 Game Objective

Collect the required number of eggs (default: 5) while:
- Avoiding the chasing cat
- Dodging fire hazards from stoves
- Managing your health (3 hearts)
- Utilizing power-ups strategically

## 🕹️ Controls

### Keyboard Controls
- **Arrow Keys / WASD**: Move forward/backward and turn left/right
- **Space**: Jump
- **Slide Key** (configurable): Slide
- **Interact Key**: Interact with objects

### Movement Features
- **Walk/Run**: Standard movement with adjustable speed
- **Jump**: Jump to avoid hazards or reach higher areas
- **Slide**: Quick dash movement with reduced drag

## ⭐ Collectibles & Power-ups

### Wheat Types
1. **Holy Wheat** (White) 🌾
   - Increases jump force temporarily
   - Plays positive sound effect

2. **Gold Wheat** (Yellow) 🌟
   - Increases movement speed temporarily
   - Plays positive sound effect

3. **Rotten Wheat** (Brown) ⚠️
   - Decreases movement speed (negative effect)
   - Avoid when possible

All wheat power-ups have a duration before returning to normal stats.

## 🎲 Game Mechanics

### Health System
- Start with 3 health points
- Take damage from fire hazards (stoves)
- Game over when health reaches 0

### Cat AI
- The cat patrols the area when the player is not detected
- Chases the player when they touch the ground
- Uses NavMesh AI for pathfinding
- Faster chase speed compared to patrol speed

### Win/Lose Conditions
- **Win**: Collect all required eggs
- **Lose**: Either get caught by the cat or health reaches zero

## 🛠️ Technical Requirements

### Unity Version
- Unity 6000.0.53f1 or compatible version

### Dependencies
- DOTween (animation library)
- Unity Input System
- NavMesh Components
- TextMesh Pro

## 📦 Project Structure

```
Assets/
├── _GameAssets/
│   ├── Scripts/          # All game scripts
│   │   ├── Player/       # Player controller, movement, animations
│   │   ├── Cat/          # Cat AI and behavior
│   │   ├── Managers/     # Game and Health managers
│   │   ├── Collectibles/ # Wheat and egg collectibles
│   │   ├── Audio/        # Sound management
│   │   ├── UI/           # User interface
│   │   └── Camera/       # Camera controller
│   ├── Scenes/           # Game scenes
│   ├── Prefabs/          # Reusable game objects
│   ├── Models/           # 3D models
│   ├── Animations/       # Animation files
│   ├── Sounds/           # Audio files
│   └── Materials/        # Materials and shaders
├── Settings/             # Project settings
└── Plugins/              # Third-party plugins
```

## 🚀 Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/qral4234/cik-wick.git
   cd cik-wick
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add" and select the project folder
   - Ensure you have Unity 6000.0.53f1 installed
   - Open the project

3. **Import Dependencies**
   - DOTween should be imported via Package Manager
   - Other dependencies should auto-import

4. **Open Main Scene**
   - Navigate to `Assets/_GameAssets/Scenes/`
   - Open `SampleScene.unity` for the main game
   - Open `MenuScene.unity` for the menu

5. **Play**
   - Press the Play button in Unity Editor
   - Or build the game for your target platform

## 🎵 Audio

The game includes:
- Background music
- Pickup sound effects (good and bad)
- Environmental sounds
- UI interaction sounds

Audio is managed through the AudioManager singleton system.

## 🔧 Development

### Key Systems

1. **Game Manager**: Singleton pattern managing game states (Play, Pause, Win, Lose)
2. **Health Manager**: Singleton pattern tracking player health
3. **Audio Manager**: Centralized sound effect and music management
4. **State Controllers**: Managing player and cat states

### Code Architecture
- Interface-based design for collectibles (`ICollectible`)
- ScriptableObjects for wheat configurations
- Event-driven architecture for game state changes
- Singleton pattern for managers

## 📝 Game States

- **Play**: Active gameplay
- **Pause**: Game paused
- **Resume**: Resuming from pause
- **Win**: Player collected all eggs
- **Lose**: Player caught or health depleted

## 🎨 Assets

The game uses custom 3D models for:
- Kitchen environment (tables, decorations, mugs, containers)
- Player character
- Cat character
- Collectibles (wheats, eggs)
- Hazards (stoves with fire)

## 🤝 Contributing

If you'd like to contribute to this project:
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## 📄 License

This project is part of a learning/development exercise. Please check with the repository owner for licensing details.

## 🐛 Known Issues

- Player movement is delayed for 5 seconds at game start (intentional design)
- Cat chase only triggers when player is on ground (tag: "Floor")

## 💡 Tips for Players

1. Stay airborne to avoid the cat temporarily
2. Collect Holy Wheat before difficult jumps
3. Collect Gold Wheat when you need to outrun the cat
4. Avoid Rotten Wheat as it slows you down
5. Watch your health bar when near stoves
6. Plan your route to collect eggs efficiently

---

**Repository**: [qral4234/cik-wick](https://github.com/qral4234/cik-wick)

**Project Name**: Cik-Wick (Wick Game)
