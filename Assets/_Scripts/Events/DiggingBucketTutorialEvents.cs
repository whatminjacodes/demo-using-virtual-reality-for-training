using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;

public struct RightLeverGrabbedEvent : IEvent {}
public struct RightLeverLetGoEvent : IEvent {}

public struct ExcavatorArmFrontMovedToCorrectLocationEvent : IEvent {}
