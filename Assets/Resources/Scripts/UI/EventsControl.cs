using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EventsControl : MonoBehaviour{

	/*Single destroy event on UI*/
	public class DisEvent  : MonoBehaviour{

		cooldownTimer dispTime;
		public Vector3 offset;
		Text attacker;
		Text link;
		Text reciever;
		/*Init destroy display*/
		public void init(string a, string l, string r, Vector3 iO)
		{
			offset = iO;

			attacker = gameObject.transform.GetChild (0).GetComponent<Text> ();
			link = gameObject.transform.GetChild (1).GetComponent<Text> ();
			reciever = gameObject.transform.GetChild (2).GetComponent<Text> ();

			dispTime = new cooldownTimer (10f);

			attacker.text = a;
			link.text = l;
			reciever.text = r;
		}

		public bool checkTimeLeft()
		{
			return dispTime.checkCD ();

		}

	}

	/* Control system for displaying events on UI*/

	LinkedList<DisEvent> nodes;
	public Vector3 preOffset;

	GameObject nodePrefab;


	/*Load Events prefabs
	 * Init Linked List to all Event nodes*/
	void Start()
	{
		nodePrefab = Resources.Load ("Prefabs/UI/DispNode") as GameObject;
		nodes = new LinkedList<DisEvent> ();
	}

	/*Check for lifetime of nodes and update accordingly*/
	void LateUpdate()
	{
		LinkedList<DisEvent> temp = new LinkedList<DisEvent>(nodes);
		int index = 0;
		foreach (DisEvent n in temp) {
			if (n.checkTimeLeft ()) {
				nodes.Remove (n);
				GameObject.Destroy (n.gameObject);
			}
			else
			{
				n.gameObject.transform.position = index * preOffset + transform.position;
			}
			index++;
		}
	}

	/*Add any event for display on UI*/
	public void addEvent(string l, string m, string r)
	{
		GameObject temp = GameObject.Instantiate (nodePrefab, transform);
		DisEvent e = temp.AddComponent<DisEvent> ();
		Vector3 initOffset = preOffset * nodes.Count + transform.position;
		e.init (l,m,r, initOffset);
		temp.transform.position = transform.position + e.offset;
		nodes.AddLast (e);
	}
}
