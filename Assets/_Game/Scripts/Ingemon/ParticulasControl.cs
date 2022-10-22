using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasControl : MonoBehaviour
{
    public ParticleSystem particulasVeneno;
    public ParticleSystem particulasCurar;
    public ParticleSystem particulasInstancia;


    [ContextMenu("Envenenar")]
    public void Envenenar()
    {
        particulasVeneno.Play(true);
    }
    [ContextMenu("Curado")]
    public void Curado()
    {
        particulasCurar.Play(true);
    }
    [ContextMenu("Instanciar")]
    public void Instanciar()
    {
        particulasInstancia.Play(true);
    }
}
