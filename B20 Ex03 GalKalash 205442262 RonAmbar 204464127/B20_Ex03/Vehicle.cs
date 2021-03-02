using System;
using System.Linq;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private VehicleStatus.eVehicleStatus m_VehicleStatus;
        private string m_ModelName;
        private readonly string r_LicennseNumber;
        private float m_PrecentOfenergyRemaining;
        private List<Wheel> m_Wheels;
        private EnergySource m_EnergySource;
        private List<string> m_InformationMessages;

        public const int k_LicenseNumberMinLen = 5;
        public const int k_LicenseNumberMaxLen = 8;
        public const int k_PhoneNumberMinLen = 9;
        public const int k_PhoneNumberMaxLen = 10;
        public const float k_MinValue = 0;
        public const float k_MaxPrecentValue = 100;

        public Vehicle(string i_LicennseNumber, EnergySource i_EnergySource)
        {
            r_LicennseNumber = i_LicennseNumber;
            m_EnergySource = i_EnergySource;
            m_VehicleStatus = VehicleStatus.eVehicleStatus.InRepair;
        }

        public static void IsValidLicenseNumberStr(string i_LicenseNumberStr)
        {
            if (ValidationChecks.IsValidStrLen(i_LicenseNumberStr, k_LicenseNumberMinLen, k_LicenseNumberMaxLen) == false)
            {
                throw new ValueOutOfRangeException(k_LicenseNumberMinLen, k_LicenseNumberMaxLen,
                    string.Format("The license number is not of range. Minimum digits is {0} and maximum {1}", k_LicenseNumberMinLen, k_LicenseNumberMaxLen));
            }

            for (int i = 0; i < i_LicenseNumberStr.Length; i++)
            {
                if (char.IsLetterOrDigit(i_LicenseNumberStr[i]) == false)
                {
                    throw new FormatException("The input must be digits or letters only");
                }
            }
        }

        public virtual List<string> GetInformationMessages()
        {
            List<string> informationMessages = new List<string>
            {
                "Please enter the owner name: ",
                "Please enter the owner phone number: ",
                "Please enter the model name: ",
                "Please enter the precent of energy remaining: "
            };

            informationMessages.AddRange(Wheel.GetInformationMessages());

            return informationMessages;
        }

        public abstract void CheckAndUpdateInformation(string i_InputToCheck, int i_MemberIndex);

        internal void CheckAndUpdateVehicleInformation(string i_InputToCheck, int i_MemberIndex)
        {
            eVehicleMembers memberToCheck = (eVehicleMembers)i_MemberIndex;

            switch (memberToCheck)
            {
                case eVehicleMembers.OwnerName:
                    {
                        OwnerName = i_InputToCheck;
                        break;
                    }
                case eVehicleMembers.OwnerPhoneNumber:
                    {
                        OwnerPhoneNumber = i_InputToCheck;
                        break;
                    }
                case eVehicleMembers.ModelName:
                    {
                        ModelName = i_InputToCheck;
                        break;
                    }
                case eVehicleMembers.PecentOfEnergyRemaining:
                    {
                        PrecentOfenergyRemaining = float.Parse(i_InputToCheck);
                        EnergySource.CurrentSourceAmount = GetSourceAmountfromPrecentOfenergyRemaining(PrecentOfenergyRemaining);
                        break;
                    }
                case eVehicleMembers.CanufacturerName:
                    {
                        foreach (Wheel wheel in Wheels)
                        {
                            wheel.CanufacturerName = i_InputToCheck;
                        }
                        break;
                    }
                case eVehicleMembers.CurrentAirPressure:
                    {
                        foreach (Wheel wheel in Wheels)
                        {
                            wheel.CurrentAirPressure = float.Parse(i_InputToCheck);
                        }
                        break;
                    }
            }
        }

        internal float GetSourceAmountfromPrecentOfenergyRemaining(float i_PrecentOfenergy)
        {
            return m_EnergySource.MaxSourceAmount * i_PrecentOfenergy / 100;
        }

        public List<string> InformationMessages
        {
            get
            {
                return m_InformationMessages;
            }
            set
            {
                m_InformationMessages = value;
            }
        }

        public VehicleStatus.eVehicleStatus _VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }
            set
            {
                m_VehicleStatus = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
            set
            {
                if (ValidationChecks.IsStrEmpty(value) == true)
                {
                    throw new FormatException("Empty input is not allowed.");
                }
                else if (ValidationChecks.IsLettersStr(value) == false)
                {
                    throw new FormatException("The input must be digits only");
                }

                m_OwnerName = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
            set
            {
                if (ValidationChecks.IsValidStrLen(value, k_PhoneNumberMinLen, k_PhoneNumberMaxLen) == false)
                {
                    throw new ValueOutOfRangeException(k_PhoneNumberMinLen, k_PhoneNumberMaxLen,
                        string.Format("The number is not of range. Minimum digits is {0} and maximum {1}", k_PhoneNumberMinLen, k_PhoneNumberMaxLen));
                }
                else if (ValidationChecks.IsNumberStr(value) == false)
                {
                    throw new FormatException("The phone number must contains digits only.");
                }

                m_OwnerPhoneNumber = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
            set
            {
                if (ValidationChecks.IsStrEmpty(value) == true)
                {
                    throw new FormatException("Empty input is not allowed.");
                }

                m_ModelName = value;
            }
        }

        public string LicennseNumber
        {
            get
            {
                return r_LicennseNumber;
            }
        }

        public float PrecentOfenergyRemaining
        {
            get
            {
                return m_PrecentOfenergyRemaining;
            }
            set
            {
                if (ValidationChecks.IsFloatNumberInRange(value, k_MinValue, k_MaxPrecentValue) == false)
                {
                    throw new ValueOutOfRangeException(value, k_MinValue, k_MaxPrecentValue);
                }

                m_PrecentOfenergyRemaining = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
            set
            {
                m_Wheels = value;
            }
        }

        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
            set
            {
                m_EnergySource = value;
            }
        }

        public override int GetHashCode()
        {
            return r_LicennseNumber.GetHashCode();
        }

        public override string ToString()
        {
            string energySource;
            string spaces;

            if (m_EnergySource is FuelTank)
            {
                energySource = "fule";
                spaces = "   ";
            }
            else
            {
                energySource = "battery";
                spaces = "";
            }

            return string.Format(@"
Licennse number:                  {0}
Owner name:                       {1}
Owner phoneNumber:                {2}
Vehicle status:                   {3}
Model name:                       {4}
Precent of {6} remaining: {9}    {5}%
Energy source:                    {6}{7}
{8}",
r_LicennseNumber,
m_OwnerName,
m_OwnerPhoneNumber,
m_VehicleStatus.ToString(),
m_ModelName,
m_PrecentOfenergyRemaining,
energySource,
EnergySource.ToString(),
m_Wheels[0].ToString(),
spaces
);
        }

        public class VehicleStatus
        {
            public static eVehicleStatus ParseFromString(string i_NumToParse)
            {
                int numToCheck = int.Parse(i_NumToParse);
                int firstVehicleStatus = (int)Enum.GetValues(typeof(eVehicleStatus)).Cast<eVehicleStatus>().First();
                int lastVehicleStatus = (int)Enum.GetValues(typeof(eVehicleStatus)).Cast<eVehicleStatus>().Last();

                if (ValidationChecks.IsFloatNumberInRange(numToCheck, firstVehicleStatus, lastVehicleStatus) == false)
                {
                    throw new ValueOutOfRangeException(numToCheck, firstVehicleStatus, lastVehicleStatus);
                }

                return (eVehicleStatus)numToCheck;
            }

            public static string GetStatusStr(eVehicleStatus i_StatusToPrint)
            {
                string statusStrToReturn;

                if (i_StatusToPrint == eVehicleStatus.InRepair)
                {
                    statusStrToReturn = "In Repair";
                }
                else
                {
                    statusStrToReturn = i_StatusToPrint.ToString();
                }

                return statusStrToReturn;
            }

            public enum eVehicleStatus
            {
                InRepair = 1,
                Repaired,
                Paid,
            }
        }
    }

    public enum eVehicleMembers
    {
        OwnerName,
        OwnerPhoneNumber,
        ModelName,
        PecentOfEnergyRemaining,
        CanufacturerName,
        CurrentAirPressure,
    }
}

