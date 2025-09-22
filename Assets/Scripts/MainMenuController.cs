using UnityEngine;
using UnityEngine.SceneManagement; // Для работы со сценами

public class MainMenuController : MonoBehaviour
{
    // Этот метод мы привяжем к кнопке "Играть"
    public void PlayGame()
    {
        // Загружаем сцену с игрой. "Game" - это точное имя вашего файла сцены.
        SceneManager.LoadScene("Game");
    }

    // Метод для кнопки "Магазин" (пока будет просто выводить сообщение)
    public void OpenShop()
    {
        Debug.Log("Открываем магазин... (логика еще не реализована)");
        // Здесь в будущем будет код для открытия окна магазина
    }

    // Метод для кнопки "Рекорды"
    public void OpenHighscores()
    {
        Debug.Log("Открываем рекорды... (логика еще не реализована)");
        // Здесь будет код для открытия окна рекордов
    }

    // Метод для кнопки "Звук"
    public void ToggleSound()
    {
        Debug.Log("Переключаем звук... (логика еще не реализована)");
        // Здесь будет код для включения/выключения звука
    }
}