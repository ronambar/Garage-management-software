using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float m_MinValue;
        public float m_MaxValue = float.MaxValue;

        public ValueOutOfRangeException(float i_Value, float i_MinValue, float i_MaxValue) :
             base(string.Format("{0} is not of range. Minimum is {1} and maximum {2}", i_Value, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(float i_Value, float i_MinValue) :
            base(string.Format("{0} is not of range. Minimum is {1}", i_Value, i_MinValue))
        {
            m_MinValue = i_MinValue;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_ErrorMessage) : base(i_ErrorMessage)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
    }
}
