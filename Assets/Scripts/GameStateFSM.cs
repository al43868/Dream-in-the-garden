using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateFSM : IFsm
{
    public int CurrentState { get; private set; }
    #region FSM Base
    private readonly Dictionary<int, Tuple<System.Action<int>, System.Action<int>>>
        _actions = new();
    private readonly List<ValueTuple<int, int, int>> _transitions = new();
    //添加状态
    public bool AddState(int state, Action<int> onEnter, Action<int> onExit)
    {
        if (_actions.ContainsKey(state))
        {
            throw new Exception($"不能重复添加状态{state}行为");
        }

        _actions.Add(state, new Tuple<Action<int>, Action<int>>(onEnter, onExit));
        return true;
    }
    //添加转换
    public bool AddTransition(int from, int to, int triggerCode)
    {
        if (!_actions.ContainsKey(from) || !_actions.ContainsKey(to))
        {
            return false;
        }
        _transitions.Add(item: (from, to, triggerCode));
        return true;
    }
    //移除状态
    public bool RemoveState(int state)
    {
        if (_actions.ContainsKey(state))
        {
            return false;
        }

        _actions.Remove(state);
        return true;
    }
    //切换状态
    public bool SwitchToState(int stateId, bool forceSwitch = false)
    {
        bool hasState = _actions.ContainsKey(stateId);
        if (!hasState)
        {
            Debug.Log("no state");
            return false;
        }
        bool stateChanged = stateId != CurrentState;
        if (!stateChanged)
        {
            if (!forceSwitch)
            {
                Debug.Log("state needChange" + stateId);
                return false;
            }
        }
        if (stateChanged)
        {
            if (_actions.TryGetValue(CurrentState, out var oldActions))
            {
                oldActions.Item2?.Invoke(stateId);
            }
        }
        var oldStateId = CurrentState;
        var newActions = _actions[stateId];
        CurrentState = stateId;
        newActions.Item1?.Invoke(oldStateId);
        Debug.Log("OrderFSMcurrentState:" + CurrentState);
        return true;
    }
    //切换状态事件
    public bool TriggerEvent(int eventCode)
    {
        foreach ((int, int, int) transition in _transitions)
        {
            if (transition.Item1 == CurrentState && transition.Item3 == eventCode)
            {
                SwitchToState(transition.Item2);
                return true;
            }
        }
        return false;
    }
    #endregion
    public void Init()
    {
        AddState(-1, null, null);
        AddState(1, null, null);//运行
        AddState(2, (x) => { Time.timeScale = 0; }, (x) => { Time.timeScale = 1; });//暂停
        AddState(3, (x) => { Time.timeScale = 0; }, (x) => { Time.timeScale = 1; });//游戏结束
    }
    public GameStateFSM(int prepareState)
    {
        Init();
        CurrentState = -1;
        SwitchToState(prepareState);
    }
}