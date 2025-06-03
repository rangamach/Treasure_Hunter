using UnityEngine;

[CreateAssetMenu(fileName = "ChestSO", menuName = "Scriptable Objects/ChestSO")]
public class ChestSO : ScriptableObject
{
    public string ChestName;
    public Sprite Icon;
    public int UnlockTimeInMinutes;
    public int MinimumCoinsRewarded, MaximumCoinsRewarded;
    public int MinimumGemsRewarded, MaximumGemsRewarded;

}
