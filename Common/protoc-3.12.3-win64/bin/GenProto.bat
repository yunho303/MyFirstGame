protoc.exe -I=./ --csharp_out=./ ./Protocol.proto 
IF ERRORLEVEL 1 PAUSE

START ../../../Server/StartMyFirstServer/PacketGenerator/bin/PacketGenerator.exe ./Protocol.proto
XCOPY /Y Protocol.cs "../../../WaterGame/Assets/Scripts/Packet"
XCOPY /Y Protocol.cs "../../../Server/StartMyFirstServer/StartMyFirstServer/Packet"
XCOPY /Y ClientPacketManager.cs "../../../WaterGame/Assets/Scripts/Packet"
XCOPY /Y ServerPacketManager.cs "../../../Server/StartMyFirstServer/StartMyFirstServer/Packet"