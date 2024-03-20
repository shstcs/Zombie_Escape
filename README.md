# my Horror Game
## 1. 기획 의도
- 3D 프로젝트를 하나 해보고 싶음.
- Navmesh를 다시 사용해보고 싶음.
  
### => 몬스터가 쫒아오고 그를 피해 탈출하는 공포 게임
#
## 2. 게임 목표
### 성공 - 몬스터의 추격을 피해 구슬 5개를 모으면 승리

### 실패 - 구슬을 다 모으기 전에 몬스터에게 잡히면 패배   
#
## 3. 게임 플레이 화면
https://github.com/shstcs/HorrorGame/assets/73222781/c7985141-9d21-445f-b27e-698bfc8d6849
#
## 4. 게임 특징
### 몬스터와의 거리에 따른 사운드 조정
- 으스스한 음악이 몬스터와의 거리에 따라 조절된다.
```
public void SetVolume()
{
    float distance = GameManager.Player.GetDistanceWithGhost();
    float minusVolume = distance - 3 > 0 ? (distance - 3) / 3 : 0;      // 3m가 최대. 점점 줄어들게 설정
    
    _audioSource.volume = 1-minusVolume;
}
```

### 손전등을 비추면 몬스터의 속도 둔화
- 몬스터에게 빛이 닿으면 속도가 줄어들게 구현하였다.
- 한번 비추면 2초동안 속도가 줄어들고 중첩되지는 않도록 하였다.
```
public void LightRaycast()
{
    if (isLight)
    {
        RaycastHit[] hits = new RaycastHit[10];
        int numhit = Physics.RaycastNonAlloc(transform.position, transform.forward, hits, 10f, ghostLayer);
        if (numhit > 0)
        {
            hits[0].collider.gameObject.GetComponent<Ghost>().SpeedDown();
        }
    }
}
```
#
```
public void SpeedDown()
{
    if(!isDebuff)
    {
        isDebuff = true;
        _agent.speed = 1f;
        StartCoroutine(nameof(RestoreSpeed));
    }
}

private IEnumerator RestoreSpeed()
{
    yield return new WaitForSecondsRealtime(2f);
    _agent.speed = 5f;
    isDebuff = false;
}
```

### Addressables 사용
- Resourcs 파일로 불러오는 대신 연습삼아 어드레서블을 사용해 보았다.
- 게임오브젝트는 바로 인스턴스화하고, 사운드 등 사용할 것들은 로드만 해 놓았다.
- 비동기 함수에 대해서, 사용하는 법에 대해서 많이 익힐 수 있었다.
```
  private void CreateGhost()
{
    Vector3 ghostPos = new Vector3(10, 0, 20);
    AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Assets/AddressableAssets/Prefabs/Characters/Zombie.prefab", ghostPos,Quaternion.identity);
    handles.Add(handle);
}

private void LoadAudio()
{
    Addressables.LoadAssetAsync<AudioClip>("Assets/Audios/suspense.mp3").Completed +=
        (handle) =>
        {
            bgCilp = handle.Result;
            GameManager.Audio.SetBGM();
        };
}
```
### Polybrush 사용
- 주변 오브젝트들을 만들고자 하는데, 에셋을 가져오기에는 많은 디테일이 필요하지 않았음.
- 따라서 텍스쳐만 따로 가져온 상태로 오브젝트의 모양을 제작하여 사용하였음.


#
## 5. 발전시켜야 할 부분
### 몬스터 FSM, 애니메이션 추가
- 만들다보니 계획한 목표에는 엄청 필요하다고 느끼지 않아서 후순위로 밀렸음.
- 뛰어오다가 손전등에 닿으면 걷는다거나 하는 여러 상태를 만들면 좋을 듯 하다.

### Light 사용 문제
- 빛을 사용하는 부분에서 겹치는건지 설정한대로 나오지 않는 경우들이 많았다.
- Light 컴포넌트에 대해 더 공부해서 사용할 필요가 있을 듯.
#
## 6. 배운 점
### 3D에 첫 발을 내딛다.
- 플레이어를 움직이는 것도 2D와 달라서 좀 헤맸었는데, 이제는 많이 익숙해졌다.
- 에디터를 다루는 것도 어려웠었는데 좀 편해져서 기쁘다.

### 어드레서블, 비동기 함수에 대한 이해.
- 꽤 헤매다 보니 비동기를 어떻게 사용하는지, 동작이 마친 후 델리게이트를 걸어서 사용하는 것들도 이해하였다.
- 다음 프로젝트에서는 코루틴과 함께 사용해보고 싶기도 하다.
