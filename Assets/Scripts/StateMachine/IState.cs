using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
}

// FSM은 보충할때 사용해보려고 남겨놓았습니다.
// 쓸데없는거 알고 있지만 한번 봐주세요..