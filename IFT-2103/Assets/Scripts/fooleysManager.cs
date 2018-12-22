using UnityEngine;
using UnityEngine.UI;
public class fooleysManager : MonoBehaviour
{
    public void Start()
    {
        gameObject.GetComponent<Slider>().value = gameParameters.getFooleysVolume();
    }

    public void setSFXVolume()
    {
        gameParameters.setFooleysVolume(gameObject.GetComponent<Slider>().value);
    }
}
