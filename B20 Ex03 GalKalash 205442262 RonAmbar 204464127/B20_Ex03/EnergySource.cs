
namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        private float m_CurrentSourceAmount;
        private float m_MaxSourceAmount;
        public const float k_MinSourceAmount = 0;

        public override string ToString()
        {
            string energySource;
            string energySourceType;
            string spaces;

            if (this is FuelTank)
            {
                energySource = "fule";
                energySourceType = "liter";
                spaces = "     ";
            }
            else
            {
                energySource = "battery";
                energySourceType = "Hours";
                spaces = "  ";
            }

            return string.Format(@"
Current {0} amount:{4}         {1} {2}
Maximum {0} amount:{4}         {3} {2}",
energySource,
m_CurrentSourceAmount,
energySourceType,
m_MaxSourceAmount,
spaces);
        }

        public float CurrentSourceAmount
        {
            get
            {
                return m_CurrentSourceAmount;
            }
            set
            {
                m_CurrentSourceAmount = value;
            }
        }

        public float MaxSourceAmount
        {
            get
            {
                return m_MaxSourceAmount;
            }
            set
            {
                m_MaxSourceAmount = value;
            }
        }

    }

}
