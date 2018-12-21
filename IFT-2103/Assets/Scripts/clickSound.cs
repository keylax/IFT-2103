using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class clickSound : MonoBehaviour {

    public AudioClip onClickSound;
    public AudioClip hoverSound;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }

    // Use this for initialization
    void Start () {
        gameObject.AddComponent<AudioSource>();
        audioSource.clip = onClickSound;
        audioSource.playOnAwake = false;
	}

    public void PlaySoundOnClick()
    {
        audioSource.PlayOneShot(onClickSound);
    }


    public void PlaySoundHover()
    {
        audioSource.PlayOneShot(hoverSound);
    }
}
