using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic; // for hashset dictionary below
using System.Linq; // for the Ienumerable method
using System.Threading.Tasks;



namespace BlazorServer.Hubs;

// This is the SignalR hub that lives within the BlazorServer app
// This is where we define our server side logic for real-time comms using SignalR
public class ChatHub : Hub //inherits from the signalR package
{
    private readonly LFGDataContext _context;
    private readonly IDictionary<int, string> _connections;

    public Task SendMessage(string user, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    // method: on connect show client ID

   
    // method: open steam app on all clients
    // method: open steam app on target machine
    // method: see if steam app is running on target machine
    // method: see all current instances of steam app opened
    // method: see current games being played
    // method: 

}


// We want to be able to ID each user that connects to the server/hub
// In-memory storage of user information using hashset dictionary
public class ConnectionMapping<T>
{
    private readonly Dictionary<T, HashSet<string>> _connections =
        new Dictionary<T, HashSet<string>>();

    // this class has a count property that gives unique users/keys that are connected.
    public int Count
    {
        get
        {
            return _connections.Count;
        }
    }

    // The Add method takes key and connect ID and adds it to the set of connections
    // associated with that user. If the user doesn't have any connections yet
    // it starts a new set for them. Also something about being thread safe with locks.
    public void Add(T key, string connectionId)
    {
        lock (_connections)
        {
            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections))
            {
                connections = new HashSet<string>();
                _connections.Add(key, connections);
            }

            lock (connections)
            {
                connections.Add(connectionId);
            }
        }
    }

    // GetConnections method returns all the connection IDs for that given user
    // if no connections it returns an empty list.
    public IEnumerable<string> GetConnections(T key)
    {
        HashSet<string> connections;
        if (_connections.TryGetValue(key, out connections))
        {
            return connections;
        }

        return Enumerable.Empty<string>();
    }

    // This remove method removes the connection ID from the user's connections
    // if this was the user's last connection then it removes the user from the dictionary.
    public void Remove(T key, string connectionId)
    {
        lock (_connections)
        {
            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections))
            {
                return;
            }

            // another lock to ensure thread safety (race conditions)
            lock (connections)
            {
                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    _connections.Remove(key);
                }
            }
        }
    }
}
