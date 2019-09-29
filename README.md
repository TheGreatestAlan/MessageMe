# MessageMe
This is a system to send and receive text messages consisting of N clients and a server.  I've been running in to separate instances of visual studio using IIS Express.  The communication is bidirections using REST requests.  The server stores everything in memory and the clients currently only persist their client id outside of memory- thus everythign else will be lost once the service shuts down. 

Functional Overview:
The server stores client information within an in memory cache and receives and stores messages on a queue.  It then has a separate process that dequeues the messages and sends them out to known online clients.  If a message is unsuccessfully received by a client, the server marks the client as offline and requeues the message.

The Client is designed to go on and offline, upon coming online checks in with server and has status marked as such.  As I have little (read no) immediate knowledge about any kind of UI for the client I left it out.  (Well, I didn't really leave it out I fiddled around but realized I was eating too far into my time) I left interface with the client as REST calls to the client API.  You can get messages the client has received as well as send messages to the server as the client via a GET call to the /api/MessageClient and a POST to the /api/MessageClient/Send endpoints.  The Messages are a JsonSerialized Message object within the MessageMe/Model directory.  

To start up I believe the location of the Client registration needs to be changed to a valid directory on the machine in which the client is running on.  It is currently hardcoded withing the ClientConstants.cs file.  Everything else should pick up and run unless you have something else already listening on the ports configured. 

things I would like to enhance:
  - Unit testing of any kind really.  
  - I should probably check if the Queue is thread safe... my guess is no
  - Authorization and other kinds of security at the endpoints
  - Some sort of UI... I guess
  - Setting the clients to display and input a name within the messages instead of a guid
  - Currently it's possible to get messages out of order depending on when the client goes offline and comes back online, and the size of the queue.  So probably a Queue of queues that gets sent instead of individual messages?
  - Persisting Messages out of memory for both server and client
  - To scale this up, I'd separate the WebAPI, the queue would probably be a separate managed service, and there would probably be multiple workers pulling and sending messages off the queue.  Step after that is probably elastic set of scaling workers in proportion with size of queue
  - It's possible with this architecture to add blobs for pictures and videos, gifs and whatnot
  - I could put a response for the "Message seen" and "Currently Typing" notifications, though not sure if that would be in-time enough with the queue system
  - I also pushed up binary files to the repo.  Ooops.  Should probably go through and strip out anything that's not actual code.

Enjoy!
