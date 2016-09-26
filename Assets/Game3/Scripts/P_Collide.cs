using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class P_Collide : MonoBehaviour
{
    public static int tokens;
    public GameObject tokenUI;
    public static int heatlh = 3;
    public AudioClip collectToken;
    public AudioClip pain;
    public SpriteRenderer rend;
    public Color altColor = Color.red;
    public Color initColor;

    void Start()
    {
        rend = this.GetComponent<SpriteRenderer>();
        initColor = rend.color;
        
    }
    // Update is called once per frame
    void Update ()
    {
	    if(heatlh<0)
        {
            StartCoroutine(GameOver());
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag =="Token")
        {

           // GetComponent<AudioSource>().PlayOneShot(collectToken, 0.5f);
            tokens++;
            tokenUI.GetComponent<Text>().text = (tokens.ToString());
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Log")
        {
            TakeDamage();
        }

    }

    void TakeDamage()
    {
        // GetComponent<AudioSource>().PlayOneShot(collectToken, 0.5f);
        heatlh--;
        StartCoroutine(ReceiveDamage());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return null;
    }

    IEnumerator ReceiveDamage()
    {
        rend.color = altColor;
        yield return new WaitForSeconds(0.1f);
        rend.color = initColor;
        yield return new WaitForSeconds(0.1f);
        rend.color = altColor;
        yield return new WaitForSeconds(0.1f);
        rend.color = initColor;
        yield return null;
    }

}
