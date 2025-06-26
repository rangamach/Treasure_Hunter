using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SlotButtonsSO", menuName = "Scriptable Objects/SlotButtonsSO")]
public class SlotButtonsSO : ScriptableObject
{
    [HideInInspector]
    public List<SlotUI> SlotUIList = new List<SlotUI>();
    //public List<Button> SlotButtons = new List<Button>();
}

[Serializable]
public class SlotUI
{
    public Button slotButton;
    public TextMeshProUGUI timerText;
}