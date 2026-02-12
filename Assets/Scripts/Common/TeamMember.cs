using UnityEngine;
public enum Team { Neutral, Player, Enemy }

public class TeamMember : MonoBehaviour
{
    [SerializeField] private Team _team;
    public Team Team => _team;

    public void ChangeTeam(Team newTeam)
    {
        _team = newTeam;
    }
}