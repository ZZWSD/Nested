using UnityEngine;

public enum DoorColor
{
    White,
    Red,
    Blue,
    Yellow
}

public class ColorDoor : MonoBehaviour
{
    public DoorColor doorColor = DoorColor.White;

    // 之後按鈕會控制這個
    public bool colorLockDisabled = false;

    public bool CanPass(DoorColor playerColor)
    {
        if (colorLockDisabled)
            return true;

        return playerColor == doorColor;
    }
}