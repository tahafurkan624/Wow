using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Chapter currentChapter;

    [SerializeField] private Transform _startMoveTrans;

    private InputManager _inputManager;
    private Player _player;

    public  Image Image;

    private void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        switch (currentChapter)
        {
            case Chapter.Chapter1:
                _player.MoveToPoint(_startMoveTrans.position);

                FMOD.Studio.EventInstance instanceGrass = FMODUnity.RuntimeManager.CreateInstance("event:/Atm/Bus");
                instanceGrass.start();
                break;
            case Chapter.Chapter2:
                DialogueSystem.instance.GetChapterText();
                DialogueSystem.instance.isDialogueOpen = true;
                break;
            case Chapter.Chapter3:
                WickedNpc = FindObjectOfType<WickedMan>();
                _player.MoveToPoint(_startMoveTrans.position);
                break;
            case Chapter.Chapter4:
                break;
        }
    }

    public void EndScene(int levelIndex)
    {
        StartCoroutine(LoadNewScene(levelIndex));
    }

    public void CallChapter2Sequence()
    {
        StartCoroutine(Chapter2Sequence());
    }
    private IEnumerator Chapter2Sequence()
    {
        _player.Agent.SetDestination(_startMoveTrans.position);
        yield return new WaitForSeconds(1f);
    }

    public void CallChapter4Sequence()
    {
        StartCoroutine(Chapter4Sequence());
    }

    public WickedMan WickedNpc;
    private IEnumerator Chapter4Sequence()
    {
        yield return new WaitForSeconds(1f);
        WickedNpc.StartTypingAnim();
    }

    public void Chapter4ButtonEvent()
    {
        StartCoroutine(Chapter4ButtonSequence());
    }

    public GameObject DecisionUI,DecisionUI2,DecisionUI3;

    private IEnumerator Chapter4ButtonSequence()
    {
        yield return new WaitForSeconds(.5f);
        DecisionUI.SetActive(true);
    }

    public void Chapter4AskKindly()
    {
        StartCoroutine(Chapter4AskKindlySequence());
    }

    private IEnumerator Chapter4AskKindlySequence()
    {
        DecisionUI.SetActive(false);
        yield return new WaitForSeconds(.5f);

        DialogueSystem.instance.GetChapterText();
        DialogueSystem.instance.isDialogueOpen = true;
    }

    public void Chapter4SetBack()
    {
        WickedNpc.StartSetBackAnim();
    }

    private IEnumerator LoadNewScene(int levelIndex)
    {
        yield return new WaitForSeconds(1);

        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            Image.color = Color.Lerp(Color.clear, Color.black, elapsedTime / 2);
            yield return new WaitForEndOfFrame();
        }
        Image.color = Color.black;

        SceneManager.LoadScene(levelIndex);
    }

    public enum Chapter
    {
        Chapter1,
        Chapter2,
        Chapter3,
        Chapter4,
    }
}
