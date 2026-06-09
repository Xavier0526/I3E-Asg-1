using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private bool hasDamaged = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasDamaged) return;

        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            hasDamaged = true;
            player.TakeDamage();
        }
    }

    void OnTriggerExit(Collider other)
    {
        hasDamaged = false;
    }
}