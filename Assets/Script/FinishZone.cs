using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("FinishZone touched by: " + other.name);

        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            Debug.Log("Showing finish UI");
            player.ShowFinishUI();
        }
    }
}