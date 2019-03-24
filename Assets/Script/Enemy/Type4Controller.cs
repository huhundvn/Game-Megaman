using UnityEngine;
using System.Collections;

public class Type4Controller : MonoBehaviour {
    public float speed, hpEnemy;
    public GameObject gold, destroy;
    public AudioClip soundDie;
    public PlayerController player;

    private AudioSource myAudio;
    private float distance;

	// Use this for initialization
	void Start () {
        myAudio = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerController>();   
	}
	
	// Update is called once per frame
	void Update () {
        move();
        if (hpEnemy <= 0)
        {
            die();
        }    
    }

    private void move()
    {
        distance = transform.position.x - player.transform.position.x;

        if (distance > 0.5f && distance < 3)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (distance < -0.5f && distance > -3)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void die()
    {
        Destroy(gameObject);
        Instantiate(destroy, transform.position, Quaternion.identity);
        Instantiate(gold, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Weabon":
                hpEnemy--;
                myAudio.PlayOneShot(soundDie);
                break;
            case "Weabon2":
                hpEnemy -= 3;
                myAudio.PlayOneShot(soundDie);
                break;
        }
    }
}
