using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public GameObject cp0, cp1, cp2, cp3;
    private bool cp0_on, cp1_on, cp2_on, cp3_on;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
        {
            cp0.SetActive(false);
        }
	}
}
