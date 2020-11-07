# FourSquare-.Net-Core-API

# Features
1. ASP.Net Core 3.1
2. Code in C#
3. JWT Authentication
4. Signal R

# How to run the project

1. Clone the repo to your local machine
2. Open the solution in Visual Studio
3. Create a new database in SQL Server
4. Run the database scripts from the 'Database Scrpts' Folder on your new database
5. Update the connection string in 'WebAPI/appsettings.json'
6. Update the FourSquare app credentials in 'WebAPI/appsettings.json', with your own app credentials. 
  If you don't have a FourSquare app, you can create one here > https://developer.foursquare.com. Or you can use mine :)
  Note: There is an image request limit for a free FourSquare account. Limited to 50 images per day. So if you do 3 tests of 15 landmarks for a given city, then your limit will be reached. 
7. Open a new command terminal in Visual studio
7. Make sure your path is 'D:\Projects\Net Core Projects\FourSquare API .Net Core 3.1\WebAPI>'. If you not already in WebApi, then type 'cd WebApi'
8. Type 'dotnet run' to host the api

# End Points

You can test the end points using Postman. You may download Postman here > https://www.postman.com/.  Open Postman and test the end points listed below.

1. Fetch landmarks by a given location 
  End point: http://localhost:4000/landmarks/search/landmark/{0} 
  Update the parameter with your city of choice eg: Durban.  There is a slight issue with searching with 2 word names, like Durban North. Will resolve the issue and update later

2. Fetch all locations
  End point: http://localhost:4000/landmarks/locations 

3. Fetch photo details
  End point: http://localhost:4000/landmarks/photo/{0}
  update the parameter with a photo id that relates to FourSquare. eg.  http://localhost:4000/landmarks/photo/{0}
