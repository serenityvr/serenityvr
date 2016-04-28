using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	//private ButterflyCycle butterfly;
	private GameUI _ui;
	// Use this for initialization
	void Start ()
	{
		//butterfly = GameObject.FindGameObjectWithTag ("Butterfly").transform.parent.GetComponent<ButterflyCycle> ();
		_ui = GameObject.Find ("UI").GetComponent<GameUI> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	private void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Butterfly")) {
			_ui.Reset();
		}
	}

	private void OnTriggerStay (Collider other)
	{
	}

	private void OnTriggerExit (Collider other)
	{
	}
}
