using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class GarageSystem
    {
        private Dictionary<int, Vehicle> m_VehicleInTheGarage;

        public GarageSystem()
        {
            m_VehicleInTheGarage = new Dictionary<int, Vehicle>();
        }

        public Dictionary<int, Vehicle> VehicleInTheGarage
        {
            get
            {
                return m_VehicleInTheGarage;
            }
        }

        internal static string EnumToString(Type i_EnumType)
        {
            string[] enumStrings = Enum.GetNames(i_EnumType);
            StringBuilder enumStr = new StringBuilder();

            for (int i = 0; i < enumStrings.Length; i++)
            {
                for (int j = 1; j < enumStrings[i].Length; j++)
                {
                    if (char.IsUpper(enumStrings[i][j]) == true)
                    {
                        enumStrings[i] = enumStrings[i].Insert(j, " ");
                        j++;
                    }
                }

                enumStr.Append(i + 1);
                enumStr.Append(". ");
                enumStr.Append(enumStrings[i]);
                if (i != enumStrings.Length - 1)
                {
                    enumStr.Append(Environment.NewLine);
                }
            }

            return enumStr.ToString();
        }

        internal static string EnumToStringWhithOutSpaces(Type i_EnumType)
        {
            string[] enumStrings = Enum.GetNames(i_EnumType);
            StringBuilder enumStr = new StringBuilder();

            for (int i = 0; i < enumStrings.Length; i++)
            {
                enumStr.Append(i + 1);
                enumStr.Append(". ");
                enumStr.Append(enumStrings[i]);
                if (i != enumStrings.Length - 1)
                {
                    enumStr.Append(Environment.NewLine);
                }
            }

            return enumStr.ToString();
        }

        public void AddVehicleToGarageSystem(Vehicle i_NewVehicleToAdd)
        {
            m_VehicleInTheGarage.Add(i_NewVehicleToAdd.GetHashCode(), i_NewVehicleToAdd);
        }

        public bool IsValidAndExistLicenseNumberStr(string i_LicenseNumStrToCheck)
        {
            bool isExistLicenseNumber = false;
            Vehicle.IsValidLicenseNumberStr(i_LicenseNumStrToCheck);

            if (m_VehicleInTheGarage.ContainsKey(i_LicenseNumStrToCheck.GetHashCode()) == true)
            {
                isExistLicenseNumber = true;
            }

            return isExistLicenseNumber;
        }

        public string GetVehicleInformation(string i_LicenseNumStrToCheck)
        {
            Vehicle vehicleToStr;

            m_VehicleInTheGarage.TryGetValue(i_LicenseNumStrToCheck.GetHashCode(), out vehicleToStr);
            StringBuilder vehicleInformation = new StringBuilder("Vehicle information: ");
            vehicleInformation.Append(vehicleToStr.ToString());

            return vehicleInformation.ToString();
        }

        public string UpdateVehicleStatus(string i_LicenseNumberToCheck, string i_VehicleStatusToUpdate)
        {
            string updateMessage;
            Vehicle vehicleToStr;

            m_VehicleInTheGarage.TryGetValue(i_LicenseNumberToCheck.GetHashCode(), out vehicleToStr);
            Vehicle.VehicleStatus.eVehicleStatus statusToUpdate = Vehicle.VehicleStatus.ParseFromString(i_VehicleStatusToUpdate);
            if (vehicleToStr._VehicleStatus == statusToUpdate)
            {
                updateMessage = string.Format("This status already exists for the vehicle with the license number: {0}", i_LicenseNumberToCheck);
            }
            else
            {
                vehicleToStr._VehicleStatus = statusToUpdate;
                updateMessage = string.Format("Updat status to vehicle with the license number: {0} Succeeded", i_LicenseNumberToCheck);
            }

            return updateMessage;
        }

        public string InflatingVehicleWheelsToMaximum(string i_LicenseNumberForInflating)
        {
            string inflatingMessage;
            Vehicle vehicleToInflating;

            m_VehicleInTheGarage.TryGetValue(i_LicenseNumberForInflating.GetHashCode(), out vehicleToInflating);
            if (vehicleToInflating.Wheels[0].CurrentAirPressure == vehicleToInflating.Wheels[0].MaxManufactureAirPressure)
            {
                inflatingMessage = string.Format("The wheels of the vehicle with the license number: {0} at maximum air pressure", i_LicenseNumberForInflating);
            }
            else
            {
                foreach (Wheel wheelToInflating in vehicleToInflating.Wheels)
                {
                    wheelToInflating.InflatingWheel(wheelToInflating.MaxManufactureAirPressure - wheelToInflating.CurrentAirPressure);
                }
                inflatingMessage = string.Format("Inflating the wheels of the vehicle with the license number: {0} succeeded", i_LicenseNumberForInflating);
            }

            return inflatingMessage;
        }

        public string LoadVehicleBattery(string i_LicenseNumberForLoad, string i_AmountOfMinutesToAdd)
        {
            string loadMessage;
            Vehicle vehicleToLoad;

            m_VehicleInTheGarage.TryGetValue(i_LicenseNumberForLoad.GetHashCode(), out vehicleToLoad);
            if (vehicleToLoad.EnergySource is Battery)
            {
                Battery curVehicleBattery = vehicleToLoad.EnergySource as Battery;
                curVehicleBattery.LoadBattery(float.Parse(i_AmountOfMinutesToAdd));
                loadMessage = string.Format("loading Battery of the vehicle with the license number: {0} succeeded.", i_LicenseNumberForLoad);
            }
            else
            {
                throw new ArgumentException(string.Format("The vehicle with the license number: {0} isn't electric.", i_LicenseNumberForLoad));
            }

            updatePrecentOfenergy(i_LicenseNumberForLoad, i_AmountOfMinutesToAdd);

            return loadMessage;
        }

        public void CheckingFuelTypeInput(string i_LicenseNumberToReful, string i_FuelType)
        {
            Vehicle vehicleToCheck;

            m_VehicleInTheGarage.TryGetValue(i_LicenseNumberToReful.GetHashCode(), out vehicleToCheck);
            if (vehicleToCheck.EnergySource is FuelTank)
            {
                FuelTank FuelTankTochek = vehicleToCheck.EnergySource as FuelTank;
                FuelType.eFuelType fuelTypeToFill = FuelType.ParseFromString(i_FuelType);
                if (FuelTankTochek._FuelType != fuelTypeToFill)
                {
                    throw new ArgumentException(string.Format("The vehicle with the license number {0} does not support {1} fuel type.", i_LicenseNumberToReful, fuelTypeToFill.ToString()));
                }
            }
        }
        
        public void CheckingVehicleFuelTankInput(string i_LicenseNumberToCheck)
        {
            Vehicle vehicleToCheck;

            m_VehicleInTheGarage.TryGetValue(i_LicenseNumberToCheck.GetHashCode(), out vehicleToCheck);
            if (vehicleToCheck.EnergySource is Battery)
            {
                throw new ArgumentException(string.Format("The vehicle with the license number: {0} is electric vehicle.", i_LicenseNumberToCheck));
            }
        }

        public string RefuelingAVehicle(string i_LicenseNumberToReful, string i_FuelType, string i_AmountOfLitersToAdd)
        {
            string loadMessage;
            Vehicle vehicleToLoad;

            m_VehicleInTheGarage.TryGetValue(i_LicenseNumberToReful.GetHashCode(), out vehicleToLoad);

            if (vehicleToLoad.EnergySource is FuelTank)
            {
                FuelTank currentVehicleFuelTank = vehicleToLoad.EnergySource as FuelTank;
                currentVehicleFuelTank.Refueling(float.Parse(i_AmountOfLitersToAdd), FuelType.ParseFromString(i_FuelType));///check EXP caching
                loadMessage = string.Format("The vehicle {0} refueling was successful.", i_LicenseNumberToReful);
            }
            else
            {
                throw new ArgumentException(string.Format("The vehicle with the license number: {0} is electric vehicle.", i_LicenseNumberToReful));
            }

            updatePrecentOfenergy(i_LicenseNumberToReful, i_AmountOfLitersToAdd);

            return loadMessage;
        }

        private void updatePrecentOfenergy(string i_LicenseNumberToUpdate, string i_AmounteOfEnergyAdded)
        {
            Vehicle vehicleToUpdate;
            float amountOfEnergy = float.Parse(i_AmounteOfEnergyAdded);

            m_VehicleInTheGarage.TryGetValue(i_LicenseNumberToUpdate.GetHashCode(), out vehicleToUpdate);
            if(vehicleToUpdate.EnergySource is Battery)
            {
                amountOfEnergy /= 60f;
            }
            float PercentageOfFuelAdded = (amountOfEnergy / vehicleToUpdate.EnergySource.MaxSourceAmount) * 100f;
            vehicleToUpdate.PrecentOfenergyRemaining += PercentageOfFuelAdded;
        }

        public string GetVehicleStatusToUpdateMessage()
        {
            return string.Format(@"Select the new status you want to update :
{0} ", EnumToString(typeof(Vehicle.VehicleStatus.eVehicleStatus)));
        }

        public string GetShowVehiclesByStatusMessage()
        {
            StringBuilder StrMassageToReturn = new StringBuilder("Press enter to show all vehicles or choose a status from the following options:");
            StrMassageToReturn.Append(Environment.NewLine);
            StrMassageToReturn.Append(EnumToString(typeof(Vehicle.VehicleStatus.eVehicleStatus)));

            return StrMassageToReturn.ToString();
        }

        public string GetAllVehiclesLicenseMessage()
        {
            int j = 1;
            StringBuilder strMassageToReturn = new StringBuilder("");

            foreach (KeyValuePair<int, Vehicle> CurrentVehicleToCheck in m_VehicleInTheGarage)
            {
                strMassageToReturn.Append(string.Format("{0}. ", j++));
                strMassageToReturn.AppendLine(CurrentVehicleToCheck.Value.LicennseNumber);
            }

            return strMassageToReturn.ToString();
        }

        private string getVehiclesLicenseMessageByStatus(Vehicle.VehicleStatus.eVehicleStatus i_VehicleStatusToShow)
        {
            int j = 1;
            StringBuilder strMassageToReturn = new StringBuilder("");

            foreach (KeyValuePair<int, Vehicle> currentVehicleToCheck in m_VehicleInTheGarage)
            {
                if (currentVehicleToCheck.Value._VehicleStatus == i_VehicleStatusToShow)
                {
                    strMassageToReturn.Append(string.Format("{0}. ", j++));
                    strMassageToReturn.AppendLine(currentVehicleToCheck.Value.LicennseNumber);
                }
            }

            return strMassageToReturn.ToString();
        }

        public string GetVehiclesLicenseMessageByChoice(string i_Choise)
        {
            StringBuilder strMassageToReturn = new StringBuilder();

            if (ValidationChecks.IsStrEmpty(i_Choise) == true)
            {
                strMassageToReturn.Append(GetAllVehiclesLicenseMessage());
                if (ValidationChecks.IsStrEmpty(strMassageToReturn.ToString()) == true)
                {
                    strMassageToReturn.AppendLine("There is no vehicles in the garage right now.");
                }
                else
                {
                    StringBuilder OpeningMassageToAdd = new StringBuilder(string.Format("The license numbers of the vehicles in the garage are:"));
                    OpeningMassageToAdd.Append(Environment.NewLine);
                    strMassageToReturn.Insert(0, OpeningMassageToAdd);
                }

            }
            else
            {
                strMassageToReturn.Append(getVehiclesLicenseMessageByStatus(Vehicle.VehicleStatus.ParseFromString(i_Choise)));
                string statusNameStr = Vehicle.VehicleStatus.GetStatusStr(Vehicle.VehicleStatus.ParseFromString(i_Choise));

                if (ValidationChecks.IsStrEmpty(strMassageToReturn.ToString()) == true)
                {
                    strMassageToReturn.AppendLine(String.Format("There is no vehicles in {0} status right now.", statusNameStr));
                }
                else
                {
                    StringBuilder openingMassageToAdd = new StringBuilder(string.Format("The vehicles in {0} status licenses are:", statusNameStr));
                    openingMassageToAdd.Append(Environment.NewLine);
                    strMassageToReturn.Insert(0, openingMassageToAdd);
                }
            }

            return strMassageToReturn.ToString();
        }

        public string GetAmountLitersToAddMessage()
        {
            StringBuilder strMassageToReturn = new StringBuilder("How many liters would you like to fuel?");

            return strMassageToReturn.ToString();
        }

        public string GetFuleTypeToFuelMessage()
        {
            StringBuilder strMassageToReturn = new StringBuilder("Choose a fuel type to fill :");
            strMassageToReturn.Append(Environment.NewLine);
            strMassageToReturn.Append(EnumToString(typeof(FuelType.eFuelType)));

            return strMassageToReturn.ToString();
        }

        public string GetLoadMessage()
        {
            return "How many minutes would you like to load?";
        }

        public static class SystemOptions
        {
            public static eSystemOptions ParseFromString(string i_UserChoice)
            {
                int numToCheck = int.Parse(i_UserChoice);
                int firstSystemOptions = (int)Enum.GetValues(typeof(eSystemOptions)).Cast<eSystemOptions>().First();
                int lastSystemOptions = (int)Enum.GetValues(typeof(eSystemOptions)).Cast<eSystemOptions>().Last();

                if (ValidationChecks.IsFloatNumberInRange(numToCheck, firstSystemOptions, lastSystemOptions) == false)
                {
                    throw new ValueOutOfRangeException(numToCheck, firstSystemOptions, lastSystemOptions);
                }

                return (eSystemOptions)numToCheck;
            }

            public static string GetSystemOptionsMessage()
            {
                return string.Format(@"Please select one of the following system options :
{0} ", EnumToString(typeof(eSystemOptions)));
            }

            public enum eSystemOptions
            {
                AddNewVehicle = 1,
                PresentTheLicenseNumbersOfAllVehiclesInTheGarage,
                UpdateVehicleStatus,
                InflatingVehicleWheelsToMax,
                RefuelingAVehicle,
                LoadAVehicleBattery,
                PresentVehicleInformation,
                Exit,
            }
        }
        
    }
}
