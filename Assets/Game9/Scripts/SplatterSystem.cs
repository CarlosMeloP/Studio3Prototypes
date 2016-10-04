using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplatterSystem : MonoBehaviour {

    public static SplatterSystem Instance;

    [Header("Splatter Prefab (make sure it's using Splatter Material):")]
    public GameObject SplatterPrefab;

    [Header("Different sprites to add variety to the Splatters")]
    public Sprite[] SplatterSprites;

    [Header("Splatter Pool Settings")]
    public bool CreatePool = true;
    public int PoolSize = 200;

    [Header("How many Splatters do we leave each time")]
    public int NumberOfSplattersPerEmission = 3;

    [Header("Scale, will be randomized between these two values:")]
    public float MinScale = 0.5f;
    public float MaxScale = 0.9f;

    [Header("Z-Rotation, will be randomized between these two values:")]
    public float MinAngle = -90;
    public float MaxAngle = 90;

    [Header("Color, Intensitity will be randomized between these two values:")]
    public float MinIntensity = 0.5f;
    public float MaxIntensity = 1.0f;

    [Header("Alpha, will be randomized between these two values (Min is 0, Max is 1):")]
    [Range(0, 1)]
    public float MinAlpha = 0.2f;
    [Range(0, 1)]
    public float MaxAlpha = 0.5f;

    [Header("Fade In Settings:")]
    public bool FadesIn = true;
    [Tooltip("How long does it take until the system reaches the Final Scale?")]
    public float FadeInDuration = 2f;
    [Tooltip("Fade in starts from Scale 0, or grows from other initial scale?")]
    [Range(0, 1)]
    public float FadeInStartingScale = 0.5f;

    [Header("Fade Out Settings:")]
    public bool FadesOut = false;
    [Tooltip("Time until Fade out starts")]
    public float SecondsUntilFadeOutStarts = 5;
    [Tooltip("Once FadeOut starts, how long until the Splatter dissappears")]
    public float FadeOutDuration = 3;

    private List<GameObject> _freeSplatters;

    void Awake()
    {
        Instance = this;

        if(CreatePool)
        {
            CreateSplatterPool(PoolSize);
        }
    }

    public void CleanSplatters()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public GameObject GetSplatter()
    {
        if (_freeSplatters.Count == 0)
            return CreateSplatter();

        var splatter = _freeSplatters.ElementAt(0);
        _freeSplatters.Remove(splatter);

        return splatter;
    }

    public void LeaveSplatter(Vector3 position, Color color)
    {
        for (int i = 0; i <= NumberOfSplattersPerEmission; i++)
        {
            EmitSingleSplatter(position, color);
        }
    }

    private void CreateSplatterPool(int size)
    {
        _freeSplatters = new List<GameObject>();

        for (int i = 0; i < size; i++)
        {
            _freeSplatters.Add(CreateSplatter());
        }
    }

    private GameObject CreateSplatter()
    {
        // INSTANTIATE A NEW EnergyParticles FROM PREFAB
        GameObject go = Instantiate(SplatterPrefab);
        // PLACE IT OUTSIDE THE CAMERA
        go.transform.position = new Vector3(-3000f, -3000f, 0f);
        go.SetActive(false);
        return go;
    }

    private void EmitSingleSplatter(Vector3 position, Color color)
    {
        //Get or create a new Splatter
        var newSplatter = GetSplatter();
        newSplatter.SetActive(true);
        newSplatter.transform.parent = transform;

        //Replace the sprite
        var spriteRenderer = newSplatter.GetComponent<SpriteRenderer>();
        var randomIndex = Random.Range(0, SplatterSprites.Length);
        spriteRenderer.sprite = SplatterSprites[randomIndex];

        //SET POSITION
        newSplatter.transform.position = position;

        //SET SCALE
        var randomScalar = Random.Range(MinScale, MaxScale);
        newSplatter.transform.localScale = Vector3.one * randomScalar;

        //SET COLOR & ALPHA
        var newColor = color;
        randomScalar = Random.Range(MinIntensity, MaxIntensity);
        newColor = newColor * randomScalar;
        randomScalar = Random.Range(MinAlpha, MaxAlpha);
        newColor.a = randomScalar;
        spriteRenderer.color = newColor;

        //SET ROTATION
        Vector3 euler = newSplatter.transform.eulerAngles;
        euler.z = Random.Range(MinAngle, MaxAngle);
        newSplatter.transform.eulerAngles = euler;

        //FADE IN
        if (FadesIn)
        {
            var originalScale = newSplatter.transform.localScale;
            StartCoroutine(FadeIn(newSplatter, FadeInDuration, originalScale * FadeInStartingScale, originalScale));    
        }

        //FADE OUT
        if (FadesOut)
        {
            StartCoroutine(FadeOut(spriteRenderer, SecondsUntilFadeOutStarts, FadeOutDuration));
        }
    }

    private IEnumerator FadeIn(GameObject toFade, float fadeDuration, Vector3 initialScale, Vector3 endScale)
    {
        var t = 0.0f;

        while (t < 1.0f)
        {
            t += (Time.deltaTime / fadeDuration);
            toFade.transform.localScale = Vector3.Lerp(initialScale, endScale, t);

            yield return null;
        }

        toFade.transform.localScale = endScale;
    }

    private IEnumerator FadeOut(SpriteRenderer toFade, float fadeOutStartDelay, float fadeOutDuration)
    {
        yield return new WaitForSeconds(fadeOutStartDelay);//During this time there's no fade

        var initialColor = toFade.color;
        var endColor = initialColor;
        endColor.a = 0;
        var t = 0.0f;

        while (t < 1.0f)
        {
            t += (Time.deltaTime / fadeOutDuration);
            toFade.color = Color.Lerp(initialColor, endColor, t);

            yield return null;
        }

        toFade.color = endColor;

        Destroy(toFade.gameObject);
    }
}
