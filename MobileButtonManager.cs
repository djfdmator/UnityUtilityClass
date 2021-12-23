using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BACK_MESSAGE
{
    public BACK_MESSAGE(GameObject mt, string mn)
    {
        target = mt;
        functionName = mn;
    }
    public GameObject target;
    public string functionName;
}

public class MobileButtonManager : MonoBehaviour
{
    private List<BACK_MESSAGE> bmList = new List<BACK_MESSAGE>();
    private Dictionary<string, BACK_MESSAGE> bmMap = new Dictionary<string, BACK_MESSAGE>();

    public bool isBackEvent = false;
    /// <summary>
    /// true = 백버튼을 눌러도 아무런 반응하지 않습니다.
    /// </summary>
    public bool isLock;

    /// <summary>
    /// 백버튼 무반응상태로 만듬.
    /// </summary>
    public void Lock()
    {
        isLock = true;
    }
    /// <summary>
    /// 백버튼 활성화
    /// </summary>
    public void UnLock()
    {
        isLock = false;
    }
    public string GetGUID()
    {
        System.Guid uniqueID = new System.Guid();
        return uniqueID.ToString();
    }

    public int GetCount() { return bmList.Count; }

    /// <summary>
    /// 싱글톤
    /// </summary>
    public static MobileButtonManager instance
    {
        get
        {
            if (_pInstance == null)
            {
                _pInstance = FindObjectOfType<MobileButtonManager>();
                if (_pInstance == null)
                {
                    _pInstance = new GameObject("Singleton of " + typeof(MobileButtonManager).ToString(), typeof(MobileButtonManager)).GetComponent<MobileButtonManager>();
                }
            }
            return _pInstance;
        }
    }
    static MobileButtonManager _pInstance;
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnPressBackbutton();
        }
        else if (Input.GetKeyUp(KeyCode.Home))
        {
            OnPressHomeButton();
        }
    }

    /// <summary>
    /// 홈버튼 발생시 콜백
    /// </summary>
    public void OnPressHomeButton()
    {
    }

    /// <summary>
    /// 백버튼 발생시 콜백
    /// </summary>
    public void OnPressBackbutton()
    {
        Back();
    }
    /// <summary>
    /// 백버튼 이벤트추가
    /// </summary>
    /// <param name="_messageTarget"> SendMessage를 전달받을 객체 </param>
    /// <param name="_methodName"> 메소드 이름 </param>
    public void Add(GameObject _messageTarget, string _methodName)
    {
        BACK_MESSAGE message = new BACK_MESSAGE(_messageTarget, _methodName);
        bmList.Add(message);
        //DebugPanel.Instance.Log(bmList.Count + ", Backmeesages");
    }
    /// <summary>
    /// 팝업을 삭제할때 백메시지가 아니면 기존 메세지를 삭제합니다.
    /// </summary>
    public void BackMessageCheck()
    {
        if (isBackEvent == false)
        {
            DeleteMessage();
            //DebugPanel.Instance.Log(bmList.Count + ", Backmeesages");
        }
    }
    /// <summary>
    /// 최상단 메세지 삭제
    /// </summary>
    public void DeleteMessage()
    {
        if (bmList.Count == 0) return;
        bmList.RemoveAt(bmList.Count - 1);

    }
    /// <summary>
    /// 메세지 클리어.
    /// </summary>
    public void Clear()
    {
        bmList.Clear();
    }
    /// <summary>
    /// 백버튼 등록 이벤트.
    /// </summary>
    public void Back()
    {
        if (isLock) return;
        if (bmList.Count == 0) return;
        isBackEvent = true;
        BACK_MESSAGE message = bmList[bmList.Count - 1];
        message.target.SendMessage(message.functionName);
        DeleteMessage();
        isBackEvent = false;
    }
    public void ClearBack()
    {
        while (bmList.Count > 0)
        {
            Back();
        }
    }
}
//public void OnClick_PopupOpen()
//{
//    open = true;
//    Debug.Log("팝업이열렸다");
//    BackButtonManager.instance.Add(this.gameObject, "OnClick_PopupClose");
//}
//public void OnClick_PopupClose()
//{
//    BackButtonManager.instance.BackMessageCheck(); //백버튼 메세지에 있는가?
//    open = false;
//    Debug.Log("팝업이닫혔다");
//}