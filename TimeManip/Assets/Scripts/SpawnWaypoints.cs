using UnityEngine;
using System.Collections;

public class SpawnWaypoints : MonoBehaviour {

	public GameObject _waypoint;
	[Header ("Nombre de Waypoints")]
	public int minNbwayp;
	public int maxNbwayp;

	[HideInInspector]
	public int nbWayP;

	[Header ("Coordinate for spawning zone of waypoints")]
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	public float minZ;
	public float maxZ;

	// Use this for initialization
	void Start () {

		if (!GetComponent<CubeManager> ().startCube) {

			nbWayP = Random.Range (minNbwayp, maxNbwayp);

			for( int i = 0; i < nbWayP ; i++)
			{
				GameObject newWP = Instantiate (_waypoint);
				newWP.transform.position = new Vector3 (Random.Range (minX,maxX), Random.Range (minY,maxY), Random.Range (minZ,maxZ));
				GetComponent<MoveToWaypoints> ().targets.Add (newWP.transform);
			}

			GetComponent<MoveToWaypoints> ().markcount = 0;
		}


	
	}

}
