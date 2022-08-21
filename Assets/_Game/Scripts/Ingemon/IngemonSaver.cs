using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  IngemonSaver
{

    public static void SaveIngemonster(Ingemonster ingemonster)
    {
        string ingemon = JsonUtility.ToJson(ingemonster);
        Debug.Log($"Ingemon: {ingemon}");
        PlayerPrefs.SetString(ingemonster.id, ingemon);
        Debug.Log($"ID: {ingemonster.id}");
    }

    public static Ingemonster LoadIngemonster(string id)
    {
        string ingemon = PlayerPrefs.GetString(id);
        Debug.Log(ingemon);
        return null;
    }
}
