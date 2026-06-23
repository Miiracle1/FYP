using UnityEngine;

/// <summary>
/// A static class to get set enums
/// </summary>
public static class GameProgressTracker
{
    public static SceneEnums Scene { get; set; } = SceneEnums.Lobby;

    public static GameModeEnums GameMode { get; set; } = GameModeEnums.Lobby;

    public static GameStateEnums GameState { get; set; } = GameStateEnums.Lobby;
}