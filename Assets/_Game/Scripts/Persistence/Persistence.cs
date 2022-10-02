using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoBehaviour
{
    #region Singleton
    public static Persistence persistence;

    private void Awake() {
        if(persistence != null){
            DestroyImmediate(this.gameObject);
            return;
        }
        persistence = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    [SerializeField] private string ingemonsNumberKey;
    [SerializeField] private string ingemonDataKey;

    public void SaveIngemon(List<Ingemonster> data, int ingemonsCount){
        PlayerPrefs.SetInt(ingemonsNumberKey, ingemonsCount);
        for (int i = 0; i < ingemonsCount; i++)
        {
            PlayerPrefs.SetString(ingemonDataKey + i.ToString(), JsonUtility.ToJson(data[i]));
        }       
    }

    public List<Ingemonster> LoadIngemon(){
        int ingemonCount = PlayerPrefs.GetInt(ingemonsNumberKey, 0);
        List<Ingemonster> ingemonsters = new List<Ingemonster>();
        for (int i = 0; i < ingemonCount; i++)
        {
            ingemonsters.Add(JsonUtility.FromJson<Ingemonster>(PlayerPrefs.GetString(ingemonDataKey + i.ToString(), "")));
        }
        return ingemonsters;
    }
}
