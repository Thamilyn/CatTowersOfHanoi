using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpawnReceiver : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if(notification is SpawnMarker spawnNotif)
        {
            Instantiate(spawnNotif.prefab, spawnNotif.pos, Quaternion.identity);
        }
    }
}
