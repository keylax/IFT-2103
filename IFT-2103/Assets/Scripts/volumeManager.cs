using UnityEngine;
using UnityEngine.UI;

public class volumeManager : MonoBehaviour {

    public Slider volumeSlider;

    public void setVolume()
    {
        gameParameters.setVolume((int)(volumeSlider.value * 100));
    }

}
