using UnityEngine;
using UnityEngine.UI;
public class sfxManager : MonoBehaviour
{

    public void Start()
    {
        gameObject.GetComponent<Slider>().value = gameParameters.getSFXVolume();
    }

    public void setSFXVolume()
    {
        gameParameters.setSFXVolume(gameObject.GetComponent<Slider>().value);
    }
}

