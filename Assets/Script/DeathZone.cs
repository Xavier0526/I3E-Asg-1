using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public float damagePerSecond = 25f;

    void OnTriggerEnter(Collider other)
    {
        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            player.ShowDeathZoneBar();
        }
    }

    void OnTriggerStay(Collider other)
    {
        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            player.TakeDeathZoneDamage(damagePerSecond * Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            player.HideDeathZoneBar();
        }
    }
}