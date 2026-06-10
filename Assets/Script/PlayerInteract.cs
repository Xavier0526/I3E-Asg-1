using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 5f;
    public Camera playerCamera;

    public GameObject cardPromptPanel;
    public GameObject scannerPromptPanel;

    public TextMeshProUGUI cardPromptText;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI scannerPromptText;
    public TextMeshProUGUI scannerStatusText;

    private float messageTimer = 0f;
    private bool showingMessage = false;

    void Update()
    {
        if (showingMessage)
        {
            messageTimer -= Time.deltaTime;

            if (messageTimer <= 0)
                showingMessage = false;
        }
        else
        {
            cardPromptPanel.SetActive(false);
            scannerPromptPanel.SetActive(false);

            cardPromptText.text = "";
            cardNameText.text = "";
            scannerPromptText.text = "";
            scannerStatusText.text = "";
        }

        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            KeyCard keyCard = hit.collider.GetComponentInParent<KeyCard>();

            if (keyCard != null && !showingMessage)
            {
                cardPromptPanel.SetActive(true);

                cardPromptText.text = "Press [E] to collect";
                cardNameText.text = keyCard.displayName;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GetComponent<PlayerRespawn>().CollectCard(keyCard.cardID);
                    keyCard.gameObject.SetActive(false);
                }

                return;
            }

            Coin coin = hit.collider.GetComponentInParent<Coin>();

            if (coin != null)
            {
                cardPromptPanel.SetActive(true);

                cardPromptText.text = "Press [E] to collect";
                cardNameText.text = "Coin";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GetComponent<PlayerRespawn>().CollectCoin(coin.value);
                    Destroy(coin.gameObject);
                }

                return;
            }

            DoorScanner scanner = hit.collider.GetComponentInParent<DoorScanner>();

            if (scanner != null)
            {
                PlayerRespawn player = GetComponent<PlayerRespawn>();

                scannerPromptPanel.SetActive(true);

                if (!showingMessage)
                {
                    scannerPromptText.text = "Press [E] to scan card";
                    scannerStatusText.text = "Required: " + scanner.requiredCardName;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (player.HasCard(scanner.requiredCardID))
                    {
                        scannerPromptText.text = "";
                        scannerStatusText.text = "ACCESS GRANTED";

                        showingMessage = true;
                        messageTimer = 2f;

                        scanner.TryOpenDoor(player);
                    }
                    else
                    {
                        scannerPromptText.text = "";
                        scannerStatusText.text =
                            "ACCESS DENIED\nRequired: " + scanner.requiredCardName;

                        showingMessage = true;
                        messageTimer = 2f;
                    }
                }

                return;
            }
        }
    }
}