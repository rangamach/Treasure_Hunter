using UnityEngine;

[CreateAssetMenu(fileName = "ChestSO", menuName = "Scriptable Objects/ChestSO")]
public class ChestSO : ScriptableObject
{
    public ChestTypes ChestType;
    public Sprite Opened;
    public Sprite Closed;
    public int TimerInMinutes;
    public int MinCoinsReward, MaxCoinsReward;
    public int MinGemsReward, MaxGemsReward;
}
