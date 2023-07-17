using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Servidor", menuName = "JuegoServidor", order = 1)]
public class Servidor : ScriptableObject
{
    public string servidor;
    public Servicio[] servicios;

    public bool ocupado = false;
    public Respuesta respuesta;

    public IEnumerator ConsumirServicio(string nombre, string[] datos, UnityAction e)
    {
        ocupado = true;
        WWWForm formulario = new WWWForm();
        Servicio s = new Servicio();
        for (int i = 0; i < servicios.Length; i++)
        {
            if (servicios[i].nombre.Equals(nombre))
            {
                s = servicios[i];
            }
        }

        for (int i = 0; i < s.parametros.Length; i++)
        {
            formulario.AddField(s.parametros[i], datos[i]);
        }
        UnityWebRequest www = UnityWebRequest.Post(servidor + "/" + s.URL, formulario);
        Logger.Instance.LogInfo(servidor + "/" + s.URL);

        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            respuesta = new Respuesta();
        }
        else
        {
            Logger.Instance.LogInfo($"RESPUESTA: {www.downloadHandler.text}");
            respuesta = JsonUtility.FromJson<Respuesta>(www.downloadHandler.text);
        }
        ocupado = false;
        www.Dispose();
        e.Invoke();
    }
}

[System.Serializable]
public class Servicio
{
    public string nombre;
    public string URL;
    public string[] parametros;
}

[System.Serializable]
public class Respuesta
{
    public int codigo;
    public string mensaje;
    public string respuesta;


    public Respuesta()
    {
        codigo = 404;
        mensaje = "Error";
    }
}

[System.Serializable]
public class dbUsuario
{
    public int id;
    public string name;
    public string password;
    public int gold;

    public static dbUsuario CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<dbUsuario>(jsonString);
    }
}