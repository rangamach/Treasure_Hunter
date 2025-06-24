using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SlotButtonsSO", menuName = "Scriptable Objects/SlotButtonsSO")]
public class SlotButtonsSO : ScriptableObject
{
    [HideInInspector]
    public List<Button> SlotButtons = new List<Button>();
}