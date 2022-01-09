using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;

public struct AButtonPressedEvent : IEvent {}
public struct BButtonPressedEvent : IEvent {}
public struct TriggerPressedEvent : IEvent {}

public struct StartTutorialEvent : IEvent {}
public struct TutorialFinishedEvent : IEvent {}

public struct TutorialModuleStartedEvent : IEvent {
    public string nameOfModuleThatIsStarting;
}

public struct TutorialModuleFinishedEvent : IEvent {
    public string nameOfModuleThatWasFinished;
}
