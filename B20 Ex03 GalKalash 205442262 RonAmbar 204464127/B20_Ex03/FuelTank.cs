using System;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class FuelTank : EnergySource
    {
        private FuelType.eFuelType m_FuelType;

        public FuelTank() : base()
        {
        }

        public FuelType.eFuelType _FuelType
        {
            get
            {
                return m_FuelType;
            }
            set
            {
                m_FuelType = value;
            }
        }

        public override string ToString()
        {
            return string.Format(@"{0}
Fuel type:                        {1}",
base.ToString(),
m_FuelType.ToString());
        }

        internal void Refueling(float i_AmountOfFuelToAdd, FuelType.eFuelType i_FuelTypeToAdd)
        {
            float minToLoad = 0;
            float maxToLoad = MaxSourceAmount - CurrentSourceAmount;

            if (i_FuelTypeToAdd != _FuelType)
            {
                throw new ArgumentException(string.Format("The vehicle does not support {0} fuel type.", i_FuelTypeToAdd));
            }

            if (i_AmountOfFuelToAdd >= minToLoad && i_AmountOfFuelToAdd <= maxToLoad)
            {
                CurrentSourceAmount += i_AmountOfFuelToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(maxToLoad, maxToLoad, string.Format("Maximum fuel to load is- {0} liters.", maxToLoad));
            }
        }    
    }
}
