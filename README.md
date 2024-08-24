# GameCore
Collection of scripts and tools for Unity version 2022 or later

This repository contains many tools I currently develop to use in my projects.

## Overview:

[Sound System](#sound-system)

[Event Factory](#event-factory)

[Gizmos](#gizmos)

[Extension and Helpers](#extensions-and-helpers)

## Sound System

```csharp
SoundSystem.Instance.PlayMusic(clip);
SoundSystem.Instance.PlaySFX(clip);
SoundSystem.Instance.PlaySFXAtPosition(clip, Vector3.zero);

// play with random pitch
SoundSystem.Instance.PlaySFX(clip, true);
SoundSystem.Instance.PlaySFXAtPosition(clip, Vector3.zero, true);
```

## Event Factory

- Add event

```csharp
// Add
EventFactory.Instance.Add("key", new GameAction());

// Get
GameAction action = EventFactory.Instance.Get<GameAction>("key");

// Register
action.GameEvent += ()=>
    {
        Debug.Log("Hello World!");
    }

// Call
action.Raise();
```

## Extensions and Helpers

### Extensions class:

- ArrayExtensions
- ListExtensions
- MathfExtensions

### Helpers class:

- GizmosHelpers
- AnimationHelpers
- CameraHelpers
- AnimationHelpers

## Extended Monobehaviour

Add more functions to MonoBehaviour

```csharp
using Gamecore.Component;

public class ExtendMonoText : CustomMonobehaviour
{
    protected override void OnEnable() { base.OnEnable(); }

    protected override void OnSceneLoaded() { }

    protected override void Start() { base.Start(); }

    protected override void OnEnableOrStart() { }

    protected override void OnEnableOrLoaded() { }

    protected override void OnDisable() { base.OnDisable(); }
}
```