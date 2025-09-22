using UnityEngine;
using UnityEngine.SceneManagement; // ��� ������ �� �������

public class MainMenuController : MonoBehaviour
{
    // ���� ����� �� �������� � ������ "������"
    public void PlayGame()
    {
        // ��������� ����� � �����. "Game" - ��� ������ ��� ������ ����� �����.
        SceneManager.LoadScene("Game");
    }

    // ����� ��� ������ "�������" (���� ����� ������ �������� ���������)
    public void OpenShop()
    {
        Debug.Log("��������� �������... (������ ��� �� �����������)");
        // ����� � ������� ����� ��� ��� �������� ���� ��������
    }

    // ����� ��� ������ "�������"
    public void OpenHighscores()
    {
        Debug.Log("��������� �������... (������ ��� �� �����������)");
        // ����� ����� ��� ��� �������� ���� ��������
    }

    // ����� ��� ������ "����"
    public void ToggleSound()
    {
        Debug.Log("����������� ����... (������ ��� �� �����������)");
        // ����� ����� ��� ��� ���������/���������� �����
    }
}