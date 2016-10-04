using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    public float SpawnDelay;
    public GameObject SpawnPoint;

    private bool _canSpawn;

    // Use this for initialization
    void Start ()
    {
        _canSpawn = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
        if (!_canSpawn) return;

        var spawn = false;
        var spawnTowardsPosition = Vector3.zero;

#if UNITY_EDITOR || UNITY_WEBGL
        if (Input.GetMouseButton(0))
        {
            spawn = true;
            spawnTowardsPosition = Input.mousePosition;
        }
#else
		// MOBILE INPUT
		int __touchCount = Input.touchCount;
		if (__touchCount > 0 )
		{
			for ( int i = 0; i < __touchCount; i++ )
			{
				if (   Input.touches [ i ].phase == TouchPhase.Began
					|| Input.touches [ i ].phase == TouchPhase.Moved
					|| Input.touches [ i ].phase == TouchPhase.Stationary )
				{
					Vector3 touchPos = Input.touches [ i ].position;
						
                    spawn = true;
                    spawnTowardsPosition = touchPos;

                    break;
				}
			}
		}
#endif
        if (spawn)
        {
            ShootFruit(spawnTowardsPosition);
        }
    }

    private void ShootFruit(Vector3 towardsPosition)
    {
        Vector2 cursorInWorldPos = Camera.main.ScreenToWorldPoint(towardsPosition);
        var newFruit = FruitSpawner.Instance.SpawnFruit();
        var direction = (Vector3)cursorInWorldPos - SpawnPoint.transform.position;
        newFruit.transform.position = SpawnPoint.transform.position;

        //Make minimum force be at least 3 for game feel purposes
        if (direction.magnitude < 3)
        {
            var scalar = 3 / direction.magnitude;
            direction *= scalar;
        }

        newFruit.GetComponent<Rigidbody2D>().AddForce(direction * 5, ForceMode2D.Impulse);

        _canSpawn = false;
        Invoke("ReenableSpawn", SpawnDelay);
    }

    private void ReenableSpawn()
    {
        _canSpawn = true;
    }
}
