using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private GameObject roomModel;
    [SerializeField] private Transform roomModelReference; // Para saber la posicion que ocupa en escena el primer room
    [SerializeField] private float rotationMultiplier = 60f;
    [SerializeField] private Vector3 roomPosOffset = new Vector3(0, 9.2f, 0);
    [SerializeField] private RoomController roomController;
    [SerializeField] private float transitionTime;
    [SerializeField] private GameObject camObject;
    [SerializeField] private Vector3 camPositionOffset;
    [SerializeField] private List<GameObject> tiles;
    private Vector3 nextCamPosition;

    private void Start()
    {
        nextCamPosition = camObject.transform.position + camPositionOffset;
    }

    public void StartRoomTransition()
    {
        RoomGeneration();
        StartCoroutine(MakeCameraTransition());
    }

    private IEnumerator MakeCameraTransition()
    {
        yield return new WaitForSeconds(3f);
        Vector3 initialPosition = camObject.transform.position;
        for (float i = 0f; i < transitionTime; i+= Time.deltaTime)
        {
            float normalizedTime = i / transitionTime;
            camObject.transform.position = Vector3.Lerp(initialPosition, nextCamPosition, normalizedTime);
            yield return null;
        }
        camObject.transform.position = nextCamPosition;
        nextCamPosition = camObject.transform.position + camPositionOffset;
        TilesTransition();
        roomController.ConfigureRoom(true);
    }

    private void RoomGeneration()
    {
        float rotationAmount = Random.Range(0, 6);
        GameObject roomCopy = Instantiate(roomModel, roomModelReference.position + roomPosOffset, Quaternion.Euler(0, rotationMultiplier * rotationAmount, 0));
        roomModelReference = roomCopy.transform;
    }

    private void TilesTransition()
    {
        foreach (GameObject tile in tiles)
        {
            tile.transform.position += roomPosOffset;
        }
    }
}
