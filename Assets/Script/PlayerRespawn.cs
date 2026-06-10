using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerRespawn : MonoBehaviour
{
    public int coinCount = 0;
    public TextMeshProUGUI coinText;
    public Transform startPoint;
    public int maxHP = 5;

    public int maxDeathZoneHP = 100;
    public float currentDeathZoneHP;

    public TextMeshProUGUI hpText;
    public Image deathEffectImage;
    public Slider deathZoneBar;

    // Card Icons
    public GameObject blueCardIcon;
    public GameObject redCardIcon;
    public GameObject yellowCardIcon;

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

        startPosition = startPoint != null ? startPoint.position : transform.position;
        respawnPosition = startPosition;

        UpdateHPUI();
        UpdateDeathZoneBar();
        UpdateCoinUI();

        if (deathZoneBar != null)
            deathZoneBar.gameObject.SetActive(true);

        if (deathEffectImage != null)
            deathEffectImage.color = new Color(1, 0, 0, 0);

        if (blueCardIcon != null) blueCardIcon.SetActive(false);
        if (redCardIcon != null) redCardIcon.SetActive(false);
        if (yellowCardIcon != null) yellowCardIcon.SetActive(false);
    }

    public void SetCheckpoint(Vector3 checkpoint)
    {
        respawnPosition = checkpoint + Vector3.up * 1f;
    }

    public void TakeDeathZoneDamage(float damage)
    {
        currentDeathZoneHP -= damage;

        if (currentDeathZoneHP < 0)
            currentDeathZoneHP = 0;

        UpdateDeathZoneBar();

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
        // Do not reset damage.
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
        }
        else
        {
            currentDeathZoneHP = maxDeathZoneHP;
            UpdateDeathZoneBar();

            Teleport(respawnPosition);
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

    void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount;
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

            if (cardID == "BlueCard" && blueCardIcon != null)
                blueCardIcon.SetActive(true);

            if (cardID == "RedCard" && redCardIcon != null)
                redCardIcon.SetActive(true);

            if (cardID == "YellowCard" && yellowCardIcon != null)
                yellowCardIcon.SetActive(true);

            Debug.Log("Collected card: " + cardID);
        }
    }

    public bool HasCard(string cardID)
    {
        return collectedCards.Contains(cardID);
    }

    public void CollectCoin(int amount)
    {
        coinCount += amount;

        UpdateCoinUI();

        Debug.Log("Coins: " + coinCount);
    }
}