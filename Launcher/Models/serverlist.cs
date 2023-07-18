using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TempDesigner.Models;

namespace Launcher.Models
{
    class serverlist
    {
    }

    public class ChivServerInfoTags
    {
        public string BuildId_s { get; set; }
        public string ServerName_s { get; set; }
        public string Region_s { get; set; }
        // Probably not needed. Before uncommenting make sure to parse strings -> bool/int
        //public bool OFFICIAL_b { get; set; }
        //public string Platform_s { get; set; }
        //public bool FFA_b { get; set; }
        //public int MS_i { get; set; }
        public string MapName_s { get; set; }
        //public int PingPort_i { get; set; }
        //public int QueryPort_i { get; set; }
        //public int T0_c { get; set; }
        //public bool pp_b { get; set; }
        //public int t1_c { get; set; }
    }
    [JsonSerializable(typeof(ChivServerInfoTags))]
    internal partial class ChivServerInfoTagsContext : JsonSerializerContext
    { }

    public class ChivServerInfo
    {
        public string Region { get; set; }
        public string LobbyId { get; set; }
        public int MaxPlayers { get; set; }
        public bool Official { get; set; }
        public string BuildVersion { get; set; }
        public string GameMode { get; set; }
        public List<string> PlayerUserIds { get; set; }
        public int RunTime { get; set; }
        public int GameServerState { get; set; }
        public ChivServerInfoTags Tags { get; set; }
        public int LastHeartbeat { get; set; }
        public string ServerHostname { get; set; }
        public string ServerIPV4Address { get; set; }
        public int ServerPort { get; set; }

        //public string PlayerCount
        //{
        //    get { return string.Format("{0} / {1}", PlayerUserIds.Count, )}
        //}
    }

    public class ServerListDatum
    {
        public int GameCount { get; set; }
        public int PlayerCount { get; set; }
        public List<ChivServerInfo> Games { get; set; }
    }

    public class ServerList
    {
        public ServerListDatum Data { get; set; }
        public bool Success { get; set; }
    }

    [JsonSerializable(typeof(ChivServerInfo))]
    internal partial class ChivServerInfoContext : JsonSerializerContext
    { }
}
