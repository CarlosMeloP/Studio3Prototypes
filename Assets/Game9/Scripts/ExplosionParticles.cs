using System.Collections;
using UnityEngine;

public class ExplosionParticles : MonoBehaviour
{
    public Color SystemStartColor;
    public float ExplosionLength;
    public ParticleSystem ParticleSystem;


    public void OnEnable()
    {
        //Set particle color
        ParticleSystem.Play();
        Destroy(gameObject, ExplosionLength);
    }

    public void SetColor(Color color)
    {
        var pr = ParticleSystem.GetComponent<ParticleSystemRenderer>();
        pr.material.color = color;
    }
}
