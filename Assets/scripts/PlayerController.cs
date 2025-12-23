using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public GameObject[] dolls;   // Doll0(最小) → Doll3(最大)
    int currentIndex = 3;
    public DollColor currentColor;
    public int currentSize = 1;

    // 用來記錄「被放在地上的外層娃娃」
    List<GameObject> droppedDolls = new List<GameObject>();

    void Start()
    {
        UpdateDolls();
    }

    void Update()
    {
        // ===== 格子移動 =====
        if (Input.GetKeyDown(KeyCode.W)) Move(Vector3.forward);
        if (Input.GetKeyDown(KeyCode.S)) Move(Vector3.back);
        if (Input.GetKeyDown(KeyCode.A)) Move(Vector3.left);
        if (Input.GetKeyDown(KeyCode.D)) Move(Vector3.right);

        // ===== 俄羅斯娃娃 =====
        if (Input.GetKeyDown(KeyCode.Q)) Separate();
        if (Input.GetKeyDown(KeyCode.E)) Combine();
    }

    void Move(Vector3 dir)
    {
        Vector3 target = transform.position + dir;

        target = new Vector3(
            Mathf.Round(target.x),
            transform.position.y,
            Mathf.Round(target.z)
        );

        // 檢查目標格子有沒有門
        Collider[] hits = Physics.OverlapBox(
            target,
            Vector3.one * 0.4f
        );

        foreach (Collider hit in hits)
        {
            ColorDoor door = hit.GetComponent<ColorDoor>();
            if (door != null)
            {
                DoorColor playerColor = GetPlayerColor();

                if (!door.CanPass(playerColor))
                {
                    // 顏色不符，不能進
                    return;
                }
            }
        }

        transform.position = target;
    }

    // ================= 核心修改 =================

    void Separate()
    {
        if (currentIndex <= 0) return;

        GameObject doll = dolls[currentIndex];

        doll.transform.parent = null;
        doll.transform.position = transform.position;

        droppedDolls.Add(doll);

        currentIndex--;
        UpdateDolls();
    }

    void Combine()
    {
        if (droppedDolls.Count == 0) return;

        Vector3 playerPos = transform.position;

        for (int i = droppedDolls.Count - 1; i >= 0; i--)
        {
            GameObject doll = droppedDolls[i];
            Vector3 dollPos = doll.transform.position;

            if (Mathf.Round(playerPos.x) == Mathf.Round(dollPos.x) &&
                Mathf.Round(playerPos.z) == Mathf.Round(dollPos.z))
            {
                doll.transform.parent = transform;
                doll.transform.localPosition = Vector3.zero;

                droppedDolls.RemoveAt(i);
                currentIndex++;

                UpdateDolls();
                return;
            }
        }
    }


    void UpdateDolls()
    {
        for (int i = 0; i < dolls.Length; i++)
        {
            if (droppedDolls.Contains(dolls[i]))
            {
                dolls[i].SetActive(true);
                continue;
            }

            dolls[i].SetActive(i <= currentIndex);
        }
    }

    DoorColor GetPlayerColor()
    {
        // 最外層娃娃的顏色
        GameObject outerDoll = dolls[currentIndex];
        DollColor dollColor = outerDoll.GetComponent<DollColor>();

        if (dollColor == null)
            return DoorColor.White;

        return dollColor.color;
    }
}