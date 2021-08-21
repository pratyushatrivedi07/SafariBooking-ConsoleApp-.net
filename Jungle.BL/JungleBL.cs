using Jungle.DAL;
using Jungle.Entities;
using Jungle.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle.BL
{
    public class JungleBL
    {
        public static bool Register(User users)
        {
            return JungleDAL.Register(users);
        }
        public static (bool IsSuccess, string) Login(User users)
        {
            return JungleDAL.Login(users);
        }


        //---------------------------ALL LISTS----------------------------------------------
        public static List<Parks> ListParks()
        {
            return JungleDAL.ListParks();
        }
        public static List<Safari> ListSafari()
        {
            return JungleDAL.ListSafari();
        }
        public static List<Vehicle> ListVehicle ()
        {
            return JungleDAL.ListVehicle();
        }
        public static List<Gate> ListGates()
        {
            return JungleDAL.ListGates();
        }
        public static List<Role> ListRole()
        {
            return JungleDAL.ListRole();
        }
        public static List<IdProofs> ListProofs()
        {
            return JungleDAL.ListProofs();
        }

        //---------------------------ALL ADD ----------------------------------------------
        public static bool AddPark(Parks park)
        {
            return JungleDAL.AddPark(park);
        }
        public static bool AddGate(Gate gate)
        {
            return JungleDAL.AddGate(gate);
        }
        public static bool AddVehicle(Vehicle v)
        {
            return JungleDAL.AddVehicle(v);
        }
        public static bool AddSafari(Safari safari)
        {
            if (safari.Date < DateTime.Today)
            {
                throw new JungleException("\nAvailable Date cannot be in Past");
            }
            return JungleDAL.AddSafari(safari);
        }
        public static bool AddTourist(Tourist t)
        {
            return JungleDAL.AddTourist(t);
        }


        //---------------------------ALL UPDATE---------------------------------------------
        public static bool UpdatePark(Parks park)
        {
            return JungleDAL.UpdatePark(park);
        }
        public static bool UpdateVehicle(Vehicle v)
        {
            return JungleDAL.UpdateVehicle(v);
        }
        public static bool UpdateSafari(Safari safari)
        {
            if (safari.Date < DateTime.Today)
            {
                throw new JungleException("\nAvailable Date cannot be in Past");
            }
            return JungleDAL.UpdateSafari(safari);
        }
        public static bool UpdateGate(Gate gate)
        {
            return JungleDAL.UpdateGate(gate);
        }


        public static bool IsParkExists(Parks park)
        {
            return JungleDAL.IsParkExists(park);
        }
        public static bool IsSafariExists(Safari safari)
        {
            return JungleDAL.IsSafariExists(safari);
        }
        public static bool IsVehicleExists(Vehicle v)
        {
            return JungleDAL.IsVehicleExists(v);
        }
        public static bool IsGateExists(Gate gate)
        {
            return JungleDAL.IsGateExists(gate);
        }


        //---------------------------ALL DELETE---------------------------------------------
        public static bool DeletePark(Parks park)
        {
            return JungleDAL.DeletePark(park);
        }
        public static bool DeleteVehicle(Vehicle v)
        {
            return JungleDAL.DeleteVehicle(v);
        }
        public static bool DeleteSafari(Safari safari)
        {
            return JungleDAL.DeleteSafari(safari);
        }
        public static bool DeleteGate(Gate gate)
        {
            return JungleDAL.DeleteGate(gate);
        }


        //---------------------------ALL TOURIST'S---------------------------------------------
        public static List<Parks> ParkByLocation(string Location)
        {
            return JungleDAL.ParkByLocation(Location);
        }
        public static List<Safari> SafariByPark(int ParkId)
        {
            return JungleDAL.SafariByPark(ParkId);
        }
        public static (bool, int) AddBooking(Booking book)
        {
            return JungleDAL.AddBooking(book);
        }
        public static (bool, int) AddBookingO(Booking book)
        {
            return JungleDAL.AddBookingO(book);
        }
        public static List<Booking> BookingStatus(int bookId)
        {
            return JungleDAL.BookingStatus(bookId);
        }


        //------------------------------MISC METHODS---------------------------------------------
        public static List<Vehicle> VehicleByPark(int ParkId)
        {
            return JungleDAL.VehicleByPark(ParkId);
        }
        public static (bool, int) AddVehicleO(Vehicle v)
        {
            return JungleDAL.AddVehicleO(v);
        }
        public static double TotalCharges(int parkId, int sId, int vId, int num)
        {
            return JungleDAL.TotalCharges(parkId, sId, vId, num);
        }
        public static double TotalChargesO(int parkId ,int sId, int num)
        {
            return JungleDAL.TotalChargesO(parkId, sId, num);
        }
        public static List<Gate> GateByPark(int ParkId)
        {
            return JungleDAL.GateByPark(ParkId);
        }
        public static bool IsBookingExist(Booking book)
        {
            return JungleDAL.IsBookingExist(book);
        }
        public static List<Booking> SendMail(int item)
        {
            return JungleDAL.SendMail(item);
        }
    }
}
