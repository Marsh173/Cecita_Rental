using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEnteredRoomEvent : UnityEvent<GameObject> { }


public class PlayerLeaveRoomEvent : UnityEvent<GameObject> { }