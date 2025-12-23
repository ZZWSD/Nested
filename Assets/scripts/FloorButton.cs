using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public ColorDoor[] targetDoors;

    void Update()
    {
        bool pressed = IsAnyDollOnTop();

        foreach (ColorDoor door in targetDoors)
        {
            door.colorLockDisabled = pressed;
        }
    }

    bool IsAnyDollOnTop()
    {
        Vector3 center = transform.position + Vector3.up * 0.5f;
        Vector3 halfSize = new Vector3(0.4f, 0.5f, 0.4f);

        Collider[] hits = Physics.OverlapBox(center, halfSize);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player") || hit.CompareTag("Doll"))
            {
                return true;
            }
        }

        return false;
    }
}