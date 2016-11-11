using UnityEngine;

public class DragCube : MonoBehaviour
{
	private Vector3 _originPos;


	void Start()
	{
		_originPos = transform.position;
	}
		

	void OnMouseDrag()
	{
		Vector3 _fingerPosInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z)));
		transform.position = new Vector3(_originPos.x,_fingerPosInWorld.y,0);
	}

	void OnMouseUp()
	{
		transform.position = _originPos;
	}
}
