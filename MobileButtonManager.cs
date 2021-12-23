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
    /// true = ���ư�� ������ �ƹ��� �������� �ʽ��ϴ�.
    /// </summary>
    public bool isLock;

    /// <summary>
    /// ���ư ���������·� ����.
    /// </summary>
    public void Lock()
    {
        isLock = true;
    }
    /// <summary>
    /// ���ư Ȱ��ȭ
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
    /// �̱���
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
    /// Ȩ��ư �߻��� �ݹ�
    /// </summary>
    public void OnPressHomeButton()
    {
    }

    /// <summary>
    /// ���ư �߻��� �ݹ�
    /// </summary>
    public void OnPressBackbutton()
    {
        Back();
    }
    /// <summary>
    /// ���ư �̺�Ʈ�߰�
    /// </summary>
    /// <param name="_messageTarget"> SendMessage�� ���޹��� ��ü </param>
    /// <param name="_methodName"> �޼ҵ� �̸� </param>
    public void Add(GameObject _messageTarget, string _methodName)
    {
        BACK_MESSAGE message = new BACK_MESSAGE(_messageTarget, _methodName);
        bmList.Add(message);
        //DebugPanel.Instance.Log(bmList.Count + ", Backmeesages");
    }
    /// <summary>
    /// �˾��� �����Ҷ� ��޽����� �ƴϸ� ���� �޼����� �����մϴ�.
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
    /// �ֻ�� �޼��� ����
    /// </summary>
    public void DeleteMessage()
    {
        if (bmList.Count == 0) return;
        bmList.RemoveAt(bmList.Count - 1);

    }
    /// <summary>
    /// �޼��� Ŭ����.
    /// </summary>
    public void Clear()
    {
        bmList.Clear();
    }
    /// <summary>
    /// ���ư ��� �̺�Ʈ.
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
//    Debug.Log("�˾��̿��ȴ�");
//    BackButtonManager.instance.Add(this.gameObject, "OnClick_PopupClose");
//}
//public void OnClick_PopupClose()
//{
//    BackButtonManager.instance.BackMessageCheck(); //���ư �޼����� �ִ°�?
//    open = false;
//    Debug.Log("�˾��̴�����");
//}