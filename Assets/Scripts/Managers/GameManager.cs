using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<PartyMember> _party;

    public List<Enemy> Enemies => _enemies;
    public List<PartyMember> Party => _party;
}