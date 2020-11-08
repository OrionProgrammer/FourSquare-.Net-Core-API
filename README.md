# Features
1. ASP.Net Core 3.1
2. Code in C#
3. JWT Authentication for user registrations and login from client app
4. SignalR for fast delivery of image data to client app
5. Search by city name or GPS coordinates
6. Asynchronous Tasks
7. Dependancy Injection

# How to run the project

1. Clone the repo to your local machine
2. Open the solution in Visual Studio
3. Create a new database in SQL Server
4. Run the database scripts from the 'Database Scrpts' Folder on your new database
5. Update the connection string in 'WebAPI/appsettings.json'
6. Update the FourSquare app credentials in 'WebAPI/appsettings.json', with your own app credentials. 
  If you don't have a FourSquare app, you can create one here > https://developer.foursquare.com. Or you can use mine :)
  Note: There is an image request limit for a free FourSquare account. Limited to 50 images per day. So if you do 3 tests of 15 landmarks for a given city, then your limit will be reached. 
7. Make sure to do a clean and build
8. Open a new command terminal in Visual studio
9. Make sure your path is pointing to the WebApi project. If you not already in WebApi, then type 'cd WebApi'
10. Type 'dotnet run' to host the api

# End Points

You can test the end points using Postman. You may download Postman here > https://www.postman.com/.  Open Postman and test the end points listed below.

1. Fetch landmarks by a given location 
  <br/>End point [Get]: http://localhost:4000/landmarks/search/landmark/{location}/{userid}
  <br/>Update the parameter {location} with your city of choice eg: Durban or a set of GPS coordinates eg: 29.8587, 31.0218
  <br/>Update the parameter {userid} with a userId. Supply 0 for anonymous user 
  <br/>Note: Testing this end point via Postman or any other API test software, will return a status code of '200 OK', however, you will not see the results as the reults are sent back to the client side app using SignalR

2. Fetch all locations
 <br/>End point [Get]: http://localhost:4000/landmarks/locations 

3. Fetch photo details
  <br/>End point [Get]: http://localhost:4000/landmarks/photo/{id}
  <br/>Update the parameter with a photo id that relates to FourSquare
  
4. Fetch location by User from local database
  <br/>End point [Get]: http://localhost:4000/landmarks/user-location/{userid}
  <br/>Update the parameter with a userid
  <br/>Note: This method requires authentication. To test via Postman, you may decorate the method with [AllowAnonymous] and rebuild the solution, else pass a valid auth token     in the header
