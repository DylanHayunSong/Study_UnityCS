using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ViewModeBase : MonoBehaviour
{
    protected ViewModeManager.ViewModes thisViewMode;
    protected ViewModeManager manager = ViewModeManager.inst;

    public Camera cam;
    public Transform pivot;

    [Header("Move")]
    public float moveSpeed = 0.1f;
    public float moveDempening = 10f;
    public Vector2 moveVerticalClamp = new Vector2(0, 1);

    [Header("Rotate")]
    public float rotateSensitivity = 0.1f;
    public float rotateDempening = 10f;
    public Vector2 rotateVerticalClamp = new Vector2(50, 310);

    protected Vector3 remainingPos;
    protected Vector3 pivotOriginalPos;
    protected Quaternion pivotOriginalRot;

    protected Vector3 remainingRot;
    protected Vector3 camOriginalPos;
    protected Quaternion camOriginalRot;

    protected bool isCamLookPivot;

    protected Vector3 newPos;
    protected Vector3 newRot;
    

    private void Start ()
    {
        manager = ViewModeManager.inst;        
        manager.OnViewModeChanged += ChangeViewMode;
        Initialize();
    }

    private void FixedUpdate ()
    {
        if (isCamLookPivot)
            cam.transform.LookAt(pivot);
        Move();
        Rotate();
    }

    protected virtual void Initialize ()
    {
        pivotOriginalPos = pivot.transform.position;
        pivotOriginalRot = pivot.transform.rotation;

        camOriginalPos = cam.transform.position;
        camOriginalRot = cam.transform.rotation;
    }

    protected virtual void ChangeViewMode (ViewModeManager.ViewModes nextMode)
    {
        ResetTransform();
        manager.viewModeObjDict[nextMode].SetActive(true);        
    }

    protected virtual void Move ()
    {
        
    }

    protected virtual void Rotate ()
    {

    }

    protected virtual void ResetTransform ()
    {
        pivot.transform.position = pivotOriginalPos;
        pivot.transform.rotation = pivotOriginalRot;
        cam.transform.position = camOriginalPos;
        cam.transform.rotation = camOriginalRot;
    }

    protected float GetDampenFactor (float dampening, float deltaTime)
    {
        if (dampening < 0.0f)
            return 1.0f;

        if (Application.isPlaying == false)
            return 1.0f;

        return 1.0f - Mathf.Exp(-dampening * deltaTime);
    }
}
