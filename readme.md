Docker setup Instructions 
1) Install Docker
2) Download and extract Take_Home_Challenge_Resources.zip 
3) Navigare to Take_Home_Challenge_Resources\database folder and run the commands
     a. docker build -t enpal-coding-challenge-db .
     b. docker run --name enpal-coding-challenge-db -p 5432:5432 -d enpal-coding-challenge-db
4) Go to Docker and verify the container is running
     
Instructios to run the application
1) Open appsettings.json file from Enpal.AppointmentBooking.API project and verify 
    DefaultConnection(connection string) varaible to make sure it points to the accurate database 
2) Restore Nuget packages for the solution
3) Build and Run the app
4) Go to http://localhost:3000/swagger/index.html to view the API documentation

Instructios to test the application
1) Goto Take_Home_Challenge_Resources\test-app 
2) Run the command npm install
3) Run the command npm test
```