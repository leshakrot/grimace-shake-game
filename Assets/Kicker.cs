using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    public GameObject gameOverPopup; // ������ �� ����� � game over
    public void Attack()
    {
        // ����� ����� ���� ��� ��� ��� ��������� ����� ������
        // ��������, ���� � ������ ���� ��������� ��������, ����� ������� ����� ��������� ����� � ������.

        // ���������� ����� � game over
        if (gameOverPopup != null)
        {
            gameOverPopup.SetActive(true);
        }
    }
}
