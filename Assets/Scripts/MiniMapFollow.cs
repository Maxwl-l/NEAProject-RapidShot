using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 20, 0);

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
