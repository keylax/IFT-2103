using UnityEngine;

public class gameManagerBehaviour : MonoBehaviour {
    private GameObject[] targets;
    private targetBehaviour[] targetsBehaviours = new targetBehaviour[3];
    private GameObject player;
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 144;
        player = GameObject.FindGameObjectWithTag("Player");
        targets = GameObject.FindGameObjectsWithTag("Target");
        for (int i = 0; i < targets.Length; i++)
        {
            targetsBehaviours[i] = targets[i].GetComponent<targetBehaviour>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        bool allTargetsHit = true;
		for (int i = 0; i < targetsBehaviours.Length; i++)
        {
            if (!targetsBehaviours[i].wasTargetHit())
                allTargetsHit = false;
        }

        if (allTargetsHit)
        {
            for (int i = 0; i < targetsBehaviours.Length; i++)
            {
                targetsBehaviours[i].reset();
            }
            player.GetComponent<moveTank>().reset();
        }
    }
}
