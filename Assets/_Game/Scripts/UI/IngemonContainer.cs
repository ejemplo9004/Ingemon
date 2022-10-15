using UnityEngine;

public class IngemonContainer : MonoBehaviour
{
    [SerializeField] private GameObject ingemonDetail;
    public int posMultiplier;
    public void Spawn(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            GameObject copy = Instantiate(ingemonDetail, Vector3.zero + new Vector3(i * posMultiplier, 0, 0),
                Quaternion.identity, gameObject.GetComponent<RectTransform>());
        }
    }
}
