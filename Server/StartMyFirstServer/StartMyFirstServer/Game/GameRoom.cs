using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf.Protocol;

namespace StartMyFirstServer.Game
{
    public class GameRoom
    {
        public static GameRoom Instance = new GameRoom();

        object _lock = new object();
        public int RoomId { get; set; }

        List<Player> _players = new List<Player>();
        //룸 안에 플레이어

        public void EnterGame(Player newPlayer)
        {
            if (newPlayer == null)
                return;

            lock(_lock){

                _players.Add(newPlayer);
                newPlayer.Room = this;
                {
                    S_EnterGame enterPacket = new S_EnterGame();
                    enterPacket.Player = newPlayer.Info;
                    //내정보 나한테
                    newPlayer.Session.Send(enterPacket);
                }

                //다른사람정보 나한테. 동기화 과정.
                {
                    S_Spawn spawnPacket = new S_Spawn();
                    foreach (Player p in _players)
                    {

                        if (newPlayer != p)
                        {
                            spawnPacket.Players.Add(p.Info);
                        }
                    }
                    newPlayer.Session.Send(spawnPacket);
                }
                

                //타인한테 내정보.
                {
                    S_Spawn spawnPacket = new S_Spawn();
                    spawnPacket.Players.Add(newPlayer.Info);
                    foreach(Player p in _players)
                    {
                        if (newPlayer != p)
                            p.Session.Send(spawnPacket);
                    }
                }
                
            }



        }

        public void LeaveGame(int playerId)
        {
            lock (_lock)
            {
                Player player = _players.Find(p => p.Info.PlayerId == playerId);
                if (player == null)
                    return;
                _players.Remove(player);
                player.Room = null;

                //본인
                {
                    S_LeaveGame leavePacket = new S_LeaveGame();
                    player.Session.Send(leavePacket);
                       
                }

                //타인
                {
                    S_Despawn despawnPacket = new S_Despawn();
                    //플레이어 아이디만 전송.
                    despawnPacket.PlayerIds.Add(player.Info.PlayerId);
                    foreach(Player p in _players)
                    {
                        if(player!=p)
                            p.Session.Send(despawnPacket);
                    }
                }
            }
        }

    }
}
