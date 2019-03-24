using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Loading : MonoBehaviour {
    public Text loading; 
    private float percent = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        percent += 10.0f;
        loading.text = "Loading: " + percent + " %";
        if ((int)percent == 100)
        {
            Application.LoadLevel("Level01");
        }
    }
}
