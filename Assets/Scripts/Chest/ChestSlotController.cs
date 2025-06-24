using System.Collections.Generic;
using UnityEngine.UI;

public class ChestSlotController
{
    private static ChestSlotModel chestSlotModel;

    public ChestSO chest;
    public int index;
    private ChestSLotStateMachine stateMachine;

    public ChestSlotController(ChestTypesSO chestTypes, int totalSlots, List<Button> slotButtons)
    {
        if(chestSlotModel == null)
            chestSlotModel = new ChestSlotModel(chestTypes, totalSlots,slotButtons);
    }
    public ChestSlotController()
    {
        //chest = new ChestSO();

        CreateStateMachine();
        stateMachine.ChangeState(ChestStates.Empty); 
    }
    private void CreateStateMachine() => stateMachine = new ChestSLotStateMachine(this);
    public ChestSlotModel GetChestSlotModel() => chestSlotModel;
    public ChestSLotStateMachine GetStateMachine() => stateMachine;
}
