using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour {

    public GameObject player;
    public Vector3 spawnPosition = new Vector3(0, 1, -7);
    public Character[] characters;

    public GameObject shipSelectPanel;

    /* Change current chosen ship */
    public void OnCharacterSelect(int characterChoice)
    {
 		PlayerPrefs.SetInt("ship", characterChoice);
    }
}
