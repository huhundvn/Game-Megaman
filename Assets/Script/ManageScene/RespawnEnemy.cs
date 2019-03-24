using UnityEngine;
using System.Collections;

public class RespawnEnemy : MonoBehaviour {
    public GameObject enemy;
    private GameObject tmp;
    private bool enabled;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (tmp == null && !enabled)
        {
            tmp = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
        }
	}

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }
}
