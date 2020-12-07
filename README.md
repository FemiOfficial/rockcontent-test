## RockContent - Like Feature

### Description
The like feature is a reusable feature component that can be integrated and used on any application. It is a REST Api that can be seen as a widget for Client Applications to manage likes on Articles or other resources on their platform.


### Technical Decision
Although, the reusable Like Feature component does not not handle Authentication, Authorization and other user management related features it is important to avoid spam on the Like Feature. These is done as described below:


### Technology Used
C#, .NET Core 3.1, XUnit, Docker, Microsoft SQL Server


### Installation
The application can installed and ran easily, either as a docker container or deploy to an IIS Server.

#### Docker Deployment
* Ensure you have docker installed properly from [Docker](https://docs.docker.com/engine/install/)
* Clone the repo `git clone https://github.com/FemiOfficial/rockcontent-test.git`
* Navigate to root folder of the project and run `docker-compose  up -d`, this creates docker containers for the app and MSSQL database
* Access app from your local machine using `http://localhost:8000//api/likefeature/ping`

