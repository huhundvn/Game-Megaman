using UnityEngine;
using System.Collections;

public class WeabonEnemyController : MonoBehaviour {
    public float speed;

    private Rigidbody2D myRigidbody2D;

    // Use this for initialization
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void destroy()
    {
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player": Destroy(gameObject); break;
        }
    }
}
