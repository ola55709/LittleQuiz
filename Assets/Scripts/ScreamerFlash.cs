using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreamerFlash : MonoBehaviour
{
    [SerializeField] 
    private Image screamerImage;
    [SerializeField] 
    private Image bg;
    [SerializeField] 
    private Sprite[] screamerSprites;
    [SerializeField] 
    private float flashTime = 0.08f;
    [SerializeField] 
    private float randomFlashTime = 0.1f;
    [SerializeField] 
    private float minRandomDelay = 7f;
    [SerializeField] 
    private float maxRandomDelay = 14f;

    private Color _visibleColorScreamer;
    private Color _invisibleColorScreamer;
    private Color _visibleColorBg;
    private Color _invisibleColorBg;

    public void Awake()
    {
        _visibleColorScreamer = new Color(screamerImage.color.r, screamerImage.color.g, screamerImage.color.b, 1);
        _invisibleColorScreamer = new Color(screamerImage.color.r, screamerImage.color.g, screamerImage.color.b, 0);
        _visibleColorBg = new Color(bg.color.r, bg.color.g, bg.color.b, 1);
        _invisibleColorBg = new Color(bg.color.r, bg.color.g, bg.color.b, 0);

        PlayScreamer();
    }


    public void PlayScreamer()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        screamerImage.color = _visibleColorScreamer;
        bg.color = _visibleColorBg;

        for (int i = 0; i < Mathf.Min(3, screamerSprites.Length); i++)
        {
            screamerImage.sprite = screamerSprites[i];
            yield return new WaitForSeconds(flashTime);
        }

        screamerImage.color = _invisibleColorScreamer;
        bg.color = _invisibleColorBg;

        while (true)
        {
            float randomDelay = Random.Range(minRandomDelay, maxRandomDelay);
            yield return new WaitForSeconds(randomDelay);
            screamerImage.color = _visibleColorScreamer;
            bg.color = _visibleColorBg;

            int randomIndex = Random.Range(0, screamerSprites.Length);
            screamerImage.sprite = screamerSprites[randomIndex];

            yield return new WaitForSeconds(randomFlashTime);
            screamerImage.color = _invisibleColorScreamer;
            bg.color = _invisibleColorBg;
        }
    }
}