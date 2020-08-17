using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        	
        GameObject netObj = GameObject.Find("Network");
        if(netObj) {		
            // Network 클래스의 컴포넌트를 획득.
            Network network  = netObj.GetComponent<Network>();
            if (network.IsConnected() == true) {
                // 패킷 데이터 생성.
                CharacterData data = new CharacterData();
				
                data.characterId = "hi";
                data.index = 1;
                data.dataNum = 4;
                data.coordinates = new CharacterCoord[4];
                for (int i = 0; i < 4; ++i) {
                    data.coordinates[i] = new CharacterCoord(0.1f,0.1f);
                }

                // 캐릭터 좌표 송신.
                CharacterDataPacket packet = new CharacterDataPacket(data);
                int sendSize = network.SendUnreliable<CharacterData>(packet);
                if (sendSize > 0) {
                    Debug.Log("Send character coord.[index:" + 4 + "]");
                }
            }
        }

        
    }
}
