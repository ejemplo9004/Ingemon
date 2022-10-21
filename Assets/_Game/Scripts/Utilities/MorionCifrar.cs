using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorionCifrar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Morion".Cifrar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class MorionEstaticos
{
    public static string Cifrar(this string texto)
    {
        int desface = 6;
        char[] abecedario = { 'a', 'A', 'b', 'B', 'y', 'Y', 'i', 'I', 'o', 'O', 'u', 'r', 'U', 'M', 'n', 's', 'S', 'E', 'e', 'v' };
        string cifrado = "";

        for (int i = 0; i < texto.Length; i++)
        {
            bool siNo = false;
            for (int j = 0; j < abecedario.Length; j++)
            {
                if (texto[i] == abecedario[j])
                {
                    siNo = true;
                    cifrado = cifrado + abecedario[(j+desface)%abecedario.Length];
                }
            }
            if (!siNo)
            {
                cifrado = cifrado + (texto[i] + ((desface * texto[0]) % texto[texto.Length - 1])).ToString();
            }
            cifrado = cifrado + ((texto[i] + ((desface * texto[i]) % texto[texto.Length - 1])).ToString());
        }
        char a = texto[0];
        char b = texto[(texto.Length - 1)/2];
        cifrado = (a*desface).ToString() + cifrado + (b*desface).ToString() ;
        return cifrado;
    }
}
