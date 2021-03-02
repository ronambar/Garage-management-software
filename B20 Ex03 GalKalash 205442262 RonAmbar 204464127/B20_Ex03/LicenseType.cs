using System;
using System.Linq;


namespace Ex03.GarageLogic
{
    public static class LicenseType
    {
        public static eLicenseType ParseFromString(string i_NumToParse)
        {
            int numToCheck = int.Parse(i_NumToParse);
            int firstLicenseType = (int)Enum.GetValues(typeof(eLicenseType)).Cast<eLicenseType>().First();
            int lastLicenseType = (int)Enum.GetValues(typeof(eLicenseType)).Cast<eLicenseType>().Last();

            if (ValidationChecks.IsFloatNumberInRange(numToCheck, firstLicenseType, lastLicenseType) == false)
            {
                throw new ValueOutOfRangeException(numToCheck, firstLicenseType, lastLicenseType);
            }

            return (eLicenseType)numToCheck;
        }

        public enum eLicenseType
        {
            A = 1,
            A1,
            AA,
            B,
        }
    }
}
