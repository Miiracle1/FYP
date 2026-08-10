using UnityEngine;

/// <summary>
/// A static class to get set enums
/// </summary>
public static class GameProgressTracker
{
    public static SceneEnums Scene { get; set; } = SceneEnums.Lobby;

    public static GameModeEnums GameMode { get; set; } = GameModeEnums.Lobby;

    public static GameStateEnums GameState { get; set; } = GameStateEnums.Lobby;

    public static PlayerStateEnums PlayerState { get; set; } = PlayerStateEnums.Alive;

    public static Mod ModeState { get; set; } = Mod.New;
}

public enum Mod
{ 
    New,
    Done
}