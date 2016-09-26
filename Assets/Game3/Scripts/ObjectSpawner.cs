using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject token;

    public GameObject[] trees;
    public GameObject bigTree;
    public GameObject[] debrees;

    private float tokenTimer = 2.0f;
    private float treeTimer = 2.0f;
    private float bigTreeTimer = 5.0f;
    private float gapTreesTimer = 1.0f;
    

    // Update is called once per frame
    void Update ()
    {
        tokenTimer -= Time.deltaTime;
        treeTimer -= Time.deltaTime;
        bigTreeTimer -= Time.deltaTime;
        gapTreesTimer -= Time.deltaTime;


        if (tokenTimer<0)
        {
            SpawnToken();
        }
        if(treeTimer<0)
        {
            SpawnTree();
        }
        if (bigTreeTimer < 0)
        {
            StartCoroutine(SpawnBigTree());
        }
        if (gapTreesTimer < 0)
        {
            StartCoroutine(SpawnGapTrees());
        }
    }
    void SpawnToken()
    {
        GameObject tok = Instantiate(token, new Vector2(Random.Range(-6, 6), 6), Quaternion.identity) as GameObject;
        tokenTimer = Random.Range(0.5f, 2.5f);

    }
    void SpawnTree()
    {
        GameObject tre = Instantiate(trees[(Random.Range(0,trees.Length))], new Vector2(Random.Range(-6, 6), 10), Quaternion.identity) as GameObject;
        if(P_Collide.tokens<25)
        {
            treeTimer = Random.Range(0.5f, 2.5f);
        }
        if (P_Collide.tokens > 24 && P_Collide.tokens< 50)
        {
            treeTimer = Random.Range(0.4f, 2.0f);
        }
        if (P_Collide.tokens > 49 && P_Collide.tokens < 75)
        {
            treeTimer = Random.Range(0.2f, 1.5f);
        }
        if (P_Collide.tokens > 74 && P_Collide.tokens < 100)
        {
            treeTimer = Random.Range(0.2f, 1.0f);
        }
        if (P_Collide.tokens > 99)
        {
            treeTimer = Random.Range(0.1f, 0.8f);
        }
        
        tre.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1, 3);
    }

    IEnumerator SpawnBigTree()
    {
        bigTreeTimer = 1000;
        Vector2 spawnSpot;
        int debreeTimer = (Random.Range(20, 30));
        int spawnOnPlayer = (Random.Range(0, 100));

        if(spawnOnPlayer<50)
        {
            spawnSpot= new Vector2 (Random.Range(-6, 6), 20);
        }
        else
        {
            spawnSpot = new Vector2(player.transform.position.x, 20);
        }
        while (debreeTimer>0)
        {
            debreeTimer--;
            float t = Random.Range(0.005f, 0.1f);
            yield return new WaitForSeconds(t);
            GameObject debree = Instantiate(debrees[(Random.Range(0, debrees.Length))], new Vector2(spawnSpot.x + Random.Range(-1.0f, 1.0f), spawnSpot.y - 10), Quaternion.identity) as GameObject;
            
        }

        yield return new WaitForSeconds(1);
        Instantiate(bigTree, spawnSpot, Quaternion.identity);
        bigTreeTimer = Random.Range(15.0f, 45.0f);
    }

    IEnumerator SpawnGapTrees()
    {
        bigTreeTimer = 1000;
        treeTimer = 1000;
        tokenTimer = 1000;
        gapTreesTimer = 1000;

        int numTreeStets = Random.Range(1, 5);
        for (int i = 0; i < numTreeStets; i++)
        {
            yield return new WaitForSeconds(1);
            int xPos = -10;
            int GapXPos = Random.Range(-6, 6);
            GameObject gapTreeGroup = new GameObject();
            gapTreeGroup.name = "Gap Tree Group";

            for (int v = 0; v < 20; v++)
            {
                GameObject tre = Instantiate(trees[(Random.Range(0, trees.Length))], new Vector2(xPos,5), Quaternion.identity) as GameObject;

                Destroy(tre.GetComponent<Rigidbody2D>());
                if (xPos != GapXPos)
                {
                    xPos++;
                }
                else
                {
                    xPos = xPos + 2;
                }
                tre.gameObject.transform.parent = gapTreeGroup.gameObject.transform;
                yield return new WaitForSeconds(0.03f);
            }
            float randonTime = Random.Range(0.5f, 3.0f);
            yield return new WaitForSeconds(randonTime);
           
            gapTreeGroup.AddComponent<Rigidbody2D>();
            gapTreeGroup.GetComponent<Rigidbody2D>().gravityScale = 10;
            gapTreeGroup.gameObject.layer = 9;
            gapTreeGroup.gameObject.tag = "Log";
            gapTreeGroup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            gapTreeGroup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        bigTreeTimer = Random.Range(15.0f, 45.0f);
        treeTimer = Random.Range(2.0f,5.0f);
        tokenTimer = Random.Range(0.5f,1.0f);
        gapTreesTimer = Random.Range(30.0f,75.0f);

    }
}
