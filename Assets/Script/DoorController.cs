using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = -90f;
    public float openSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;

    void Start()
    {
        closedRotation = transform.rotation;

        openRotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + openAngle,
            transform.eulerAngles.z
        );
    }

    void Update()
    {
        if (isOpen)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                openRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    public void OpenDoor()
    {
        Debug.Log("Door opening");
        isOpen = true;
    }
}