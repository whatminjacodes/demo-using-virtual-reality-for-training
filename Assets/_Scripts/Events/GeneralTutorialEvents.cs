using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;

public struct StartTutorialEvent : IEvent {}

public struct TutorialModuleStartedEvent : IEvent {
    public string nameOfModuleThatIsStarting;
}

public struct TutorialModuleFinishedEvent : IEvent {
    public string nameOfModuleThatWasFinished;
}
