using UnityEngine;
using Game.Entity;
using Game.InputManagment;

[RequireComponent(typeof(Player))]
public sealed class PlayerController : MonoBehaviour
{
    private Player _player;

    private IPlayerMover _playerMover;

    private void Awake()
    {
        _player = this.GetComponent<Player>();
        _playerMover = new PlayerMoveModule(_player);
    }

    private void FixedUpdate()
    {
        _playerMover.Update();
    }
}