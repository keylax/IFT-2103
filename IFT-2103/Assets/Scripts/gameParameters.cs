using UnityEngine;

public static class gameParameters {

    private static int volumePercentage = 0;

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

    public static void setVolume(int volumePerc)
    {
        volumePercentage = volumePerc;
    }

    public static int getVolume()
    {
        return volumePercentage;
    }
}
