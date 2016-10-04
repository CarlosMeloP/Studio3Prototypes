using UnityEngine;

public class FruitSpawner : MonoBehaviour {

    public static FruitSpawner Instance;
    public GameObject FruitPrefab;


    void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnFruit()
    {
        var newFruit = Instantiate(FruitPrefab);


        return newFruit;
    }
}
