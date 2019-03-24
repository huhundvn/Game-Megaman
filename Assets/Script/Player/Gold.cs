using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("destroy", 3.0f);
	}

    private void destroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player": Destroy(gameObject); break;
            case "Type-2": Destroy(gameObject); break;
            case "Type-3": Destroy(gameObject); break;
            case "Type-4": Destroy(gameObject); break;
        }
    }
}
