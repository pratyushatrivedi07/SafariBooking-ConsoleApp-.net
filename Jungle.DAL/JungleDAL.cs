using Jungle.Entities;
using Jungle.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Jungle.DAL
{
    public class JungleDAL
    {
        public static bool Register(User users)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(UserId) from Users where UserName like '{users.Username}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value > 0)
                    {
                        throw new JungleException("\nUser Already Exist");
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Insert into Users(UserName, Password, Name, RoleId) Values('{users.Username}', '{users.Password}', '{users.Name}', '{users.RoleId}')";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static (bool IsSuccess, string) Login(User users)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"SELECT u.UserName, u.Password, r.Name FROM Users u join Role r on u.RoleId = r.Id WHERE username = '{users.Username}' AND password='{users.Password}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        reader.Read();
                        return (true, (string)reader["Name"]);
                    }
                    else { return (false, null); }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }

        //---------------------------ALL ADD ----------------------------------------------
        public static bool AddPark(Parks park)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(ParkId) from Parks where Name like '{park.Name}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value > 0)
                    {
                        throw new JungleException("\nPark Already Exist");
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Insert into Parks(Name, Location, Fee) Values('{park.Name}', '{park.Location}',{park.Fee})";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool AddGate(Gate gate)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(GateId) from Gate where Name like '{gate.Name}' and ParkId = {gate.ParkId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value > 0)
                    {
                        throw new JungleException("\nGate Already Exist");
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Insert into Gate(Name, ParkId) Values('{gate.Name}', {gate.ParkId})";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool AddSafari(Safari safari)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(SafariId) from SafariDetail where SafariName like '{safari.Name}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value > 0)
                    {
                        throw new JungleException("\nSafari Already Exist");
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Insert into SafariDetail(SafariName,SafariDate,SafariTime,SafariCost, ParkId) values('{safari.Name}','{safari.Date}','{safari.SafariTime}','{safari.Cost}',{safari.ParkId})";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool AddVehicle(Vehicle v)
        {

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Insert into Vehicle(VType,Name,Capacity,EntryCost, ParkId) values('{v.VehicleType}','{v.Name}','{v.capacity}','{v.Cost}',{v.ParkId})";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static (bool,int) AddVehicleO(Vehicle v)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Insert into Vehicle(VType,Name,Capacity,EntryCost, ParkId) values('Own','{v.Name}','{v.capacity}',1000,{v.ParkId})";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string q = $"select Max(VId) from Vehicle";
                        SqlCommand command1 = new SqlCommand(q, connection);
                        var result = command1.ExecuteScalar();
                        int value = (int)result;

                        return (true, value);
                    }
                    else
                    {
                        return (false, 0);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }


        //---------------------------ALL LIST ----------------------------------------------
        public static List<Parks> ListParks()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "Select * from Parks";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Parks> p = new List<Parks>();
                    while (reader.Read())
                    {
                        Parks parks = new Parks();
                        parks.ParkId = (int)reader["ParkId"];
                        parks.Name = (string)reader["Name"];
                        parks.Location = (string)reader["Location"];
                        parks.Fee = Convert.ToDouble(reader["Fee"]);

                        p.Add(parks);
                    }
                    return p;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static List<Gate> ListGates()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "Select * from Gate";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Gate> g = new List<Gate>();
                    while (reader.Read())
                    {
                        Gate gates = new Gate();
                        gates.GateId = (int)reader["GateId"];
                        gates.Name = (string)reader["Name"];
                        gates.ParkId = (int)reader["ParkId"];

                        g.Add(gates);
                    }
                    return g;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static List<Safari> ListSafari()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "Select * from SafariDetail";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Safari> s = new List<Safari>();
                    while (reader.Read())
                    {
                        Safari safaris = new Safari();
                        safaris.SafariId = (int)reader["SafariId"];
                        safaris.Name = (string)reader["SafariName"];
                        safaris.Date = (DateTime)reader["SafariDate"];
                        safaris.SafariTime = (slot)Enum.Parse(typeof(slot),reader["SafariTime"].ToString());
                        safaris.Cost = Convert.ToDouble(reader["SafariCost"]);
                        safaris.ParkId = (int)reader["ParkId"];

                        s.Add(safaris);
                    }
                    return s;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static List<Role> ListRole ()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "Select * from Role";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Role> r = new List<Role>();
                    while (reader.Read())
                    {
                        Role roles = new Role();
                        roles.Id = (int)reader["Id"];
                        roles.Name = (string)reader["Name"];

                        r.Add(roles);
                    }
                    return r;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static List<Vehicle> ListVehicle()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "Select * from Vehicle where VType like 'Park'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Vehicle> v = new List<Vehicle>();
                    while (reader.Read())
                    {
                        Vehicle vehicles = new Vehicle();
                        vehicles.Id = (int)reader["VId"];
                        vehicles.Name = (string)reader["Name"];
                        vehicles.capacity = (Capacity)Enum.Parse(typeof(Capacity), reader["Capacity"].ToString());
                        vehicles.Cost = Convert.ToDouble(reader["EntryCost"]);
                        vehicles.ParkId = (int)reader["ParkId"];

                        v.Add(vehicles);
                    }
                    return v;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static List<IdProofs> ListProofs()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = "Select * from IdentityProof";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<IdProofs> i = new List<IdProofs>();
                    while (reader.Read())
                    {
                        IdProofs proofs = new IdProofs();
                        proofs.Id = (int)reader["IdentityId"];
                        proofs.Name = (string)reader["IdentityName"];

                        i.Add(proofs);
                    }
                    return i;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }


        //---------------------------ALL DELETE ----------------------------------------------
        public static bool DeleteSafari(Safari safari)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(SafariId) from SafariDetail where SafariId like {safari.SafariId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value == 0)
                    {
                        throw new JungleException("\nSafari does not exist");
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Delete from SafariDetail where SafariId = {safari.SafariId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool DeletePark(Parks park)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(ParkId) from Parks where Name like '{park.Name}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value == 0)
                    {
                        throw new JungleException("\nPark does not exist");
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Delete from Parks where Name = '{park.Name}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool DeleteVehicle(Vehicle v)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(VId) from Vehicle where VId like {v.Id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value == 0)
                    {
                        throw new JungleException("\nVehicle does not exist");
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Delete from Vehicle where VId = {v.Id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool DeleteGate(Gate gate)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(GateId) from Gate where Name like '{gate.Name}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value == 0)
                    {
                        throw new JungleException("\nGate does not exist");
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Delete from Gate where Name = '{gate.Name}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }


        //---------------------------ALL UPDATE ----------------------------------------------
        public static bool UpdatePark(Parks park)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Update Parks set Name = '{park.Name}', Location = '{park.Location}', Fee =  '{park.Fee}'where ParkId = {park.ParkId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool IsParkExists(Parks park)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(ParkId) from Parks where ParkId = {park.ParkId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value == 0)
                    {
                        throw new JungleException("\nPark does not exist");
                    }
                    else { return true; }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool IsSafariExists(Safari safari)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(SafariId) from SafariDetail where SafariId = {safari.SafariId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value == 0)
                    {
                        throw new JungleException("\nSafari does not exist");
                    }
                    else { return true; }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool UpdateSafari(Safari safari)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Update SafariDetail set SafariName = '{safari.Name}',SafariDate = '{safari.Date}',SafariTime = '{safari.SafariTime}',SafariCost = '{safari.Cost}', ParkId =  {safari.ParkId} where SafariId = {safari.SafariId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool IsGateExists(Gate gate)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(GateId) from Gate where GateId = {gate.GateId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value == 0)
                    {
                        throw new JungleException("\nGate does not exist");
                    }
                    else { return true; }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool UpdateGate(Gate gate)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Update Gate set Name = '{gate.Name}', ParkId =  {gate.ParkId} where GateId = {gate.GateId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }    
        public static bool IsVehicleExists(Vehicle v)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(VId) from Vehicle where VId = {v.Id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value == 0)
                    {
                        throw new JungleException("\nVehicle does not exist");
                    }
                    else { return true; }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool UpdateVehicle(Vehicle v)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Update Vehicle set VType = '{v.VehicleType}',Name = '{v.Name}', Capacity = '{v.capacity}', EntryCost = '{v.Cost}', ParkId =  {v.ParkId} where VId = {v.Id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }


        //---------------------------ALL TOURIST ----------------------------------------------
        public static List<Parks> ParkByLocation(string Location)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    //string query = $"Select * from Parks where Location = '{location}'";
                    string query = $"usp_ParkByLocation";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Location", Location);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Parks> p = new List<Parks>();
                    while (reader.Read())
                    {
                        Parks parks = new Parks();
                        parks.Name = (string)reader["Name"];

                        p.Add(parks);
                    }
                    return p;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static List<Safari> SafariByPark(int ParkId)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    //string query = $"Select * from SafariDetail where ParkId = '{parkId}'";
                    string query = $"usp_SafariByPark";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ParkId", ParkId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Safari> s = new List<Safari>();
                    while (reader.Read())
                    {
                        Safari safaris = new Safari();
                        safaris.SafariId = (int)reader["SafariId"];
                        safaris.Name = (string)reader["SafariName"];
                        safaris.Date = (DateTime)reader["SafariDate"];
                        safaris.SafariTime = (slot)Enum.Parse(typeof(slot), reader["SafariTime"].ToString());
                        safaris.Cost = Convert.ToDouble(reader["SafariCost"]);

                        s.Add(safaris);
                    }
                    return s;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static bool AddTourist(Tourist t)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Insert into Tourist(Name, Gender, DateOfBirth, MobileNo, City, Country, EmailId, IdentityName, IdentityNumber) Values('{t.Name}','{t.Gender}','{t.DateOfBirth.ToString("d")}','{t.MobileNo}','{t.City}','{t.Country}','{t.Email}','{t.IDName}','{t.IDNumber}')";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static (bool, int) AddBooking(Booking book)
        {

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    //Status,PId, SafariId, GateId, VehicleId,TotalCost

                    string query = $"Insert into Booking Values('Success',{book.ParkId}, {book.SafariId},{book.GateId},{book.VehicleId},{book.TotalCost})";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string q = $"select Max(Id) from Booking";
                        SqlCommand command1 = new SqlCommand(q, connection);
                        var result = command1.ExecuteScalar();
                        int value = (int)result;
                        return (true, value);
                    }
                    else
                    {
                        return (false, 0);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static (bool, int) AddBookingO(Booking book)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    //Status,PId, SafariId, GateId, VehicleId,TotalCost
                    string query = $"Insert into Booking Values('Success',{book.ParkId}, {book.SafariId},{book.GateId},{book.VehicleId},'{book.TotalCost}')";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string q = $"select Max(Id) from Booking";
                        SqlCommand command1 = new SqlCommand(q, connection);
                        var result = command1.ExecuteScalar();
                        int value = (int)result;
                        return (true, value);
                    }
                    else
                    {
                        return (false, 0);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static List<Booking> BookingStatus(int bookId)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    //string query = $"Select * from SafariDetail where ParkId = '{parkId}'";
                    string query = $"usp_BookingStatus";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", bookId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Booking> b = new List<Booking>();
                    while (reader.Read())
                    {
                        Booking books = new Booking();
                        books.Id = (int)reader["BookingId"];
                        books.Status = (string)reader["Status"];
                        books.PName = (string)reader["ParkName"];
                        books.Vname = (string)reader["VehicleName"];
                        books.SName = (string)reader["SafariName"];
                        books.Date = (DateTime)reader["Date"];
                        books.TotalCost = Convert.ToDouble(reader["TotalCost"]);

                        b.Add(books);
                    }
                    return b;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }


        //------------------------------MISC METHODS---------------------------------------------
        public static List<Vehicle> VehicleByPark(int ParkId)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    //string query = $"Select * from SafariDetail where ParkId = '{parkId}'";
                    string query = $"usp_VehicleByPark";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ParkId", ParkId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Vehicle> v = new List<Vehicle>();
                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle();
                        vehicle.Id = (int)reader["VId"];
                        vehicle.Name = (string)reader["Name"];
                        vehicle.capacity = (Capacity)Enum.Parse(typeof(Capacity), reader["Capacity"].ToString());
                        vehicle.Cost = Convert.ToDouble(reader["EntryCost"]);

                        v.Add(vehicle);
                    }
                    return v;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static List<Gate> GateByPark(int ParkId)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    //string query = $"Select * from SafariDetail where ParkId = '{parkId}'";
                    string query = $"usp_GateByPark";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ParkId", ParkId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Gate> g = new List<Gate>();
                    while (reader.Read())
                    {
                        Gate gates = new Gate();
                        gates.GateId = (int)reader["GateId"];
                        gates.Name = (string)reader["Name"];

                        g.Add(gates);
                    }
                    return g;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static double TotalChargesO(int parkId, int sId, int num)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query1 = $" select Fee from Parks where ParkId = {parkId}";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    connection.Open();
                    var count1 = command1.ExecuteScalar();
                    decimal PFee = (decimal)count1 * num;
                    connection.Close();

                    string query2 = $" select SafariCost from SafariDetail where SafariId = {sId}";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    connection.Open();
                    var count2 = command2.ExecuteScalar();
                    decimal SFee = (decimal)count2 * num;
                    connection.Close();

                    decimal totalcost = (decimal)PFee + (decimal)SFee + 1000;
                    double cost = Convert.ToDouble(totalcost);
                    return cost;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);

            }

            catch (InvalidOperationException ex)
            {

                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public static double TotalCharges(int parkId, int sId, int vId, int num)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query1 = $" select Fee from Parks where ParkId = {parkId}";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    connection.Open();
                    var count1 = command1.ExecuteScalar();
                    decimal PFee = (decimal)count1 * num;
                    connection.Close();

                    string query2 = $" select SafariCost from SafariDetail where SafariId = {sId}";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    connection.Open();
                    var count2 = command2.ExecuteScalar();
                    decimal SFee = (decimal)count2 * num;
                    connection.Close();

                    string query3 = $" select EntryCost from Vehicle where VId = {vId}";
                    SqlCommand command3 = new SqlCommand(query3, connection);
                    connection.Open();
                    var count3 = command3.ExecuteScalar();
                    decimal c3 = (decimal)count3;
                    connection.Close();

                    decimal totalcost = (decimal)PFee + (decimal)SFee + (decimal)c3;
                    double cost = Convert.ToDouble(totalcost);
                    return cost;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);

            }

            catch (InvalidOperationException ex)
            {

                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public static bool IsBookingExist(Booking book)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    string query = $"Select count(Id) from Booking where Id = {book.Id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    var value = (int)result;
                    if (value == 0)
                    {
                        throw new JungleException("\nNo Booking to view");
                    }
                    else { return true; }
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }
        public static List<Booking> SendMail(int item)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ToString();
                using (SqlConnection connection = new SqlConnection(connString))
                {

                    string query = $"usp_BookingStatus";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", item);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Booking> p = new List<Booking>();
                    while (reader.Read())
                    {
                        Booking b = new Booking();
                        b.Id= (int)reader["BookingId"];
                        //b.Status = (string)reader["Status"];
                        b.PName = (string)reader["ParkName"];
                        b.SName = (string)reader["SafariName"];
                        b.Vname = (string)reader["VehicleName"];
                        //b.Date = (DateTime)reader["Date"];
                        b.TotalCost = Convert.ToDouble(reader["TotalCost"]);


                        p.Add(b);
                    }
                    return p;
                }
            }
            catch (SqlException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                throw new JungleException(ex.Message);
            }
            catch (ArgumentException ex)
            {

                throw new JungleException(ex.Message);
            }
        }

    }
}
