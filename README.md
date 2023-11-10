# jellyfish-backend-ddd

## Intro ##
Recreation of Jellyfish Backend for the Messaging Application Jellyfish for Android and iOS.
The Backend is fully written in domain driven design with clean architecture.



## Technology Stack ##
Backend  		| Web-Frontend			| Mobile Application
------------- 	| -------------		| -------------
C#/NET 7  		| C#/NET 7 			| C#/NET 7
ASP NET  		| Blazor Server 	| .NET MAUI
MySql 8.0.35   	| Razor Pages 		| 
SignalR  		| Mudblazor UI Components 	| 
Docker  		| 
EF Core  		| 

Note: Messaging Mobile App (will be added soon and is part of another repo that is currently private).

## Testing ##
Unit testing is my common way to test my methods and process flows.
Preface: I know not every method is testet, the reason why is: you can over engeneer and you can over test. My common way is the middle of path. Critical processes and flows or methods will be tested.
The unittest behaviour is: `arrange, act, assertion`. The common way in softwaretesting.

## Motivation ##
The motivation for this project is to test NET MAUI on the upper named mobile platforms.
A side effect of this project is to give everyone the oportunity to host a messaging infrastructure by your own.
Without big amount of used hardware resources e.g. running the infrastructure on raspberry pi.

## Future ##
The security aspect is important too:
The last steps before the first prod version will be the implementation of end to end encryption and the protection of privacy for each user through forgetable
payload pattern.
Anorher **future** sight is the implementation of **artificial inteligence (AI)** support bots with google llama or chatgpt and the opportunity **to host the infrastructure in blockchain**.


## To Do ##

### Backend ###
- [ ] An uncompleted task
- [x] A completed task

### Web Frontend ###
- [ ] An uncompleted task
- [x] A completed task
- [ ] 
### Mobile Application ###
- [ ] An uncompleted task
- [x] A completed task


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

## Enhanced Entity Relation Diagramm ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/eer_db_screenshot1.png "EER Domain")
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/eer_db_screenshot2.png "EER Domain2")



