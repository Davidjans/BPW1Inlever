using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class PlayerLook : MonoBehaviour
{
    private PlayerCharacterManager m_PlayerCharacterManager;

    [FoldoutGroup("Settings")]
    public float rotateSpeed;
    [HideInEditorMode]
    private Vector2 m_HeadRotation;
    [HideInEditorMode]
    private Vector2 m_BodyRotation;
    [HideInEditorMode]
    private Transform m_MainTransform;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerCharacterManager = GetComponentInParent<PlayerCharacterManager>();
        m_MainTransform = m_PlayerCharacterManager.GetComponent<Transform>();
        m_BodyRotation = m_MainTransform.transform.localEulerAngles;
        m_HeadRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        var look = m_PlayerCharacterManager.m_PlayerInput.gameplay.look.ReadValue<Vector2>();
        Look(look);
    }

    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.01)
            return;
        var scaledRotateSpeed = rotateSpeed * Time.deltaTime;
        m_BodyRotation.y += rotate.x * scaledRotateSpeed;
        m_HeadRotation.x = Mathf.Clamp(m_HeadRotation.x - rotate.y * scaledRotateSpeed, -89, 89);
        transform.localEulerAngles = m_HeadRotation;
        m_MainTransform.localEulerAngles = m_BodyRotation;
    }
}
