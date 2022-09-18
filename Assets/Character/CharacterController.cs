using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    private Animator m_pAnimator;
    private Character m_pCharacter;

    private void Start()
    {
        m_pAnimator = GetComponent<Animator>();
        m_pCharacter = GetComponent<Character>();
    }

    private void Update()
    {
        Vector2 vDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            vDirection.x = -1;
            m_pAnimator.SetInteger("Direction", 3);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            vDirection.x = 1;
            m_pAnimator.SetInteger("Direction", 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            vDirection.y = 1;
            m_pAnimator.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vDirection.y = -1;
            m_pAnimator.SetInteger("Direction", 0);
        }

        vDirection.Normalize();
        m_pAnimator.SetBool("IsMoving", vDirection.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = m_pCharacter.Movespeed.Value * vDirection;
    }
}
