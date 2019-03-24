using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void retryGame()
    {
        Application.LoadLevel("Level01");
    }

    public void quit()
    {
        Application.LoadLevel("MainMenu");
    }
}
