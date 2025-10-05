using System.Text;
using System.Threading.Tasks;

public class ServerHandler {
    private string serverAddress;
    private int serverPort;
    private string serverProtocol = "http";
    private HttpClient http;
    private string addr;
    private Uri server;


    public ServerHandler(string server) {
        var ip = server.Split(":");
        this.serverAddress = ip[0];
        this.serverPort = int.Parse(ip[1]);
        this.addr = serverProtocol + "://" + serverAddress + ":" + serverPort + "/";
        this.http = new HttpClient();
        this.server = new Uri(addr);
    }

    public void SendState(string state) {
        
        Console.WriteLine("GAME: Sending state to server at " + addr);
        var content = new StringContent(state, Encoding.UTF8, "application/json");
        http.PostAsync(server + "gamestate", content);
    }

    public async Task<string> GetState() {
        string state = await http.GetStringAsync(server + "gamestate");
        return state;
    }
}