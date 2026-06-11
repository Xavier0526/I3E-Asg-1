using UnityEngine;

public class ParticleDamageZone : MonoBehaviour
{
    public float damagePerSecond = 20f;

    void OnTriggerEnter(Collider other)
    {
        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            Debug.Log("Player entered particle damage zone");
            player.ShowDeathZoneBar();
        }
    }

    void OnTriggerStay(Collider other)
    {
        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            Debug.Log("Particle damaging player");
            player.TakeDeathZoneDamage(damagePerSecond * Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerRespawn player = other.GetComponentInParent<PlayerRespawn>();

        if (player != null)
        {
            Debug.Log("Player left particle damage zone");
            player.HideDeathZoneBar();
        }
    }
}