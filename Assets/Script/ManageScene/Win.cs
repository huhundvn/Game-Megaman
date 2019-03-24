using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Win : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("back", 3.0f);
    }

    private void back()
    {
        Application.LoadLevel("MainMenu");
    }
}
