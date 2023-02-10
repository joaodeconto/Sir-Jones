using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public delegate void GameStateEvent(State state);
    public static event GameStateEvent OnStateChange;

    public enum State
    {
        Intro,
        Start,
        Gameplay,
        Pause,
        Wait
    }

    public static State state;

    void Start()
    {
        state = State.Intro;
        OnStateChange?.Invoke(state);
    }

    public static void MenuStart()
    {
        state = State.Start;
        OnStateChange?.Invoke(state);
    }

    public static void StartGameplay()
    {
        state = State.Gameplay;
        OnStateChange?.Invoke(state);
    }

    public static void Pause()
    {
        state = State.Pause;
        OnStateChange?.Invoke(state);
    }

    public static void Wait()
    {
        state = State.Wait;
        OnStateChange?.Invoke(state);
    }
}