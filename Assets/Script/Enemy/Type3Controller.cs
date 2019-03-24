using UnityEngine;
using System.Collections;

public class Type3Controller : MonoBehaviour {
    public GameObject weabonEnemy, gold, destroy;
    public AudioClip soundShot, soundProtect, soundDie;
    public Transform gunEnemy;
    public float hpEnemy;
    public PlayerController player;

    private Animator myAnimator;
    private Rigidbody2D myRigidbody2D;
    private AudioSource myAudio;

    private GameObject tmp;
    private Vector3 myScale;
    private float delay = 4;
    private float distance;
    private bool isProtect, isRight;

    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        myAudio = GetComponent<AudioSource>();
        gunEnemy = transform.Find("GunEnemy");
        isProtect = true;
        isRight = true;
    }
	
	// Update is called once per frame
	void Update () {
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
            if ((int) delay <= 0)
            {
                isProtect = !isProtect;
                myAudio.PlayOneShot(soundShot);
                myAnimator.SetBool("shot", !isProtect);
                Instantiate(weabonEnemy, gunEnemy.position, Quaternion.identity);
                Instantiate(weabonEnemy, gunEnemy.position, Quaternion.EulerAngles(new Vector3(0, 0, 150)));
                Instantiate(weabonEnemy, gunEnemy.position, Quaternion.EulerAngles(new Vector3(0, 0, -150)));
                delay = 4;
            }
        }
    }

    private void die()
    {
        Destroy(gameObject);
        Instantiate(destroy, transform.position, Quaternion.identity);
        Instantiate(gold, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Weabon":
                if (isProtect)
                {
                    myAudio.PlayOneShot(soundProtect);
                    myScale = other.transform.localEulerAngles;
                    myScale.z += 135;
                    other.transform.localEulerAngles = myScale;
                } else
                {
                    Destroy(other.gameObject);
                    hpEnemy--;
                    myAudio.PlayOneShot(soundDie);
                }
                break;
            case "Weabon2":
                if (isProtect)
                {
                    myAudio.PlayOneShot(soundProtect);
                    myScale = other.transform.localEulerAngles;
                    myScale.z += 135;
                    other.transform.localEulerAngles = myScale;
                }
                else
                {
                    Destroy(other.gameObject);
                    hpEnemy -= 3;
                    myAudio.PlayOneShot(soundDie);
                }
                break;
        }
    }
}
