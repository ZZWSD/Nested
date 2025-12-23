using UnityEngine;
using UnityEngine.Playables;

public class GoalCircle : MonoBehaviour
{
    public DollColor requiredColor;
    public int requiredSize = 1;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        if (player.currentColor == requiredColor &&
            player.currentSize == requiredSize)
        {
            Debug.Log("Level Clear");
            // 這裡你原本怎麼過關就怎麼寫
        }
    }
}