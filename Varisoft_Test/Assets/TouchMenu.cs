
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TouchMenu : MonoBehaviour, IPointerClickHandler
{
   public void OnPointerClick(PointerEventData _eventData) => SceneManager.LoadScene(1);
}
