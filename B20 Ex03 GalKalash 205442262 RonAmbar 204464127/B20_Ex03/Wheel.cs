using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_CanufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxManufactureAirPressure;

        public const float k_MinAirPressureValue = 0;
        
        public Wheel(){}

        public static List<string> GetInformationMessages()
        {
            List<string> informationMessages = new List<string>
            {
                "Please enter Wheel Canufacturer Name: ",
                "Please enter current Air Pressure: "
            };
        
            return informationMessages;
        }

        public override string ToString()
        {
            return string.Format(@"Wheels information: 
Canufacturer name:                {0}
Current air pressure:             {1}
Maximum manufacture air pressure: {2}",
m_CanufacturerName,
m_CurrentAirPressure,
m_MaxManufactureAirPressure);

        }

        public void InflatingWheel(float i_AmountOfAirToAdd)
        {
            float minToinflating = 0;
            float maxToinflating = (m_MaxManufactureAirPressure - m_CurrentAirPressure);

            if (i_AmountOfAirToAdd <= maxToinflating && i_AmountOfAirToAdd >= minToinflating)
            {
                m_CurrentAirPressure += i_AmountOfAirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(minToinflating, maxToinflating, string.Format("Maximum inflatable air pressure is- {0}.", maxToinflating));
            }
        }

        public string CanufacturerName
        {
            get
            {
                return m_CanufacturerName;
            }
            set
            {
                if (ValidationChecks.IsStrEmpty(value) == true)
                {
                    throw new FormatException("Empty input is not allowed.");
                }

                m_CanufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (ValidationChecks.IsFloatNumberInRange(value, k_MinAirPressureValue, m_MaxManufactureAirPressure) == false)
                {
                    throw new ValueOutOfRangeException(value, k_MinAirPressureValue, m_MaxManufactureAirPressure);
                }

                m_CurrentAirPressure = value;
            }
        }

        public float MaxManufactureAirPressure
        {
            get
            {
                return m_MaxManufactureAirPressure;
            }
            set
            {
                m_MaxManufactureAirPressure = value;
            }
        }
    }
}
