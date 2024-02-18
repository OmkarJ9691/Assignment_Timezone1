How to run the project ->
1) Download project from Git (URL : )
2) Open project in Visual Studio 2022 and Build & Run the project.
3) Download Postman collection file (GuestAPI.postman_collection.json) present in the root path of applicaion and Import it in Postman App.
4) Test endpoint with dummy data.
5) In-Memory storage mechanism is used for this.
6) Clean architecture with CQRS+MediatR is used in the project.
7) Logs are storing in Log/

Any assumptions made ->


Any challenges faced and how they were addressed ->
1) It is little time consuming as I have to create everything from scrach.


Future improvements if given more time ->
1) We can create generic structure so that we do not need to pass hard-coded types in business layer. For example in future if we need to do crud operations on any other type we do not need to create separate class in business layer. This can be achived through Generic Architecture.
2) Can add more Unit test cases for corner-case scenarios.
3) We can do token based authentication in future.
