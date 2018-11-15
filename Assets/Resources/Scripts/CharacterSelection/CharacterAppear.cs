using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppear : MonoBehaviour {
    private int index;
    private int prevIndex;


	/* Make the prev chosen ship appear */
	void Start () {
        index = PlayerPrefs.GetInt("ship");
		transform.GetChild (index).gameObject.SetActive (true);
    }
	
	/* Change the current ship on screen to the chosen one */
	void LateUpdate () {
        transform.GetChild(index).gameObject.SetActive(true);
        prevIndex = index;
        index = PlayerPrefs.GetInt("ship");
        if (prevIndex != index)
        {
            transform.GetChild(prevIndex).gameObject.SetActive(false);
            transform.GetChild(index).gameObject.SetActive(true);

        }
                

}
}
