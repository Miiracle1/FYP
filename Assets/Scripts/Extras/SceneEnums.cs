using UnityEngine;

/// <summary>
/// Stores scene enums
/// </summary>
public enum SceneEnums
{
    Lobby,
    Greenhouse,
    Garage
}

/// <summary>
/// Stores gamemode enums
/// </summary>
public enum GameModeEnums
{ 
    Lobby,
    Normal,
    Hard
}

/// <summary>
/// Tracks game state
/// </summary>
public enum GameStateEnums
{ 
    Lobby,
    Loading,
    Playing,
    Paused,
    GameOver,
    Victory
}

/// <summary>
/// Tracks hard mode game level
/// </summary>
public enum GameLevelEnums
{ 
    Level1,
    Level2,
    Level3,
    Level4
}
