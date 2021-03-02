namespace Ex03.GarageLogic
{
    public class Battery : EnergySource
    {
        public Battery() : base() { }
        
        public void LoadBattery(float i_AmountOfMinutesToAdd)
        {
            float minToLoad = 0;
            int minutesInHour = 60;
            float maxToLoad = (MaxSourceAmount - CurrentSourceAmount)* minutesInHour;

            if (i_AmountOfMinutesToAdd <= maxToLoad && i_AmountOfMinutesToAdd >= minToLoad)
            {
                CurrentSourceAmount += i_AmountOfMinutesToAdd / minutesInHour;
            }
            else
            {
                throw new ValueOutOfRangeException(minToLoad, maxToLoad , string.Format("Maximum minutes to load is- {0}.", maxToLoad));
            }
        }
    }
}
