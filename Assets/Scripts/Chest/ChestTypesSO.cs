using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestTypesSO", menuName = "Scriptable Objects/ChestTypesSO")]
public class ChestTypesSO : ScriptableObject
{
    public List<ChestType> ChestTypes;
}
[System.Serializable]
public class ChestType
{
    public ChestTypes Type;
    public ChestSO ChestSO;
    [Range(0,100)]
    public float PercentChance;
}
