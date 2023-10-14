using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Protocol;

namespace StartMyFirstServer.Game
{
    
    public class GameRoom
    {
        public static GameRoom Instance = new GameRoom();

        object _lock = new object();
        public int RoomId { get; set; }

        public Dictionary<int , Player> _players = new Dictionary<int, Player>();
        //룸 안에 플레이어
        public Dictionary<int, ItemInfo> _items = new Dictionary<int, ItemInfo>();
        int itemId = 0;
        int beWide = 0;
        public void MakeItem()
        {
            ++itemId;
            beWide+=3;

            _items.Add(itemId, new ItemInfo { ItemId = itemId, 
                PosX =new Random().Next(-450 < -beWide ? -beWide : -450 , 450 > beWide ? beWide : 450), 
                PosY = new Random().Next(-29, -1),
                PosZ = new Random().Next(-450 < -beWide ? -beWide : -450, 450 > beWide ? beWide : 450)
            });
            S_Makeitem makeItemPacket = new S_Makeitem();
            makeItemPacket.Iteminfo = _items[itemId];
            Broadcast(makeItemPacket);
        }

        public void GiveAllScoreInfo(int itemId)
        {
            //아이템 아이디정보와 전체 스코어정보. -1일경우 아이템 x 그냥 broad
            S_Score scorePacket = new S_Score();
            scorePacket.ItemId = itemId;
            foreach (Player p in GameRoom.Instance._players.Values)
            {
                scorePacket.ScoreInfo.Add(new ScoreInfo { PlayerId = p.Info.PlayerId, Score = p.score });

            }
            GameRoom.Instance.Broadcast(scorePacket);
        }

        public void GiveItemInfo(int playerId)
        {
            //특정 플레이어에게 아이템 정보 전송
            S_Giveiteminfo S_GiveiteminfoPacket = new S_Giveiteminfo();
            foreach (ItemInfo I in _items.Values)
            {
                S_GiveiteminfoPacket.Iteminfos.Add(I);
            }
            _players[playerId].Session.Send(S_GiveiteminfoPacket);
        }

        public void DeleteItem(int itemnum)
        {
            lock (_lock)
            {

                _items.Remove(itemnum);
            }
        }
        public void EnterGame(Player newPlayer)
        {
            if (newPlayer == null)
                return;

            lock(_lock){

                _players.Add(newPlayer.Info.PlayerId, newPlayer);
                newPlayer.Room = this;
                {
                    S_EnterGame enterPacket = new S_EnterGame();
                    enterPacket.Player =newPlayer.Info;
                    //내정보 나한테
                    newPlayer.Session.Send(enterPacket);

                    GiveItemInfo(newPlayer.Info.PlayerId);
                }

                //다른사람정보 나한테. 동기화 과정.
                {
                    S_Spawn spawnPacket = new S_Spawn();
                    foreach (Player p in _players.Values)
                    {

                        if (newPlayer != p)
                        {
                            spawnPacket.Players.Add(p.Info);
                        }
                    }
                    newPlayer.Session.Send(spawnPacket);
                }

                //스코어정보 나한테
                {
                    GameRoom.Instance.GiveAllScoreInfo(-1);
                }
                

                //타인한테 내정보.
                {
                    S_Spawn spawnPacket = new S_Spawn();
                    spawnPacket.Players.Add(newPlayer.Info);
                    foreach(Player p in _players.Values)
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
                Player player = null;
                _players.TryGetValue(playerId, out player);
                if (player == null)
                    return;
                _players.Remove(playerId);
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
                    foreach(Player p in _players.Values)
                    {
                        if(player!=p)
                            p.Session.Send(despawnPacket);
                    }
                }
            }
        }
        public void Broadcast(IMessage packet)
        {
            lock (_lock)
            {
                foreach (Player p in _players.Values)
                {
                    p.Session.Send(packet);
                }
            }
        }
    }
}
