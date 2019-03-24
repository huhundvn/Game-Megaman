using UnityEngine;
using System.Collections;

public class WeabonController : MonoBehaviour {
    public float speed;
    public Vector3 direction;

    private Rigidbody2D myRigidbody2D;

    // Use this for initialization
    void Start () {
        myRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Type-1": Destroy(gameObject); break;
            case "Type-2": Destroy(gameObject); break;
            case "Type-4": Destroy(gameObject); break;
        }   
    }
}
