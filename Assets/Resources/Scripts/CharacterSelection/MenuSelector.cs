using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour {

        public void ChangeScenes(string selScene)
    {
        PlayerPrefs.SetInt("CharacterSelected", 0);
        SceneManager.LoadScene(selScene);
    }
}
