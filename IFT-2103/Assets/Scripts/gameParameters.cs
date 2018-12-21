using UnityEngine;

public static class gameParameters {

    private static float volume = 1;

    public static void setQualityLevelToLow()
    {
        QualitySettings.SetQualityLevel(1, true);
    }

    public static void setQualityLevelToNormal()
    {
        QualitySettings.SetQualityLevel(2, true);
    }

    public static void setQualityLevelToHigh()
    {
        QualitySettings.SetQualityLevel(3, true);
    }

    public static void setVolume(float chosenVolume)
    {
        volume = chosenVolume;
    }

    public static float getVolume()
    {
        return volume;
    }
}
