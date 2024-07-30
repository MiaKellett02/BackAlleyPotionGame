using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private float m_fadeTime = 1.0f;

    [SerializeField] private GameObject mainButtonsHolder;
    [SerializeField] private GameObject attributionsPanel;

    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private AnimationClip zoomIntoShopClip;

    [SerializeField] private AudioSource door;
    [SerializeField] private AudioClip doorClip;

    public void Start()
    {
        // Limit to 60 fps as any more is unnecessary and may cause performance issues
        Application.targetFrameRate = 60;
    }

    public void PlayGame ()
    {
        cameraAnimator.Play(zoomIntoShopClip.name);

        StartCoroutine(FadeToSceneAfterAnimation());
    }

    public void OpenAttributions ()
    {
        mainButtonsHolder.SetActive(false);
        attributionsPanel.SetActive(true);
    }

    public void CloseAttributions()
    {
        mainButtonsHolder.SetActive(true);
        attributionsPanel.SetActive(false);
    }

    private IEnumerator FadeToSceneAfterAnimation()
    {
        float timeUntilFade = (zoomIntoShopClip.length / 3) * 2;

        yield return new WaitForSeconds(timeUntilFade);

        door.PlayOneShot(doorClip);

        FadeUI.Instance.FadeOut(m_fadeTime, () => { SceneManager.LoadScene(1); });
    }
}
