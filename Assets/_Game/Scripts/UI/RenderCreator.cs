using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderCreator : MonoBehaviour
{
    [SerializeField] private List<RenderTexture> textures;
    [SerializeField] private List<Camera> cameras;
    [SerializeField] private List<RawImage> images;
    public Rect size;
    private void Start()
    {
        size = images[0].gameObject.GetComponent<RectTransform>().rect;
        textures = new List<RenderTexture>();
        CreateTextures();
        AssignTexturesToCameras();
        AssignTexturesToImages();
    }

    private void OnDisable()
    {
        for (int i = 0; i < textures.Count; i++)
        {
            textures[i].Release();
        }
    }

    private void CreateTextures()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            RenderTexture render = new RenderTexture(Mathf.RoundToInt(size.width), Mathf.RoundToInt(size.height), 16, RenderTextureFormat.ARGB32);
            render.name = "RT" + i;
            render.Create();
            textures.Add(render);
        }
    }

    private void AssignTexturesToCameras()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].targetTexture = textures[i];
        }
    }

    private void AssignTexturesToImages()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            images[i].texture = textures[i];
            images[i].gameObject.GetComponentInParent<IngemonDetails>().CreateInfo(i);
        }

        if (cameras.Count != images.Count)
        {
            for (int i = cameras.Count; i < images.Count; i++)
            {
                images[i].color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void AddImages(List<RawImage> rawImages)
    {
        foreach (RawImage rawImage in rawImages)
        {
            images.Add(rawImage);
        }
    }
    
    public List<Camera> Cameras
    {
        get => cameras;
        set => cameras = value;
    }
}
