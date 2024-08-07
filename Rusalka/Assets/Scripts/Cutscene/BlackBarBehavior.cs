using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BlackBarBehavior : MonoBehaviour
{
    [SerializeField] private GameObject TopBar;
    [SerializeField] private GameObject BottomBar;
    [SerializeField] private float BarPositionMovement;
    [SerializeField] private float BarMovementTime;

    private Vector3 topBarStartPosition;
    private Vector3 bottomBarStartPosition;
    [SerializeField] private GameObject topFinalPosition;
    [SerializeField] private GameObject bottomFinalPosition;

    // Start is called before the first frame update
    void Start()
    {
        topBarStartPosition = TopBar.transform.position;
        bottomBarStartPosition = BottomBar.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        float elapsedTime = 0f;

        Vector3 topBarTargetPosition = topBarStartPosition + new Vector3(0, -BarPositionMovement, 0);
        Vector3 bottomBarTargetPosition = bottomBarStartPosition + new Vector3(0, BarPositionMovement, 0);

        while (elapsedTime < BarMovementTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / BarMovementTime;

            TopBar.transform.position = Vector3.Lerp(topBarStartPosition, topBarTargetPosition, t);
            BottomBar.transform.position = Vector3.Lerp(bottomBarStartPosition, bottomBarTargetPosition, t);

            yield return null;
        }

        // Ensure the bars reach the target positions
        TopBar.transform.position = topBarTargetPosition;
        BottomBar.transform.position = bottomBarTargetPosition;
    }

    private IEnumerator RemoveBars()
    {
        float elapsedTime = 0f;

        Vector3 topBarTargetPosition = topBarStartPosition + new Vector3(0, -BarPositionMovement, 0);
        Vector3 bottomBarTargetPosition = bottomBarStartPosition + new Vector3(0, BarPositionMovement, 0);

        while (elapsedTime < BarMovementTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / BarMovementTime);

            TopBar.transform.position = Vector3.Lerp(topBarTargetPosition, topBarStartPosition, t);
            BottomBar.transform.position = Vector3.Lerp(bottomBarTargetPosition, bottomBarStartPosition, t);

            yield return null;
        }

        // Ensure the bars return to their start positions
        TopBar.transform.position = topBarStartPosition;
        BottomBar.transform.position = bottomBarStartPosition;
    }

}
