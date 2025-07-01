public interface IState
{
    public ChestSlotController Owner { get; set; }
    public void OnStateEnter();
    public void Update();
    public void OnStateExit();

}
