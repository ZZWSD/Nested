using UnityEngine;

public class NestedDollController : MonoBehaviour
{
    public GameObject[] dolls;
    public int currentOuterIndex = 3;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDolls();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Separate();

        if (Input.GetKeyDown(KeyCode.E))
            Combine();
    }

    void Separate()
    {
        if (currentOuterIndex <= 0) return;

        currentOuterIndex--;
        UpdateDolls();
    }
    void Combine()
    {
        if (currentOuterIndex >= dolls.Length - 1) return;

        currentOuterIndex++;
        UpdateDolls();
    }

    void UpdateDolls()
    {
        for (int i = 0; i < dolls.Length; i++)
        {
            dolls[i].SetActive(i <= currentOuterIndex);
        }
    }
}
