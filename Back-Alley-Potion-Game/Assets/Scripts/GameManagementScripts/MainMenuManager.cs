using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private float m_fadeTime = 1.0f;

    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private AnimationClip zoomIntoShopClip;

    public void PlayGame ()
    {
        cameraAnimator.Play(zoomIntoShopClip.name);

        StartCoroutine(FadeToSceneAfterAnimation());
    }

    private IEnumerator FadeToSceneAfterAnimation()
    {
        yield return new WaitForSeconds(zoomIntoShopClip.length);

        FadeUI.Instance.FadeOut(m_fadeTime, () => { SceneManager.LoadScene(1); });
    }
}
