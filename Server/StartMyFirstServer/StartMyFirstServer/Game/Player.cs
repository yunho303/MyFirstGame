using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf.Protocol;
using Server;

namespace StartMyFirstServer.Game
{
    public class Player
    {
        //내 정보를 들고 있도록.
        public PlayerInfo Info { get; set; } = new PlayerInfo();
        public GameRoom Room{get;set;}
        public ClientSession Session { get; set; }

        public int score { get; set; }
    }
}
