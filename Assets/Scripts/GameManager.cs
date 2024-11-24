using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Room Settings")]
    public int roomCount = 0; // Tracks the number of rooms
    public GameObject currentPlacer; // Reference to the current placer object
    public string combination; // String to locate room placers
    public Vector3 offsetTotal; // Tracks the cumulative offset for room placement
    private bool needsOffset; // Flag to determine if an offset is required
    private int whichOffset; // Specifies which offset to apply

    [Header("Room Offsets")]
    public Vector3 lightningLevel1 = new Vector3(0f, 18.17f, 0f); // Example offset for Lightning Level 1

    void Start()
    {
        GenerateRooms();
    }

    /// <summary>
    /// Generates rooms based on random logic.
    /// </summary>
    private void GenerateRooms()
    {
        for (int i = 1; i <= 6; i++)
        {
            // Find the current room placer
            combination = $"Placer ({i})";
            currentPlacer = GameObject.Find(combination);

            if (currentPlacer == null)
            {
                Debug.LogWarning($"Placer object '{combination}' not found!");
                continue;
            }

            // Determine the random room type
            int randomRoom = Random.Range(0, 6);
            Debug.Log($"Placer {i}: Random Room = {randomRoom}");

            // If the room requires an offset
            if (randomRoom == 5)
            {
                needsOffset = true;
                whichOffset = 1;
                Debug.Log($"Room {i} needs offset. Offset: {lightningLevel1}");
            }

            // Create a normal room or boss room
            if (i < 6)
            {
                currentPlacer.SendMessage("SetPosition", offsetTotal);
                currentPlacer.SendMessage("CreateRoom", randomRoom);
            }
            else
            {
                currentPlacer.SendMessage("SetPosition", offsetTotal);
                currentPlacer.SendMessage("CreateBossRoom", SendMessageOptions.DontRequireReceiver);
            }

            // Apply the offset if required
            if (needsOffset)
            {
                ApplyOffset();
            }
        }
    }

    /// <summary>
    /// Applies the required offset based on the current offset type.
    /// </summary>
    private void ApplyOffset()
    {
        switch (whichOffset)
        {
            case 1:
                offsetTotal += lightningLevel1;
                break;
            default:
                Debug.LogWarning("Unknown offset type. No offset applied.");
                break;
        }

        needsOffset = false; // Reset the flag
        whichOffset = 0; // Reset the offset type
    }

    /// <summary>
    /// Increments the room count.
    /// </summary>
    public void IncreaseRoomCount()
    {
        roomCount++;
        Debug.Log($"Room count increased to {roomCount}");
    }

    /// <summary>
    /// Sends the current room count to the specified GameObject.
    /// </summary>
    public void SendRoomCount(GameObject other)
    {
        if (other != null)
        {
            Debug.Log($"Sending room count {roomCount} to {other.name}");
            other.SendMessage("GetRoomCount", roomCount, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Debug.LogWarning("SendRoomCount called with a null GameObject.");
        }
    }
}