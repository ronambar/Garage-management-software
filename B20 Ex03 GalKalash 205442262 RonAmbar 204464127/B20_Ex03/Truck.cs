using System.Collections.Generic;
using System;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsTransportsDangerousMaterials;
        private float m_CargoVolume;

        public const int k_MinCargoVolume = 0;
        public const float k_MaxCargoVolume = float.MaxValue;

        public Truck(string i_LicennseNumber, EnergySource i_EnergySource) : base(i_LicennseNumber, i_EnergySource)
        {
            InformationMessages = GetInformationMessages();
        }

        public override string ToString()
        {
            return string.Format(@"
{0}
Is Transports Dangerous Materials:{1}
Cargo volume:                     {2}",
base.ToString(),
m_IsTransportsDangerousMaterials.ToString(),
m_CargoVolume);
        }

        private bool isTransportsDangerousMaterials
        {
            get
            {
                return m_IsTransportsDangerousMaterials;
            }
            set
            {
                m_IsTransportsDangerousMaterials = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                if (ValidationChecks.IsFloatNumberInRange(value, k_MinCargoVolume, k_MaxCargoVolume) == false)
                {
                    throw new ValueOutOfRangeException(value, k_MinCargoVolume);
                }

                m_CargoVolume = value;
            }
        }

        public override List<string> GetInformationMessages()
        {
            List<string> informationMessages = new List<string>();

            informationMessages.AddRange(base.GetInformationMessages());
            informationMessages.Add("If the materials for transport are dangerous press 1, otherwise press 0.");
            informationMessages.Add("please enter Cargo Volume: ");

            return informationMessages;
        }

        public override void CheckAndUpdateInformation(string i_InputToCheck, int i_MemberIndex)
        {
            int firstTruckMember = (int)Enum.GetValues(typeof(eTruckMembers)).Cast<eTruckMembers>().First();

            if (i_MemberIndex < firstTruckMember)
            {
                CheckAndUpdateVehicleInformation(i_InputToCheck, i_MemberIndex);
            }
            else
            {
                eTruckMembers memberToCheck = (eTruckMembers)i_MemberIndex;

                switch (memberToCheck)
                {
                    case eTruckMembers.IsTransportsDangerousMaterials:
                        {
                            int inputToCheck = int.Parse(i_InputToCheck);

                            if (inputToCheck == 1)
                            {
                                isTransportsDangerousMaterials = true;
                            }
                            else if (inputToCheck == 0)
                            {
                                isTransportsDangerousMaterials = false;
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(inputToCheck, 0, 1);
                            }

                            break;
                        }
                    case eTruckMembers.CargoVolume:
                        {
                            CargoVolume = float.Parse(i_InputToCheck);
                            break;
                        }
                }
            }
        }

        public enum eTruckMembers
        {
            IsTransportsDangerousMaterials = 6,
            CargoVolume,
        }
    }
}
