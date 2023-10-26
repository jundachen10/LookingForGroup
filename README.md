#### What is this?
- LAN center (game cafe) administration app
#### What it do?
1. Admin front end that allows...
- the owner to see computers that are currently active and online
- can send commands to computers in the the center for ex: reboot, add remove games
- live dashboards to show metrics/state of the connected computers in the network
2. Client front end that allows...
- patrons to launch games seamlessly
- view catalogue of availble content to play

#### Architecture
1. Blazor server app that holds the signalR hub and admin front end
2. WPF desktop client app that lives in the tray
3. Microsoft SQL Server on Azure to store computer/user info as well as service the client front end
4. Next.js or Blazor pages single page application for customers


#### 📝 What I am learning

##### 10/26/23
- We want to use Blazor server app to set up the signalR hub and server side logic
- Here's how to connect to a database using entity framework in Blazor server,
1. nuget entity framework core
2. nuget entity framework core sqlserver (microsoft sql server)
- the concept of factory
1. use method AddDbContextFactor for dependency injection - the factory is injected into components and used to create new DbContext instances
