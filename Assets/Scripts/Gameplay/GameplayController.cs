//using System.Linq;
//using System.Runtime.CompilerServices;
//using UnityEngine;

//public class GameplayController
//{
//    public void AddChest(ChestTypesSO chestTypesSO)
//    {
//        ChestSO chest = GetRandomChest(chestTypesSO);
//        GameService.Instance.GetUIService().UpdateChestSlotUI(chest);
//    }

//    public ChestSO GetRandomChest(ChestTypesSO chestTypesSO)
//    {
//        float cumulative = 0;
//        float totalPercentage = chestTypesSO.ChestTypes.Sum(box => box.PercentChance);

//        float roll = Random.value * totalPercentage;

//        foreach (var box in chestTypesSO.ChestTypes)
//        {
//            cumulative += box.PercentChance;
//            if (roll <= cumulative)
//                return box.ChestSO;
//        }

//        return chestTypesSO.ChestTypes.Count > 0 ? chestTypesSO.ChestTypes[^1].ChestSO : null;
//    }

//    private int GetRandomNumber(int min, int max) => Random.Range(min, max + 1);
//}
