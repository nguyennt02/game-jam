using Assets.Scripts.Interface;
using UnityEngine;

public class WarterArea : MonoBehaviour, IActivated
{
    /// <summary>
    /// If the water level is 0, no one can cross. 
    /// If the water level is 1, only big characters can cross
    /// If the water level is more than 2, everyone can cross
    /// </summary>
    [SerializeField] private int m_WaterLevel = 0;

    public void Activate()
    {
        m_WaterLevel++;
    }


    /// <summary>
    /// Check the entering object and remove it when the conditions are met
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private bool CauseDeath(Player player)
    {
        if (player == null) return false;
        if (m_WaterLevel == 0 || (m_WaterLevel == 1 && player is PlayerSmall))
        {
            player.Die();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Check whether the object is entering or exiting and perform the corresponding action
    /// </summary>
    /// <param name="player"></param>
    /// <param name="exitArea"></param>
    private void Drag(ISpeed player, bool exitArea = true)
    {
        if (player == null) return;
        player.SetSpeed(exitArea ? 2 : 0.5f);
    }


    /// <summary>
    /// Method check object is entering
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        var character = other.GetComponent<ISpeed>();
        if(character == null) return;
        if (character is Player && CauseDeath(character as Player)) return;
        Drag(character, false);
    }

    /// <summary>
    /// Method check object is exiting
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null) return;
        var character = other.GetComponent<ISpeed>();
        if (character == null) return;
        Drag(character);
    }
}
