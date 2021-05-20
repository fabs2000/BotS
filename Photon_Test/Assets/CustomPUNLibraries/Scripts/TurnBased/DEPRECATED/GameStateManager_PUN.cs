using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameStateManager_PUN : MonoBehaviourPunCallbacks, IPunTurnManagerCallbacks
{
    static public GameStateManager_PUN Instance; 
    
    [Serializable]
    public class TimedPlayerAction
    {
        public string ActionName = "";
        public float TimedDelay = 0f;
        public UnityEvent UEvent = null;
    }
    
    #region Private Variables
    
    private PunTurnManager _turnManager;

    #endregion

    #region Serialized Variables

    [Tooltip("Check this if your game requires a preparation stage")]
    [SerializeField] private bool _hasPrepStage;
    
    [Space(20)]
    [SerializeField] private List<TimedPlayerAction> _playerActions = null;

    [SerializeField] private Text _timeText;
    
    #endregion
    
    #region Public Variables
    
    
    
    #endregion

    void Start()
    {
        _turnManager = GetComponent<PunTurnManager>();
        _turnManager.TurnManagerListener = this;
        
        //This is incredibly dumb but it solves the "IsOver" issue
        //_turnManager.SendMove(0, true);

        //Debug for states
        print("Sates: " + _turnManager.IsOver + _turnManager.IsFinishedByMe + _turnManager.IsCompletedByAll);

        // StartCoroutine(CheckPlayers());
    }

    private void Update()
    { 
        _timeText.text = _turnManager.RemainingSecondsInTurn.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            if (_turnManager.IsOver)
            {
                if(PhotonNetwork.IsMasterClient)
                    _turnManager.BeginTurn();
            }
            else
            {
                print("Turn's not over");
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (!_turnManager.IsOver)
            {
                if(PhotonNetwork.IsMasterClient)
                    _turnManager.SendMove(0, true);
            }
            else
            {
                print("Turn is over cant stop");
            }
        }
    }

    #region Custom

    private void StartTurn()
    {
    }

    private void EndTurn()
    {
        
    }

    #endregion

    private IEnumerator CheckPlayers()
    {
        while (true)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                print("Game Starting!");

                StopCoroutine(CheckPlayers());
            }
            else
            {
                //Do Nothing
                print("Only " + PhotonNetwork.CurrentRoom.PlayerCount + " players out of "
                      + PhotonNetwork.CurrentRoom.MaxPlayers + " in room, waiting for more players");
            }


            yield return new WaitForSeconds(1f);
        } 
    }
    
    #region PunTurnManager Callbacks
    
    public void OnTurnBegins(int turn)
    {
        print("Turn: (" + turn +") Begins");
    }

    public void OnTurnCompleted(int turn)
    {
        print("Turn: (" + turn +") Completed");
    }

    public void OnPlayerMove(Player player, int turn, object move)
    {
        print("Player: (" + player +") Moved, with Turn: (" + turn + ")");
    }

    public void OnPlayerFinished(Player player, int turn, object move)
    {
        print("Player: (" + player +") Finished, with Turn: (" + turn + ")");
    }

    public void OnTurnTimeEnds(int turn)
    {
        print("Turn: (" + turn + ") Time ended");
        
        
    }
    
    #endregion
}
