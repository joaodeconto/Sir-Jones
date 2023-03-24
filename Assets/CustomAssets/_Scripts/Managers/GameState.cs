using UnityEngine;
using UnityEngine.Events;


namespace BWV
{
    public class GameState : MonoBehaviour
    {
        public delegate void GameStateEvent(State state);
        public static event GameStateEvent OnStateChange;

        public enum State
        {
            Pause,
            Wait,
            Intro,
            MenuStart,
            InGame,
            EndGame
        }

        public static State state;

        public static bool IsInGame { get { return state == State.InGame; }}
        public static bool IsPaused { get { return state == State.Pause; }}

        void Start()
        {
            state = State.Wait;
            OnStateChange(state);
        }

        public static void MenuStart()
        {
            state = State.MenuStart;
            OnStateChange(state);
        }

        public static void InGame()
        {
            state = State.InGame;
            OnStateChange(state);
        }
        public static void EndGame()
        {
            state = State.EndGame;
            OnStateChange(state);
        }

        public static void Pause()
        {
            state = State.Pause;
            OnStateChange(state);
        }

        public static void Wait()
        {
            state = State.Wait;
            OnStateChange(state);
        }
    }
}