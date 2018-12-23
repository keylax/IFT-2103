using UnityEngine;
using System.Collections;

public class crossOverMusic : MonoBehaviour {

    public AudioClip finalAudioClip;

    // Use this for initialization
    void Start() {
        In();
    }

    // Update is called once per frame
    void Update() {

    }

    public void In()
    {
        StartCoroutine(FadeIn(1.5f));
    }

    public void Out()
    {
        StartCoroutine(FadeOut(1.5f));
    }

    public IEnumerator FadeOut(float FadeTime)
    {
        float startVolume = gameObject.GetComponent<AudioSource>().volume;

        while (gameParameters.getMusicVolume() > 0)
        {
            float currentVolume = gameParameters.getMusicVolume();
            currentVolume -= startVolume * Time.deltaTime / FadeTime;

            gameParameters.setMusicVolume(currentVolume);

            yield return null;
        }

        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().volume = startVolume;
    }

    public IEnumerator FadeIn(float FadeTime)
    {
        gameObject.GetComponent<AudioSource>().Play();
        gameParameters.setMusicVolume(0f);

        while (gameParameters.getMusicVolume() < 1)
        {
            float currentVolume = gameParameters.getMusicVolume();
            currentVolume += Time.deltaTime / FadeTime;

            gameParameters.setMusicVolume(currentVolume);

            yield return null;
        }
    }

    public IEnumerator FinalSong()
    {
        float startVolume = gameObject.GetComponent<AudioSource>().volume;

        while (gameParameters.getMusicVolume() > 0)
        {
            float currentVolume = gameParameters.getMusicVolume();
            currentVolume -= startVolume * Time.deltaTime / 1.5f;

            gameParameters.setMusicVolume(currentVolume);

            yield return null;
        }

        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().volume = startVolume;

        gameObject.GetComponent<AudioSource>().clip = finalAudioClip;
        gameObject.GetComponent<AudioSource>().loop = true;

        gameObject.GetComponent<AudioSource>().Play();
        gameParameters.setMusicVolume(0f);

        while (gameParameters.getMusicVolume() < 1)
        {
            float currentVolume = gameParameters.getMusicVolume();
            currentVolume += Time.deltaTime / 1.5f;

            gameParameters.setMusicVolume(currentVolume);

            yield return null;
        }
    }


    public void playFinalSong()
    {
        StartCoroutine(FinalSong());
    }
}
 
