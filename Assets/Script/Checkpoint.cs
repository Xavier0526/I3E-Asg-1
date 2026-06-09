using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            if (respawnPoint != null)
                player.SetCheckpoint(respawnPoint.position);
            else
                player.SetCheckpoint(transform.position);

            Debug.Log("New checkpoint touched: " + gameObject.name);
        }
    }
}