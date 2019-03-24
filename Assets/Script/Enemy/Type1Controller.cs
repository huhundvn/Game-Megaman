using UnityEngine;
using System.Collections;

public class Type1Controller : MonoBehaviour {
    public float hpEnemy;
    public float speed;

    public GameObject destroy;
    public AudioClip soundDie;

    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private AudioSource myAudio;

    private bool die;
    private Vector3 myScale;

    // Use this for initialization
    void Start () {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (hpEnemy <= 0)
        {
            die = true;
            myAnimator.SetBool("die", die);
            Invoke("destroyEnemy", 10);
        }
	}

    private void destroyEnemy()
    {
        Destroy(gameObject);
        Instantiate(destroy, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Weabon":
                if (!die)
                {
                    hpEnemy--;
                    myAudio.PlayOneShot(soundDie);
                }
                break;
            case "Weabon2":
                if (!die)
                {
                    hpEnemy -= 3;
                    myAudio.PlayOneShot(soundDie);
                }
                break;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Player":
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;
        }
    }
}
