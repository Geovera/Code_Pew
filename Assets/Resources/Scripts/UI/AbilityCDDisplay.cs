using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCDDisplay : MonoBehaviour {

	/*Display ship ability on UI with CD included*/

	public Image darkMask;
	public Text cdTextDisplay;

	[SerializeField] private Ability ability;

	Image abilityImage;
	cooldownTimer displayCD;

	// Use this for initialization
	void Start () {
		initialize(ability);
	}

	/*Initiliaze bility icon linked to ship ability*/
	public void initialize(Ability selectedAbility)
	{
		ability = selectedAbility;
		abilityImage = GetComponent<Image> ();
		darkMask = gameObject.transform.GetChild (1).gameObject.GetComponent<Image> ();
		cdTextDisplay = gameObject.transform.GetChild (0).gameObject.GetComponent<Text> ();

		abilityImage.sprite = ability.aSprite;
		darkMask.sprite = ability.aSprite;

		displayCD = ability.coolDown;
		abilityReady ();

	}
		
	/* Update UI display according to CoolDown*/
	void Update () {
		if (displayCD.checkCD())
			abilityReady ();
		else
			coolDown ();
	}
	/* Make the mask invisible so that the ability is clear*/
	void abilityReady()
	{
		cdTextDisplay.enabled = false;
		darkMask.enabled = false;
	}

	/* Update the CD counter on the Ability Icon*/
	void coolDown()
	{
		
		float roundedCd = (float)(decimal.Round((decimal)displayCD.getNextReady(),1));
		cdTextDisplay.text = roundedCd.ToString();
		darkMask.fillAmount = (roundedCd / displayCD.TimeCD);
	}

	/* Start CoolDown timer once the ability is triggered */
	public void ButtonTriggered()
	{
		if (displayCD.checkCD()) {
			darkMask.enabled = true;
			cdTextDisplay.enabled = true;
		}
	}
}
