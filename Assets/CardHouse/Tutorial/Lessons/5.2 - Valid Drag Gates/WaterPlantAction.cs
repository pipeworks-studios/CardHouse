namespace CardHouse.Tutorial
{
    public class WaterPlantAction : CardTargetCardOperator
    {
        protected override void ActOnTarget()
        {
            var plant = Target.GetComponent<Plant>();
            if (plant != null)
            {
                plant.Water();
            }
        }
    }
}
