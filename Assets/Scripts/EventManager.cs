using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }

    private EventManager()
    {
    }

    public event Action<int> OnPlayerInput;
    public event Action<Waterdrop> OnDropDespawn;
    public event Action OnDropPop;
    public event Action OnGameOver;

    public void PlayerInput(int input)
    {
        OnPlayerInput?.Invoke(input);
    }

    public void DropDespawn(Waterdrop waterdrop)
    {
        OnDropDespawn?.Invoke(waterdrop);
    }

    public void PopDrop()
    {    
        OnDropPop?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
