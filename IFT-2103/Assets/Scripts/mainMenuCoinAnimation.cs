using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mainMenuCoinAnimation : MonoBehaviour {

    public Image coinImage;
    private Vector3 currentPos;

	// Use this for initialization
	void Start () {
        currentPos = coinImage.transform.position;
        StartCoroutine(anim());
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator MoveToPosition()
    {
        yield return null;

        float time = 3f;
        float elapsedTime = 0f;

        int height = Screen.height;
        int width = Screen.width;

        float randomX = Random.Range(0, width);
        float randomY = Random.Range(0, height);

        Vector3 nextPos= new Vector3(randomX, randomY, 0f);

        while (elapsedTime < time)
        {
            coinImage.transform.position = Vector3.Lerp(currentPos, nextPos, Ease(elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        coinImage.transform.position = nextPos;
        currentPos = coinImage.transform.position;
    }

    IEnumerator anim()
    {
        while (true)
        {
            StartCoroutine(MoveToPosition());
            yield return null;
        }
    }

    private float Ease(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
