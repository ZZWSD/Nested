using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCircle : MonoBehaviour
{
    public DoorColor requiredColor = DoorColor.White;
    public int requiredSize = 1;
    public GameObject winPanel;
    bool isCleared = false;
    // 防止重複觸發

    public void TryClear(PlayerController player)
    {
        if (isCleared) return;

        DoorColor playerColor = player.DollColor();
        int playerSize = player.GetCurrentSize();

        // 只要踏上來就會印這行，用來確認偵測是否成功
        Debug.Log($"[偵測中] 玩家顏色: {playerColor}, 尺寸: {playerSize}");

        if (playerColor == requiredColor && playerSize == requiredSize)
        {
            isCleared = true;
            Debug.Log("🎉 條件達成！通關成功！");

            if (winPanel != null)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0f; // 停止遊戲
            }
        }
    }
}