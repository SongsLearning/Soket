using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
enum PickupState
{
	None = 0,
	Pickingup,
	Pickedup,
	Dropping,
	Dropped,
}
*/
enum PickupState
{
	Growing = 0, 			// 발생 중.
	None, 					// 미취득.
	PickingUp,				// 취득 중.
	Picked,					// 취득.
	Dropping,				// 폐기 중.
	Dropped,				// 폐기.
}

public class GameServer : MonoBehaviour {

	// 미취득 시 소유자 ID.
	private const string 	ITEM_OWNER_NONE = "";

	// 게임 서버에서 사용하는 포트 번호.
	private const int 		serverPort = 50764;

	// 게임 서버의 버전.
	public const int		SERVER_VERSION = 1; 	
		


	// 통신 모듈 컴포넌트.
	Network					network_ = null;

	
	void Awake() {
	

		GameObject go = new GameObject("ServerNetwork");
		network_ = go.AddComponent<Network>();
		if (network_ != null) {

			// 이벤트 핸들러.
			network_.RegisterEventHandler(OnEventHandling);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public bool StartServer()
	{
		return network_.StartServer(NetConfig.SERVER_PORT, Network.ConnectionType.TCP);
	}

	public void StopServer()
	{
		network_.StopServer();
	}

	
	private void DisconnectClient()
	{
		Debug.Log("[SERVER]DisconnectClient");

		network_.Disconnect();
	}


	// test

	void OnGUI()
	{
	}

	// ================================================================ //
	
	
	public void OnEventHandling(NetEventState state)
	{
		switch (state.type) {
		case NetEventType.Connect:
			Debug.Log("[SERVER]NetEventType.Connect");
			break;

		case NetEventType.Disconnect:
			Debug.Log("[SERVER]NetEventType.Disconnect");
			DisconnectClient();
			break;
		}
	}

}
