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

    public GameObject gameOverPanel;
    public float gameOverDuration = 2f;
    public GameObject finishPanel;
    public TextMeshProUGUI finishStatsText;

    public AudioSource audioSource;
    public AudioClip coinCollectSound;
    public AudioClip cardCollectSound;

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

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        currentHP = maxHP;
        currentDeathZoneHP = maxDeathZoneHP;

        startPosition = startPoint != null ? startPoint.position : transform.position;
        respawnPosition = startPosition;

        UpdateHPUI();
        UpdateDeathZoneBar();
        UpdateCoinUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
            
        if (finishPanel != null)
            finishPanel.SetActive(false);

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
        // Do not reset DeathZone HP.
    }

    public void TakeDamage()
    {
        StartCoroutine(DeathEffect());

        currentHP--;
        UpdateHPUI();

        if (currentHP <= 0)
        {
            StartCoroutine(GameOverRoutine());
        }
        else
        {
            currentDeathZoneHP = maxDeathZoneHP;
            UpdateDeathZoneBar();

            Teleport(respawnPosition);
        }
    }

    IEnumerator GameOverRoutine()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(gameOverDuration);

        currentHP = maxHP;
        UpdateHPUI();

        currentDeathZoneHP = maxDeathZoneHP;
        UpdateDeathZoneBar();

        Teleport(startPosition);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
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

            if (audioSource != null && cardCollectSound != null)
                audioSource.PlayOneShot(cardCollectSound);

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

        if (audioSource != null && coinCollectSound != null)
            audioSource.PlayOneShot(coinCollectSound);

        Debug.Log("Coins: " + coinCount);
    }

    public void ShowFinishUI()
    {
        if (finishPanel != null)
            finishPanel.SetActive(true);

        if (finishStatsText != null)
        {
            finishStatsText.text =
            "GAME COMPLETED\n\n" +
            "Coins Collected : " + coinCount + "\n" +
            "Cards Collected : " + collectedCards.Count + "/3\n" +
            "HP Remaining : " + currentHP;
        }

        Time.timeScale = 0f;
    }
}