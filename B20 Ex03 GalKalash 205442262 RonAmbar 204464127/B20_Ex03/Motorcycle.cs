using System;
using System.Linq;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private LicenseType.eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public const int k_MinEngineValue = 50;
        public const int k_MaxEngineVolume = 2000;

        public Motorcycle(string i_LicennseNumber, EnergySource i_EnergySource) : base(i_LicennseNumber, i_EnergySource)
        {
            InformationMessages = GetInformationMessages();
        }

        public LicenseType.eLicenseType _LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                if (ValidationChecks.IsFloatNumberInRange(value, k_MinEngineValue, k_MaxEngineVolume) == false)
                {
                    throw new ValueOutOfRangeException(value, k_MinEngineValue, k_MaxEngineVolume);
                }

                m_EngineVolume = value;
            }
        }

        public override List<string> GetInformationMessages()
        {
            List<string> informationMessages = new List<string>();

            informationMessages.AddRange(base.GetInformationMessages());
            string licensesType = GarageSystem.EnumToStringWhithOutSpaces(typeof(LicenseType.eLicenseType));
            informationMessages.Add(string.Format(@"Please select one of the following licenses type : 
{0}", licensesType));
            informationMessages.Add("Please enter the engine volume: ");

            return informationMessages;
        }

        public override void CheckAndUpdateInformation(string i_InputToCheck, int i_MemberIndex)
        {
            int firstMotorcycleMembers = (int)Enum.GetValues(typeof(eMotorcycleMembers)).Cast<eMotorcycleMembers>().First();

            if (i_MemberIndex < firstMotorcycleMembers)
            {
                CheckAndUpdateVehicleInformation(i_InputToCheck, i_MemberIndex);
            }
            else
            {
                eMotorcycleMembers memberToCheck = (eMotorcycleMembers)i_MemberIndex;
                switch (memberToCheck)
                {
                    case eMotorcycleMembers.LicenseType:
                        {
                            _LicenseType = LicenseType.ParseFromString(i_InputToCheck);
                            break;
                        }
                    case eMotorcycleMembers.EngineVolume:
                        {
                            EngineVolume = int.Parse(i_InputToCheck);
                            break;
                        }
                }
            }
        }

        public override string ToString()
        {
            return string.Format(@"{0}
License type:                     {1}
Engine volume:                    {2}",
base.ToString(),
m_LicenseType.ToString(),
m_EngineVolume);
        }

        public enum eMotorcycleMembers
        {
            LicenseType = 6,
            EngineVolume,
        }
    }
}
