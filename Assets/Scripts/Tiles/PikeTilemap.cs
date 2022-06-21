using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PikeTilemap : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;

    [SerializeField] private int _damage;

    [SerializeField] private float _speedOfFrames;
    [SerializeField] private int _fullFramesOfAnimation;
    [SerializeField] private int _framesToStayAnimation;

    [SerializeField] private float _timeOfStaying;

    [SerializeField] private float _couldownPikes;

    private Vector3Int _currentTileVector;
    private PikeTile _currentTile;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("OnTrigger");
            _currentTileVector = _tilemap.WorldToCell(player.transform.position);
            _currentTile = _tilemap.GetTile<PikeTile>(_currentTileVector);

            if (_currentTile != null && _currentTile.ActivationStarted == false)
                StartCoroutine(Activation(_currentTile, _currentTileVector));
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player) && _currentTile != null && _currentTile.PikeActivated && _currentTile.CouldownStarted == false)
        {
            player.TakingDamage(_damage);
            StartCoroutine(Couldown(_currentTile));
        }
    }

    private IEnumerator Activation(PikeTile currentTile, Vector3Int currentTileVector)
    {
        Debug.Log("Activation");
        currentTile.ActivationStarted = true;

        for (int i = 0; i < _framesToStayAnimation; i++)
        {

            _tilemap.SetAnimationFrame(currentTileVector, i);

            yield return new WaitForSeconds(_speedOfFrames);
        }

        _currentTile.PikeActivated = true;

        yield return new WaitForSeconds(_timeOfStaying);

        for(int i = _tilemap.GetAnimationFrame(currentTileVector); i < _fullFramesOfAnimation; i++)
        {

            _tilemap.SetAnimationFrame(currentTileVector, i);

            yield return new WaitForSeconds(_speedOfFrames);
        }

        currentTile.ActivationStarted = false;
        currentTile.PikeActivated = false;
    }

    private IEnumerator Couldown(PikeTile currentTile)
    {
        currentTile.PikeActivated = false;
        currentTile.CouldownStarted = true;
        Debug.Log("Couldown");

        var expiredTime = 0f;

        while(expiredTime < _couldownPikes)
        {
            expiredTime+=Time.deltaTime;
            yield return null;
        }
        currentTile.CouldownStarted = false;
        currentTile.PikeActivated = true;
    }
}
