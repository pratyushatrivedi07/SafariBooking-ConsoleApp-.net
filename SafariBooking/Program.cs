using Jungle.BL;
using Jungle.Entities;
using Jungle.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextToAsciiArt;
using System.Drawing;
using Console = Colorful.Console;
using Xceed.Wpf.Toolkit;

namespace SafariBooking
{
    class Program
    {
        [STAThread]

        static void Main(string[] args)
        {
            
            do
            {
                Console.Clear();
                Initialize();
                Console.ForegroundColor = Color.DarkOrange;
                Console.WriteLine("\n*********** Do you want to Register or Login? ***********");
                Console.ResetColor();
                Console.ForegroundColor = Color.White;
                Console.WriteLine("1.Register\n2.Login\n3.Exit");
                Console.ResetColor();
                System.Console.Write("\nEnter Choice:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice <= 3 && choice != 0)
                {
                    switch (choice)
                    {
                        case 1:
                            Register();
                            break;

                        case 2:
                            Login();
                            break;

                        case 3:
                            Environment.Exit(0);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Choice entered");
                    Console.ResetColor();
                }
                Console.ReadLine();
            } while (true);
        }

        private static void Login()
        {
            User users = new User();
            bool isPass = false;
            do
            {
                Console.Clear();
                Initialize();
                Console.ForegroundColor = Color.Lime;
                System.Console.WriteLine("\n--------Login Page--------");
                Console.ResetColor();
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Username: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                users.Username = input;
                    Console.ForegroundColor = Color.White;
                    Console.WriteLine("\nEnter Password: ");
                    Console.ResetColor();
                    //input = Console.ReadLine();
                    input = ReadPassword();
                    if (!string.IsNullOrEmpty(input) && input.Length >= 8)
                    {
                        users.Password = input;
                        var result = JungleBL.Login(users);
                        try
                        {
                            if (result.IsSuccess)
                            {
                                if (result.Item2 == "Admin")
                                {
                                    AdminMenu();
                                    isPass = true;
                                    
                                }
                                else if (result.Item2 == "Tourist")
                                {
                                    TouristMenu();
                                    isPass = true;
                                    
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = Color.Red;
                                Console.WriteLine("Login Failed");
                                Console.ResetColor();
                                isPass = true;
                            }
                        }
                        catch (JungleException ex)
                        {

                            Console.ForegroundColor = Color.Yellow;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            Console.ForegroundColor = Color.White;
                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                            Console.ResetColor();
                            input = Console.ReadLine();
                            if (input == "n" || input == "N")
                            {

                                isPass = true;

                            }

                        }
                    }
                    else 
                    {
                        Console.ForegroundColor = Color.Red;
                        Console.WriteLine("\nInvalid Password\nPassword should be of minimum 8 characters.");
                        Console.ResetColor();
                        Console.ForegroundColor = Color.White;
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {

                            isPass = true;

                        }
                        
                    }   
                }
                else 
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nUsername cannot be empty");
                    Console.ResetColor();
                }
                Console.ReadLine();
            } while (!isPass) ;
        }
        private static void Register()
        {
            //Console.Clear();
            User users = new User();
            bool name = false;
            do
            {
                Console.Clear();
                Initialize();
                Console.ForegroundColor = Color.Lime;
            System.Console.WriteLine("\n--------Register Page--------");
            Console.ResetColor();
           
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter First Name: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    Regex regex = new Regex(@"^[A-Z][a-zA-Z]*$");
                    if (regex.IsMatch(input))
                    {
                        users.Name = input;
                        bool user = false;
                        do
                        {
                            Console.ForegroundColor = Color.White;
                            Console.WriteLine("\nEnter Username: ");
                            Console.ResetColor();
                            input = Console.ReadLine();

                            if (!string.IsNullOrEmpty(input))
                            {
                                users.Username = input;
                                bool pass = false;
                                do
                                {

                                    Console.WriteLine("\n*** Password cannot be empty and should be of minimum 8 characters ***");
                                    Console.ForegroundColor = Color.White;
                                    Console.WriteLine("\n\nEnter Password: ");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(input) && input.Length >= 8)
                                    {
                                        users.Password = input;
                                        ListRoles();
                                        bool id = false;
                                        do
                                        {
                                            Console.ForegroundColor = Color.White;
                                            Console.WriteLine("\nEnter Role Id: ");
                                            Console.ResetColor();
                                            input = Console.ReadLine();
                                            if (int.TryParse(input, out int rId) && rId <=2 && rId!=0 )
                                            {
                                                users.RoleId = rId;

                                                try
                                                {
                                                    bool isRegistered = JungleBL.Register(users);
                                                    if (isRegistered)
                                                    {
                                                        Console.ForegroundColor = Color.Green;
                                                        Console.WriteLine("\nRegisteration Successful");
                                                        Console.ResetColor();
                                                        name = true;
                                                        user = true;
                                                        pass = true;
                                                        id = true;
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = Color.Red;
                                                        Console.WriteLine("\nRegisteration Failed");
                                                        Console.ResetColor();
                                                        name = true;
                                                        user = true;
                                                        pass = true;
                                                        id = true;
                                                    }
                                                }
                                                catch (JungleException ex)
                                                {
                                                    Console.ForegroundColor = Color.Yellow;
                                                    Console.WriteLine(ex.Message);
                                                    Console.ResetColor();
                                                    name = true;
                                                    user = true;
                                                    pass = true;
                                                    id = true;
                                                }
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = Color.Red;
                                                Console.WriteLine("\nInvalid Role selected");
                                                Console.ResetColor();
                                                Console.ForegroundColor = Color.White;
                                                System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                Console.ResetColor();
                                                input = Console.ReadLine();
                                                if (input == "n" || input == "N")
                                                {
                                                    id = true;
                                                    name = true;
                                                    pass = true;
                                                    user = true;
                                                }
                                            }
                                        } while (!id);
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = Color.Red;
                                        Console.WriteLine("\nPassword cannot be empty\nPassword should be of minimum 8 characters");
                                        Console.ResetColor();
                                        Console.ForegroundColor = Color.White;
                                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                        Console.ResetColor();
                                        input = Console.ReadLine();
                                        if (input == "n" || input == "N")
                                        {
                                            name = true;
                                            pass = true;
                                            user = true;
                                        }

                                    }
                                } while (!pass);
                            }
                            else
                            {
                                Console.ForegroundColor = Color.Red;
                                Console.WriteLine("\nUsername cannot be empty");
                                Console.ResetColor();
                                Console.ForegroundColor = Color.White;
                                System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                Console.ResetColor();
                                input = Console.ReadLine();
                                if (input == "n" || input == "N")
                                {
                                    
                                    user = true;
                                   
                                }
                            }
                        } while (!user);
                        
                    }
                    else
                    {
                        Console.ForegroundColor = Color.Red;
                        System.Console.WriteLine("\nInvalid Name");
                        Console.ResetColor();
                        Console.ForegroundColor = Color.White;
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {
                            name = true;
                           
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nName cannot be Empty");
                    Console.ResetColor();
                }
                Console.ReadLine();
            } while (!name);
            
        }


        //---------------------------ALL MENUS----------------------------------------------
        private static void AdminMenu()
        {
            //Console.Clear();
            bool logout = false;
            do
            {
                Console.Clear();
                Console.ForegroundColor = Color.Lime;
                Console.WriteLine("\n------Admin Menu------");
                Console.ResetColor();
                Console.ForegroundColor = Color.White;
                Console.WriteLine("1.Parks\n2.Gates\n3.Vehicles\n4.Safaris\n5.Logout\n");
                Console.ResetColor();
                System.Console.Write("\nEnter Choice:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice <= 5 && choice != 0)
                {
                    switch (choice)
                    {
                        case 1:
                            ParkMenu();
                            break;

                        case 2:
                            GateMenu();
                            break;

                        case 3:
                            VehicleMenu();
                            break;

                        case 4:
                            SafariMenu();
                            break;

                        case 5:
                            logout = true;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Choice");
                    Console.ResetColor();
                }
                Console.ReadLine();
            } while (!logout);
        }
        private static void VehicleMenu()
        {
            
            bool logout = false;
            do
            {
                Console.Clear();
                Console.ForegroundColor = Color.BlueViolet;
                Console.WriteLine("\n-----------------VEHICLE MENU-----------------");
                Console.ResetColor();
                Console.ForegroundColor = Color.White;
                Console.WriteLine("1.Add Vehicle\n2.Update Vehicle\n3.Delete Vehicle\n4.List Vehicles\n5.Back to Admin Menu\n");
                Console.ResetColor();
                System.Console.Write("\nEnter Choice:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice <= 5 && choice != 0)
                {
                    switch (choice)
                    {
                        case 1:
                            AddVehicle();
                            break;

                        case 2:
                            UpdateVehicle();
                            break;

                        case 3:
                            DeleteVehicle();
                            break;

                        case 4:
                            ListVehicles();
                            break;

                        case 5:
                            logout = true;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Choice");
                    Console.ResetColor();
                }
                Console.ReadLine();
            } while (!logout);
        }
        private static void SafariMenu()
        {
            //Console.Clear();
            bool logout = false;
            do
            {
                Console.Clear();
                Console.ForegroundColor = Color.BlueViolet;
                Console.WriteLine("\n-----------------SAFARI MENU-----------------");
                Console.ResetColor();
                Console.ForegroundColor = Color.White;
                Console.WriteLine("1.Add Safari\n2.Update Safari\n3.Delete Safari\n4.List Safaris\n5.Back to Admin Menu\n");
                Console.ResetColor();
                Console.Write("\nEnter Choice:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice <= 5 && choice != 0)
                {
                    switch (choice)
                    {
                        case 1:
                            AddSafari();
                            break;

                        case 2:
                            UpdateSafari();
                            break;

                        case 3:
                            DeleteSafari();
                            break;

                        case 4:
                            ListSafari();
                            break;

                        case 5:
                            logout = true;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Choice");
                    Console.ResetColor();
                }
                Console.ReadLine();
            }
            
            while (!logout);
        }
        private static void GateMenu()
        {
            //Console.Clear();
            bool logout = false;
            do
            {
                Console.Clear();
                Console.ForegroundColor = Color.BlueViolet;
                Console.WriteLine("\n-----------------GATE MENU-----------------");
                Console.ResetColor();
                Console.ForegroundColor = Color.White;
                Console.WriteLine("1.Add Gate\n2.Update Gate\n3.Delete Gate\n4.List Gates\n5.Back to Admin Menu\n");
                Console.ResetColor();
                Console.Write("\nEnter Choice: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice <= 5 && choice != 0)
                {
                    switch (choice)
                    {
                        case 1:
                            AddGate();
                            break;

                        case 2:
                            UpdateGate();
                            break;

                        case 3:
                            DeleteGate();
                            break;

                        case 4:
                            ListGate();
                            break;

                        case 5:
                            logout = true;
                            break;

                        default:
                            break;
                    }
                }
                else 
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Choice");
                    Console.ResetColor();
                }
                Console.ReadLine();
            }
            while (!logout);
        }
        private static void ParkMenu()
        {
            //Console.Clear();
            bool logout = false;
            do
            {
                Console.Clear();
                Console.ForegroundColor = Color.BlueViolet;
                Console.WriteLine("\n-----------------PARK MENU-----------------");
                Console.ResetColor();
                Console.ForegroundColor = Color.White;
                Console.WriteLine("1.Add Park\n2.Update Park\n3.Delete Park\n4.List Parks\n5.Back to Admin Menu\n");
                Console.ResetColor();
                Console.Write("\nEnter Choice: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice <= 5 && choice != 0)
                {
                    switch (choice)
                    {
                        case 1:
                            AddPark();
                            break;

                        case 2:
                            UpdatePark();
                            break;

                        case 3:
                            DeletePark();
                            break;

                        case 4:
                            ListPark();
                            break;

                        case 5:
                            logout = true;
                            break;

                        default:
                            break;
                    }
                }
                else 
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Choice");
                    Console.ResetColor();
                }
                Console.ReadLine();
            }
            while (!logout);
        }
        private static void TouristMenu()
        {
            Console.Clear();
            bool logout = false;
            do
            {
                Console.Clear();
                Console.ForegroundColor = Color.Lime;
                Console.WriteLine("\n------Tourist Menu------");
                Console.ResetColor();
                Console.ForegroundColor = Color.White;
                Console.WriteLine("1.Search Parks by Location\n2.View Safari Details by Park\n3.Book a Safari\n4.View Booking\n5.Logout\n");
                Console.ResetColor();
                Console.Write("\nEnter Choice: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice <= 5 && choice != 0)
                {
                    switch (choice)
                    {
                        case 1:
                            ParksByLocation();
                            break;

                        case 2:
                            SafariByPark();
                            break;

                        case 3:
                            BookSafari();
                            break;

                        case 4:
                            ViewBooking();
                            break;

                        case 5:
                            logout = true;
                            break;

                        default:
                            break;
                    }
                }
                else 
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Choice");
                    Console.ResetColor();
                }
                Console.ReadLine();
            } while (!logout);
        }


        //---------------------------ALL ADD METHODS----------------------------------------------

        private static void AddVehicle()
        {
            Console.Clear();
            Parks park = new Parks();
            Vehicle v = new Vehicle();
            bool typev = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Vehicle Type (ParK): ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (Enum.TryParse(input, out vType type) && input == "Park")
                {
                    v.VehicleType = type;
                    bool namev = false;
                    do
                    {
                        Console.ForegroundColor = Color.White;
                        Console.WriteLine("\nEnter Vehicle Name: ");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (!string.IsNullOrEmpty(input))
                        {
                            v.Name = input;
                            bool capy = false;
                            do
                            {
                                Console.ForegroundColor = Color.White;
                                Console.WriteLine("\nEnter the Capacity of Vehicle (Seat6 | Seat4): ");
                                Console.ResetColor();
                                input = Console.ReadLine();
                                if (Enum.TryParse(input, out Capacity cap))
                                {
                                    v.capacity = cap;
                                    bool c = false;
                                    do
                                    {
                                        Console.ForegroundColor = Color.White;
                                        Console.WriteLine("\nEnter the Entry Cost: ");
                                        Console.ResetColor();
                                        input = Console.ReadLine();
                                        if (double.TryParse(input, out double cost))
                                        {
                                            v.Cost = cost;
                                            ListPark();
                                            bool p = false;
                                            do
                                            {
                                                Console.ForegroundColor = Color.White;
                                                Console.WriteLine("\nEnter the Park Id where Vehicle belongs: ");
                                                Console.ResetColor();
                                                input = Console.ReadLine();
                                                if (int.TryParse(input, out int pId))
                                                {
                                                    v.ParkId = pId;
                                                    park.ParkId = pId;
                                                    try
                                                    {
                                                        bool isExisting = JungleBL.IsParkExists(park);
                                                        if (isExisting)
                                                        {
                                                            try
                                                            {
                                                                bool isAdded = JungleBL.AddVehicle(v);
                                                                if (isAdded)
                                                                {
                                                                    Console.ForegroundColor = Color.Green;
                                                                    Console.WriteLine("\nVehicle Added");
                                                                    Console.ResetColor();
                                                                    p = true;
                                                                    c = true;
                                                                    capy = true;
                                                                    namev = true;
                                                                    typev = true;
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = Color.Red;
                                                                    Console.WriteLine("\nFailed to Add");
                                                                    Console.ResetColor();
                                                                    p = true;
                                                                    c = true;
                                                                    capy = true;
                                                                    namev = true;
                                                                    typev = true;
                                                                }
                                                            }
                                                            catch (JungleException ex)
                                                            {
                                                                Console.ForegroundColor = Color.Yellow;
                                                                Console.WriteLine(ex.Message);
                                                                Console.ResetColor();
                                                                Console.ForegroundColor = Color.White;
                                                                System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                                                Console.ResetColor();
                                                                input = Console.ReadLine();
                                                                if (input == "n" || input == "N")
                                                                {
                                                                    p = true;
                                                                    namev = true;
                                                                    c = true;
                                                                    capy = true;
                                                                    typev = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch(JungleException ex)
                                                    {
                                                        Console.ForegroundColor = Color.Yellow;
                                                        Console.WriteLine(ex.Message);
                                                        Console.ResetColor();
                                                        Console.ForegroundColor = Color.White;
                                                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                                        Console.ResetColor();
                                                        input = Console.ReadLine();
                                                        if (input == "n" || input == "N")
                                                        {
                                                            p = true;
                                                            namev = true;
                                                            c = true;
                                                            capy = true;
                                                            typev = true;
                                                        }
                                                    }
                                                }
                                                else 
                                                { 
                                                    Console.ForegroundColor = Color.Red; 
                                                    Console.WriteLine("\nInvalid Park Id"); 
                                                    Console.ResetColor();
                                                    Console.ForegroundColor = Color.White;
                                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                                    Console.ResetColor();
                                                    input = Console.ReadLine();
                                                    if (input == "n" || input == "N")
                                                    {
                                                        p = true;
                                                    }
                                                }
                                            } while (!p);
                                        }
                                        else 
                                        {
                                            Console.ForegroundColor = Color.Red; 
                                            Console.WriteLine("\nInvalid Cost"); 
                                            Console.ResetColor();
                                            Console.ForegroundColor = Color.White;
                                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                            Console.ResetColor();
                                            input = Console.ReadLine();
                                            if (input == "n" || input == "N")
                                            {
                                                c = true;
                                            }
                                        }
                                    } while (!c);
                                }
                                else 
                                { 
                                    Console.ForegroundColor = Color.Red; 
                                    Console.WriteLine("\nInvalid Capacity"); 
                                    Console.ResetColor();
                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                    if (input == "n" || input == "N")
                                    {
                                        capy = true;
                                    }
                                }
                            } while (!capy);
                        }
                        else 
                        { 
                            Console.ForegroundColor = Color.Red; 
                            Console.WriteLine("\nVehicle Name cannot be empty"); 
                            Console.ResetColor();
                            Console.ForegroundColor = Color.White;
                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                            Console.ResetColor();
                            input = Console.ReadLine();
                            if (input == "n" || input == "N")
                            {
                                namev = true;
                            }
                        }

                    } while (!namev);
                }
                else 
                { 
                    Console.ForegroundColor = Color.Red; 
                    Console.WriteLine("\nInvalid Type"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if(input == "n"|| input =="N")
                    {
                        typev = true;
                    }
                }
            } while (!typev);
        }
        private static void AddSafari()
        {
            Console.Clear();
            Safari safari = new Safari();
            Parks park = new Parks();
            bool name = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Safari Name: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    safari.Name = input;
                    bool d = false;
                    do
                    {
                        Console.ForegroundColor = Color.White;
                        Console.WriteLine("\nEnter Safari Available Date(YYYY-MM-DD): ");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (DateTime.TryParse(input, out DateTime date))
                        {
                            safari.Date = date;
                            Console.ForegroundColor = Color.White;
                            bool t = false;
                            do
                            {
                                Console.WriteLine("\nEnter Time Slot for Safari (Morning | Evening):");
                                Console.ResetColor();
                                input = Console.ReadLine();
                                if (Enum.TryParse(input, out slot Time))
                                {
                                    safari.SafariTime = Time;
                                    Console.ForegroundColor = Color.White;
                                    bool c = false;
                                    do
                                    {
                                        Console.WriteLine("\nEnter Safari Cost:");
                                        Console.ResetColor();
                                        input = Console.ReadLine();
                                        if (double.TryParse(input, out double fee))
                                        {
                                            safari.Cost = fee;
                                            ListPark();
                                            bool p = false;
                                            do
                                            {
                                                Console.ForegroundColor = Color.White;
                                                Console.WriteLine("\nEnter Park Id to which the Safari belongs: ");
                                                Console.ResetColor();
                                                input = Console.ReadLine();
                                                if (int.TryParse(input, out int parkid))
                                                {
                                                    safari.ParkId = parkid;
                                                    park.ParkId = parkid;
                                                    try
                                                    {
                                                        bool isExisting = JungleBL.IsParkExists(park);
                                                        if (isExisting)
                                                        {

                                                            try
                                                            {
                                                                bool isAdded = JungleBL.AddSafari(safari);
                                                                if (isAdded)
                                                                {
                                                                    Console.ForegroundColor = Color.Green;
                                                                    Console.WriteLine("\nSafari Added");
                                                                    Console.ResetColor();
                                                                    c = true;
                                                                    d = true;
                                                                    name = true;
                                                                    t = true;
                                                                    p = true;

                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = Color.Red;
                                                                    Console.WriteLine("\nFailed to Add");
                                                                    Console.ResetColor();
                                                                    c = true;
                                                                    d = true;
                                                                    name = true;
                                                                    t = true;
                                                                    p = true;
                                                                }
                                                            }
                                                            catch (JungleException ex)
                                                            {
                                                                Console.ForegroundColor = Color.Yellow;
                                                                Console.WriteLine(ex.Message);
                                                                Console.ResetColor();
                                                               
                                                                    c = true;
                                                                    d = true;
                                                                    name = true;
                                                                    t = true;
                                                                    p = true;
                                                                
                                                            }
                                                        }
                                                    }
                                                    catch (JungleException ex)
                                                    {
                                                        Console.ForegroundColor = Color.Yellow;
                                                        Console.WriteLine(ex.Message);
                                                        Console.ResetColor();
                                                        
                                                            c = true;
                                                            d = true;
                                                            name = true;
                                                            t = true;
                                                            p = true;
                                                        
                                                    }
                                                }
                                                else 
                                                { 
                                                    Console.ForegroundColor = Color.Red; 
                                                    Console.WriteLine("\nInvalid Park Id"); 
                                                    Console.ResetColor();
                                                    Console.ForegroundColor = Color.White;
                                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                                    Console.ResetColor();
                                                    input = Console.ReadLine();
                                                    if (input == "n" || input == "N")
                                                    {
                                                        p = true;
                                                    }
                                                }
                                            } while (!p);
                                        }
                                        else 
                                        { 
                                            Console.ForegroundColor = Color.Red; 
                                            Console.WriteLine("Invalid Cost"); 
                                            Console.ResetColor();
                                            Console.ForegroundColor = Color.White;
                                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                            Console.ResetColor();
                                            input = Console.ReadLine();
                                            if (input == "n" || input == "N")
                                            {
                                                c = true;
                                            }
                                        }
                                    } while (!c);
                                }
                                else 
                                { 
                                    Console.ForegroundColor = Color.Red;
                                    Console.WriteLine("\nInvalid Slot"); 
                                    Console.ResetColor();
                                    Console.ForegroundColor = Color.White;
                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                    if (input == "n" || input == "N")
                                    {
                                        t = true;
                                    }
                                }
                            } while (!t);
                        }
                        else
                        {
                            Console.ForegroundColor = Color.Red;
                            Console.WriteLine("\nInvalid Date");
                            Console.ResetColor();
                            Console.ForegroundColor = Color.White;
                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                            Console.ResetColor();
                            input = Console.ReadLine();
                            if (input == "n" || input == "N")
                            {
                                d = true;
                            }
                        }
                    } while (!d);
                }
                else 
                { 
                    Console.ForegroundColor = Color.Red; 
                    Console.WriteLine("\nSafari Name cannot be empty"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {

                        name = true;

                    }
                }
            } while (!name);
        }
        private static void AddGate()
        {
            Console.Clear();
            Gate g = new Gate();
            Parks park = new Parks();
            bool n = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Gate Name: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    g.Name = input;
                    ListPark();
                    bool p = false;
                    do
                    {
                        Console.ForegroundColor = Color.White;
                        Console.WriteLine("\nEnter Park Id the Gate belongs: ");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (int.TryParse(input, out int Id))
                        {
                            g.ParkId = Id;
                            park.ParkId = Id;
                            try
                            {
                                bool isExisting = JungleBL.IsParkExists(park);
                                if (isExisting)
                                    try
                                    {
                                        bool isAdded = JungleBL.AddGate(g);
                                        if (isAdded)
                                        {
                                            Console.ForegroundColor = Color.Green;
                                            Console.WriteLine("\nGate Added");
                                            Console.ResetColor();
                                            n = true;
                                            p = true;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = Color.Red;
                                            Console.WriteLine("\nFailed to Add");
                                            Console.ResetColor();
                                            n = true;
                                            p = true;
                                        }

                                    }
                                    catch (JungleException ex)
                                    {
                                        Console.ForegroundColor = Color.Yellow;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
                                        Console.ForegroundColor = Color.White;
                                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                        Console.ResetColor();
                                        input = Console.ReadLine();
                                        if (input == "n" || input == "N")
                                        {

                                            n = true;
                                            p = true;

                                        }
                                        
                                        
                                    }
                            }
                            catch (JungleException ex)
                            {
                                Console.ForegroundColor = Color.Yellow;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                                Console.ForegroundColor = Color.White;
                                System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                Console.ResetColor();
                                input = Console.ReadLine();
                                if (input == "n" || input == "N")
                                {

                                    n = true;
                                    p = true;

                                }
                            }

                        }
                        else
                        {
                            Console.ForegroundColor = Color.Red;
                            Console.WriteLine("\nInvalid Park Id");
                            Console.ResetColor();
                            Console.ForegroundColor = Color.White;
                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                            Console.ResetColor();
                            input = Console.ReadLine();
                            if (input == "n" || input == "N")
                            {

                                p = true;

                            }
                        }

                    } while (!p);
                }
                else 
                { 
                    Console.ForegroundColor = Color.Red; 
                    Console.WriteLine("\nGate Name cannot be empty"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {

                        n = true;

                    }
                }

            } while (!n);
        }
        private static void AddPark()
        {
            Console.Clear();
            Parks park = new Parks();
            bool name = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Park Name: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    park.Name = input;
                    bool l = false;
                    do
                    {
                        Console.ForegroundColor = Color.White;
                        Console.WriteLine("\nEnter Park Location: ");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (!string.IsNullOrEmpty(input))
                        {
                            park.Location = input;
                            bool f = false;
                            do
                            {
                                Console.ForegroundColor = Color.White;
                                Console.WriteLine("\nEnter Park Entry Fee: ");
                                Console.ResetColor();
                                input = Console.ReadLine();
                                if (double.TryParse(input, out double fee))
                                {
                                    park.Fee = fee;
                                    try
                                    {
                                        bool isAdded = JungleBL.AddPark(park);
                                        if (isAdded)
                                        {
                                            Console.ForegroundColor = Color.Green;
                                            Console.WriteLine("\nPark Added");
                                            Console.ResetColor();
                                            f = true;
                                            l = true; name = true;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = Color.Red;
                                            Console.WriteLine("\nFailed to Add Park");
                                            Console.ResetColor();
                                            f = true;
                                            l = true; name = true;
                                        }
                                    }
                                    catch (JungleException ex)
                                    {
                                        Console.ForegroundColor = Color.Yellow;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
                                        Console.ForegroundColor = Color.White;
                                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                        Console.ResetColor();
                                        input = Console.ReadLine();
                                        if (input == "n" || input == "N")
                                        {

                                            f = true;
                                            l = true; name = true;

                                        }

                                    }
                                }
                                else 
                                { 
                                    Console.ForegroundColor = Color.Red; 
                                    Console.WriteLine("\nInvalid Fee"); 
                                    Console.ResetColor();
                                    Console.ForegroundColor = Color.White;
                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                    if (input == "n" || input == "N")
                                    {

                                        f = true;
                                        

                                    }
                                }
                            } while (!f);
                        }
                        else 
                        { 
                            Console.ForegroundColor = Color.Red; 
                            Console.WriteLine("\nLocation cannot be Empty"); 
                            Console.ResetColor();
                            Console.ForegroundColor = Color.White;
                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                            Console.ResetColor();
                            input = Console.ReadLine();
                            if (input == "n" || input == "N")
                            {

                                l = true;

                            }
                        }
                    } while (!l);
                }
                else 
                { 
                    Console.ForegroundColor = Color.Red; 
                    Console.WriteLine("\nPark Name cannot be empty"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {

                        name = true;
                      

                    }
                }
            } while (!name);            
        }


        //---------------------------ALL DELETE METHODS----------------------------------------------

        private static void DeleteSafari()
        {
            Console.Clear();
            ListSafari();
            Safari safari = new Safari();
            bool n = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Safari Id to be deleted: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int id))
                {
                    safari.SafariId = id;
                    try
                    {
                        bool isDeleted = JungleBL.DeleteSafari(safari);
                        if (isDeleted)
                        {
                            Console.ForegroundColor = Color.Blue;
                            Console.WriteLine("\nSafari Deleted");
                            Console.ResetColor();
                            n = true;
                        }
                        else
                        {
                            Console.ForegroundColor = Color.Red;
                            Console.WriteLine("\nFailed to Delete");
                            Console.ResetColor();
                            n = true;
                        }
                    }
                    catch (JungleException ex)
                    {
                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        Console.ForegroundColor = Color.White;
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {

                            n = true;


                        }
                        //n = true;
                    }

                }
                else 
                { 
                    Console.ForegroundColor = Color.Red; 
                    Console.WriteLine("\nInvalid Safari Id"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {

                        n = true;


                    }
                }
            } while (!n);
        }
        private static void DeleteVehicle()
        {
            Console.Clear();
            ListVehicles();
            Vehicle v = new Vehicle();
            bool n = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Vehicle Id  to be deleted: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int Id))
                {
                    v.Id = Id;
                    try
                    {
                        bool isDeleted = JungleBL.DeleteVehicle(v);
                        if (isDeleted)
                        {
                            Console.ForegroundColor = Color.Blue;
                            Console.WriteLine("\nVehicle Deleted");
                            Console.ResetColor();
                            n = true;
                        }
                        else
                        {
                            Console.ForegroundColor = Color.Red;
                            Console.WriteLine("\nFailed to Delete");
                            Console.ResetColor();
                            n = true;
                        }
                    }
                    catch (JungleException ex)
                    {
                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        Console.ForegroundColor = Color.White;
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {

                            n = true;


                        }
                        //n = true;
                    }

                }
                else 
                { 
                    Console.ForegroundColor = Color.Yellow; 
                    Console.WriteLine("\nInvalid Vehicle Id"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {

                        n = true;


                    }
                }
            } while (!n);
        }
        private static void DeletePark()
        {
            Console.Clear();
            ListPark();
            Parks park = new Parks();
            bool n = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Name to be deleted: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    park.Name = input;
                    try
                    {
                        bool isDeleted = JungleBL.DeletePark(park);
                        if (isDeleted)
                        {
                            Console.ForegroundColor = Color.Blue;
                            Console.WriteLine("\nPark Deleted");
                            Console.ResetColor();
                            n = true;
                        }
                        else
                        {
                            Console.ForegroundColor = Color.Red;
                            Console.WriteLine("\nFailed to Delete");
                            Console.ResetColor();
                            n = true;
                        }
                    }
                    catch (JungleException ex)
                    {

                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {

                            n = true;


                        }
                        //n = true;
                    }

                }
                else 
                { 
                    Console.ForegroundColor = Color.Red; 
                    Console.WriteLine("\nInvalid Name"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {

                        n = true;


                    }
                }
            } while (!n);
        }
        private static void DeleteGate()
        {
            Console.Clear();
            ListGate();
            Gate g = new Gate();
            bool n = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Name to be deleted: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    g.Name = input;
                    try
                    {
                        bool isDeleted = JungleBL.DeleteGate(g);
                        if (isDeleted)
                        {
                            Console.ForegroundColor = Color.Blue;
                            Console.WriteLine("\nGate Deleted");
                            Console.ResetColor();
                            n = true;
                        }
                        else
                        {
                            Console.ForegroundColor = Color.Red;
                            Console.WriteLine("\nFailed to Delete");
                            Console.ResetColor();
                            n = true;
                        }
                    }
                    catch (JungleException ex)
                    {

                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {

                            n = true;


                        }
                        //n = true;
                    }

                }
                else 
                { 
                    Console.ForegroundColor = Color.Red; 
                    Console.WriteLine("\nInvalid Name"); 
                    Console.ResetColor();
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {

                        n = true;


                    }
                }
            } while (!n);   
        }


        //---------------------------ALL UPDATE METHODS----------------------------------------------

        private static void UpdateVehicle()
        {
            Console.Clear();
            Vehicle v = new Vehicle();
            Parks park = new Parks();
            ListVehicles();
            bool id = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Vehicle Id to Update: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int Id))
                {
                    v.Id = Id;
                    try
                    {
                        bool isExisting = JungleBL.IsVehicleExists(v);
                        if (isExisting)
                        {
                            bool typev = false;
                            do
                            {
                                Console.ForegroundColor = Color.White;
                                Console.WriteLine("\nEnter Vehicle Type (Park): ");
                                Console.ResetColor();
                                input = Console.ReadLine();
                                if (Enum.TryParse(input, out vType type) && input == "Park")
                                {
                                    v.VehicleType = type;
                                    bool name = false;
                                    do
                                    {
                                        Console.ForegroundColor = Color.White;
                                        Console.WriteLine("\nEnter New Vehicle Name: ");
                                        Console.ResetColor();
                                        input = Console.ReadLine();
                                        if (!string.IsNullOrEmpty(input))
                                        {
                                            v.Name = input;
                                            bool capy = false;
                                            do
                                            {
                                                Console.ForegroundColor = Color.White;
                                                Console.WriteLine("\nEnter the New Capacity of Vehicle (Seat6 | Seat4): ");
                                                Console.ResetColor();
                                                input = Console.ReadLine();
                                                if (Enum.TryParse(input, out Capacity cap))
                                                {
                                                    v.capacity = cap;
                                                    bool c = false;
                                                    do
                                                    {
                                                        Console.ForegroundColor = Color.White;
                                                        Console.WriteLine("\nEnter the New Entry Cost: ");
                                                        Console.ResetColor();
                                                        input = Console.ReadLine();
                                                        if (double.TryParse(input, out double cost))
                                                        {
                                                            v.Cost = cost;
                                                            ListPark();
                                                            bool p = false;
                                                            do
                                                            {
                                                                Console.ForegroundColor = Color.White;
                                                                Console.WriteLine("\nEnter the New Park Id where Vehicle belongs: ");
                                                                Console.ResetColor();
                                                                input = Console.ReadLine();
                                                                if (int.TryParse(input, out int pId))
                                                                {
                                                                    v.ParkId = pId;
                                                                    park.ParkId = pId;
                                                                    try
                                                                    {
                                                                        bool isExisting1 = JungleBL.IsParkExists(park);
                                                                        if (isExisting1)
                                                                        {
                                                                            try
                                                                            {
                                                                                bool isUpdate = JungleBL.UpdateVehicle(v);
                                                                                if (isUpdate)
                                                                                {
                                                                                    Console.ForegroundColor = Color.HotPink;
                                                                                    Console.WriteLine("\nVehicle Updated");
                                                                                    Console.ResetColor();
                                                                                    p = true;
                                                                                    capy = true;
                                                                                    c = true;
                                                                                    typev = true;
                                                                                    name = true;
                                                                                    id = true;
                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.ForegroundColor = Color.Red;
                                                                                    Console.WriteLine("\nFailed to Update");
                                                                                    Console.ResetColor();
                                                                                    p = true;
                                                                                    capy = true;
                                                                                    c = true;
                                                                                    typev = true;
                                                                                    name = true;
                                                                                    id = true;
                                                                                }
                                                                            }
                                                                            catch (JungleException ex)
                                                                            {
                                                                                Console.ForegroundColor = Color.Yellow;
                                                                                Console.WriteLine(ex.Message);
                                                                                Console.ResetColor();
                                                                                Console.ForegroundColor = Color.White;
                                                                                System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                                                Console.ResetColor();
                                                                                input = Console.ReadLine();
                                                                                if (input == "n" || input == "N")
                                                                                {

                                                                                    p = true;
                                                                                    capy = true;
                                                                                    c = true;
                                                                                    typev = true;
                                                                                    name = true;
                                                                                    id = true;


                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    catch (JungleException ex)
                                                                    {
                                                                        Console.ForegroundColor = Color.Yellow;
                                                                        Console.WriteLine(ex.Message);
                                                                        Console.ResetColor();
                                                                        Console.ForegroundColor = Color.White;
                                                                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                                        Console.ResetColor();
                                                                        input = Console.ReadLine();
                                                                        if (input == "n" || input == "N")
                                                                        {

                                                                            p = true;
                                                                            capy = true;
                                                                            c = true;
                                                                            typev = true;
                                                                            name = true;
                                                                            id = true;


                                                                        }
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = Color.Red;
                                                                    Console.WriteLine("\nInvalid Park Id");
                                                                    Console.ResetColor();
                                                                    Console.ForegroundColor = Color.White;
                                                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                                    Console.ResetColor();
                                                                    input = Console.ReadLine();
                                                                    if (input == "n" || input == "N")
                                                                    {

                                                                        p = true;
                                                                    }

                                                                }
                                                            } while (!p);
                                                        }
                                                        else
                                                        {
                                                            Console.ForegroundColor = Color.Red;
                                                            Console.WriteLine("\nInvalid Cost");
                                                            Console.ResetColor();
                                                            Console.ForegroundColor = Color.White;
                                                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                            Console.ResetColor();
                                                            input = Console.ReadLine();
                                                            if (input == "n" || input == "N")
                                                            {

                                                                c = true;
                                                            }
                                                        }
                                                    } while (!c);
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = Color.Red;
                                                    Console.WriteLine("\nInvalid Capacity");
                                                    Console.ResetColor();
                                                    Console.ForegroundColor = Color.White;
                                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                    Console.ResetColor();
                                                    input = Console.ReadLine();
                                                    if (input == "n" || input == "N")
                                                    {

                                                        capy = true;
                                                    }
                                                }

                                            } while (!capy);
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = Color.Red;
                                            Console.WriteLine("\nName cannot be empty");
                                            Console.ResetColor();
                                            Console.ForegroundColor = Color.White;
                                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                            Console.ResetColor();
                                            input = Console.ReadLine();
                                            if (input == "n" || input == "N")
                                            {

                                                name = true;
                                            }
                                        }
                                    } while (!name);
                                }
                                else
                                {
                                    Console.ForegroundColor = Color.Red;
                                    Console.WriteLine("\nInvalid Type");
                                    Console.ResetColor();
                                    Console.ForegroundColor = Color.White;
                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                    if (input == "n" || input == "N")
                                    {

                                        typev = true;
                                    }
                                }
                            } while (!typev);
                        }
                    }
                    catch (JungleException ex)
                    {

                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                }
                else {
                    Console.ForegroundColor = Color.Red; 
                    Console.WriteLine("\nInvalid Id"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {

                        id = true;
                    }
                }
            } while (!id);
      

        }
        private static void UpdateSafari()
        {
            Console.Clear();
            Safari safari = new Safari();
            Parks park = new Parks();
            ListSafari();
            bool id = true;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Safari Id to Update: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int Id))
                {
                    safari.SafariId = Id;
                    try
                    {
                        bool isExisting = JungleBL.IsSafariExists(safari);
                        if (isExisting)
                        {
                            bool name = false;
                            do
                            {
                                Console.ForegroundColor = Color.White;
                                Console.WriteLine("\nEnter New Safari Name: ");
                                Console.ResetColor();
                                input = Console.ReadLine();
                                if (!string.IsNullOrEmpty(input))
                                {
                                    safari.Name = input;
                                    bool d = false;
                                    do
                                    {
                                        Console.ForegroundColor = Color.White;
                                        Console.WriteLine("\nEnter new Safari Available Date: ");
                                        Console.ResetColor();
                                        input = Console.ReadLine();
                                        if (DateTime.TryParse(input, out DateTime date))
                                        {
                                            safari.Date = date;
                                            bool s = false;
                                            do
                                            {
                                                Console.ForegroundColor = Color.White;
                                                Console.WriteLine("\nEnter New Time Slot for Safari (Morning | Evening):");
                                                Console.ResetColor();
                                                input = Console.ReadLine();
                                                if (Enum.TryParse(input, out slot Time))
                                                {
                                                    safari.SafariTime = Time;
                                                    bool c = false;
                                                    do
                                                    {
                                                        Console.ForegroundColor = Color.White;
                                                        Console.WriteLine("\nEnter New Safari Cost:");
                                                        Console.ResetColor();
                                                        input = Console.ReadLine();
                                                        if (double.TryParse(input, out double fee))
                                                        {
                                                            safari.Cost = fee;
                                                            ListPark();
                                                            bool p = false;
                                                            do
                                                            {
                                                                Console.ForegroundColor = Color.White;
                                                                Console.WriteLine("\nEnter New Park Id to which the Safari belongs: ");
                                                                Console.ResetColor();
                                                                input = Console.ReadLine();
                                                                if (int.TryParse(input, out int parkid))
                                                                {
                                                                    safari.ParkId = parkid;
                                                                    park.ParkId = parkid;
                                                                    try
                                                                    {
                                                                        bool isExisting1 = JungleBL.IsParkExists(park);
                                                                        if (isExisting1)
                                                                        {

                                                                            try
                                                                            {
                                                                                bool isUpdate = JungleBL.UpdateSafari(safari);
                                                                                if (isUpdate)
                                                                                {
                                                                                    Console.ForegroundColor = Color.HotPink;
                                                                                    Console.WriteLine("\nSafari Updated");
                                                                                    Console.ResetColor();
                                                                                    name = true;
                                                                                    d = true;
                                                                                    s = true;
                                                                                    c = true;
                                                                                    p = true;
                                                                                    id = true;

                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.ForegroundColor = Color.Red;
                                                                                    Console.WriteLine("\nFailed to Update");
                                                                                    Console.ResetColor();
                                                                                    name = true;
                                                                                    d = true;
                                                                                    s = true;
                                                                                    c = true;
                                                                                    p = true;
                                                                                    id = true;
                                                                                }

                                                                            }
                                                                            catch (JungleException ex)
                                                                            {
                                                                                Console.ForegroundColor = Color.Yellow;
                                                                                Console.WriteLine(ex.Message);
                                                                                Console.ResetColor();
                                                                                Console.ForegroundColor = Color.White;
                                                                                System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                                                Console.ResetColor();
                                                                                input = Console.ReadLine();
                                                                                if (input == "n" || input == "N")
                                                                                {
                                                                                    name = true;
                                                                                    d = true;
                                                                                    s = true;
                                                                                    c = true;
                                                                                    p = true;
                                                                                    id = true;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    catch (JungleException ex)
                                                                    {
                                                                        Console.ForegroundColor = Color.Yellow;
                                                                        Console.WriteLine(ex.Message);
                                                                        Console.ResetColor();
                                                                        Console.ForegroundColor = Color.White;
                                                                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                                        Console.ResetColor();
                                                                        input = Console.ReadLine();
                                                                        if (input == "n" || input == "N")
                                                                        {
                                                                            name = true;
                                                                            d = true;
                                                                            s = true;
                                                                            c = true;
                                                                            p = true;
                                                                            id = true;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Console.ForegroundColor = Color.Red;
                                                                    Console.WriteLine("\nInvalid Park Id");
                                                                    Console.ResetColor();
                                                                    Console.ForegroundColor = Color.White;
                                                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                                    Console.ResetColor();
                                                                    input = Console.ReadLine();
                                                                    if (input == "n" || input == "N")
                                                                    {

                                                                        p = true;
                                                                    }
                                                                }
                                                            } while (!p);

                                                        }
                                                        else
                                                        {
                                                            Console.ForegroundColor = Color.Red;
                                                            Console.WriteLine("Invalid Cost");
                                                            Console.ResetColor();
                                                            Console.ForegroundColor = Color.White;
                                                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                            Console.ResetColor();
                                                            input = Console.ReadLine();
                                                            if (input == "n" || input == "N")
                                                            {

                                                                c = true;

                                                            }
                                                        }
                                                    } while (!c);

                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Slot");
                                                    Console.ResetColor();
                                                    Console.ForegroundColor = Color.White;
                                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                    Console.ResetColor();
                                                    input = Console.ReadLine();
                                                    if (input == "n" || input == "N")
                                                    {

                                                        s = true;

                                                    }
                                                }
                                            } while (!s);

                                        }
                                        else
                                        {
                                            Console.ForegroundColor = Color.Red;
                                            Console.WriteLine("\nInvalid Date");
                                            Console.ResetColor();
                                            Console.ForegroundColor = Color.White;
                                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                            Console.ResetColor();
                                            input = Console.ReadLine();
                                            if (input == "n" || input == "N")
                                            {

                                                d = true;

                                            }
                                        }
                                    } while (!d);
                                }
                                else
                                {
                                    Console.ForegroundColor = Color.Red;
                                    Console.WriteLine("\nSafari Name cannot be empty");
                                    Console.ResetColor();
                                    Console.ForegroundColor = Color.White;
                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                    if (input == "n" || input == "N")
                                    {
                                        name = true;

                                    }
                                }
                            } while (!name);
                        }
                    }
                    catch (JungleException ex)
                    {

                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }

                }
                else 
                { 
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Id"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {
                        id = true;

                    }
                }
            } while (!id);
            

        }
        private static void UpdateGate()
        {
            Console.Clear();
            ListGate();
            Gate g = new Gate();
            Parks park = new Parks();
            bool id = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Gate Id to Update: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int Id))
                {
                    g.GateId = Id;
                    try
                    {
                        bool isExisting = JungleBL.IsGateExists(g);
                        if (isExisting)
                        {
                            bool n = false;
                            do
                            {
                                Console.ForegroundColor = Color.White;
                                Console.WriteLine("\nEnter New Gate Name: ");
                                Console.ResetColor();
                                input = Console.ReadLine();
                                if (!string.IsNullOrEmpty(input))
                                {
                                    g.Name = input;
                                    ListPark();
                                    bool p = false;
                                    do
                                    {
                                        Console.ForegroundColor = Color.White;
                                        Console.WriteLine("\nEnter New Park Id: ");
                                        Console.ResetColor();
                                        input = Console.ReadLine();
                                        if (int.TryParse(input, out int pId))
                                        {
                                            g.ParkId = pId;
                                            park.ParkId = pId;
                                            try
                                            {
                                                bool isExisting1 = JungleBL.IsParkExists(park);
                                                if (isExisting1)
                                                {
                                                    try
                                                    {
                                                        bool isUpdate = JungleBL.UpdateGate(g);
                                                        if (isUpdate)
                                                        {
                                                            Console.ForegroundColor = Color.HotPink;
                                                            Console.WriteLine("\nGate Updated");
                                                            Console.ResetColor();
                                                            p = true;
                                                            n = true;
                                                            id = true;
                                                        }
                                                        else
                                                        {
                                                            Console.ForegroundColor = Color.Red;
                                                            Console.WriteLine("\nFailed to Update");
                                                            Console.ResetColor();
                                                            p = true;
                                                            n = true;
                                                            id = true;
                                                        }
                                                    }
                                                    catch (JungleException ex)
                                                    {
                                                        Console.ForegroundColor = Color.Yellow;
                                                        Console.WriteLine(ex.Message);
                                                        Console.ResetColor();
                                                        Console.ForegroundColor = Color.White;
                                                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                        Console.ResetColor();
                                                        input = Console.ReadLine();
                                                        if (input == "n" || input == "N")
                                                        {
                                                            p = true;
                                                            n = true;
                                                            id = true;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (JungleException ex)
                                            {
                                                Console.ForegroundColor = Color.Yellow;
                                                Console.WriteLine(ex.Message);
                                                Console.ResetColor();
                                                Console.ForegroundColor = Color.White;
                                                System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!  and Enter to Continue");
                                                Console.ResetColor();
                                                input = Console.ReadLine();
                                                if (input == "n" || input == "N")
                                                {
                                                    p = true;
                                                    n = true;
                                                    id = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = Color.Red;
                                            Console.WriteLine("\nInvalid Park Id");
                                            Console.ResetColor();
                                            Console.ForegroundColor = Color.White;
                                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!  and Enter to Continue");
                                            Console.ResetColor();
                                            input = Console.ReadLine();
                                            if (input == "n" || input == "N")
                                            {
                                                p = true;
                                                //n = true;
                                            }
                                        }
                                    } while (!p);
                                }
                                else
                                {
                                    Console.ForegroundColor = Color.Red;
                                    Console.WriteLine("\nGate Name cannot be empty ");
                                    Console.ResetColor();
                                    Console.ForegroundColor = Color.White;
                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!  and Enter to Continue");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                    if (input == "n" || input == "N")
                                    {
                                        //p = true;
                                        n = true;
                                    }
                                }
                            } while (!n);
                        }
                    }
                    catch (JungleException ex)
                    {

                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        Console.ForegroundColor = Color.White;
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {
                            //p = true;
                            //n = true;
                            id = true;
                        }
                    }

                }
                else 
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Id");
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {
                        //p = true;
                        //n = true;
                        id = true;
                    }
                }
            } while (!id);
            
        }
        private static void UpdatePark()
        {
            Console.Clear();
            ListPark();
            Parks park = new Parks();
            bool id = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter Park Id to Update: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int Id))
                {
                    park.ParkId = Id;
                    try
                    {
                        bool isExisting = JungleBL.IsParkExists(park);
                        if (isExisting)
                        {
                            bool n = false;
                            do
                            {
                                Console.ForegroundColor = Color.White;
                                Console.WriteLine("\nEnter New Park Name: ");
                                Console.ResetColor();
                                input = Console.ReadLine();
                                if (!string.IsNullOrEmpty(input))
                                {
                                    park.Name = input;
                                    bool l = false;
                                    do
                                    {
                                        Console.ForegroundColor = Color.White;
                                        Console.WriteLine("\nEnter New Location: ");
                                        Console.ResetColor();
                                        input = Console.ReadLine();
                                        if (!string.IsNullOrEmpty(input))
                                        {
                                            park.Location = input;
                                            bool c = false;
                                            do
                                            {
                                                Console.ForegroundColor = Color.White;
                                                Console.WriteLine("\nEnter New Entry Fee: ");
                                                Console.ResetColor();
                                                input = Console.ReadLine();
                                                if (double.TryParse(input, out double fee))
                                                {
                                                    park.Fee = fee;
                                                    try
                                                    {
                                                        bool isUpdate = JungleBL.UpdatePark(park);
                                                        if (isUpdate)
                                                        {
                                                            Console.ForegroundColor = Color.HotPink;
                                                            Console.WriteLine("\nPark Updated");
                                                            Console.ResetColor();
                                                            c = true;
                                                            l = true;
                                                            n = true;
                                                            id = true;
                                                        }
                                                        else
                                                        {
                                                            Console.ForegroundColor = Color.Red;
                                                            Console.WriteLine("\nFailed to Update");
                                                            Console.ResetColor();
                                                            c = true;
                                                            l = true;
                                                            n = true;
                                                            id = true;
                                                        }
                                                    }
                                                    catch (JungleException ex)
                                                    {
                                                        Console.ForegroundColor = Color.Yellow;
                                                        Console.WriteLine(ex.Message);
                                                        Console.ResetColor();
                                                        Console.ForegroundColor = Color.White;
                                                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                        Console.ResetColor();
                                                        input = Console.ReadLine();
                                                        if (input == "n" || input == "N")
                                                        {
                                                            c = true;
                                                            l = true;
                                                            n = true;
                                                            id = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = Color.Red;
                                                    Console.WriteLine("\nInvalid Fee");
                                                    Console.ResetColor();
                                                    Console.ForegroundColor = Color.White;
                                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                                    Console.ResetColor();
                                                    input = Console.ReadLine();
                                                    if (input == "n" || input == "N")
                                                    {
                                                        c = true;

                                                    }
                                                }
                                            } while (!c);

                                        }
                                        else
                                        {
                                            Console.ForegroundColor = Color.Red;
                                            Console.WriteLine("\nLocation cannot be empty");
                                            Console.ResetColor();
                                            Console.ForegroundColor = Color.White;
                                            System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                                            Console.ResetColor();
                                            input = Console.ReadLine();
                                            if (input == "n" || input == "N")
                                            {

                                                l = true;

                                            }
                                        }
                                    } while (!l);

                                }
                                else
                                {
                                    Console.ForegroundColor = Color.Red;
                                    Console.WriteLine("\nPark Name cannot be empty ");
                                    Console.ResetColor();
                                    Console.ForegroundColor = Color.White;
                                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!  and Enter to Continue");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                    if (input == "n" || input == "N")
                                    {

                                        n = true;
                                    }
                                }
                            } while (!n);
                        }
                    }
                    catch (JungleException ex)
                    {

                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        Console.ForegroundColor = Color.White;
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {
                            //c = true;
                            //l = true;
                            //n = true;
                            id = true;
                        }
                    }
                }
                else 
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Id"); 
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {
                        id = true;
                    }
                }
            } while (!id);
   
        }


        //---------------------------ALL LIST METHODS----------------------------------------------

        private static void ListRoles()
        {
            List<Role> role = JungleBL.ListRole();
            Console.ForegroundColor = Color.BlueViolet;
            Console.WriteLine("\n-----------------ROLES-----------------");
            Console.ResetColor();
            Console.ForegroundColor = Color.Yellow;
            Console.WriteLine("\n{0,5} {1,10}", "Id", "Roles");
            Console.ResetColor();
            foreach (Role r in role)
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\n{0,5} {1,11}\n", r.Id, r.Name);
                Console.ResetColor();

                //Console.WriteLine($"{r.Id}\t{r.Name}");
            }
        }
        private static void ListVehicles()
        {
            Console.Clear();
            List<Vehicle> vehicles = JungleBL.ListVehicle();
            Console.WriteWithGradient(@"
            ================================    VEHICLES    ================================", Color.Aquamarine, Color.Yellow, 1);
            Console.ForegroundColor = Color.Yellow;
            Console.WriteLine("\n\n{0,5} {1,20} {2,25} {3,30} {4,35}", "Id", "Vehicle Name", "Vehicle Capacity", "Vehicle Cost (Rs.)", "Park Id");
            Console.ResetColor();
            foreach (Vehicle v in vehicles)
            {
                Console.ForegroundColor = Color.White;

                Console.WriteLine("\n{0,5} {1,15} {2,22} {3,32} {4,40}\n", v.Id, v.Name, v.capacity, v.Cost, v.ParkId);
                Console.ResetColor();
            }

        }
        private static void ListSafari()
        {
            Console.Clear();
            List<Safari> safaris = JungleBL.ListSafari();
            Console.WriteWithGradient(@"                                                                                                                                                                           
            ================================    SAFARIS    ================================", Color.Aquamarine, Color.Yellow, 1);
            Console.ForegroundColor = Color.Yellow;
            Console.WriteLine("\n\n{0,5} {1,20} {2,22} {3,24} {4,27} {5, 30}", "Id", "Safari Name", "Date Available", "Time Slot", "Cost (Rs.)", "Park Id");
            Console.ResetColor();
            foreach (Safari s in safaris)
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\n{0,6} {1,20} {2,22} {3,23} {4,26} {5,29}\n", s.SafariId, s.Name, s.Date.ToString("d"), s.SafariTime, s.Cost, s.ParkId);
                Console.ResetColor();
            }
        }
        private static void ListGate()
        {
            Console.Clear();
            List<Gate> gate = JungleBL.ListGates();
            Console.WriteWithGradient(@"                                                                                                                                                                           
            ================================    GATES    ================================", Color.Aquamarine, Color.Yellow, 1);
            Console.ForegroundColor = Color.Yellow;
            Console.WriteLine("\n\n{0,5} {1,20} {2,22}", "Id", "Gate Name", "Park Id");
            Console.ResetColor();
            foreach (Gate g in gate)
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\n{0,5} {1,20} {2,22}\n", g.GateId, g.Name, g.ParkId);
                Console.ResetColor();
            }
        }
        private static void ListPark()
        {
            Console.Clear();
            List<Parks> park = JungleBL.ListParks();
            Console.WriteWithGradient(@"                                                                                                                                                                           
            ================================    PARKS    ================================", Color.Aquamarine, Color.Yellow, 1);
            Console.ForegroundColor = Color.Yellow;
            Console.WriteLine("\n\n{0,5} {1,20} {2,22} {3, 25}", "Id", "Park Name", "Location", "Cost(Rs.)");
            Console.ResetColor();
            foreach (Parks p in park)
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\n{0,5} {1,20} {2,22} {3, 25}\n", p.ParkId, p.Name, p.Location, p.Fee);
                Console.ResetColor();
            }

        }
        private static void ListIdentity()
        {
            List<IdProofs> Id = JungleBL.ListProofs();
            //Console.ForegroundColor = Color.BlueViolet;
            Console.WriteLine("\n-----------------IDENTITY PROOFS-----------------");
            //Console.ResetColor();
            Console.ForegroundColor = Color.Yellow;
            Console.WriteLine("\n{0,5} {1,15}", "Id", "Identities");
            Console.ResetColor();
            foreach (IdProofs i in Id)
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\n{0,5} {1,12}\n", i.Id, i.Name);
                Console.ResetColor();
            }
        }


        //---------------------------TOURIST'S METHODS----------------------------------------------

        private static void SafariByPark()
        {
            Parks park = new Parks();
            Console.Clear();
            ListPark();
            Parks parks = new Parks();
            bool id = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter the Park Id for which you want to see the Available Safari: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int Id))
                {
                    int ParkId = Id;
                    parks.ParkId = Id;
                    try
                    {
                        bool isExisting = JungleBL.IsParkExists(parks);
                        if (isExisting)
                        {
                            List<Safari> safari = (List<Safari>)JungleBL.SafariByPark(ParkId);
                            Console.WriteWithGradient(@"                                                                                                                                                                           
            ================================    Safaris Available in this Park    ================================", Color.Turquoise, Color.Yellow, 1);
                            Console.ForegroundColor = Color.Yellow;
                            Console.WriteLine("\n\n{0,5} {1,20} {2,22} {3,24} {4,27}", "Id", "Safari Name", "Date Available", "Time Slot", "Cost (Rs.)");
                            Console.ResetColor();
                            System.Collections.IList list = safari;
                            for (int i = 0; i < list.Count; i++)
                            {
                                Safari s = (Safari)list[i];
                                Console.ForegroundColor = Color.White;
                                Console.WriteLine("\n{0,6} {1,20} {2,22} {3,23} {4,26}\n", s.SafariId, s.Name, s.Date.ToString("d"), s.SafariTime, s.Cost);
                                Console.ResetColor();
                                id = true;
                            }
                        }
                    }
                    catch(JungleException ex)
                    {
                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        Console.ForegroundColor = Color.White;
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {
                            id = true;
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nInvalid Park Id");
                    Console.ResetColor();
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    if (input == "n" || input == "N")
                    {
                        id = true;
                    }

                }
            } while (!id);
        }
        private static void ParksByLocation()
        {
            Console.Clear();
            bool l = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter the Location to Search: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    string Location = input;

                    try
                    {
                        List<Parks> park = (List<Parks>)JungleBL.ParkByLocation(Location);
                        Console.WriteWithGradient(@"                                                                                                                                                                           
            ================================    Parks in this Location    ================================", Color.Turquoise, Color.Yellow, 1);
                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine("\n\n{0,5}", "Park Name");
                        Console.ResetColor();
                        System.Collections.IList list = park;
                        for (int i = 0; i < list.Count; i++)
                        {
                            Parks p = (Parks)list[i];
                            Console.ForegroundColor = Color.White;
                            Console.WriteLine("\n{0,5}\n", p.Name);
                            Console.ResetColor();
                            l = true;
                        }
                    }
                    catch (JungleException ex)
                    {
                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        Console.ForegroundColor = Color.White;
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO!");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {
                            l = true;
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("\nLocation cannot be empty");
                    Console.ResetColor();
                    l = true;
                }
            } while (!l);
        }
        private static void BookSafari()
        {
            Console.Clear();
            Booking book = new Booking();
            Payment pay = new Payment();
            Parks park = new Parks();
            Safari safaris = new Safari();
            Gate gates = new Gate();
            Vehicle vehicles = new Vehicle();

            ListPark();

            Console.ForegroundColor = Color.White;
            Console.WriteLine("\nPlease Enter the Park Id: ");
            Console.ResetColor();
            string input = Console.ReadLine();
            if (int.TryParse(input, out int parkId))
            {
                park.ParkId = parkId;
                book.ParkId = parkId;
                try
                {
                    bool isExisting = JungleBL.IsParkExists(park);
                    if (isExisting)
                    {

                        List<Safari> safari = (List<Safari>)JungleBL.SafariByPark(parkId);
                        Console.WriteWithGradient(@"                                                                                                                                                                           
                ================================    Safaris Available in this Park    ================================", Color.Turquoise, Color.Yellow, 1);
                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine("\n\n{0,5} {1,20} {2,22} {3,24} {4,27}", "Id", "Safari Name", "Date Available", "Time Slot", "Cost (Rs.)");
                        Console.ResetColor();
                        System.Collections.IList list = safari;
                        for (int i = 0; i < list.Count; i++)
                        {
                            Safari s = (Safari)list[i];
                            Console.ForegroundColor = Color.White;
                            Console.WriteLine("\n{0,6} {1,20} {2,22} {3,23} {4,26}\n", s.SafariId, s.Name, s.Date.ToString("d"), s.SafariTime, s.Cost);
                            Console.ResetColor();
                        }
                        Console.ForegroundColor = Color.White;
                        Console.WriteLine("\nPlease Enter the Safari Id: ");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (int.TryParse(input, out int sId))
                        {
                            safaris.SafariId = sId;
                            book.SafariId = sId;
                            try
                            {
                                bool isExisting1 = JungleBL.IsSafariExists(safaris);
                                if (isExisting1)
                                {
                                    List<Gate> gate = (List<Gate>)JungleBL.GateByPark(parkId);
                                    Console.WriteWithGradient(@"                                                                                                                                                                           
                    ================================   Gates in this Park    ================================", Color.Turquoise, Color.Yellow, 1);
                                    Console.ForegroundColor = Color.Yellow;
                                    Console.WriteLine("\n\n{0,5} {1,20}", "Id", "Gate Name");
                                    Console.ResetColor();
                                    System.Collections.IList list3 = gate;
                                    for (int i = 0; i < list3.Count; i++)
                                    {
                                        Gate g = (Gate)list3[i];
                                        Console.ForegroundColor = Color.White;
                                        Console.WriteLine("\n\n{0,5} {1,20}", g.GateId, g.Name);
                                        Console.ResetColor();
                                    }
                                    Console.ForegroundColor = Color.White;
                                    Console.WriteLine("\nPlease Enter the Gate Id: ");
                                    Console.ResetColor();
                                    input = Console.ReadLine();
                                    if (int.TryParse(input, out int gId))
                                    {
                                        gates.GateId = gId;
                                        book.GateId = gId;
                                        try
                                        {
                                            bool isExisting2 = JungleBL.IsGateExists(gates);
                                            if (isExisting2)
                                            {
                                                Console.ForegroundColor = Color.White;
                                                Console.WriteLine("\nEnter your Name: ");
                                                Console.ResetColor();
                                                input = Console.ReadLine();
                                                if (!string.IsNullOrEmpty(input))
                                                {

                                                    Tourist t = new Tourist();
                                                    t.Name = input;
                                                    Console.ForegroundColor = Color.White;
                                                    Console.WriteLine("\nEnter Gender (M | F | T): ");
                                                    Console.ResetColor();
                                                    input = Console.ReadLine();
                                                    if (Enum.TryParse(input, out Gender g))
                                                    {
                                                        t.Gender = g;
                                                        Console.ForegroundColor = Color.White;
                                                        Console.WriteLine("\nEnter Mobile No.: ");
                                                        Console.ResetColor();
                                                        input = Console.ReadLine();
                                                        if (!string.IsNullOrEmpty(input) && input.Length == 10)
                                                        {
                                                            t.MobileNo = input;
                                                            Console.ForegroundColor = Color.White;
                                                            Console.WriteLine("\nEnter your City: ");
                                                            Console.ResetColor();
                                                            input = Console.ReadLine();
                                                            if (!string.IsNullOrEmpty(input))
                                                            {
                                                                string email = input;
                                                                t.City = input;
                                                                Console.ForegroundColor = Color.White;
                                                                Console.WriteLine("\nEnter your Country: ");
                                                                Console.ResetColor();
                                                                input = Console.ReadLine();
                                                                if (!string.IsNullOrEmpty(input))
                                                                {
                                                                    t.Country = input;
                                                                    Console.ForegroundColor = Color.White;
                                                                    Console.WriteLine("\nEnter your Email Id: ");
                                                                    Console.ResetColor();
                                                                    input = Console.ReadLine();
                                                                    Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                                                                    if (regex.IsMatch(input))
                                                                    {
                                                                        t.Email = input;
                                                                        ListIdentity();
                                                                        Console.ForegroundColor = Color.White;
                                                                        Console.WriteLine("\nPlease Select one of the Id Proof Document: ");
                                                                        Console.ResetColor();
                                                                        input = Console.ReadLine();
                                                                        if (!string.IsNullOrEmpty(input) && input == "Aadhar")
                                                                        {

                                                                            t.IDName = input;
                                                                            Console.ForegroundColor = Color.White;
                                                                            Console.WriteLine("\nEnter the ID Proof Number: ");
                                                                            Console.ResetColor();
                                                                            input = Console.ReadLine();
                                                                            if (!string.IsNullOrEmpty(input))
                                                                            {
                                                                                Regex regex1 = new Regex(@"^\d{4}\s\d{4}\s\d{4}$");
                                                                                if (regex1.IsMatch(input))
                                                                                {
                                                                                    t.IDNumber = input;
                                                                                    try
                                                                                    {
                                                                                        bool isAdded = JungleBL.AddTourist(t);
                                                                                        if (isAdded)
                                                                                        {
                                                                                            book.TouristId = t.Id;
                                                                                            Console.ForegroundColor = Color.White;
                                                                                            Console.WriteLine("\nDo you want to Hire a Vehicle?(y/n): ");
                                                                                            Console.ResetColor();
                                                                                            input = Console.ReadLine();
                                                                                            if (input == "y" || input == "Y")
                                                                                            {

                                                                                                int ParkId = parkId;
                                                                                                List<Vehicle> vehicle = (List<Vehicle>)JungleBL.VehicleByPark(ParkId);
                                                                                                Console.WriteWithGradient(@"                                                                                                                                                                           
                                            ================================   Vehicles in this Park    ================================", Color.Turquoise, Color.Yellow, 1);
                                                                                                Console.ForegroundColor = Color.Yellow;
                                                                                                Console.WriteLine("\n\n{0,5} {1,20} {2,23} {3,25}", "Id", "Vehicle Name", "Vehicle Capacity", "Vehicle Cost(Rs.)");
                                                                                                Console.ResetColor();
                                                                                                System.Collections.IList list1 = vehicle;
                                                                                                for (int i = 0; i < list1.Count; i++)
                                                                                                {
                                                                                                    Vehicle v = (Vehicle)list1[i];
                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                    Console.WriteLine("\n{0,5} {1,20} {2,23} {3,25}\n", v.Id, v.Name, v.capacity, v.Cost);
                                                                                                    Console.ResetColor();
                                                                                                }
                                                                                                Console.ForegroundColor = Color.White;
                                                                                                Console.WriteLine("\nEnter the Vehicle Id to Hire: ");
                                                                                                Console.ResetColor();
                                                                                                input = Console.ReadLine();
                                                                                                if (int.TryParse(input, out int vId))
                                                                                                {
                                                                                                    book.VehicleId = vId;
                                                                                                    vehicles.Id = vId;
                                                                                                    try
                                                                                                    {
                                                                                                        bool isExisting4 = JungleBL.IsVehicleExists(vehicles);
                                                                                                        if (isExisting4)
                                                                                                        {
                                                                                                            Console.ForegroundColor = Color.White;
                                                                                                            System.Console.WriteLine("\nEnter the Number of People: ");
                                                                                                            Console.ResetColor();
                                                                                                            input = Console.ReadLine();
                                                                                                            if (int.TryParse(input, out int num) && num <= 6)
                                                                                                            {
                                                                                                                t.NoOfTourist = num;

                                                                                                                double totalcost = TotalCharges(parkId, sId, vId, num);

                                                                                                                book.TotalCost = totalcost;

                                                                                                                try
                                                                                                                {
                                                                                                                    var isBooked = JungleBL.AddBooking(book);
                                                                                                                    if (isBooked.Item1)
                                                                                                                    {
                                                                                                                        
                                                                                                                        SendMail(isBooked.Item2);
                                                                                                                        Console.ForegroundColor = Color.Green;
                                                                                                                        Console.WriteLine("\nBooking Successful");
                                                                                                                        Console.ResetColor();

                                                                                                                        Console.ForegroundColor = Color.Fuchsia;
                                                                                                                        Console.WriteLine($"\n*** YOUR BOOKING ID: {isBooked.Item2} ***");
                                                                                                                        Console.ResetColor();
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        Console.ForegroundColor = Color.Red;
                                                                                                                        Console.WriteLine("\nFailed to Book");
                                                                                                                        Console.ResetColor();
                                                                                                                    }
                                                                                                                }
                                                                                                                catch (JungleException ex)
                                                                                                                {

                                                                                                                    Console.ForegroundColor = Color.Yellow;
                                                                                                                    Console.WriteLine(ex.Message);
                                                                                                                    Console.ResetColor();
                                                                                                                }
                                                                                                                Console.ForegroundColor = Color.White;
                                                                                                                Console.WriteLine($"\nTotal Payable Amount: {totalcost}");
                                                                                                                Console.ResetColor();
                                                                                                                Console.ForegroundColor = Color.DarkRed;
                                                                                                                Console.WriteLine("\n*********** Please carry your Id proof and Show Booking Id to Pay Cash***********");
                                                                                                                Console.ResetColor();
                                                                                                            }
                                                                                                            else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nInvalid No. of People\n Number of people should be less than or equal to 6"); Console.ResetColor(); }
                                                                                                        }
                                                                                                    }
                                                                                                    catch (JungleException ex)
                                                                                                    {
                                                                                                        Console.ForegroundColor = Color.Yellow;
                                                                                                        Console.WriteLine(ex.Message);
                                                                                                        Console.ResetColor();
                                                                                                    }
                                                                                                }
                                                                                                else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Vehicle ID"); Console.ResetColor(); }
                                                                                            }
                                                                                            else if (input == "n" || input == "N")
                                                                                            {
                                                                                                Vehicle v = new Vehicle();
                                                                                                book.VehicleId = v.Id;
                                                                                                int vId = v.Id;
                                                                                                vehicles.Id = v.Id;
                                                                                                v.VehicleType = vType.Own;
                                                                                                Console.ForegroundColor = Color.White;
                                                                                                Console.WriteLine("\nEnter your Vehicle Name: ");
                                                                                                Console.ResetColor();
                                                                                                input = Console.ReadLine();
                                                                                                if (!string.IsNullOrEmpty(input))
                                                                                                {
                                                                                                    v.Name = input;
                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                    Console.WriteLine("\nEnter the Capacity of Vehicle (Seat6 | Seat4): ");
                                                                                                    Console.ResetColor();
                                                                                                    input = Console.ReadLine();
                                                                                                    if (Enum.TryParse(input, out Capacity cap))
                                                                                                    {
                                                                                                        v.capacity = cap;
                                                                                                            v.ParkId = parkId;
                                                                                                        Console.ForegroundColor = Color.White;
                                                                                                        Console.WriteLine("\nEntry Cost for this Vehicle is Rs.1000");
                                                                                                        Console.ResetColor();
                                                                                                        v.Cost = 1000;
                                                                                                            try
                                                                                                            {
                                                                                                                var isAddedv = JungleBL.AddVehicleO(v);
                                                                                                                if (isAddedv.Item1)
                                                                                                                {
                                                                                                                    book.VehicleId = isAddedv.Item2;
                                                                                                                    try
                                                                                                                    {
                                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                                    System.Console.WriteLine("\nEnter the Number of People: ");
                                                                                                                    Console.ResetColor();
                                                                                                                    input = Console.ReadLine();
                                                                                                                        if (int.TryParse(input, out int num)&& num <= 6)
                                                                                                                        {
                                                                                                                            t.NoOfTourist = num;
                                                                                                                            double totalcost = TotalChargesO(parkId, sId, num);
                                                                                                                            book.TotalCost = totalcost;

                                                                                                                            var isBooked = JungleBL.AddBookingO(book);
                                                                                                                            if (isBooked.Item1)
                                                                                                                            {
                                                                                                                            
                                                                                                                                SendMail(isBooked.Item2);

                                                                                                                                Console.ForegroundColor = Color.Green;
                                                                                                                                Console.WriteLine("\nBooking Successful");
                                                                                                                                Console.ResetColor();

                                                                                                                                Console.ForegroundColor = Color.Fuchsia;
                                                                                                                                Console.WriteLine($"\n*** YOUR BOOKING ID: {isBooked.Item2} ***");
                                                                                                                                Console.ResetColor();
                                                                                                                            Console.ForegroundColor = Color.White;
                                                                                                                            Console.WriteLine($"Total Payable Amount: {book.TotalCost}");
                                                                                                                            Console.ResetColor();


                                                                                                                                Console.ForegroundColor = Color.DarkRed;
                                                                                                                                Console.WriteLine("\n*********** Please carry your Id proof and Show Booking Id to Pay Cash***********");
                                                                                                                                Console.ResetColor();

                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                Console.ForegroundColor = Color.Red;
                                                                                                                                Console.WriteLine("\nSorry could not book");
                                                                                                                                Console.ResetColor();
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nInvalid No. of people\n Number of people should be less than or equal to 6"); Console.ResetColor(); }
                                                                                                                    }
                                                                                                                    catch (JungleException ex)
                                                                                                                    {

                                                                                                                        Console.ForegroundColor = Color.Yellow;
                                                                                                                        Console.WriteLine(ex.Message);
                                                                                                                        Console.ResetColor();
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    Console.ForegroundColor = Color.Red;
                                                                                                                    Console.WriteLine("\nFailed to Book");
                                                                                                                    Console.ResetColor();
                                                                                                                }
                                                                                                            }
                                                                                                            catch (JungleException ex)
                                                                                                            {

                                                                                                                Console.ForegroundColor = Color.Yellow;
                                                                                                                Console.WriteLine(ex.Message);
                                                                                                                Console.ResetColor();
                                                                                                            }                                                                                                                                                                                                         
                                                                                                    }
                                                                                                    else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Capacity"); Console.ResetColor(); }
                                                                                                }
                                                                                                else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nName cannot be empty"); Console.ResetColor(); }
                                                                                            }
                                                                                            else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nWrong Choice"); Console.ResetColor(); }
                                                                                        }
                                                                                        else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nFailed to Add Tourist"); Console.ResetColor(); }
                                                                                    }
                                                                                    catch (JungleException ex)
                                                                                    {
                                                                                        Console.ForegroundColor = Color.Yellow;
                                                                                        Console.WriteLine(ex.Message);
                                                                                        Console.ResetColor();
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.ForegroundColor = Color.Red;
                                                                                    Console.WriteLine("\nInvalid Aadhar Number");
                                                                                    Console.ResetColor();
                                                                                }
                                                                            }
                                                                            else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nAadhar Number cannot be empty"); Console.ResetColor(); }
                                                                        }
                                                                        else if (!string.IsNullOrEmpty(input) && input == "PAN")
                                                                        {

                                                                            t.IDName = input;
                                                                            Console.ForegroundColor = Color.White;
                                                                            Console.WriteLine("\nEnter the ID Proof Number: ");
                                                                            Console.ResetColor();
                                                                            input = Console.ReadLine();
                                                                            if (!string.IsNullOrEmpty(input))
                                                                            {
                                                                                Regex regex2 = new Regex(@"[A-Za-z]{5}\d{4}[A-Za-z]{1}");
                                                                                if (regex2.IsMatch(input))
                                                                                {
                                                                                    t.IDNumber = input;
                                                                                    try
                                                                                    {
                                                                                        bool isAdded = JungleBL.AddTourist(t);
                                                                                        if (isAdded)
                                                                                        {
                                                                                            book.TouristId = t.Id;
                                                                                            Console.ForegroundColor = Color.White;
                                                                                            Console.WriteLine("\nDo you want to Hire a Vehicle?(y/n): ");
                                                                                            Console.ResetColor();
                                                                                            input = Console.ReadLine();
                                                                                            if (input == "y" || input == "Y")
                                                                                            {

                                                                                                int ParkId = parkId;
                                                                                                List<Vehicle> vehicle = (List<Vehicle>)JungleBL.VehicleByPark(ParkId);
                                                                                                Console.WriteWithGradient(@"                                                                                                                                                                           
                                            ================================   Vehicles in this Park    ================================", Color.Turquoise, Color.Yellow, 1);
                                                                                                Console.ForegroundColor = Color.Yellow;
                                                                                                Console.WriteLine("\n\n{0,5} {1,20} {2,23} {3,25}", "Id", "Vehicle Name", "Vehicle Capacity", "Vehicle Cost(Rs.)");
                                                                                                Console.ResetColor();
                                                                                                System.Collections.IList list1 = vehicle;
                                                                                                for (int i = 0; i < list1.Count; i++)
                                                                                                {
                                                                                                    Vehicle v = (Vehicle)list1[i];
                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                    Console.WriteLine("\n{0,5} {1,20} {2,23} {3,25}\n", v.Id, v.Name, v.capacity, v.Cost);
                                                                                                    Console.ResetColor();
                                                                                                }
                                                                                                Console.ForegroundColor = Color.White;
                                                                                                Console.WriteLine("\nEnter the Vehicle Id to Hire: ");
                                                                                                Console.ResetColor();
                                                                                                input = Console.ReadLine();
                                                                                                if (int.TryParse(input, out int vId))
                                                                                                {
                                                                                                    book.VehicleId = vId;
                                                                                                    vehicles.Id = vId;
                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                    System.Console.WriteLine("\nEnter the Number of People: ");
                                                                                                    Console.ResetColor();
                                                                                                    input = Console.ReadLine();
                                                                                                    if (int.TryParse(input, out int num)&& num <= 6)
                                                                                                    {
                                                                                                        t.NoOfTourist = num;
                                                                                                        double totalcost = TotalCharges(parkId, sId, vId, num);

                                                                                                        book.TotalCost = totalcost;

                                                                                                        try
                                                                                                        {
                                                                                                            var isBooked = JungleBL.AddBooking(book);
                                                                                                            if (isBooked.Item1)
                                                                                                            {
                                                                                                                

                                                                                                                SendMail(isBooked.Item2);

                                                                                                                Console.ForegroundColor = Color.Green;
                                                                                                                Console.WriteLine("\nBooking Successful");
                                                                                                                Console.ResetColor();

                                                                                                                Console.ForegroundColor = Color.Fuchsia;
                                                                                                                Console.WriteLine($"\n*** YOUR BOOKING ID: {isBooked.Item2} ***");
                                                                                                                Console.ResetColor();
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                Console.ForegroundColor = Color.Red;
                                                                                                                Console.WriteLine("\nFailed to Book");
                                                                                                                Console.ResetColor();
                                                                                                            }
                                                                                                        }
                                                                                                        catch (JungleException ex)
                                                                                                        {

                                                                                                            Console.ForegroundColor = Color.Yellow;
                                                                                                            Console.WriteLine(ex.Message);
                                                                                                            Console.ResetColor();
                                                                                                        }
                                                                                                        Console.ForegroundColor = Color.White;
                                                                                                        Console.WriteLine($"\nTotal Payable Amount: {totalcost}");
                                                                                                        Console.ResetColor();
                                                                                                        Console.ForegroundColor = Color.DarkRed;
                                                                                                        Console.WriteLine("\n*********** Please carry your Id proof and Show Booking Id to Pay Cash***********");
                                                                                                        Console.ResetColor();
                                                                                                    }
                                                                                                    else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nInvalid no. of people\nNumber of people should be less than or equal to 6"); Console.ResetColor(); }
                                                                                                }
                                                                                                else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Vehicle ID"); Console.ResetColor(); }
                                                                                            }
                                                                                            else if (input == "n" || input == "N")
                                                                                            {
                                                                                                Vehicle v = new Vehicle();
                                                                                                book.VehicleId = v.Id;
                                                                                                int vId = v.Id;
                                                                                                vehicles.Id = v.Id;
                                                                                                v.VehicleType = vType.Own;
                                                                                                Console.ForegroundColor = Color.White;
                                                                                                Console.WriteLine("\nEnter your Vehicle Name: ");
                                                                                                Console.ResetColor();
                                                                                                input = Console.ReadLine();
                                                                                                if (!string.IsNullOrEmpty(input))
                                                                                                {
                                                                                                    v.Name = input;
                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                    Console.WriteLine("\nEnter the Capacity of Vehicle (Seat6 | Seat4): ");
                                                                                                    Console.ResetColor();
                                                                                                    input = Console.ReadLine();
                                                                                                    if (Enum.TryParse(input, out Capacity cap))
                                                                                                    {
                                                                                                        v.capacity = cap;
                                                                                                            v.ParkId = parkId;
                                                                                                        Console.ForegroundColor = Color.White;
                                                                                                        Console.WriteLine("\nEntry Cost for this Vehicle is Rs.1000");
                                                                                                        Console.ResetColor();
                                                                                                        v.Cost = 1000;
                                                                                                            try
                                                                                                            {
                                                                                                                var isAddedv = JungleBL.AddVehicleO(v);
                                                                                                                if (isAddedv.Item1)
                                                                                                                {
                                                                                                                    book.VehicleId = isAddedv.Item2;
                                                                                                                    try
                                                                                                                    {
                                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                                    System.Console.WriteLine("\nEnter the Number of People: ");
                                                                                                                    Console.ResetColor();
                                                                                                                    input = Console.ReadLine();
                                                                                                                        if (int.TryParse(input, out int num) && num <=6)
                                                                                                                        {
                                                                                                                            t.NoOfTourist = num;
                                                                                                                            double totalcost = TotalChargesO(parkId, sId, num);
                                                                                                                            book.TotalCost = totalcost;

                                                                                                                            var isBooked = JungleBL.AddBookingO(book);
                                                                                                                            if (isBooked.Item1)
                                                                                                                            {
                                                                                                                            
                                                                                                                                SendMail(isBooked.Item2);

                                                                                                                                Console.ForegroundColor = Color.Green;
                                                                                                                                Console.WriteLine("\nBooking Successful");
                                                                                                                                Console.ResetColor();

                                                                                                                                Console.ForegroundColor = Color.Fuchsia;
                                                                                                                                Console.WriteLine($"\n*** YOUR BOOKING ID: {isBooked.Item2} ***");
                                                                                                                                Console.ResetColor();
                                                                                                                            Console.ForegroundColor = Color.White;
                                                                                                                            Console.WriteLine($"Total Payable Amount: {book.TotalCost}");
                                                                                                                            Console.ResetColor();


                                                                                                                                Console.ForegroundColor = Color.DarkRed;
                                                                                                                                Console.WriteLine("\n*********** Please carry your Id proof and Show Booking Id to Pay Cash***********");
                                                                                                                                Console.ResetColor();

                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                Console.ForegroundColor = Color.Red;
                                                                                                                                Console.WriteLine("\nSorry could not book");
                                                                                                                                Console.ResetColor();
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nInvalid No. of People\nNumber of people should be less than or equal to 6"); Console.ResetColor(); }
                                                                                                                    }
                                                                                                                    catch (JungleException ex)
                                                                                                                    {

                                                                                                                        Console.ForegroundColor = Color.Yellow;
                                                                                                                        Console.WriteLine(ex.Message);
                                                                                                                        Console.ResetColor();
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    Console.ForegroundColor = Color.Red;
                                                                                                                    Console.WriteLine("\nFailed to Book");
                                                                                                                    Console.ResetColor();
                                                                                                                }
                                                                                                            }
                                                                                                            catch (JungleException ex)
                                                                                                            {

                                                                                                                Console.ForegroundColor = Color.Yellow;
                                                                                                                Console.WriteLine(ex.Message);
                                                                                                                Console.ResetColor();
                                                                                                            }
                                                                                                        }
                                                                                                        else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Park Id"); Console.ResetColor(); }
                                                                                                    }
                                                                                                    else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Capacity"); Console.ResetColor(); }
                                                                                                }
                                                                                                else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nName cannot be empty"); Console.ResetColor(); }
                                                                                            }
                                                                                        
                                                                                        else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nFailed to Add Tourist"); Console.ResetColor(); }
                                                                                    }
                                                                                    catch (JungleException ex)
                                                                                    {
                                                                                        Console.ForegroundColor = Color.Yellow;
                                                                                        Console.WriteLine(ex.Message);
                                                                                        Console.ResetColor();
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.ForegroundColor = Color.Red; 
                                                                                    Console.WriteLine("\nInvalid PAN Number");
                                                                                    Console.ResetColor();
                                                                                }
                                                                            }
                                                                            else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nPAN number cannot be empty"); Console.ResetColor(); }
                                                                        }
                                                                        else if (!string.IsNullOrEmpty(input) && input == "Passport")
                                                                        {
                                                                            t.IDName = input;
                                                                            Console.ForegroundColor = Color.White;
                                                                            Console.WriteLine("\nEnter the ID Proof Number: ");
                                                                            Console.ResetColor();
                                                                            input = Console.ReadLine();
                                                                            if (!string.IsNullOrEmpty(input))
                                                                            {
                                                                                Regex regex3 = new Regex(@"^(?!^0+$)[a-zA-Z0-9]{6,9}$");
                                                                                if (regex3.IsMatch(input))
                                                                                {
                                                                                    t.IDNumber = input;
                                                                                    try
                                                                                    {
                                                                                        bool isAdded = JungleBL.AddTourist(t);
                                                                                        if (isAdded)
                                                                                        {
                                                                                            book.TouristId = t.Id;
                                                                                            Console.ForegroundColor = Color.White;
                                                                                            Console.WriteLine("\nDo you want to Hire a Vehicle?(y/n): ");
                                                                                            Console.ResetColor();
                                                                                            input = Console.ReadLine();
                                                                                            if (input == "y" || input == "Y")
                                                                                            {

                                                                                                int ParkId = parkId;
                                                                                                List<Vehicle> vehicle = (List<Vehicle>)JungleBL.VehicleByPark(ParkId);
                                                                                                Console.WriteWithGradient(@"                                                                                                                                                                           
                                            ================================   Vehicles in this Park    ================================", Color.Turquoise, Color.Yellow, 1);
                                                                                                Console.ForegroundColor = Color.Yellow;
                                                                                                Console.WriteLine("\n\n{0,5} {1,20} {2,23} {3,25}", "Id", "Vehicle Name", "Vehicle Capacity", "Vehicle Cost(Rs.)");
                                                                                                Console.ResetColor();
                                                                                                System.Collections.IList list1 = vehicle;
                                                                                                for (int i = 0; i < list1.Count; i++)
                                                                                                {
                                                                                                    Vehicle v = (Vehicle)list1[i];
                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                    Console.WriteLine("\n{0,5} {1,20} {2,23} {3,25}\n", v.Id, v.Name, v.capacity, v.Cost);
                                                                                                    Console.ResetColor();
                                                                                                }
                                                                                                Console.ForegroundColor = Color.White;
                                                                                                Console.WriteLine("\nEnter the Vehicle Id to Hire: ");
                                                                                                Console.ResetColor();
                                                                                                input = Console.ReadLine();
                                                                                                if (int.TryParse(input, out int vId))
                                                                                                {
                                                                                                    book.VehicleId = vId;
                                                                                                    vehicles.Id = vId;
                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                    System.Console.WriteLine("\nEnter the Number of People: ");
                                                                                                    Console.ResetColor();
                                                                                                    input = Console.ReadLine();
                                                                                                    if (int.TryParse(input, out int num) && num <=6)
                                                                                                    {
                                                                                                        t.NoOfTourist = num;
                                                                                                        double totalcost = TotalCharges(parkId, sId, vId, num);

                                                                                                        book.TotalCost = totalcost;

                                                                                                        try
                                                                                                        {
                                                                                                            var isBooked = JungleBL.AddBooking(book);
                                                                                                            if (isBooked.Item1)
                                                                                                            {

                                                                                                                
                                                                                                                SendMail(isBooked.Item2);

                                                                                                                Console.ForegroundColor = Color.Green;
                                                                                                                Console.WriteLine("\nBooking Successful");
                                                                                                                Console.ResetColor();

                                                                                                                Console.ForegroundColor = Color.Fuchsia;
                                                                                                                Console.WriteLine($"\n*** YOUR BOOKING ID: {isBooked.Item2} ***");
                                                                                                                Console.ResetColor();
                                                                                                                Console.ForegroundColor = Color.White;
                                                                                                                Console.WriteLine($"\nTotal Payable Amount: {totalcost}");
                                                                                                                Console.ResetColor();
                                                                                                                Console.ForegroundColor = Color.DarkRed;
                                                                                                                Console.WriteLine("\n*********** Please carry your Id proof and Show Booking Id to Pay Cash***********");
                                                                                                                Console.ResetColor();
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                Console.ForegroundColor = Color.Red;
                                                                                                                Console.WriteLine("\nFailed to Book");
                                                                                                                Console.ResetColor();
                                                                                                            }
                                                                                                        }
                                                                                                        catch (JungleException ex)
                                                                                                        {

                                                                                                            Console.ForegroundColor = Color.Yellow;
                                                                                                            Console.WriteLine(ex.Message);
                                                                                                            Console.ResetColor();
                                                                                                        }
                                                                                                        
                                                                                                    }
                                                                                                    else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nInvalid No. of People\n Number of People should be less than or equal to 6 "); Console.ResetColor(); }
                                                                                                }
                                                                                                else { Console.WriteLine("\nInvalid Vehicle ID"); }
                                                                                            }
                                                                                            else if (input == "n" || input == "N")
                                                                                            {
                                                                                                Vehicle v = new Vehicle();
                                                                                                book.VehicleId = v.Id;
                                                                                                int vId = v.Id;
                                                                                                vehicles.Id = v.Id;
                                                                                                v.VehicleType = vType.Own;
                                                                                                Console.ForegroundColor = Color.White;
                                                                                                Console.WriteLine("\nEnter your Vehicle Name: ");
                                                                                                Console.ResetColor();
                                                                                                input = Console.ReadLine();
                                                                                                if (!string.IsNullOrEmpty(input))
                                                                                                {
                                                                                                    v.Name = input;
                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                    Console.WriteLine("\nEnter the Capacity of Vehicle (Seat6 | Seat4): ");
                                                                                                    Console.ResetColor();
                                                                                                    input = Console.ReadLine();
                                                                                                    if (Enum.TryParse(input, out Capacity cap))
                                                                                                    {
                                                                                                        v.capacity = cap;
                                                                                                        
                                                                                                            v.ParkId = parkId;
                                                                                                        Console.ForegroundColor = Color.White;
                                                                                                        Console.WriteLine("\nEntry Cost for this Vehicle is Rs.1000");
                                                                                                        Console.ResetColor();
                                                                                                        v.Cost = 1000;
                                                                                                            try
                                                                                                            {
                                                                                                                var isAddedv = JungleBL.AddVehicleO(v);
                                                                                                                if (isAddedv.Item1)
                                                                                                                {
                                                                                                                    book.VehicleId = isAddedv.Item2;
                                                                                                                    try
                                                                                                                    {
                                                                                                                    Console.ForegroundColor = Color.White;
                                                                                                                    System.Console.WriteLine("\nEnter the Number of People: ");
                                                                                                                    Console.ResetColor();
                                                                                                                    input = Console.ReadLine();
                                                                                                                        if (int.TryParse(input, out int num) && num <=6)
                                                                                                                        {
                                                                                                                            t.NoOfTourist = num;
                                                                                                                            double totalcost = TotalChargesO(parkId, sId, num);
                                                                                                                            book.TotalCost = totalcost;

                                                                                                                            var isBooked = JungleBL.AddBookingO(book);
                                                                                                                            if (isBooked.Item1)
                                                                                                                            {
                                                                                                                               
                                                                                                                                SendMail(isBooked.Item2);

                                                                                                                                Console.ForegroundColor = Color.Green;
                                                                                                                                Console.WriteLine("\nBooking Successful");
                                                                                                                                Console.ResetColor();

                                                                                                                                Console.ForegroundColor = Color.Fuchsia;
                                                                                                                                Console.WriteLine($"\n*** YOUR BOOKING ID: {isBooked.Item2} ***");
                                                                                                                                Console.ResetColor();
                                                                                                                            Console.ForegroundColor = Color.White;
                                                                                                                            Console.WriteLine($"Total Payable Amount: {book.TotalCost}");
                                                                                                                            Console.ResetColor();


                                                                                                                                Console.ForegroundColor = Color.DarkRed;
                                                                                                                                Console.WriteLine("\n*********** Please carry your Id proof and Show Booking Id to Pay Cash***********");
                                                                                                                                Console.ResetColor();

                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                Console.ForegroundColor = Color.Red;
                                                                                                                                Console.WriteLine("\nSorry could not book");
                                                                                                                                Console.ResetColor();
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nInvalid No. of People\nNumber of People should be less than or equal to 6"); Console.ResetColor(); }
                                                                                                                    }
                                                                                                                    catch (JungleException ex)
                                                                                                                    {

                                                                                                                        Console.ForegroundColor = Color.Yellow;
                                                                                                                        Console.WriteLine(ex.Message);
                                                                                                                        Console.ResetColor();
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    Console.ForegroundColor = Color.Red;
                                                                                                                    Console.WriteLine("\nFailed to Book");
                                                                                                                    Console.ResetColor();
                                                                                                                }
                                                                                                            }
                                                                                                            catch (JungleException ex)
                                                                                                            {

                                                                                                                Console.ForegroundColor = Color.Yellow;
                                                                                                                Console.WriteLine(ex.Message);
                                                                                                                Console.ResetColor();
                                                                                                            }
                                                                                                        }
                                                                                                        else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Park Id"); Console.ResetColor(); }
                                                                                                    }
                                                                                                    else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Capacity"); Console.ResetColor(); }
                                                                                                }
                                                                                                else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nName cannot be empty"); Console.ResetColor(); }
                                                                                            }
                                                                                        
                                                                                        else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nFailed to Add Tourist"); Console.ResetColor(); }
                                                                                    }
                                                                                    catch (JungleException ex)
                                                                                    {
                                                                                        Console.ForegroundColor = Color.Yellow;
                                                                                        Console.WriteLine(ex.Message);
                                                                                        Console.ResetColor();
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.ForegroundColor = Color.Red;
                                                                                    Console.WriteLine("\nInvalid Passport Number");
                                                                                    Console.ResetColor();
                                                                                }
                                                                            }
                                                                            else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nPassport Number cannot be empty"); Console.ResetColor(); }
                                                                        }
                                                                        else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nInvalid Id selected"); Console.ResetColor(); }
                                                                    }
                                                                    else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Email Id"); Console.ResetColor(); }
                                                                }
                                                                else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nCountry cannot be empty"); Console.ResetColor(); }
                                                            }
                                                            else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nCity cannot be empty"); Console.ResetColor(); }
                                                        }
                                                        else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Mobile No."); Console.ResetColor(); }
                                                    }
                                                    else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Gender"); Console.ResetColor(); }
                                                }
                                                else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nName cannot be empty"); Console.ResetColor(); }
                                            }
                                        }
                                        catch (JungleException ex)
                                        {
                                            Console.ForegroundColor = Color.Yellow;
                                            Console.WriteLine(ex.Message);
                                            Console.ResetColor();
                                        }
                                    }
                                    else { Console.ForegroundColor = Color.Red; System.Console.WriteLine("\nInvalid Gate Id"); Console.ResetColor(); }
                                }
                            }
                            catch (JungleException ex)
                            {
                                Console.ForegroundColor = Color.Yellow;
                                Console.WriteLine(ex.Message);
                                Console.ResetColor();
                            }
                        }
                        else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid Safari Id"); Console.ResetColor(); }
                    }
                }
                catch (JungleException ex)
                {
                    Console.ForegroundColor = Color.Yellow;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
            else { Console.ForegroundColor = Color.Red; Console.WriteLine("\nInvalid ParkId"); Console.ResetColor(); }
        }
        private static void ViewBooking()
        {
            Console.Clear();
            Booking books = new Booking();
            bool id = false;
            do
            {
                Console.ForegroundColor = Color.White;
                Console.WriteLine("\nEnter your Booking Id to view the status: ");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int bId))
                {
                    books.Id = bId;
                    int bookId = bId;
                    try
                    {
                        bool isExists = JungleBL.IsBookingExist(books);
                        if (isExists)
                        {

                        }
                        List<Booking> book = (List<Booking>)JungleBL.BookingStatus(bookId);
                        Console.WriteWithGradient(@"                                                                                                                                                                           
                ================================   Your Booking Information    ================================", Color.Turquoise, Color.Yellow, 1);
                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine("\n\n{0,5} {1,20} {2,23} {3,25} {4,27} {5,29}", "Booking Id", "Park Name", "Vehicle Name", "Safari Name", "Date of Visit", "Total Cost");
                        Console.ResetColor();
                        System.Collections.IList list = book;
                        for (int i = 0; i < list.Count; i++)
                        {
                            Booking b = (Booking)list[i];
                            Console.ForegroundColor = Color.White;
                            Console.WriteLine("\n{0,5} {1,23} {2,23} {3,26} {4,26} {5,29}\n", b.Id, b.PName, b.Vname, b.SName, b.Date.ToString("d"), b.TotalCost);
                            Console.ResetColor();
                            id = true;
                        }
                    }
                    catch (JungleException ex)
                    {

                        Console.ForegroundColor = Color.Yellow;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        Console.ForegroundColor = Color.White;
                        System.Console.WriteLine("\nDo you want to Continue?\n\nPress N or n for NO! and Enter to Continue");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        if (input == "n" || input == "N")
                        {
                            id = true;
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = Color.Red;
                    Console.WriteLine("Invalid Booking Id");
                    Console.ResetColor();
                }
            } while (!id);
        }


        //--------------------------------MISC METHODS USED--------------------------------------------
        private static double TotalCharges(int parkid, int safariid, int vehicleid, int num)
        {
            return JungleBL.TotalCharges(parkid, safariid, vehicleid, num);
        }
        private static double TotalChargesO(int parkId, int sId, int num)
        {
            return JungleBL.TotalChargesO(parkId, sId, num);
        }
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
        public static void Initialize()
        {
            Console.WriteWithGradient(@"
   
     ██╗██╗   ██╗███╗   ██╗ ██████╗ ██╗     ███████╗    ███████╗ █████╗ ███████╗ █████╗ ██████╗ ██╗    ██████╗  ██████╗  ██████╗ ██╗  ██╗██╗███╗   ██╗ ██████╗ 
     ██║██║   ██║████╗  ██║██╔════╝ ██║     ██╔════╝    ██╔════╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██║    ██╔══██╗██╔═══██╗██╔═══██╗██║ ██╔╝██║████╗  ██║██╔════╝ 
     ██║██║   ██║██╔██╗ ██║██║  ███╗██║     █████╗      ███████╗███████║█████╗  ███████║██████╔╝██║    ██████╔╝██║   ██║██║   ██║█████╔╝ ██║██╔██╗ ██║██║  ███╗
██   ██║██║   ██║██║╚██╗██║██║   ██║██║     ██╔══╝      ╚════██║██╔══██║██╔══╝  ██╔══██║██╔══██╗██║    ██╔══██╗██║   ██║██║   ██║██╔═██╗ ██║██║╚██╗██║██║   ██║
╚█████╔╝╚██████╔╝██║ ╚████║╚██████╔╝███████╗███████╗    ███████║██║  ██║██║     ██║  ██║██║  ██║██║    ██████╔╝╚██████╔╝╚██████╔╝██║  ██╗██║██║ ╚████║╚██████╔╝
 ╚════╝  ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚══════╝╚══════╝    ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝    ╚═════╝  ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
", Color.GreenYellow, Color.Yellow, 1);
        }
        private static void SendMail(int item)
        {
            Tourist t = new Tourist();
            Console.ForegroundColor = Color.White;
            Console.WriteLine("\nPlease Enter your Email Id to confirm Booking: ");
            Console.ResetColor();
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (regex.IsMatch(input))
                {
                    Console.ForegroundColor = Color.White;
                    System.Console.WriteLine("\nPlease Wait while we confirm your Booking");
                    Console.ResetColor();
                    string mail = input;
                    List<Booking> book = (List<Booking>)JungleBL.SendMail(item);
                    System.Collections.IList list = book;
                    try
                    {
                        System.Net.Mail.MailMessage mailing = new System.Net.Mail.MailMessage();
                        System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");

                        mailing.From = new System.Net.Mail.MailAddress("junglesafarisprint2021@gmail.com");
                        mailing.To.Add(mail);
                        mailing.Subject = $"Safari Booking Confirmation";
                        for (int i = 0; i < list.Count; i++)
                        {
                            Booking b = (Booking)list[i];
                            mailing.Body = $"\n**** Thank you for booking a Safari ****\n\nHere are your booking details\n\nBooking ID: {b.Id}\n\nPark Name: {b.PName}\n\nSafari Name: {b.SName}" +
                                $"\n\nVehicle Name: {b.Vname}\n\nAmount to be Paid: Rs.{b.TotalCost}/-\n\n\n*********** Please carry your Id proof and Show Booking Id to Pay Cash***********" +
                                $"\n\nWe are looking forward for your vist\n\n\n--Team Jungle Safari Booking";
                        }
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("junglesafarisprint2021@gmail.com", "Asdfg@123");
                        SmtpServer.EnableSsl = true;

                        SmtpServer.Send(mailing);
                        //Console.ForegroundColor = Color.Green;
                        System.Console.WriteLine("\nPlease check your mail for Booking Information");
                        //Console.ResetColor();
                    }
                    catch (JungleException ex)
                    {
                        Console.ForegroundColor = Color.Yellow;
                        System.Console.WriteLine(ex.Message);
                        Console.ResetColor();

                    }
                }
            }
        }
    }
}









