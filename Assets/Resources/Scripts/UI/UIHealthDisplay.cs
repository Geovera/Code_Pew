using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthDisplay : MonoBehaviour {

	Image greenBar;
	Text healthPercent;

	float percent;

	HealthSystem shipHs;
	// Use this for initialization
	public void init(HealthSystem hs)
	{
		greenBar = gameObject.transform.GetChild (1).GetComponent<Image>();
		healthPercent = gameObject.transform.GetChild (2).GetComponent<Text> ();

		shipHs = hs;
	}
	
	// Update is called once per frame
	void Update () {
		if (shipHs != null) {
			percent = shipHs.Health / shipHs.MaxHealth;
			healthPercent.text = (percent * 100).ToString () + "%";
			greenBar.fillAmount = (percent);

		} else if (greenBar != null){
			healthPercent.text = "0%";
			greenBar.fillAmount = 0f;
		}
		
	}
}
