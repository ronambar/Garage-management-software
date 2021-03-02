using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class CreatingNewVehicle
    {
        public static string GetVehicleTypeMessage()
        {
            return string.Format(@"Please select one of the following vehicle types :
{0} ", SupportedVehicles.SupportedVehiclesToString());
        }

        public static string GetlicennseNumberMessage()
        {
            return "Please enter licennse number: ";
        }

        public static Vehicle CreatingNewVehicleToGarage(string i_LicennseNumber, SupportedVehicles.eSupportedVehicles i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case SupportedVehicles.eSupportedVehicles.FuelMotorcycle:
                    {
                        FuelTank FuelTank = new FuelTank();
                        newVehicle = new Motorcycle(i_LicennseNumber, FuelTank);
                        break;
                    }
                case SupportedVehicles.eSupportedVehicles.ElectricMotorcycle:
                    {
                        Battery battery = new Battery();
                        newVehicle = new Motorcycle(i_LicennseNumber, battery);
                        break;
                    }
                case SupportedVehicles.eSupportedVehicles.FuelCar:
                    {
                        FuelTank fuelTank = new FuelTank();
                        newVehicle = new Car(i_LicennseNumber, fuelTank);
                        break;
                    }
                case SupportedVehicles.eSupportedVehicles.ElectricCar:
                    {
                        Battery battery = new Battery();
                        newVehicle = new Car(i_LicennseNumber, battery);
                        break;
                    }
                case SupportedVehicles.eSupportedVehicles.FuelTruck:
                    {
                        FuelTank fuelTank = new FuelTank();
                        newVehicle = new Truck(i_LicennseNumber, fuelTank);
                        break;
                    }
            }

            SupportedVehicles.UpdateSupportedValues(newVehicle);

            return newVehicle;
        }

        public class SupportedVehicles
        {
            public static void UpdateSupportedValues(Vehicle i_NewVehicle)
            {
                if (i_NewVehicle is Motorcycle)
                {
                    Motorcycle newMotorcycleToUpdat = i_NewVehicle as Motorcycle;
                    UpdateSupportedMotorcycleValues(newMotorcycleToUpdat);
                }
                else if (i_NewVehicle is Car)
                {
                    Car newCarToUpdat = i_NewVehicle as Car;
                    UpdateSupportedCarValues(newCarToUpdat);
                }
                else if (i_NewVehicle is Truck)
                {
                    Truck newTruckToUpdat = i_NewVehicle as Truck;
                    UpdateSupportedTruckValues(newTruckToUpdat);
                }
            }

            public static void UpdateSupportedMotorcycleValues(Motorcycle i_NewVehicle)
            {
                i_NewVehicle.Wheels = new List<Wheel>();

                for (int i = 0; i < 2; i++)
                {
                    i_NewVehicle.Wheels.Add(new Wheel());
                    i_NewVehicle.Wheels[i].MaxManufactureAirPressure = 30;
                }

                if (i_NewVehicle.EnergySource is FuelTank)
                {
                    FuelTank fuleMotorcycleToUpdat = i_NewVehicle.EnergySource as FuelTank;
                    fuleMotorcycleToUpdat._FuelType = FuelType.eFuelType.Octan95;
                    fuleMotorcycleToUpdat.MaxSourceAmount = 7;
                }
                else
                {
                    Battery fuleMotorcycleToUpdat = i_NewVehicle.EnergySource as Battery;
                    fuleMotorcycleToUpdat.MaxSourceAmount = float.Parse("1.2");
                }
            }

            public static void UpdateSupportedCarValues(Car i_NewVehicle)
            {
                i_NewVehicle.Wheels = new List<Wheel>();

                for (int i = 0; i < 4; i++)
                {
                    i_NewVehicle.Wheels.Add(new Wheel());
                    i_NewVehicle.Wheels[i].MaxManufactureAirPressure = 30;
                }

                if (i_NewVehicle.EnergySource is FuelTank)
                {
                    FuelTank fuleMotorcycleToUpdat = i_NewVehicle.EnergySource as FuelTank;
                    fuleMotorcycleToUpdat._FuelType = FuelType.eFuelType.Octan96;
                    fuleMotorcycleToUpdat.MaxSourceAmount = 60;
                }
                else
                {
                    Battery fuleMotorcycleToUpdat = i_NewVehicle.EnergySource as Battery;
                    fuleMotorcycleToUpdat.MaxSourceAmount = float.Parse("2.1");
                }
            }

            public static void UpdateSupportedTruckValues(Truck i_NewVehicle)
            {
                i_NewVehicle.Wheels = new List<Wheel>();

                for (int i = 0; i < 16; i++)
                {
                    i_NewVehicle.Wheels.Add(new Wheel());
                    i_NewVehicle.Wheels[i].MaxManufactureAirPressure = 28;
                }

                FuelTank fuleMotorcycleToUpdat = i_NewVehicle.EnergySource as FuelTank;
                fuleMotorcycleToUpdat._FuelType = FuelType.eFuelType.Soler;
                fuleMotorcycleToUpdat.MaxSourceAmount = 120;
            }

            public static eSupportedVehicles ParseFromString(string i_NumToParse)
            {
                int numToCheck = int.Parse(i_NumToParse);
                int firstSupportedVehicle = (int)Enum.GetValues(typeof(eSupportedVehicles)).Cast<eSupportedVehicles>().First();
                int lastSupportedVehicle = (int)Enum.GetValues(typeof(eSupportedVehicles)).Cast<eSupportedVehicles>().Last();

                if (ValidationChecks.IsFloatNumberInRange(numToCheck, firstSupportedVehicle, lastSupportedVehicle) == false)
                {
                    throw new ValueOutOfRangeException(numToCheck, firstSupportedVehicle, lastSupportedVehicle);
                }

                return (eSupportedVehicles)numToCheck;
            }

            public static string SupportedVehiclesToString()
            {
                return string.Format(@"
1. Fuel motorcycle-      2 wheels with maximum air pressure 30, Octan95 fuel, 7 liter fuel tank. 
2. Electric motorcycle - 2 wheels with maximum air pressure 30, maximum battery time - 2.1 hours.
3. Fuel car -            4 wheels with maximum air pressure 32, Octan96 fuel, 60 liter fuel tank.
4. Electric car -        4 wheels with maximum air pressure 32, maximum battery time - 1.2 hours.
5. Fuel truck-           6 wheels with maximum air pressure 28, Soler fuel, 120 liter fuel tank"
);
            }

            public enum eSupportedVehicles
            {
                FuelMotorcycle = 1,
                ElectricMotorcycle,
                FuelCar,
                ElectricCar,
                FuelTruck,
            }
        }
    }
}
