using UnityEngine;

public class MoveCam : MonoBehaviour
{
    //https://www.youtube.com/watch?v=f473C43s8nE&t=1s

    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
