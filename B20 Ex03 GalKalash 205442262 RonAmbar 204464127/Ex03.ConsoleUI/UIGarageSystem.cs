using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using SystemOptions = Ex03.GarageLogic.GarageSystem.SystemOptions;
using SupportedVehicles = Ex03.GarageLogic.CreatingNewVehicle.SupportedVehicles;

namespace Ex03.ConsoleUI
{
    public class UIGarageSystem
    {
        private GarageSystem m_GarageSystem;

        private const int k_MinNameLen = 1;
        private const string k_InRepair = "1";

        public UIGarageSystem()
        {
            m_GarageSystem = new GarageSystem();
        }

        public void RunGarageSystem()
        {
            bool continueSystem = true;
            string systemOptionsMessage = SystemOptions.GetSystemOptionsMessage();

            while (continueSystem == true)
            {
                Console.WriteLine(systemOptionsMessage);
                SystemOptions.eSystemOptions systemOptionsFromUser = getSystemOptionsFromUser();

                switch (systemOptionsFromUser)
                {
                    case SystemOptions.eSystemOptions.AddNewVehicle:
                        {
                            addNewVehicle();
                            System.Threading.Thread.Sleep(2000);
                            break;
                        }
                    case SystemOptions.eSystemOptions.PresentTheLicenseNumbersOfAllVehiclesInTheGarage:
                        {
                            presenGarageLicenseNumbersByStatus();
                            break;
                        }
                    case SystemOptions.eSystemOptions.UpdateVehicleStatus:
                        {
                            updateVehicleStatus();
                            System.Threading.Thread.Sleep(2000);
                            break;
                        }
                    case SystemOptions.eSystemOptions.InflatingVehicleWheelsToMax:
                        {
                            inflatingVehicleWheelsToMax();
                            System.Threading.Thread.Sleep(2000);
                            break;
                        }
                    case SystemOptions.eSystemOptions.RefuelingAVehicle:
                        {
                            refuelingAVehicle();
                            System.Threading.Thread.Sleep(2000);
                            break;
                        }
                    case SystemOptions.eSystemOptions.LoadAVehicleBattery:
                        {
                            loadAVehicleBattery();
                            System.Threading.Thread.Sleep(2000);
                            break;
                        }
                    case SystemOptions.eSystemOptions.PresentVehicleInformation:
                        {
                            presentVehicleInformation();
                            break;
                        }
                    case SystemOptions.eSystemOptions.Exit:
                        {
                            continueSystem = false;
                            break;
                        }
                }

                Console.Clear();
            }
        }

        private void addNewVehicle()
        {
            string licennseNumber;
            bool isExistVehicle = getVehicleLicenseNumberStrFromUser(out licennseNumber);

            if (isExistVehicle == false)
            {
                SupportedVehicles.eSupportedVehicles userVehicleTypeChoice = getVehicleTypeFromUser();
                Vehicle newVehicleToAdd = CreatingNewVehicle.CreatingNewVehicleToGarage(licennseNumber, userVehicleTypeChoice);
                List<string> informatinMessage = newVehicleToAdd.InformationMessages;
                string inputToCheck;

                for (int i = 0; i < informatinMessage.Count; i++)
                {
                    Console.WriteLine(informatinMessage[i]);
                    bool isValidInput = false;
                    while (isValidInput == false)
                    {
                        inputToCheck = Console.ReadLine();
                        try
                        {
                            newVehicleToAdd.CheckAndUpdateInformation(inputToCheck, i);
                            isValidInput = true;
                        }
                        catch (ValueOutOfRangeException RangEX)
                        {
                            Console.Write(RangEX.Message);
                            Console.WriteLine(" please try again.");
                        }
                        catch (FormatException FormatEx)
                        {
                            Console.WriteLine("Invalid input format, please try again.");
                        }
                        catch (Exception InvalidEx)
                        {
                            Console.WriteLine("invalid input, please try again.");
                        }
                    }
                }

                m_GarageSystem.AddVehicleToGarageSystem(newVehicleToAdd);
                Console.WriteLine("Adding the vehicle was successful");
            }
            else
            {
                Console.WriteLine(string.Format("vehicle with the license number: {0} vehicle with the license number: {0} Already exists", licennseNumber));
                m_GarageSystem.UpdateVehicleStatus(licennseNumber, k_InRepair);
                Console.WriteLine(string.Format("Updat status to \"inRepair\", Succeeded"));
            }
        }

        private SupportedVehicles.eSupportedVehicles getVehicleTypeFromUser()
        {
            bool isValidInput = false;
            string userVehicleChoiceStr;
            Console.WriteLine(CreatingNewVehicle.GetVehicleTypeMessage());
            SupportedVehicles.eSupportedVehicles userVehicleChoice = new SupportedVehicles.eSupportedVehicles();
            while (isValidInput == false)
            {
                userVehicleChoiceStr = Console.ReadLine();
                try
                {
                    userVehicleChoice = SupportedVehicles.ParseFromString(userVehicleChoiceStr);
                    isValidInput = true;
                }
                catch (ValueOutOfRangeException RangEX)
                {
                    Console.Write(RangEX.Message);
                    Console.WriteLine(" please try again.");
                }
                catch (FormatException FormatEx)
                {
                    Console.WriteLine("Invalid input format, please try again.");
                }
                catch (Exception InvalidEX)
                {
                    Console.WriteLine("invalid input, please try again.");
                }
            }

            return userVehicleChoice;
        }

