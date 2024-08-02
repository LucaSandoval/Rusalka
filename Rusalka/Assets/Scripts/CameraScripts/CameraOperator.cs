using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CameraOperator : Singleton<CameraOperator>
{
    private float shakeTime;
    [SerializeField] private List<CameraZone> zones;
    // private variable storing camera position
    [SerializeField] private float minXBoundary;
    [SerializeField] private float maxXBoundary;
    [SerializeField] private float minYBoundary;
    [SerializeField] private float maxYBoundary;
    // xDistance - how far the camera is going to be from the player in x axis when they are moving
    // yDistance - how far away from the camera player has to move in y axis for it to start following them
    //[SerializeField] private float xDistance;
    //[SerializeField] private float yDistance;
    // How fast will the camera follow player
    [SerializeField] private float cameraSpeed = 12f;
    // How fast will the camera zoom in and out
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private Transform dynamicTarget;
    // Another object you might want the camera to lock onto
    [SerializeField] private Vector3 staticPoint = new(0f,0f,-10f);
    // Size of camera for normal view
    [SerializeField] private float normalSize = 12f;
    // Size of camera for vista points
    [SerializeField] private float vistaSize = 25f;

    /* -------------------------------------------
       Default Static Variables for CameraOperator
       ------------------------------------------- */
    private static float minXBound;
    private static float maxXBound;
    private static float minYBound;
    private static float maxYBound;
    private static float camSpeed;
    private static float zomSpeed;
    private static Transform dynTarget;
    private static Vector3 staPoint;
    private static float smallSize;
    private static float bigSize;
    private static CameraMode mode;
    private static Target target;
    private static float xOffset;
    private static float yOffset;
    private static bool xShakeOn;
    private static bool yShakeOn;
    private static float xShakePow;
    private static float yShakePow;

    void Start()
    {
        dynamicTarget = GameObject.FindWithTag("Player").transform;
        shakeTime = 0f;
        zones = new List<CameraZone>();
        // Assigning defaults
        minXBound = minXBoundary;
        maxXBound = maxXBoundary;
        minYBound = minYBoundary;
        maxYBound = maxYBoundary;
        camSpeed = cameraSpeed;
        zomSpeed = zoomSpeed;
        dynTarget = dynamicTarget;
        staPoint = staticPoint;
        smallSize = normalSize;
        bigSize = vistaSize;
        mode = cameraMode;
        target = cameraTarget;
        xOffset = xAxisOffset;
        yOffset = yAxisOffset;
        xShakeOn = xAxisShakeEnabled;
        yShakeOn = yAxisShakeEnabled;
        xShakePow = xShakeStrength;
        yShakePow = yShakeStrength;
    }
    private void resetToDefaults()
    {
        minXBoundary = minXBound;
        minYBoundary = minYBound;
        maxXBoundary = maxXBound;
        maxYBoundary = maxYBound;
        cameraSpeed = camSpeed;
        zoomSpeed = zomSpeed;
        dynamicTarget = dynTarget;
        staticPoint = staPoint;
        normalSize = smallSize;
        vistaSize = bigSize;
        cameraMode = mode;
        cameraTarget = target;
        xAxisOffset = xOffset; 
        yAxisOffset = yOffset;
        isShaking = false;
        xAxisShakeEnabled = xShakeOn;
        yAxisShakeEnabled = yShakeOn;
        xShakeStrength = xShakePow;
        yShakeStrength = yShakePow;
        xAxisMoveEnabled = true;
        yAxisMoveEnabled = true;
    }
    public void addCameraZone(CameraZone zone)
    {
        zones.Add(zone);
        zones.Sort((x,y) => y.getPriority().CompareTo(x.getPriority()));
    }
    public void removeCameraZone(CameraZone zone)
    {
        zones.Remove(zone);
    }
    // Who the camera is currently following
    public enum Target{
        Dynamic,
        Static
    }
    // Is camera zoomed in on the player or doing a vista point
    public enum CameraMode{
        Normal,
        VistaPoint
    }
    // default camera settings
    [SerializeField] private Target cameraTarget = Target.Dynamic;
    [SerializeField] private CameraMode cameraMode = CameraMode.Normal;
    [SerializeField] private bool isShaking = false;
    [SerializeField] private float xShakeStrength = 2f;
    [SerializeField] private float yShakeStrength = 2f;
    [SerializeField] private bool xAxisShakeEnabled = true;
    [SerializeField] private bool yAxisShakeEnabled = true;
    [SerializeField] private float xAxisOffset = 0.1f;
    [SerializeField] private float yAxisOffset = 0.1f;
    [SerializeField] private bool xAxisMoveEnabled = true;
    [SerializeField] private bool yAxisMoveEnabled = true;    
    /// <summary>
    /// Changes the size of camera to a specified size
    /// </summary>
    public void ResizeCamera(Camera C, float Size){
        C.orthographicSize = Mathf.SmoothStep(C.orthographicSize, Size, zoomSpeed * Time.fixedDeltaTime);
    }
    /// <summary>
    /// Quickly changes camera position with pre-definied strength to imitate camera shaking
    ///  <list>+Runs on Update instead of FixedUpdate</list>
    ///  <list>+Uses an "internal" timer</list>
    ///  <list>+Works with all Follow targets and Camera Modes</list>
    /// </summary>    
    public float GetCameraSpeed(){
        return cameraSpeed;
    }
    public void SetCameraSpeed(float val){
        cameraSpeed = val;
    }

    public float GetZoomSpeed(){
        return zoomSpeed;
    }
    public void SetZoomSpeed(float val){
        zoomSpeed = val;
    }
    public Transform GetDynamicTarget(){
        return dynamicTarget;
    }
    public void SetDynamicTarget(Transform t){
        dynamicTarget = t;
    }
    public Vector3 GetStaticPoint(){
        return staticPoint;
    }
    public void SetStaticPoint(Vector3 vec){
        staticPoint = vec;
    }
    public float GetNormalSize(){
        return normalSize;
    }
    public void SetNormalSize(float f){
        normalSize = f;
    }
    public float GetVistaSize(){
        return vistaSize;
    }
    public void SetVistaSize(float f){
        vistaSize = f;
    }
    public Target GetCameraTarget(){
        return cameraTarget;
    }
    public void SetCameraTarget(Target fol){
        cameraTarget = fol;
    }
    public CameraMode GetCameraMode(){
        return cameraMode;
    }
    public void SetCameraMode(CameraMode cm){
        cameraMode = cm;
    }
    public bool GetIsShaking(){
        return isShaking;
    }
    public void SetIsShaking(bool boolean){
        isShaking = boolean;
    }
    public float GetXShakeStrength(){
        return xShakeStrength;
    }
    public void SetXShakeStrength(float f){
        xShakeStrength = f;
    }
    public float GetYShakeStrength()
    {
        return yShakeStrength;
    }
    public void SetYShakeStrength(float f)
    {
        yShakeStrength = f;
    }
    public bool GetXAxisShakeEnabled(){
        return xAxisShakeEnabled;
    }
    public void SetXAxisShakeEnabled(bool boolean){
        xAxisShakeEnabled = boolean;
    }
    public bool GetYAxisShakeEnabled(){
        return yAxisShakeEnabled;
    }
    public void SetYAxisShakeEnabled(bool boolean){
        yAxisShakeEnabled = boolean;
    }
    public float GetXAxisOffset(){
        return xAxisOffset;
    }
    public void SetXAxisOffset(float val){
        xAxisOffset = val;
    }
    public float GetYAxisOffset(){
        return yAxisOffset;
    }
    public void SetYAxisOffset(float val){
        yAxisOffset = val;
    }
    public bool GetXAxisMoveEnabled(){
        return xAxisMoveEnabled;
    }
    public void SetXAxisMoveEnabled(bool boolean){
        xAxisMoveEnabled = boolean;
    }
    public bool GetYAxisMoveEnabled(){
        return yAxisMoveEnabled;
    }
    public void SetYAxisMoveEnabled(bool boolean){
        yAxisMoveEnabled = boolean;
    }
    public float GetMinXBoundary(){
        return minXBoundary;
    }
    public void SetMinXBoundary(float f){
        minXBoundary = f;
    }
    public float GetMinYBoundary(){
        return minYBoundary;
    }
    public void SetMinYBoundary(float f){
        minYBoundary = f;
    }
    public float GetMaxXBoundary(){
        return maxXBoundary;
    }
    public void SetMaxXBoundary(float f){
        maxXBoundary = f;
    }
    public float GetMaxYBoundary(){
        return maxYBoundary;
    }
    public void SetMaxYBoundary(float f){
        maxYBoundary = f;
    }
    /*public float GetXDistance(){
        return xDistance;
    }
    /*public void SetXDistance(float f){
        xDistance = f;
    }
    public float GetYDistance(){
        return yDistance;
    }
    public void SetYDistance(float f){
        yDistance = f;
    }*/
    public IEnumerator ShakeCamera()
    {
        Vector3 cameraPos = transform.position; 
        if (xAxisShakeEnabled)
        {
            cameraPos.x += UnityEngine.Random.Range(-xShakeStrength, xShakeStrength);
        }
        if (yAxisShakeEnabled)
        {
            cameraPos.y += UnityEngine.Random.Range(-yShakeStrength, yShakeStrength);
        }
        transform.position = Vector3.Slerp(transform.position, cameraPos, cameraSpeed / 3 * Time.fixedDeltaTime);
        yield return new WaitForSeconds(Time.fixedDeltaTime);

    }
    void FixedUpdate()
    {
        // fetching main camera object
        Camera Cam = Camera.main;
        if (PauseController.Instance == null || !PauseController.Instance.IsGamePaused()){
        float facing = 1;
        /*bool grounded = true;
        float movementSpeed = 0;*/
        shakeTime += Time.fixedDeltaTime;
        if (dynamicTarget != null && dynamicTarget.CompareTag("Player"))
        {
            facing = dynamicTarget.GetComponent<PlayerController>().Facing().x > 0 ? 1 : -1;
            /*grounded = dynamicTarget.GetComponent<PlayerController>().IsGrounded();
            movementSpeed = dynamicTarget.GetComponent<PlayerController>().GetMovementSpeed();*/
        }
        if (zones.Count == 0)
            {
                // if player is in no camera zones, changes to camera reset back to default
                resetToDefaults();
            }
        else
            {   
                // Otherwise it will use the settings from the highest priority zone
                zones[0].ApplyChanges();
            }
        // moves the camera as specified by the target
        switch(cameraTarget){
            case Target.Dynamic:
                float x = dynamicTarget.position.x;
                float y = dynamicTarget.position.y;
                Vector3 newCameraPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
                if (xAxisMoveEnabled){
                    if (x - xAxisOffset > maxXBoundary) 
                        newCameraPosition.x = maxXBoundary;
                    else if (x - xAxisOffset < minXBoundary) 
                        newCameraPosition.x = minXBoundary;
                    else
                        newCameraPosition.x = x - xAxisOffset;
                }
                if (yAxisMoveEnabled){
                    if (y - yAxisOffset > maxYBoundary)
                        newCameraPosition.y = maxYBoundary;
                    else if (y - yAxisOffset < minYBoundary)
                        newCameraPosition.y = minYBoundary;
                    else
                        newCameraPosition.y = y - yAxisOffset;
                }
                transform.position = Vector3.SlerpUnclamped(
                    transform.position, newCameraPosition, cameraSpeed * Time.fixedDeltaTime);
                break;
            case Target.Static:
                newCameraPosition = staticPoint;
                transform.position = Vector3.SlerpUnclamped(transform.position, newCameraPosition, cameraSpeed/3 * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
        // swaps between camera modes as specified
        switch (cameraMode){
            case CameraMode.Normal:
                ResizeCamera(Cam, normalSize);
                break;
            case CameraMode.VistaPoint:
                ResizeCamera(Cam, vistaSize);
                break;
            default: 
                break;        
        }
        if (isShaking && shakeTime < 1)
        {
            StartCoroutine(ShakeCamera());
        }
        if (shakeTime > 1) shakeTime = 0;
    }
    }
}