# TrippieApi - Personal Tourist Attraction Trip Generator 🗺️

TrippieApi is a .NET Web API in early development stage, designed to generate personalized tourist attraction trips around selected location. It leverages the Mapbox API to create curated trips based on your preferences. Explore and discover interesting places, landmarks, and attractions with ease.

<hr>

## How to run ⚡

#### To run this API on your local machine you will need to do the following:

1. Clone this repository to your local machine
    
        $ git clone https://github.com/laa66/statly-backend-api.git

2. Set ``MAP_API_PUBLIC_TOKEN`` environment variable with public token from your [mapbox](https://account.mapbox.com/) account 

3. Make sure you have SQL Server installed on your machine.
Update the connection string in the appsettings.json file to point to your SQL Server.

4. Run Entity Framework Migrations.

        $ dotnet restore
        $ dotnet ef database update

5. Run TrippieApi
    
        $ dotnet build
        $ dotnet run


#### Access the API in your web browser at ``http://localhost:5023``
#### You can also explore API enpoints using Swagger UI at ``http://localhost:5023/swagger``

## How to use 🗺️

1. Try to create your account with POST request to /api/user endpoint with appropriate body

2. Explore API endpoints

## Features 📌
#### Here, you can check app features:

* Enable users to create accounts, providing a personalized experience within the application.

* Implement the functionality to delete user accounts when necessary.

* Display a list of users within the application.

* Showcase the trips created by users and provide the ability to delete them.

* Allow users to create routes by searching for selected attractions around a specified point on the map using the Mapbox API.

* Optimize routes through efficient nearest neighbor search using KD-tree algorithms.

## Built with 🔨

#### Technologies & tools used:

- C#
- .NET CORE
- ASP.NET CORE
- Entity Framework Core
- KdTree
- MS SQL
- Mapbox API
- Visual Studio Code
- SQL Server Management Studio
- SwaggerUI
- XUnit
- Moq
- Postman


## To-do 💡

1. Create and integrated Client for better user experience
2. Implement user navigation along the generated route
3. Add text and voice trip-point description
