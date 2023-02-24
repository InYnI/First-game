using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _fire1;
    [SerializeField] private SpriteRenderer _fire2;
    [SerializeField] private SpriteRenderer _fire3;

    private float _timer = 1;
    private float _time;

    private void Update()
    {
        _timer += Time.deltaTime;
        _time = _timer;

        if ((_time == 1) | (_time == _timer + 4))
        {
            Instantiate(_fire1);
        }
        
    }
}
