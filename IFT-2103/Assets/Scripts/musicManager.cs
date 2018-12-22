using UnityEngine;
using UnityEngine.UI;

public class musicManager : MonoBehaviour {

    public void Start()
    {
        gameObject.GetComponent<Slider>().value = gameParameters.getMusicVolume();
    }

    public void setMusicVolume()
    {
        gameParameters.setMusicVolume(gameObject.GetComponent<Slider>().value);
    }
}
