using UnityEngine;
using System.Collections;

public class ButterflyCycle : MonoBehaviour
{
	private GameObject[] trees;
	private float speed = 5;
	public bool _cycleEnable;
	private int treeCycleCount = 0;

	private bool levelHard;
	private Vector3 playerPosition;

	public bool CycleEnable { get { return _cycleEnable; } set { _cycleEnable = value; } }

	public float Speed { get { return speed; } set { speed = value; } }

	public bool LevelHard { get { return levelHard; } set { levelHard = value; } }

	public Vector3 PlayerPosition { get { return playerPosition; } set { playerPosition = value; } }


	// Use this for initialization

	void Start ()
	{
		trees = GameObject.FindGameObjectsWithTag ("Tree");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_cycleEnable) {
			TreeCycle ();
		}
	}

	private void TreeCycle ()
	{
		if (treeCycleCount < trees.Length) {
			float maxdistanceX, maxdistanceZ, playerDistanceX, playerDistanceZ;
			Vector3 newPos = new Vector3 (trees [treeCycleCount].transform.position.x, Random.Range (3, 10), trees [treeCycleCount].transform.position.z);
			transform.position = Vector3.MoveTowards (transform.position, newPos, speed * Time.deltaTime);
			transform.rotation = Quaternion.LookRotation (newPos, new Vector3 (0, 1, 0));

			maxdistanceX = Mathf.Abs ((Mathf.Abs (transform.position.x) - Mathf.Abs (trees [treeCycleCount].transform.position.x)));
			maxdistanceZ = Mathf.Abs ((Mathf.Abs (transform.position.z) - Mathf.Abs (trees [treeCycleCount].transform.position.z)));

			//playerDistanceX = Mathf.Abs ((Mathf.Abs (transform.position.x) - Mathf.Abs (playerPosition.x)));
			//playerDistanceZ = Mathf.Abs ((Mathf.Abs (transform.position.z) - Mathf.Abs (playerPosition.z)));

			if (maxdistanceX < 1 || maxdistanceZ < 1) {
				treeCycleCount += 1;
			}
//			if (levelHard && (playerDistanceX < 1 || playerDistanceZ < 1)) {
//				treeCycleCount += 1;
//			}

		}
		if (treeCycleCount == trees.Length) {
			treeCycleCount = 0;
		}
	}
}
