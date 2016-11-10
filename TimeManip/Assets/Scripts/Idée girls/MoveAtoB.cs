using UnityEngine;
using System.Collections;

public class MoveAtoB : MonoBehaviour {

		public bool dirRight = true;
		public float speedX = 2.0f;
		public float speedY = 2.0f;
		public float speedZ = 2.0f;
		public float maxX;
		public float minX;
		public float maxY;
		public float minY;
		public float maxZ;
		public float minZ;
		
		void Update ()
	{
		if (dirRight){
			transform.Translate (Vector3.right * speedX * Time.deltaTime);
			transform.Translate (Vector3.up * speedY * Time.deltaTime);
			transform.Translate (Vector3.forward * speedZ * Time.deltaTime);}
		else{
			transform.Translate (-Vector3.right * speedX * Time.deltaTime);
			transform.Translate (-Vector3.up * speedY * Time.deltaTime);
			transform.Translate (-Vector3.forward * speedZ * Time.deltaTime);}

		if (transform.position.x > maxX || transform.position.y > maxY || transform.position.z > maxZ) {
			dirRight = false;
		}

		if (transform.position.x < minX || transform.position.y < minY || transform.position.z < minZ) {
			dirRight = true;
		}
	}
}