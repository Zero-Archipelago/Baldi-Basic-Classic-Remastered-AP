using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BaldiAP;

public class SlotData
{
    public readonly bool DeathLink;
    public SlotData(Dictionary<string, object> slotDict)
    {
        foreach (var x in slotDict) BaldiConsole.Instance.DebugLog($"{x.Key} {x.Value}");
        if (slotDict.TryGetValue("DeathLink", out var rawDeathLink) &&
            rawDeathLink is JObject deathLink)
            DeathLink = deathLink.Value<bool>();
    }
}