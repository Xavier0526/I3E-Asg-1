using UnityEngine;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    public Transform startPoint;
    public int maxHP = 5;

    // Drag your HP Text (TextMeshPro) here in the Inspector
    public TextMeshProUGUI hpText;

    private int currentHP;
    private Vector3 startPosition;
    private Vector3 respawnPosition;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentHP = maxHP;

        if (startPoint != null)
            startPosition = startPoint.position;
        else
            startPosition = transform.position;

        respawnPosition = startPosition;

        UpdateHPUI();
    }

    public void SetCheckpoint(Vector3 checkpoint)
    {
        respawnPosition = checkpoint + Vector3.up * 1f;
        Debug.Log("Checkpoint saved at: " + respawnPosition);
    }

    public void TakeDamage()
    {
        currentHP--;
        UpdateHPUI();

        Debug.Log("HP left: " + currentHP);

        if (currentHP <= 0)
        {
            currentHP = maxHP;
            UpdateHPUI();

            Teleport(startPosition);

            Debug.Log("0 HP. Respawned at start. HP reset to " + maxHP + ".");
        }
        else
        {
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
        hpText.text = "HP: " + currentHP;
    }
}