# FlowSocial.Api
 social media API built with Clean Architecture using .NET

FlowSocial API is a lightweight social media platform API built using .NET and follows the principles of Clean Architecture. It includes functionality for user management, authentication, posts, messages, followers, stories, and roles. The project is designed with scalability, maintainability, and testability in mind.

## Features
Authentication & Authorization

User Registration & Login

Password Management: Forget and Change Password

Role-based Access Control

User Follow System

Follow/Unfollow Users

List Followers and Following for a User

Messaging

Send and Receive Messages

Mark Messages as Read

Edit or Delete Messages

Posts

Create, Edit, Delete, and Retrieve Posts

Like and Comment on Posts

Remove Likes and Comments

Stories

Add and View User Stories

View Stories from Followed Users

Delete Stories

## Clean Architecture

The project is organized into four layers following Clean Architecture principles:

Domain Layer: Contains core business logic and domain entities.

Application Layer: Holds application logic, such as use cases and services.

Infrastructure Layer: Deals with data access, external API integration, and persistence.

API Layer: Exposes the API endpoints for client interaction.

##Getting Started

Prerequisites
.NET 8 SDK
SQL Server (or any other database)
Visual Studio or any other C# IDE
Installation
Clone the repository:

bash
Copy code
git clone https://github.com/your-username/FlowSocial-API.git
Navigate to the project directory:

bash
Copy code
cd FlowSocial-API
Restore dependencies:

bash
Copy code
dotnet restore
Update the appsettings.json file with your database connection string.

Apply migrations:

bash
Copy code
dotnet ef database update
Run the application:

bash
Copy code
dotnet run
# API Endpoints
Account
![image](https://github.com/user-attachments/assets/3c2e405a-5ed8-4fc5-a6c9-681c449de84b)


![image](https://github.com/user-attachments/assets/d812cf31-5094-40ec-85b3-5bdb29cd54c0)



![image](https://github.com/user-attachments/assets/db4a0289-5cfc-4af3-abda-d2903d8d13b1)
![image](https://github.com/user-attachments/assets/1a7c7b36-8dcd-40c1-b186-f0be05035812)
![image](https://github.com/user-attachments/assets/51598dc8-72b5-4a43-a907-6d0db2a9024a)


Contributing
Contributions are welcome! Please fork this repository and submit pull requests.

License
This project is licensed under the MIT License.


