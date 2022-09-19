
using UnityEngine;

[RequireComponent(typeof(MorionCambioColores))]
public class MorionCambioMascaras : MonoBehaviour
{
    MorionCambioColores mColores;
    [Header("M�scaras")]
    public Texture2D[]  manchas;
    public Texture2D[]  dorsos;
    public int          iManchas;
    public int          iDorsos;

    void Awake()
    {
        mColores = GetComponent<MorionCambioColores>();
    }

    void AplicarCambios()
    {
        mColores.m.SetTexture("_MascaraManchas", manchas[iManchas]);
        mColores.m.SetTexture("_MascaraDorsal", dorsos[iDorsos]);
    }

    public void CambiarTexturas(int _dorso, int _manchas)
    {
        iManchas = _manchas;
        iDorsos = _dorso;
        AplicarCambios();
    }

    [ContextMenu("Aleatorizar")]
    public void Aleatorizar()
    {
        CambiarTexturas(Random.Range(0, 10), Random.Range(0, 10));
        mColores.AplicarColoresAleatorios();
    }
}
