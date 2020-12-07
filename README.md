## RockContent - Like Feature

### Description
The Like Feature is a reusable feature component that can be integrated and used on any application. It is a REST Api that can be seen as a widget for Client Applications to manage likes on Articles or other resources on their platform.

### Resources
[Postman_Documentation]https://documenter.getpostman.com/view/6547388/TVmQfbxK


### Technology Used
C#, .NET Core 3.1, XUnit, Docker, Microsoft SQL Server


### Technical Decision
Although, the reusable Like Feature component does not handle Authentication, Authorization and other user management related features it is important to avoid spam on the Like Feature. These is done as described below:


1. **Like Request Model:**
In this section, a decription of what is expected from clients is provided.

| Key      | Data Type | Description |
| ----------- | ----------- | ----------- |
| PostId      |  String | Resource (Article or Post) Identifier from client-side   |
| RequestUsername  | String | Username from client-side handling authentication (this captures who liked a particular post)   |
| ClientReferenceId  | String |Client Reference Id issued to clients integrating to the feature (unique) | 


2. **From Client Side Request:**
In this section, a decription of how components of the client request contributes to ensure spam (duplicated like request) is avoided using the **Token** and **RequestUsername**

| Value      | Request Content | Expected As | Descrption |
| ----------- |  ----------- | ----------- | ----------- |
| Token      |  Request Headers   | Base64(Hmac(ClientReferenceId)) |  This is calulated as a first level of verification using the a secret key issued to clients |
| RequestUsername      |  Request Body   | String | Username from client-side handling authentication (this ensures who liked a particular post) |


3. **From Server Side:** 
On the server-side, the **Request Origin IpAddress** and **Request Http User-Agent** are captured to ensure the like action is not taken twice from the same device by the same username.


### Installation
The application can installed and ran easily, either as a docker container or deploy to an IIS Server.

#### Docker Deployment
* Ensure you have docker installed properly from [Docker](https://docs.docker.com/engine/install/)
* Clone the repo `git clone https://github.com/FemiOfficial/rockcontent-test.git`
* Navigate to root folder of the project and run `docker-compose  up -d`, this creates docker containers for the app and MSSQL database
* Access app from your local machine using `http://localhost:8000//api/likefeature/ping`

#### IIS Server Deployment



### Optimizations
To effectively optimize the Like feature, although measures have been taken on the implementation level for all Read Queries on Read-only entities to have *No Tracking* thereby avoiding the overhead of tracking changes on the Database level. 
However, this only reduces response time on the query but a *Messaging Queue* is to be considered to further improve performance to handle millions of concurrent like requests with an asychronous processing. 


### Improvements
* Write more unit test and integration test on components of the Like feature.
* Implement a CI/CD Pipeline for change management and effective production deployment pipeline.




