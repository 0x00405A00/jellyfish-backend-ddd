<img src="https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/jellyfish_image.svg" align="right" height="256" width="256" />

# Jellyfish Mobile Application

## Intro ##
The project focuses on recreating the Jellyfish Backend, a critical component of the Messaging Application Jellyfish designed for Android and iOS platforms. The backend is meticulously developed following the principles of domain-driven design and clean architecture. Communication within the system is orchestrated using the Meditr library, emphasizing command-query separation (CQS) for improved code clarity and separation of concerns.

While the current deployment operates as a monolith, the architecture is inherently flexible. It can swiftly adapt to a microservices paradigm with the incorporation of tools such as API gateways (e.g., Ocelot, YARP), command-query responsibility segregation (CQRS), and background database synchronization processes. The latter employs optimistic locking with row versioning to ensure consistency between read and write databases.

In anticipation of future enhancements, the architecture envisions the implementation of asynchronous communication through AMQP over RabbitMQ. This will empower the system to efficiently handle events through queues, laying the groundwork for robust publish/subscribe behaviors.

## Technology Stack ##
  Backend  		| Web-Frontend			| Mobile Application
------------- 	| -------------		| -------------
C#/NET 7  		| C#/NET 7 			| C#/NET 7
ASP NET  		| Blazor Server 	| .NET MAUI
MySql 8.0.35   	| Razor Components 		| 
SignalR  		| Mudblazor UI Components (https://mudblazor.com/) 	| 
Docker  		| 
EF Core  		| 

<div align="left">
  <img src="https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/Microsoft_.NET_logo.svg.png" height="96" width="96" />
  <img src="https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/blazor.svg" height="96" width="96" />
  <img src="https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/MySQL_logo.svg" height="96" width="96" />
  <img src="https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/Logo_C_sharp.svg" height="96" width="96" />
  <img src="https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/postman-icon.svg" height="96" width="96" />
  <img src="https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/signalr.svg" height="96" width="96" />
  <img src="https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/docker-svgrepo-com.svg" height="96" width="96" />
  <img src="https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/mudblazor-icon.svg" height="96" width="96" />
</div>

## Testing ##
- **Unit Testing Approach:**
  - Unit testing is a fundamental practice applied in this project to verify the correctness and functionality of methods and process flows. While it is acknowledged that not every method is individually tested, a balanced approach is adopted. The principle is to avoid over-engineering and over-testing, finding a middle ground that ensures critical processes and methods are thoroughly tested.
  - The unit testing behavior adheres to the standard `arrange, act, assertion` methodology, which is a common and effective approach in software testing. This methodology provides a structured framework for setting up the test conditions, executing the specific action, and validating the expected outcomes.

- **End-to-End Testing with Postman:**
  - End-to-end testing is conducted using Postman, leveraging the imported generated Swagger API documentation. Postman serves as a powerful tool for comprehensive API testing, allowing for the verification of the entire system's functionality. The integration of Swagger API documentation enhances the efficiency of the testing process by providing a clear and interactive interface for exploring and validating API endpoints.

## Documentation ##
- **Extensive Documentation:**
  - The project places a strong emphasis on comprehensive documentation to facilitate ease of use and understanding for all stakeholders. Currently, the documentation includes:
    - **Entity-Relationship Diagram (EER) Documentation:**
      - A detailed EER documentation has been prepared to provide insights into the data model and relationships within the system. This documentation serves as a valuable resource for developers, aiding in a clear understanding of the underlying database structure.
    - **Swagger Documentation:**
      - The project leverages Swagger Documentation to ensure detailed and interactive API documentation. Swagger provides an intuitive and user-friendly interface for exploring and understanding the available API endpoints, parameters, and responses. This documentation is essential for developers, enabling seamless integration and interaction with the provided APIs.

## Deployment ##
Legacy over binary or with docker container.

## Project Motivation and Objectives ##
- **Testing NET MAUI on Multiple Mobile Platforms:**
  - The primary motivation behind initiating this project is to rigorously test NET MAUI on various mobile platforms. This involves comprehensive testing and optimization to ensure seamless performance and compatibility across the specified platforms, contributing valuable insights to the NET MAUI community.
- **Empowering Users to Host Their Own Messaging Infrastructure:**
  - One of the key objectives of this project is to empower users by providing them with the opportunity to host their messaging infrastructure independently. This aims to democratize the accessibility of messaging services, allowing users to take control of their communication environment.
- **Optimal Resource Utilization:**
  - A noteworthy side effect of this initiative is the focus on enabling messaging infrastructure hosting with minimal hardware resources. Specifically, the project targets efficient utilization, allowing users to run the infrastructure on accessible hardware like Raspberry Pi. This emphasis on resource efficiency aligns with the project's commitment to accessibility and sustainability.

## Security and Future Features ##
- **End-to-End Encryption and Privacy Protection:**
  - Ensuring the security of user data is paramount. The upcoming steps before the release of the first production version will include the meticulous implementation of end-to-end encryption. This will provide a robust layer of protection for sensitive user information. Additionally, the adoption of the forgettable payload pattern will further enhance privacy by minimizing data retention, aligning with privacy best practices.
- **AI Support Bots:**
  - Embracing the future of user interaction, there are plans to integrate artificial intelligence (AI) support bots into the system. Consideration is given to leveraging advanced AI technologies such as Google Llama or ChatGPT. These AI-driven bots will enhance user engagement, streamline support processes, and contribute to an intelligent and responsive user experience.
- **Blockchain Infrastructure:**
  - Exploring cutting-edge technologies, there is a forward-looking vision to investigate the feasibility of hosting the system's infrastructure on the blockchain. The benefits of blockchain technology, including enhanced security, decentralization, and transparency, will be assessed. This forward-thinking approach aims to future-proof the system and align it with emerging trends in secure and decentralized architectures.

## To Do ##

**Backend:**
- [ ] Docker.compose for JellyfishBackend, PostgreSql
- [ ] RateLimiter for specific actions
- [X] IModelBinder+IModelBinderProvider for HttpQuery and HttpBody to resolve and validate data that is inside of 'ApiDataTransferObject<T>'
- [x] CRUD + Search Filter (Transition from http request (json) to dto filter model to linq expression to sql)
- [x] Linq Expressions to sql: override/create operators in value objects and Domain models and translate them to linq expressions
- [X] Cache Functionality (currently IMemoryCache)
- [x] Password Reset Endpoint: Enable password reset via the "Edit User" feature; domain logic change: UpdateUser, updateuser command+handler update.
- [ ] Password encrytion in database with best practise encryption method
- [x] Auth/Authorization: Match Jwt Data (Claims etc.) with the database
  - [ ] Trigger events if token claims do not match roles from the database or if IP location (Geo IP for IPv4+v6 or User-Agent different from token creation) is vastly different. Notify users via email.
- [ ] File Upload / User Profile Pictures / Attachments from Jellyfish Messages or Media / Storing Strategy: Avoid storing in the database for performance reasons. Implement caching strategy, build up cache during backend start. Conduct virus checks using content and an external AI service (Azure= [link](https://azure.microsoft.com/en-us/products/ai-services/ai-content-safety), AWS) to detect uploads of violent media/pornography.
  - [x] Chain Of Responsibility for virus and inadmissible/violent content (abstraction implemented)
- [x] Domain: Implement Chat Business Logic checks.
- GDPR: Module for GDPR to make all GDPR actions available (reporting, deletion etc.)
- [ ] Implementation of Presentation/Infrastructure/Application of Domain Entity Chat/Message/Userfriendship requests: Utilize SignalR to notify target users, etc. -> Event Handler
- [ ] Forgettable Payload -> GDPR (German: DSGVO).
- [ ] End-to-End encryption for Jellyfish users. Sequence: Users that will interact  to each other will share the Public Keys. When a message will be created by User-A, the message will be encrypted with the public key of User-B. The message will send to backend and will be stored (encrypted) in the database. User-B received the message over signalr or polls all unacknowleged messages from backend (polling by reconnect, when the connection is gone e.g. lost connection to radio cell). User-B acknowledge the message to backend (received rake will be  shown in User-A's App). User-B decrypt the message with the private key. 
  - [ ] Delete messages that are successfully delivered to target
- [ ] SSL Encryption for Backend.
- [x] Swagger Documentation
- [x] Rewrite some default ASP.NET Core response messages with a filter: e.g., HTTP error 422 or unauthorized response -> rewrite to JSONAPI error response ----------> Presentation.Extension.JsonApiResultExtension.
- [x] FluentValidation: Prepare Validators with response handling for JSONAPI error response due to validation errors. CommandHandlerValidators already implemented: ValidationPipelineBehavior; only the validation rule needs definition in constructors of each CommandValidation.
- [ ] Chat Bounding Context implementations:
    - [x] POST: /api/v1/chat, Create chat  (only with friends, Domain-Logic requirements defined the rule that a chat needs at least one message to exists)
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [ ] Domain (removed completion state because of New Business rule)
        - [ ] Implementation
        - [ ] Test (Unit-Test)
    - [x] GET: /api/v1/chat/{chatId}, Gets the chat
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] PUT: /api/v1/chat/{chatId}, Update chat informations such as description, name
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] DELETE: /api/v1/chat/{chatId}, Delete chat
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [ ] PUT: /api/v1/chat/{chatId}/picture/{messageId}, Sets the chat picture
      - [ ] Presentation
        - [ ] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [ ] Command/Query
        - [ ] Validation (Fluentvalidation)
        - [ ] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] PUT: /api/v1/chat/{chatId}/member/{userId}, Add user to chat (new member)
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] DELETE: /api/v1/chat/{chatId}/member/{userId}, Remove member from chat
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] PUT: /api/v1/chat/{chatId}/admin/{userId}, Assign admin action
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] DELETE: /api/v1/chat/{chatId}/admin/{userId}, Revoke admin action
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] POST: /api/v1/chat/{chatId}/message, Message create action
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] PUT: /api/v1/chat/{chatId}/message/{messageId}, Message update action
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] DELETE: /api/v1/chat/{chatId}/message/{messageId}, Message delete action
      - [x] Presentation
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [ ] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
- [x] User Bounding Context implementations:
    - [x] POST: /api/v1/user/register, Register user
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] POST: /api/v1/user/password/reset/request, Creates a password reset requests
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] POST: /api/v1/user/password/change/{id?}, Change password endpoint for admin UI
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] POST: /api/v1/user/password/reset/{base64Token}, Resets password with user received password reset url (link from mail)
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] POST: /api/v1/user/activation/{base64Token}, Activate user with them received activation url (link from mail)
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] GET: /api/v1/user/user-types, Get all available user types
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] GET: /api/v1/user/messenger/{guid}, Get messenger profile from user (less information than default user profile)
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] POST: /api/v1/user/friend/request, Create friendship request
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] DELETE: /api/v1/user/friend/request, Remove friendship request
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] GET: /api/v1/user/friend/request, Get friendship requests
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] POST: /api/v1/user/friend/request/accept, Accept friendship request
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] GET: /api/v1/user/friend, Get Friends
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] DELETE: /api/v1/user/friend, Remove friend 
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] PUT: /api/v1/user/{id}/profile-picture, Add profile picture
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] DELETE: /api/v1/user/{id}/profile-picture, Remove profile picture
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] PATCH: /api/v1/user/{id}/role, Assign role to user
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] DELETE: /api/v1/user/{id}/role, Revoke role from user
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] POST: /api/v1/user/, Create a user
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] GET: /api/v1/user/{id}, Get a user
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] PUT: /api/v1/user/{id}, Update a user
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
    - [x] DELETE: /api/v1/user/{id}, Delete a user
      - [x] Presentation
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [x] Infrastructure
        - [x] Implementation
        - [x] Test (Grey Box Test)
      - [ ] Application
        - [x] Command/Query
        - [x] Validation (Fluentvalidation)
        - [x] Handler
        - [ ] EventHandling
      - [x] Domain
        - [x] Implementation
        - [x] Test (Unit-Test)
  

