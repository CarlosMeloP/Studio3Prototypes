using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Platform manager script creats the pool for the platforms that will be ised in the game and manage them
/// </summary>
public class PlatformManager : MonoBehaviour {
	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 scale_;
	public float gap;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	void Start () {
		objectQueue = new Queue<Transform>(numberOfObjects);
		for(int i = 0; i < numberOfObjects; i++){
			objectQueue.Enqueue(Instantiate(prefab));
		}
		nextPosition = new Vector3(startPosition.x - scale_.x/2,startPosition.y - scale_.y/2,startPosition.z);
		for(int i = 0; i < numberOfObjects; i++){
			Recycle();
		}
	}

	void Update () {
		if(objectQueue.Peek().localPosition.y + recycleOffset < Player.distanceTraveled){
			Recycle();
		}
	}
	private void Recycle () {
		Vector3 scale = new Vector3(scale_.x,scale_.y,scale_.y);

		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		position.z += startPosition.z;

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		objectQueue.Enqueue(o);
		o.GetComponent<CreateWalls> ().positionEnemies ();
		nextPosition += new Vector3 (0, gap, 0);
	}


}