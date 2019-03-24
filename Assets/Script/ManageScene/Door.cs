using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    private Animator myAnimator;
    public AudioClip soundOpen;
    private AudioSource myAudio;

    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void loadWin()
    {
        Application.LoadLevel("Win");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            myAnimator.SetBool("open", true);
            myAudio.PlayOneShot(soundOpen);
            Invoke("loadWin", 2);
        }
    }
}
