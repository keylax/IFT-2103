using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mainMenuCoinAnimation : MonoBehaviour {

    public Image coinImage;
    private Vector3 currentPos;
    private bool isRunning = false;

	// Use this for initialization
	void Start () {
        currentPos = coinImage.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(!isRunning)
        {
           StartCoroutine(MoveToPosition());
        }
    }

    IEnumerator MoveToPosition()
    {
        isRunning = true;

        float time = Random.Range(2f, 4f) ;
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

        isRunning = false;
        yield return new WaitForSeconds(time);
    }

    private float Ease(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
