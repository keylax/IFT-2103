using UnityEngine;

public static class gameParameters {

    private static float SFXvolume = 1;
    private static float musicVolume = 1;
    private static float fooleyVolume = 1;

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

    public static void setSFXVolume(float chosenVolume)
    {
        SFXvolume = chosenVolume;
    }

    public static float getSFXVolume()
    {
        return SFXvolume;
    }

    public static void setMusicVolume(float chosenVolume)
    {
        musicVolume = chosenVolume;
    }

    public static float getMusicVolume()
    {
        return musicVolume;
    }

    public static void setFooleysVolume(float chosenVolume)
    {
        fooleyVolume = chosenVolume;
    }

    public static float getFooleysVolume()
    {
        return fooleyVolume;
    }


}
