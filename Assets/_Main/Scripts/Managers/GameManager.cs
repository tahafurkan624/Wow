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
                break;
            case Chapter.Chapter2:
                DialogueSystem.instance.GetChapterText();
                DialogueSystem.instance.isDialogueOpen = true;
                break;
            case Chapter.Chapter3:
                break;
            case Chapter.Chapter4:
                break;
        }
    }

    public void EndScene()
    {
        StartCoroutine(LoadNewScene());
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

    private IEnumerator LoadNewScene()
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

        SceneManager.LoadScene(1);
    }

    public enum Chapter
    {
        Chapter1,
        Chapter2,
        Chapter3,
        Chapter4,
    }
}
