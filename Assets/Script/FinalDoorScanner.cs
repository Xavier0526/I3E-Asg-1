using UnityEngine;

public class FinalDoorScanner : MonoBehaviour
{
    public DoorController door;
    public int requiredCoins = 10;

    public string blueCardID = "BlueCard";
    public string redCardID = "RedCard";
    public string yellowCardID = "YellowCard";

    public bool CanOpenFinalDoor(PlayerRespawn player)
    {
        return player.HasCard(blueCardID)
            && player.HasCard(redCardID)
            && player.HasCard(yellowCardID)
            && player.coinCount >= requiredCoins;
    }

    public void TryOpenFinalDoor(PlayerRespawn player)
    {
        if (CanOpenFinalDoor(player))
        {
            door.OpenDoor();
            Debug.Log("Final door opened");
        }
        else
        {
            Debug.Log("Need all cards and enough coins");
        }
    }
}