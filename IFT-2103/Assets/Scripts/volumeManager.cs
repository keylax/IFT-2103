using UnityEngine;
using UnityEngine.UI;

public class volumeManager : MonoBehaviour {

    public Slider volumeSlider;

    public void setMusicVolume()
    {
        gameParameters.setMusicVolume(volumeSlider.value);
    }

    public void setSFXVolume()
    {
        gameParameters.setSFXVolume(volumeSlider.value);
    }

}
