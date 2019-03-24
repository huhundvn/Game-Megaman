using UnityEngine;
using System.Collections;

public class Type2Controller : MonoBehaviour {
    public GameObject weabonEnemy, gold, destroy;
    public AudioClip soundShot, soundDie;
    public Transform gunEnemy;
    public float hpEnemy;
    public PlayerController player;

    private Animator myAnimator;
    private Rigidbody2D myRigidbody2D;
    private AudioSource myAudio;

    private GameObject tmp;
    private Vector3 myScale;
    private float delay = 3;
    private float distance;

    // Use this for initialization
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>();
        gunEnemy = transform.Find("GunEnemy");
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        attack();
        if (hpEnemy <= 0)
        {
            die();
        }
    }

    private void attack()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 5)
        {
            delay -= Time.deltaTime;
            if ((int)delay == 0)
            {
                myAudio.PlayOneShot(soundShot);
                myAnimator.SetTrigger("shot");
                Instantiate(weabonEnemy, gunEnemy.position, Quaternion.identity);
                Instantiate(weabonEnemy, gunEnemy.position, Quaternion.EulerAngles(new Vector3(0, 0, 150)));
                Instantiate(weabonEnemy, gunEnemy.position, Quaternion.EulerAngles(new Vector3(0, 0, -150)));
                delay = 3;
            }
        }
    }

    private void die()
    {
        Destroy(gameObject);
        Instantiate(destroy, transform.position, Quaternion.identity);
        Instantiate(gold, transform.position, Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Weabon":
                Destroy(other.gameObject);
                hpEnemy--;
                break;
            case "Weabon2":
                Destroy(other.gameObject);
                hpEnemy -= 3;
                break;
        }
    }
}
