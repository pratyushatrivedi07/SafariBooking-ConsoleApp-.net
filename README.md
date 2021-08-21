# Safari-Booking-Console-App-.Net
This project is aimed at developing an online Jungle Safari Booking Management System for Forest Department. It is a .Net Console Application that can be used to Book Jungle Safari for various Tiger Reserves and Sanctuaries. Based on the roles (Admin, Tourist) the user can perform and access different features of the application such as Book the Safari (Tourist), add, update or delete parks or safaris (Admin). This system also uses SQL to store the Users, Parks, Safaris, etc.
<br />

There are two categories of users who would access the system viz. “Tourists” & “Admin”. Each one of them would have some exclusive privileges, for e.g.:
<br />
1. Tourist is be able to:
    <br />
    a. Register into the system.
    <br />
    b. Login to the system using its credentials.
    <br />
    c. Search Tiger Reserves or Sanctuaries and their Safari details.
    <br />
    d. Book Safari.
    <br />
    e. View Booking Status
    <br />
    <br/>
2. Admin is able to:
    <br />
    a. Register into the system.
    <br />
    b. Login to the system using its credentials.
    <br />
    c. Add/Modify/Delete Sanctuaries, Safaris, Countries, Gender, VehicleTypes
    <br />
    
    
    
    #### The Project follows 3 layer architecture:
    The Presentation Layer (PL) interacts with the Business Layer (BL) which interacts with the Data Access Layer (DAL) which Insert/Update/Delete/Fetches data from the database and follows the same route i.e. from DAL-BL-PL and shows the output in the Console.
    
