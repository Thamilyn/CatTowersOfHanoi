using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SpawnMarker : Marker, INotification
{
    public GameObject prefab;
    public Vector3 pos;
    public PropertyName id => new PropertyName();
}
