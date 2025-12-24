using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public ColorDoor[] targetDoors;

    void Update()
    {
        bool pressed = IsAnyDollOnTop();

        foreach (ColorDoor door in targetDoors)
        {
            if (door != null)
            {
                // 當按鈕被壓住 (pressed 為 true)，門應該「消失」(SetActive(false))
                // 當按鈕放開 (pressed 為 false)，門應該「出現」(SetActive(true))
                door.gameObject.SetActive(!pressed);
            }
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