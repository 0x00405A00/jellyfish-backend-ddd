# jellyfish-backend-ddd

## Intro ##
Recreation of Jellyfish Backend for the Messaging Application Jellyfish for Android and iOS.
The Backend is fully designed and developed in **domain driven design** with **clean architecture**.
Communication from presentation to application over Meditr. library with command-query separation (cqs).
Currently the application is running as monolith, but the current architecture allows it to switch very fast to microservice with api gateway (ocelot, yarp etc.),command-query responsibility segregation and background database sync processed between read/write databases (optimistic locking with rowversioning) and ampq communication over rabbitmq to handle events over queues or publish/subscibe behaviours.




## Technology Stack ##
Backend  		| Web-Frontend			| Mobile Application
------------- 	| -------------		| -------------
C#/NET 7  		| C#/NET 7 			| C#/NET 7
ASP NET  		| Blazor Server 	| .NET MAUI
MySql 8.0.35   	| Razor Pages 		| 
SignalR  		| Mudblazor UI Components (https://mudblazor.com/) 	| 
Docker  		| 
EF Core  		| 

Note: Messaging Mobile App (will be added soon and is part of another repo that is currently private).

## Testing ##
Unit testing is my common way to test my methods and process flows.
Preface: I know not every method is testet, the reason why is: you can over engeneer and you can over test. My common way is the middle of path. Critical processes and flows or methods will be tested.
The unittest behaviour is: `arrange, act, assertion`. The common way in softwaretesting.

For end to end tests I used Postman with an import of generated swagger api documentation.

## Documentation ##
Currently only EER and Swagger Documentation.

## Deployment ##
Legacy over binary or with docker container.

## Motivation ##
The motivation for this project is to test NET MAUI on the upper named mobile platforms.
A side effect of this project is to give everyone the opportunity to host a messaging infrastructure by your own.
Without big amount of used hardware resources e.g. running the infrastructure on raspberry pi.

## Future ##
The security aspect is important too:
The last steps before the first prod version will be the implementation of end to end encryption and the protection of privacy for each user through forgetable
payload pattern.
Anorher **future** sight is the implementation of **artificial inteligence (AI)** support bots with google llama or chatgpt and the opportunity **to host the infrastructure in blockchain**.


## To Do ##


**Backend:**
- [ ] Password Reset Endpoint: Enable password reset via the "Edit User" feature; domain logic change: UpdateUser, updateuser command+handler update.
- [ ] Password encrytion in database with best practise encryption method
- [ ] Auth/Authorization: Match Jwt Data (Claims etc.) with the database, trigger events if token claims do not match roles from the database or if IP location (Geo IP for IPv4+v6 or User-Agent different from token creation) is vastly different. Notify users via email.
- [ ] File Upload / User Profile Pictures / Attachments from Jellyfish Messages or Media / Storing Strategy: Avoid storing in the database for performance reasons. Implement caching strategy, build up cache during backend start. Conduct virus checks using content and an external AI service (Azure= [link](https://azure.microsoft.com/en-us/products/ai-services/ai-content-safety), AWS) to detect uploads of violent media/pornography.
- [ ] Domain: Implement Chat Business Logic checks.
- [ ] Implementation of Presentation/Infrastructure/Application of Domain Entity Chat/Message/Userfriendship requests: Utilize SignalR to notify target users, etc.
- [ ] Forgettable Payload -> GDPR (German: DSGVO).
- [ ] End-to-End encryption for Jellyfish users.
- [ ] SSL Encryption for Backend.
- [ ] Rewrite some default ASP.NET Core response messages with a filter: e.g., HTTP error 422 or unauthorized response -> rewrite to JSONAPI error response ----------> Presentation.Extension.JsonApiResultExtension.
- [ ] FluentValidation: Prepare Validators with response handling for JSONAPI error response due to validation errors. CommandHandlerValidators already implemented: ValidationPipelineBehavior; only the validation rule needs definition in constructors of each CommandValidation.

**Web-Frontend:**
- [ ] Icons: [MudBlazor Icons](https://mudblazor.com/features/icons#icons).
- [ ] NavBar:
  - [ ] Features
  - [ ] Privacy
  - [ ] Help Section:
    - [ ] NavBar as Sidebar with Help points
    - [ ] Body with Search for Help points
    - [ ] Popular Articles
      - [ ] Android and iOS Help tabs under Help points
  - [ ] Blog:
    - [ ] Latest information about Jellyfish
  - [ ] Download:
    - [ ] Link to App stores
- [ ] Footer:
  - [ ] Jellyfish Icon
  - [ ] Copyright
  - [ ] Terms of Use and Privacy Policy
  - [ ] Social Media Icons with Channels

**Features:**
- [ ] Color and Blackmode (via MudBlazor Themes)
- [ ] Snackbar for notifications (any action like edit user, get notification of service x/y, etc.)
- [ ] SignIn Page with Password recovery function
- [ ] Impressum
- [ ] GDPR:
  - [ ] Cookie Disclaimer
- [ ] Dashboard.razor:
  - [ ] Charts:
    - [ ] Outgoing mails
    - [ ] Registered users compared to the previous month
    - [ ] Chart background slightly gray
- [ ] _Host.cshtml: Default Messages changing
- [ ] App.razor:
  - [ ] PageNotFound Page
- [ ] Signup.razor (Login + Register):
  - [ ] Component arrangement
  - [ ] Success page after registration
- [ ] Users.razor:
  - [ ] CRUD+Search (Search maybe with elastic search)
  - [ ] Password Reset Action separately
  - [ ] View User Profile Picture ([MudBlazor Image](https://mudblazor.com/components/image#playground))
  - [ ] Upload User Profile Picture ([MudBlazor FileUpload](https://mudblazor.com/components/fileupload#form-validation))
  - [ ] User Edit/Delete Audit Log (all events in a separate table)
  - [ ] View users' friends/friendship requests in-outcoming
- [ ] Blog:
  - [ ] CRUD Posts
  - [ ] Embedded Media
  - [ ] Blog: Load Posts vertically with MudBlazor vertical Progressbar (animated)
  - [ ] Blog: Posts look at with Timeline with left and right order
- [ ] Authentication: Timer in the Background that will be visible in UI when the token is only 5 minutes valid anymore, then the activation tracking will start. When the user triggers actions in UI, the token will be refreshed in the backend automatically.
- [ ] Session store: Store the last visited URL
- [ ] Own profil
  - [ ] View
  - [ ] Edit/Manage 

**Mobile App:**
- [ ] Remove old DTO and Model structure and old namespaces
- [ ] Jsonhandler
- [ ] Rewrite to new structure
- [ ] Message/user cache in SQLite, adapt to new structure
- [ ] Add new WebApiClient and SignalRClient (WebAPI client from WebFrontend into shared, new SignalR client (typed client) into shared for reusability)
- [ ] Msg-interceptor: Maintain the invoker chain for message/notification procedures (main messaging functionality)
- [ ] Rewrite target endpoint URLs to backend to new backend structure
- [ ] Chat: Messaging (UI+App backend)
- [ ] Chat+Friendlist: See profile
- [ ] Chat: Messaging (send attachments)

**General:**
- [ ] DNS Preconf, Windows hosts-file equal names like Docker
- [ ] License definition
- [ ] Terms of use
- [ ] Donation
- [ ] Manual for installation and execution


#  #
Stay tuned 👍. 


## Admin Panel Alpha 0.1 ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/admin-panel-alpha-prev.jpg "Admin Panel Alpha 0.1")

## Dashboard ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/dashboard-preview.png "Dashboard")

## User Management (Users.razor) w/ Pagination ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/pagination-preview.png "User Management")

## User Management (Users.razor) - Add/Edit ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/add-user-preview.png "Add/Edit User")

## User Management (Users.razor) - Delete ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/del-user-preview.png "Delete User(s)")

## VIDEO: Preview Transitioning Filter from Http Request to Linq Expression and finally to MySql Query via ORM (dynamically) / THUMBNAIL PNG BELOW (HREF) ##
[![Preview Transitioning](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/filter-data-request-transition-json-to-linq-to-mysql-query-thumbnail.png)](https://youtu.be/-IK_6MZd1MQ)

## Enhanced Entity Relation Diagram ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/eer_db_screenshot1.png "EER Domain")
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/eer_db_screenshot2.png "EER Domain2")

## Codemetrical analysis after adding MobileApp to solution ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/20231110_codemetric.png "CodemetricAfterAddMobileAppToSolution")