**Web-Frontend:**
- [ ] Check all http request to backend and catch all state above all errors, so http codes 4xx and 5xx
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
  - [x] Jellyfish Icon
  - [ ] Copyright
  - [ ] Terms of Use and Privacy Policy
  - [x] Social Media Icons with Channels

***Features (from Web-Frontend):***
- [ ] Snackbar for notifications (any action like edit user, get notification of service x/y, etc.) => https://mudblazor.com/components/snackbar#5ac08464-80c3-4c34-8cac-24f0947275e7
- [x] SignIn Page with Password recovery function
- [ ] Impressum
- [ ] GDPR:
  - [ ] Cookie Disclaimer
- [ ] Dashboard.razor:
  - [ ] Charts:
    - [x] Outgoing mails
    - [x] Registered users compared to the previous month
    - [ ] Chart background slightly gray
- [ ] _Host.cshtml: Default Messages changing
- [ ] App.razor:
  - [ ] PageNotFound Page
- [x] Signup.razor (Login + Register):
  - [x] Component arrangement
  - [ ] Success page after registration
- [ ] Users.razor:
  - [x] CRUD+Search (Search maybe with elastic search)
  - [x] Password Reset Action separately
  - [ ] View User Profile Picture ([MudBlazor Image](https://mudblazor.com/components/image#playground))
  - [ ] Upload User Profile Picture ([MudBlazor FileUpload](https://mudblazor.com/components/fileupload#form-validation))
  - [ ] User Edit/Delete Audit Log (all events in a separate table)
  - [ ] View users' friends/friendship requests in-outcoming
- [ ] Health.razor:
  - [ ] Prettify the healthcheck view (currently default healthcheck view as presentation)
- [ ] Blog:
  - [ ] CRUD Posts
  - [ ] Embedded Media
  - [ ] Blog: Load Posts vertically with MudBlazor vertical Progressbar (animated)
  - [ ] Blog: Posts look at with Timeline with left and right order
- [x] Authentication: Timer in the Background that will be visible in UI when the token is only 5 minutes valid anymore, then the activation tracking will start. When the user triggers actions in UI, the token will be refreshed in the backend automatically.
- [x] Session store: Store the last visited URL
- [ ] Own profil
  - [ ] View
  - [ ] Edit/Manage 


**Mobile App:**
- [x] Remove old DTO and Model structure andold namespaces
- [ ] Rewrite to new structure
- [ ] Message/user cache in SQLite, adapt to new structure
- [x] Add new WebApiClient and SignalRClient (WebAPI client from WebFrontend into shared, new SignalR client (typed client) into shared for reusability)
- [ ] Msg-interceptor: Maintain the invoker chain for message/notification procedures (main messaging functionality)
- [ ] Rewrite target endpoint URLs to backend to new backend structure
- [ ] Chat: Messaging (UI+App backend)
- [ ] Chat+Friendlist: See profile
- [ ] Chat: Messaging (send attachments)
- [ ] Standalone characteristics opposite to WhatsApp etc.: 
1. Mark messages as not able to screenshot: These messages a blured by Screenshot creation. Same by profile picture.
2. Group calenders: Planing private activities together in a Groupchat calender.
3. Not caring about sharing: Mark messages as not shareable, to make no care about not permitted share :)
4. Ability to host the messaging infrastructue by your own. No dependency to others!
5. Against violence: Todays private chats  like Telegramm are abused by people for violence and adult content. So jellyfish brings the ability to avoid the share for such content. Machine Learning algorithm evaluate the Media that will be shared over the messenger. When violent content is recognized the message sent will be avoid. ** Requires Azure AI Vision or similar ML-Algorithm **
6. Location share with live track: unnessesary if in groups or private chats, your movements will be streamed live to the chat members when the spectator Mode is on (only availble in live track map).
7. Oblivion: The right to forget is a cornerstone of todays GDPR. By removing the Account all data (also chat messages that are shared with friends) will be removed. Of course the media too. Im aware that a deletion of Media that is sent to chat members is not the today practice but every human has the right that his created data can be forgotten.
8. Polls/Votes: In example to WhatsApp: Votes will be also available in jellyfish.


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
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/dashboard-preview-updated.png "Dashboard")

## User Management (Users.razor) w/ Pagination and Search ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/admin-ui-users-list-new.png "User Management")
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/admin-ui-users-list-new-search.png "User Search")

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


## Continious Integration workflow with Github Action ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/github-action-continous-integration.png "CI")

## Testing from Core (Use cases & Domain) ##
![Alt text](https://github.com/0x00405A00/jellyfish-backend-ddd/blob/main/Presentation/Preview-Media/unit-tests-approach.png "TestApproachUnitTestForCore")
