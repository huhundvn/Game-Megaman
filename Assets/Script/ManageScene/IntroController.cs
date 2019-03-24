using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroController : MonoBehaviour {
    public MovieTexture film;

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = film as MovieTexture;
        film.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!film.isPlaying)
        {
            Application.LoadLevel("MainMenu");
        }
	}

    public void skipIntro()
    {
        film.Stop();
        Application.LoadLevel("MainMenu");
    }
}
