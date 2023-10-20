
#### 📝 What I am learning
https://learn.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/mapping-users-to-connections
https://learn.microsoft.com/en-us/training/modules/aspnet-core-signalr/2-what-is-signalr

- SignalR abstracts away the complexities of multiple real time protocols.

Such as:
1. WebSockets
2. Server sent events
3. Long polling
4. SignalR automatically picks the best of the above methods in that order.

- The SignalR server maps to a hub and can exist on local, in cloud, or with Azure SignalR service.
Server exposes both hub methods and events that clients can call (REMOTE PROCEDURES).

- Hub is where the stuff happens. Allows clients and servers to call methods on each other across machines.

- Users can be individual or also part of a group (groups may be useful for actions that affect all clients ex: we wish to uninstall a game from all machines)
- Groups have specified name which acts as its unique identifier.

- Single user can connect from multiple clients ex: mobile, browser, wpf, and get real time updates on all surfaces.
- BUT each connection is represented by a unique ID. Each client has a unique connection to the server.

- Clients are responsible for establishing a connection to the server through the HubConnection object. A client is any connected app ex: browser, mobile, desktop
#### Here's how to use
- ASP.NET Core SignalR client libary: Microsoft.AspNetCore.SignalR.Client
- This package allows clients to connect to SignalR Hubs