        private bool getVehicleLicenseNumberStrFromUser(out string o_LicenseNumberToReturn)
        {
            Console.WriteLine(CreatingNewVehicle.GetlicennseNumberMessage());
            string licenseNumberStrToCheck = null;
            bool isValidLicenseNumber = false;
            bool isExistLicenseNumber = false;

            while (isValidLicenseNumber == false)
            {
                licenseNumberStrToCheck = Console.ReadLine();
                try
                {
                    isExistLicenseNumber = m_GarageSystem.IsValidAndExistLicenseNumberStr(licenseNumberStrToCheck);
                    isValidLicenseNumber = true;
                }
                catch (ValueOutOfRangeException RangEX)
                {
                    Console.Write(RangEX.Message);
                    Console.WriteLine(" please try again.");
                }
                catch (FormatException FormatEx)
                {
                    Console.WriteLine("Invalid input format, please try again.");
                }
                catch (Exception InvalidEX)
                {
                    Console.WriteLine("invalid input, please try again.");
                }
            }

            o_LicenseNumberToReturn = licenseNumberStrToCheck;

            return isExistLicenseNumber;
        }

        private SystemOptions.eSystemOptions getSystemOptionsFromUser()
        {
            string inputStrFromUser;
            bool isValidInput = false;
            SystemOptions.eSystemOptions systemOptionsFromUser = new SystemOptions.eSystemOptions();

            while (isValidInput == false)
            {
                inputStrFromUser = Console.ReadLine();

                try
                {
                    systemOptionsFromUser = SystemOptions.ParseFromString(inputStrFromUser);
                    isValidInput = true;
                }
                catch (ValueOutOfRangeException RangEX)
                {
                    Console.Write(RangEX.Message);
                    Console.WriteLine(" please try again.");
                }
                catch (FormatException FormatEx)
                {
                    Console.WriteLine("Invalid input format, please try again.");
                }
                catch (Exception InvalidEX)
                {
                    Console.WriteLine("invalid input, please try again.");
                }
            }

            return systemOptionsFromUser;
        }

