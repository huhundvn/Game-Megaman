using UnityEngine;
using System.Collections;

public class SmokeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("die", 0.2f);
	}

    private void die()
    {
        Destroy(gameObject);
    }
}
