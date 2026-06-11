using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    public DoorController door;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Door trigger touched by: " + other.name);

        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            Debug.Log("Closing door");
            door.CloseDoor();
        }
    }
}