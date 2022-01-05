using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;

public interface ITutorialModule : IEventReceiver<TutorialModuleStartedEvent>
{
    void OnEvent(TutorialModuleStartedEvent e);

    void RegisterEvents();

    void UnRegisterEvents();

    void FinishAndCloseTutorial();
}
