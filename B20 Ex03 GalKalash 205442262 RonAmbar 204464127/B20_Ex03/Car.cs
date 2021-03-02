using System.Collections.Generic;
using System;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private CarColor.eCarColor m_CarColor;
        private NumOfCarDoors.eNumOfCarDoors m_NumOfCarDoors;

        public Car(string i_LicennseNumber, EnergySource i_EnergySource) : base(i_LicennseNumber, i_EnergySource)
        {
            InformationMessages = GetInformationMessages();
        }

        public CarColor.eCarColor _CarColor
        {
            get
            {
                return m_CarColor;
            }
            set
            {
                m_CarColor = value;
            }
        }

        public NumOfCarDoors.eNumOfCarDoors _NumOfCarDoors
        {
            get
            {
                return m_NumOfCarDoors;
            }
            set
            {
                m_NumOfCarDoors = value;
            }
        }

        public override List<string> GetInformationMessages()
        {
            List<string> informationMessages = new List<string>();
            string carColor = GarageSystem.EnumToString(typeof(CarColor.eCarColor));

            informationMessages.AddRange(base.GetInformationMessages());
            informationMessages.Add(string.Format(@"Please select one of the following car colors: 
{0}", carColor));
            informationMessages.Add("Please select the number of the car doors: 2,3,4 or 5 ");

            return informationMessages;
        }

        public override string ToString()
        {
            return string.Format(@" 
{0}
Car color:                        {1}
Num of car doors:                 {2}",
base.ToString(),
m_CarColor.ToString(),
m_NumOfCarDoors.ToString());
        }

        public override void CheckAndUpdateInformation(string i_InputToCheck, int i_MemberIndex)
        {
            int firstCarMember = (int)Enum.GetValues(typeof(eCarMembers)).Cast<eCarMembers>().First();

            if (i_MemberIndex < firstCarMember)
            {
                CheckAndUpdateVehicleInformation(i_InputToCheck, i_MemberIndex);
            }
            else
            {
                eCarMembers memberToCheck = (eCarMembers)i_MemberIndex;
                switch(memberToCheck)
                {
                    case eCarMembers.CarColor:
                        {
                            m_CarColor = CarColor.ParseFromString(i_InputToCheck);
                            break;
                        }
                    case eCarMembers.NumOfCarDoors:
                        {
                            m_NumOfCarDoors = NumOfCarDoors.ParseFromString(i_InputToCheck);
                            break;
                        }
                }
            }
        }

        private enum eCarMembers
        {
            CarColor = 6,
            NumOfCarDoors,
        }

        public class CarColor
        {
            public static eCarColor ParseFromString(string i_NumToParse)
            {
                int numToCheck = int.Parse(i_NumToParse);
                int firstCarColor = (int)Enum.GetValues(typeof(eCarColor)).Cast<eCarColor>().First();
                int lastCarColor = (int)Enum.GetValues(typeof(eCarColor)).Cast<eCarColor>().Last();

                if (ValidationChecks.IsFloatNumberInRange(numToCheck, firstCarColor, lastCarColor) == false)
                {
                    throw new ValueOutOfRangeException(numToCheck, firstCarColor, lastCarColor);
                }

                return (eCarColor)numToCheck;
            }

            public enum eCarColor
            {
                Red = 1,
                White,
                Black,
                Silver,
            }
        }

        public class NumOfCarDoors
        {
            public static eNumOfCarDoors ParseFromString(string i_NumToParse)
            {
                int numToCheck = int.Parse(i_NumToParse);
                int firstNumOfCarDoors = (int)Enum.GetValues(typeof(eNumOfCarDoors)).Cast<eNumOfCarDoors>().First();
                int carDoorsMaxSize = (int)Enum.GetValues(typeof(eNumOfCarDoors)).Cast<eNumOfCarDoors>().Last();

                if (ValidationChecks.IsFloatNumberInRange(numToCheck, firstNumOfCarDoors, carDoorsMaxSize) == false)
                {
                    throw new ValueOutOfRangeException(numToCheck, firstNumOfCarDoors, carDoorsMaxSize);
                }

                return (eNumOfCarDoors)numToCheck;
            }

            public enum eNumOfCarDoors
            {
                Two = 2,
                Three,
                Four,
                Five,
            }
        }
    }
}
