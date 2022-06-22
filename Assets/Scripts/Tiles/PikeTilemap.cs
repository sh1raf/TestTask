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

    private bool _activationStarted;
    private bool _pikeIsActivated;

    private Vector3Int _currentTileVector = new Vector3Int(0,0,0);
    private PikeTile _currentTile;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity))
        {
            _currentTile = _tilemap.GetTile<PikeTile>(_currentTileVector);

            if (_currentTile != null && _activationStarted == false)
            {
                StartCoroutine(Activation());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity) && _currentTile != null && _pikeIsActivated)
        {
            entity.TakingDamage(_damage);
            _pikeIsActivated = false;

            if (_activationStarted == false)
                StartCoroutine(Activation());
        }
    }

    private IEnumerator Activation()
    {
        Debug.Log("Activation");
        _activationStarted = true;

        for (int i = 0; i < _framesToStayAnimation; i++)
        {

            _tilemap.SetAnimationFrame(_currentTileVector, i);

            yield return new WaitForSeconds(_speedOfFrames);
        }

        _pikeIsActivated = true;

        yield return new WaitForSeconds(_timeOfStaying);

        for(int i = _tilemap.GetAnimationFrame(_currentTileVector); i < _fullFramesOfAnimation; i++)
        {

            _tilemap.SetAnimationFrame(_currentTileVector, i);

            yield return new WaitForSeconds(_speedOfFrames);
        }

        _activationStarted = false;
        _pikeIsActivated = false;
    }
}
