using Assets.Scripts;
using Assets.Scripts.Controls;

public enum GameMode
{
    ONLINE_MP,
    OFFLINE_MP,
    VERSUS_AI
};

public static class GameParameters {
    private static GameMode chosenGameMode;
    //À ENVLEVER À LA FIN
    private static ControlScheme playerOneControls = new WASDControls();
    private static ControlScheme playerTwoControls = new ArrowsControls();

    public static void setGameMode(GameMode chosenMode)
    {
        chosenGameMode = chosenMode;
    }

    public static GameMode getGameMode()
    {
        return chosenGameMode;
    }

    public static void setPlayerOneControls(ControlScheme controlScheme)
    {
        playerOneControls = controlScheme;
    }

    public static ControlScheme getPlayerOneControls()
    {
        return playerOneControls;
    }

    public static void setPlayerTwoControls(ControlScheme controlScheme)
    {
        playerTwoControls = controlScheme;
    }

    public static ControlScheme getPlayerTwoControls()
    {
        return playerTwoControls;
    }
}
