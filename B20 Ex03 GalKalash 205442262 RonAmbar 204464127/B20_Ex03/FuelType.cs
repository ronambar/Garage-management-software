using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelType
    {
        public static eFuelType ParseFromString(string i_NumToParse)
        {
            int numToCheck = int.Parse(i_NumToParse);
            int firstFuelType = (int)Enum.GetValues(typeof(eFuelType)).Cast<eFuelType>().First();
            int lastFuelType = (int)Enum.GetValues(typeof(eFuelType)).Cast<eFuelType>().Last();

            if (ValidationChecks.IsFloatNumberInRange(numToCheck, firstFuelType, lastFuelType) == false)
            {
                throw new ValueOutOfRangeException(numToCheck, firstFuelType, lastFuelType);
            }

            return (eFuelType)numToCheck;
        } 

        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98,
        }
    }
}
