using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;

public class LobbyManager : NetworkBehaviour
{
    public string lobbyName = "MySingleLobby";
    public int maxPlayerCount = 4;

    private Lobby currentLobby;
    private float hearBeatTimer;

    private async void Awake()
    {
        // �Ʒ��Լ��� ������ ����ǵ��� async��
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        JoinOrCreate();
    }

        public void JoinOrCreate()
    {
        // ������ Ŭ���̾�Ʈ�� ������ ��, �κ� �����ϵ��� �õ��մϴ�.
        if (NetworkManager.Singleton.IsServer)
        {
            CreateLobby();
        }
        else
        {
            JoinLobby();
        }
    }

    public void CreateLobby()
    {
        // �κ� �����մϴ�.
        CreateLobbyOptions lobbyOptions = new CreateLobbyOptions
        {
            IsPrivate = false,
            Data = new Dictionary<string, DataObject>
            {
                { "LobbyName", new DataObject(DataObject.VisibilityOptions.Public, lobbyName) }
            }
        };

        Lobbies.Instance.CreateLobbyAsync(lobbyName, maxPlayerCount, lobbyOptions).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                currentLobby = task.Result;
                Debug.Log("Lobby created: " + currentLobby.Id);
                NetworkManager.Singleton.StartHost();
            }
            else
            {
                Debug.LogError("Failed to create lobby: " + task.Exception);
            }
        });
    }

    public void JoinLobby()
    {
        Lobbies.Instance.QuickJoinLobbyAsync().ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                currentLobby = task.Result;
                Debug.Log("Joined lobby: " + currentLobby.Id);
                NetworkManager.Singleton.StartClient();
            }
            else
            {
                Debug.LogError("Failed to join lobby: " + task.Exception);
            }
        });
    }

    private void Update()
    {
        if (hearBeatTimer > 15)
        {
            hearBeatTimer -= 15;

            if (currentLobby != null && currentLobby.HostId == AuthenticationService.Instance.PlayerId)
                LobbyService.Instance.SendHeartbeatPingAsync(currentLobby.Id);
        }

        hearBeatTimer += Time.deltaTime;
    }
}
