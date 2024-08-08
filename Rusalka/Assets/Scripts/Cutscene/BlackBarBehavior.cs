using System.Collections;
using UnityEngine;

public class BlackBarBehavior : MonoBehaviour
{
    [SerializeField] private RectTransform TopBar;
    [SerializeField] private RectTransform BottomBar;
    [SerializeField] private float BarPositionMovementPercentage = 0.1f; // Percentage of the canvas height
    [SerializeField] private float BarMovementTime;

    private Vector2 topBarStartPosition;
    private Vector2 bottomBarStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        topBarStartPosition = TopBar.anchoredPosition;
        bottomBarStartPosition = BottomBar.anchoredPosition;
    }

    public void EnableBars()
    {
        StartCoroutine(RollInBars());
    }

    public void DisableBars()
    {
        StartCoroutine(RemoveBars());
    }

    private IEnumerator RollInBars()
    {
        TopBar.gameObject.SetActive(true);
        BottomBar.gameObject.SetActive(true);
        float elapsedTime = 0f;

        float canvasHeight = TopBar.GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height;
        float movementDistance = canvasHeight * BarPositionMovementPercentage;

        Vector2 topBarTargetPosition = topBarStartPosition + new Vector2(0, -movementDistance);
        Vector2 bottomBarTargetPosition = bottomBarStartPosition + new Vector2(0, movementDistance);

        while (elapsedTime < BarMovementTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / BarMovementTime;

            TopBar.anchoredPosition = Vector2.Lerp(topBarStartPosition, topBarTargetPosition, t);
            BottomBar.anchoredPosition = Vector2.Lerp(bottomBarStartPosition, bottomBarTargetPosition, t);

            yield return new WaitForEndOfFrame();
        }

        // Ensure the bars reach the target positions
        TopBar.anchoredPosition = topBarTargetPosition;
        BottomBar.anchoredPosition = bottomBarTargetPosition;
    }

    private IEnumerator RemoveBars()
    {
        float elapsedTime = 0f;

        Vector2 topBarCurrentPosition = TopBar.anchoredPosition;
        Vector2 bottomBarCurrentPosition = BottomBar.anchoredPosition;

        while (elapsedTime < BarMovementTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / BarMovementTime;

            TopBar.anchoredPosition = Vector2.Lerp(topBarCurrentPosition, topBarStartPosition, t);
            BottomBar.anchoredPosition = Vector2.Lerp(bottomBarCurrentPosition, bottomBarStartPosition, t);

            yield return new WaitForEndOfFrame();
        }

        // Ensure the bars return to their start positions
        TopBar.anchoredPosition = topBarStartPosition;
        BottomBar.anchoredPosition = bottomBarStartPosition;
        TopBar.gameObject.SetActive(false);
        BottomBar.gameObject.SetActive(false);
    }
}
