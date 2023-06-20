namespace CardHouse.Tutorial
{
    public class WaterTargetPlantGate : Gate<TargetCardParams>
    {
        protected override bool IsUnlockedInternal(TargetCardParams gateParams)
        {
            return gateParams.Target.GetComponent<Plant>()?.CanBeWatered() ?? false;
        }
    }
}
