using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerRespawn : MonoBehaviour
{
    public Transform startPoint;
    public int maxHP = 5;

    public int maxDeathZoneHP = 100;
    public float currentDeathZoneHP;

    public TextMeshProUGUI hpText;
    public Image deathEffectImage;
    public Slider deathZoneBar;

    private int currentHP;
    private Vector3 startPosition;
    private Vector3 respawnPosition;
    private CharacterController characterController;

    public List<string> collectedCards = new List<string>();

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        currentHP = maxHP;
        currentDeathZoneHP = maxDeathZoneHP;

        if (startPoint != null)
            startPosition = startPoint.position;
        else
            startPosition = transform.position;

        respawnPosition = startPosition;

        UpdateHPUI();
        UpdateDeathZoneBar();

        if (deathZoneBar != null)
            deathZoneBar.gameObject.SetActive(true);

        if (deathEffectImage != null)
            deathEffectImage.color = new Color(1, 0, 0, 0);
    }

    public void SetCheckpoint(Vector3 checkpoint)
    {
        respawnPosition = checkpoint + Vector3.up * 1f;
        Debug.Log("Checkpoint saved at: " + respawnPosition);
    }

    public void TakeDeathZoneDamage(float damage)
    {
        currentDeathZoneHP -= damage;

        if (currentDeathZoneHP < 0)
            currentDeathZoneHP = 0;

        UpdateDeathZoneBar();

        Debug.Log("DeathZone HP: " + currentDeathZoneHP);

        if (currentDeathZoneHP <= 0)
        {
            currentDeathZoneHP = maxDeathZoneHP;
            UpdateDeathZoneBar();

            TakeDamage();
        }
    }

    public void ShowDeathZoneBar()
    {
        if (deathZoneBar != null)
            deathZoneBar.gameObject.SetActive(true);
    }

    public void HideDeathZoneBar()
    {
        // Don't reset the bar when leaving the DeathZone.
        // Just hide it so it keeps its previous value.

        
    }

    public void TakeDamage()
    {
        StartCoroutine(DeathEffect());

        currentHP--;
        UpdateHPUI();

        if (currentHP <= 0)
        {
            currentHP = maxHP;
            UpdateHPUI();

            currentDeathZoneHP = maxDeathZoneHP;
            UpdateDeathZoneBar();

            Teleport(startPosition);

            Debug.Log("0 HP. Respawned at start. HP reset to " + maxHP + ".");
        }
        else
        {
            currentDeathZoneHP = maxDeathZoneHP;
            UpdateDeathZoneBar();

            Teleport(respawnPosition);
            Debug.Log("Damaged. Respawned at checkpoint.");
        }
    }

    void Teleport(Vector3 position)
    {
        characterController.enabled = false;
        transform.position = position;
        characterController.enabled = true;
    }

    void UpdateHPUI()
    {
        if (hpText != null)
            hpText.text = "HP: " + currentHP;
    }

    void UpdateDeathZoneBar()
    {
        if (deathZoneBar != null)
        {
            deathZoneBar.maxValue = maxDeathZoneHP;
            deathZoneBar.value = currentDeathZoneHP;
        }
    }

    IEnumerator DeathEffect()
    {
        if (deathEffectImage != null)
        {
            float alpha = 0f;

            while (alpha < 0.7f)
            {
                alpha += Time.deltaTime * 2f;
                deathEffectImage.color = new Color(1, 0, 0, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(0.3f);

            while (alpha > 0f)
            {
                alpha -= Time.deltaTime * 2f;
                deathEffectImage.color = new Color(1, 0, 0, alpha);
                yield return null;
            }

            deathEffectImage.color = new Color(1, 0, 0, 0);
        }
    }

    public void CollectCard(string cardID)
    {
        if (!collectedCards.Contains(cardID))
        {
            collectedCards.Add(cardID);
            Debug.Log("Collected card: " + cardID);
        }
    }
    public bool HasCard(string cardID)
    {
        return collectedCards.Contains(cardID);
    }
}