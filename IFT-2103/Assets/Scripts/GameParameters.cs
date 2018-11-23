using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    ONLINE_MP,
    OFFLINE_MP,
    VERSUS_AI
};

public static class GameParameters {
    private static GameMode chosenGameMode;

	public static void setGameMode(GameMode chosenMode)
    {
        chosenGameMode = chosenMode;
    }

    public static GameMode getGameMode()
    {
        return chosenGameMode;
    }
}
