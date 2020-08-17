using System.Collections;
using System.IO;


// 캐릭터 좌표 패킷 정의.
//
public class CharacterDataPacket : IPacket<CharacterData>
{
	class CharacterDataSerializer : Serializer
	{
		//
		public bool Serialize(CharacterData packet)
		{
			
			Serialize(packet.characterId, CharacterData.characterNameLength);
			
			Serialize(packet.index);
			Serialize(packet.dataNum);
			
			for (int i = 0; i < packet.dataNum; ++i) {
				// CharacterCoord
				Serialize(packet.coordinates[i].x);
				Serialize(packet.coordinates[i].z);
			}	
			
			return true;
		}
		
		//
		public bool Deserialize(ref CharacterData element)
		{
			if (GetDataSize() == 0) {
				// 데이터가 설정되어 있지 않습니다.
				return false;
			}
			
			Deserialize(ref element.characterId, CharacterData.characterNameLength);
			
			Deserialize(ref element.index);
			Deserialize(ref element.dataNum);
			
			element.coordinates = new CharacterCoord[element.dataNum];
			for (int i = 0; i < element.dataNum; ++i) {
				// CharacterCoord
				Deserialize(ref element.coordinates[i].x);
				Deserialize(ref element.coordinates[i].z);
			}
			
			return true;
		}
	}
	
	// 패킷 데이터의 실체.
	CharacterData		m_packet;
	
	public CharacterDataPacket(CharacterData data)
	{
		m_packet = data;
	}
	
	public CharacterDataPacket(byte[] data)
	{
		CharacterDataSerializer serializer = new CharacterDataSerializer();
		
		serializer.SetDeserializedData(data);
		serializer.Deserialize(ref m_packet);
	}
	
	public PacketId	GetPacketId()
	{
		return PacketId.CharacterData;
	}
	
	public CharacterData	GetPacket()
	{
		return m_packet;
	}
	
	
	public byte[] GetData()
	{
		CharacterDataSerializer serializer = new CharacterDataSerializer();
		
		serializer.Serialize(m_packet);
		
		return serializer.GetSerializedData();
	}
}
