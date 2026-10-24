using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.MessageLog.Parts;
using JetBrains.Annotations;
using UnityEngine;

namespace BaldiAP;

public class ArchipelagoHandler : MonoBehaviour
{
    [CanBeNull] public ArchipelagoSession Session { get; set; }
    [CanBeNull] private string Server { get; set; }
    [CanBeNull] public string SlotName { get; set; }
    [CanBeNull] private string Password { get; set; }
    [CanBeNull] public string seed;
    public bool IsConnected => Session?.Socket.Connected ?? false;
    
    [CanBeNull] public event System.Action OnConnected;
    [CanBeNull] public event Action<string> OnConnectionFailed;
    public event System.Action OnDisconnected;
    
    private ConcurrentQueue<long> _locationsToCheck = new ConcurrentQueue<long>();

    public volatile bool connectionFinished;
    public volatile bool connectionSucceeded;
    private readonly bool _queueBreak = false;

    [CanBeNull] private string _lastDeath;
    [CanBeNull] private DateTime _lastDeathTime = DateTime.Now;

    private readonly Dictionary<string, List<string>> _deathMessagesByType = new()
    {
        { "Baldi", [
                "sums up his deaths.",
                "got their head subtracted from their shoulders.",
                "got got by Baldi."
            ]
        },
        { "Other", [
                "failed to get out while they still could.",
                "died because they felt like it.",
                "wasn't welcomed to the schoolhouse.",
                "got put in detention."
            ]
        }
    };
    
    public void CreateSession(string server, string slot, string password)
    {
        if (Server != null)
            return;
        Server = server;
        SlotName = slot;
        Password = password;
        _locationsToCheck = new ConcurrentQueue<long>();
        Session = ArchipelagoSessionFactory.CreateSession(Server);
        Session.MessageLog.OnMessageReceived += OnMessageReceived;
        Session.Socket.ErrorReceived += OnError;
        Session.Socket.SocketClosed += OnSocketClosed;
        Session.Items.ItemReceived += ItemReceived;
    }

    public IEnumerator ConnectRoutine()
    {
        BaldiConsole.Instance.Log($"Logging in to {Server} as {SlotName}");
        var connectTask = Session!.ConnectAsync();
        
        yield return new WaitUntil(() => connectTask.IsCompleted);

        if (connectTask.Exception != null)
        {
            BaldiConsole.Instance.Log(connectTask.Exception.ToString());
            yield break;
        }

        seed = connectTask.Result.SeedName;

        var loginTask = Session?.LoginAsync(
            Main.GameName,
            SlotName,
            ItemsHandlingFlags.AllItems,
            new System.Version(0, 6, 7),
            [],
            password: Password
            );
        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            BaldiConsole.Instance.Log($"Good luck bro");
            var successResult = (LoginSuccessful)loginTask.Result;
            // Main.SlotData = new Baldi 

            Main.ArchipelagoHandler.StartCoroutine(RunCheckQueue());
            connectionSucceeded = true;
            connectionFinished = true;
            OnConnected?.Invoke();
            yield break;
        }
        
        connectionSucceeded = false;
        connectionFinished = true;
        if (loginTask.Result != null)
        {
            var failure = (LoginFailure)loginTask.Result;
            var errorMessage = $"yeah bro idk what went wrong, but it did NOT connect";
            errorMessage = failure.Errors.Aggregate(errorMessage, (current, error) => current + $"\n    {error}");
            errorMessage = failure.ErrorCodes.Aggregate(errorMessage, (current, error) => current + $"\n    {error}");
            OnConnectionFailed?.Invoke(errorMessage);
            BaldiConsole.Instance.Log(errorMessage);
        }
        
        BaldiConsole.Instance.Log($"I swear to you, I will be trying my absolute hardest to login to {Server} as {SlotName} this time.");
    }
    
    public void Connect()
    {
        StartCoroutine(ConnectRoutine());
    }

    public void Disconnect()
    {
        if (Session == null)
            return;
        StopAllCoroutines();
        Session.Socket.DisconnectAsync();
        Session = null;
        BaldiConsole.Instance.Log("Disconnected from Archipelago.");
    }

    private void OnError(Exception exception, string message)
    {
        BaldiConsole.Instance.Log($"Socket error: {message} - {exception.Message}");
    }

    private void OnSocketClosed(string reason)
    {
        StopAllCoroutines();
        BaldiConsole.Instance.Log($"Socket closed: {reason}");
        OnDisconnected?.Invoke();
    }

    private void ItemReceived(ReceivedItemsHelper helper)
    {
        try
        {
            while (helper.Any())
            {
                var itemIndex = helper.Index;
                var item = helper.DequeueItem();
                Main.ItemHandler.HandleItem(itemIndex, item);
            }
        }
        catch (Exception ex)
        {
            BaldiConsole.Instance.Log($"[ItemReceived ERROR] {ex}");
            throw;
        }
    }
    
    private IEnumerator RunCheckQueue()
    {
        while (true)
        {
            if (_locationsToCheck.TryDequeue(out var locationId))
            {
                Session?.Locations.CompleteLocationChecks(locationId);
                BaldiConsole.Instance.DebugLog($"Sent location check: {locationId}");
            }
            
            if (_queueBreak)
                yield break;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private void OnMessageReceived(LogMessage message)
    {
        string messageString;
        if (message.Parts.Any(x => x.Type == MessagePartType.Player) &&
            Main.FilterLog != null &&
            Main.FilterLog.Value &&
            !message.Parts.Any(x => x.Text.Contains(Session!.Players.GetPlayerName(Session.ConnectionInfo.Slot))))
            return;

        if (message.Parts.Length == 1)
        {
            messageString = message.Parts[0].Text;
        }
        else
        {
            var builder = new StringBuilder();
            foreach (var part in message.Parts)
            {
                builder.Append($"{part.Text}");
            }
            
            messageString = builder.ToString();
        }
        
        BaldiConsole.Instance.Log(messageString);
    }
}