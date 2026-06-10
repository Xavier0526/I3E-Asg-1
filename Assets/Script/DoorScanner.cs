using UnityEngine;

public class DoorScanner : MonoBehaviour
{
    public string requiredCardID = "BlueCard";
    public string requiredCardName = "Blue Access Card";
    public DoorController door;

    public void TryOpenDoor(PlayerRespawn player)
    {
        if (player.HasCard(requiredCardID))
        {
            door.OpenDoor();
            Debug.Log("Access Granted");
        }
        else
        {
            Debug.Log("Access Denied. Required: " + requiredCardName);
        }
    }
}