        private void presenGarageLicenseNumbersByStatus()
        {
            Console.WriteLine(m_GarageSystem.GetShowVehiclesByStatusMessage());
            string userChoiceStr = null;
            bool isValidChoice = false;

            while (isValidChoice == false)
            {
                userChoiceStr = Console.ReadLine();
                try
                {
                    userChoiceStr = m_GarageSystem.GetVehiclesLicenseMessageByChoice(userChoiceStr);
                    isValidChoice = true;
                }
                catch (ValueOutOfRangeException RangEX)
                {
                    Console.Write(RangEX.Message);
                    Console.WriteLine(" please try again.");
                }
                catch (FormatException FormatEx)
                {
                    Console.WriteLine("Invalid input format, please try again.");
                }
                catch (Exception InvalidEX)
                {
                    Console.WriteLine("invalid input, please try again.");
                }
            }

            Console.WriteLine(userChoiceStr);
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        private void updateVehicleStatus()
        {
            string vehicleStatusToUpdate;
            string updateMessage;
            string licenseNumberToCheck;
            bool isValidInput = false;
            bool isExistVehicle = getVehicleLicenseNumberStrFromUser(out licenseNumberToCheck);

            if (isExistVehicle == true)
            {
                while (isValidInput == false)
                {
                    Console.WriteLine(m_GarageSystem.GetVehicleStatusToUpdateMessage());
                    vehicleStatusToUpdate = Console.ReadLine();
                    try
                    {
                        updateMessage = m_GarageSystem.UpdateVehicleStatus(licenseNumberToCheck, vehicleStatusToUpdate);
                        Console.WriteLine(updateMessage);
                        isValidInput = true;
                    }
                    catch (ValueOutOfRangeException RangEx)
                    {
                        Console.Write(RangEx.Message);
                        Console.WriteLine("Please try again.");
                    }
                    catch (FormatException FormatEx)
                    {
                        Console.WriteLine("Invalid input format, please try again.");
                    }
                    catch (Exception InvalidEX)
                    {
                        Console.WriteLine("Invalid input, please try again.");
                    }
                }
            }
            else
            {
                Console.WriteLine(string.Format("The vehicle with the license number - {0} doesn't exist on the system", licenseNumberToCheck));
            }
        }

        private void presentVehicleInformation()
        {            
            string licenseNumberToCheck;
            bool isExistVehicle = getVehicleLicenseNumberStrFromUser(out licenseNumberToCheck);

            if (isExistVehicle == true)
            {
                Console.WriteLine(m_GarageSystem.GetVehicleInformation(licenseNumberToCheck));
            }
            else
            {
                Console.WriteLine(string.Format("The vehicle with the license number - {0} doesn't exist on the system", licenseNumberToCheck));
            }

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        private void inflatingVehicleWheelsToMax()
        {
            
            string licenseNumberForInflating;
            bool isExistVehicle = getVehicleLicenseNumberStrFromUser(out licenseNumberForInflating);

            if (isExistVehicle == true)
            {
                Console.WriteLine(m_GarageSystem.InflatingVehicleWheelsToMaximum(licenseNumberForInflating));
            }
            else
            {
                Console.WriteLine(string.Format("The vehicle with the license number - {0} doesn't exist on the system" , licenseNumberForInflating));
            }
        }

        private string getFuleTypeToFuelFromUser(string i_LicenseNumberToReful)
        {
            bool isFuleTypeCorrect = false;
            string fuleTypeToFuel = null;
            Console.WriteLine(m_GarageSystem.GetFuleTypeToFuelMessage());

            while (isFuleTypeCorrect == false)
            {
                fuleTypeToFuel = Console.ReadLine();
                try
                {
                    m_GarageSystem.CheckingFuelTypeInput(i_LicenseNumberToReful, fuleTypeToFuel);
                    isFuleTypeCorrect = true;
                }
                catch (ValueOutOfRangeException RangEx)
                {
                    Console.Write(RangEx.Message);
                    Console.WriteLine(" please try again");
                }
                catch (FormatException FormatEx)
                {
                    Console.WriteLine("Invalid input format, please try again");
                }
                catch (ArgumentException ArgumentEx)
                {
                    Console.Write(ArgumentEx.Message);
                    Console.WriteLine(" please try again");
                }
            }

            return fuleTypeToFuel;
        }

        private void refuelingAVehicle()
        {           
            string licenseNumberToReful;
            string litersToAdd;
            string fuleTypeToFuel;
            string refuelingMessage = null;
            bool isValidInput = false;
            bool isFuelVehicle = true;
            bool isExistVehicle = getVehicleLicenseNumberStrFromUser(out licenseNumberToReful);

            if (isExistVehicle == true)
            {
                try
                {
                    m_GarageSystem.CheckingVehicleFuelTankInput(licenseNumberToReful);
                    isFuelVehicle = true;
                }
                catch (ArgumentException ArgumentEx)
                {
                    Console.WriteLine(ArgumentEx.Message);
                    isFuelVehicle = false;
                }

                if (isFuelVehicle == true)
                {
                    fuleTypeToFuel = getFuleTypeToFuelFromUser(licenseNumberToReful);
                    Console.WriteLine(m_GarageSystem.GetAmountLitersToAddMessage());
                    while (isValidInput == false)
                    {
                        try
                        {
                            litersToAdd = Console.ReadLine();
                            refuelingMessage = m_GarageSystem.RefuelingAVehicle(licenseNumberToReful, fuleTypeToFuel, litersToAdd);
                            isValidInput = true;
                        }
                        catch (FormatException FormatEx)
                        {
                            Console.WriteLine("Invalid input format, please try again.");
                        }
                        catch (ValueOutOfRangeException RangeEx)
                        {
                            Console.Write(RangeEx.Message);
                            Console.WriteLine("please try again.");
                        }
                        catch (Exception InvalidEX)
                        {
                           Console.WriteLine("invalid input.");
                        }
                    }

                    Console.WriteLine(refuelingMessage);
                }

            }
            else
            {
                Console.WriteLine(string.Format("The vehicle with the license number - {0} doesn't exist on the system", licenseNumberToReful));
            }
        }

        private void loadAVehicleBattery()
        {
            string licenseNumberForLoding;
            string loadMessage;
            string minutesToLoad;
            bool isValidInput = false;
            bool isExistVehicle = getVehicleLicenseNumberStrFromUser(out licenseNumberForLoding);

            if (isExistVehicle == true)
            {
                Console.WriteLine(m_GarageSystem.GetLoadMessage());
                while (isValidInput == false)
                {
                    minutesToLoad = Console.ReadLine();
                    try
                    {
                        loadMessage = m_GarageSystem.LoadVehicleBattery(licenseNumberForLoding, minutesToLoad);
                        Console.WriteLine(loadMessage);
                        isValidInput = true;
                    }
                    catch (FormatException FormatEx)
                    {
                        Console.WriteLine("Invalid input format, please try again.");
                    }
                    catch (ArgumentException ArgumentEx)
                    {
                        Console.WriteLine(ArgumentEx.Message);
                        break;
                    }
                    catch(ValueOutOfRangeException RangeEx)
                    {
                        Console.Write(RangeEx.Message);
                        Console.WriteLine(", please try again.");
                    }
                    catch (Exception InvalidEX)
                    {
                        Console.WriteLine("invalid input, please try again.");
                    }
                }
            }
            else
            {
                Console.WriteLine(string.Format("The vehicle with the license number - {0} doesn't exist on the system", licenseNumberForLoding));
            }
        }
    }
}
