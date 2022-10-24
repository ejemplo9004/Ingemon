using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraSwap : MonoBehaviour
{
    public CinemachineDollyCart cart;
    public CinemachineSmoothPath mainPath;
    public Transform mainAim;
    public CinemachineSmoothPath[] paths;

    public CinemachineVirtualCamera vc;
    // Start is called before the first frame update
    void Awake()
    {
        cart.m_Path = mainPath;
    }
    void Start()
    {
        StartCoroutine(ChangePath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ChangePath(){
        yield return new WaitForSeconds(Random.Range(18,20));
        cart.m_Path = paths[Random.Range(0, paths.Length)];
        StartCoroutine(ChangePath());
    }
}
