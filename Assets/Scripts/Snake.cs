using CodeMonkey;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private enum State
    {
        Alive,
        Dead
    }
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    private bool canChangeDirection = true;
    private bool canMovement = true;
    private State state;
    [SerializeField]private Transform segmentPrefab;
    [SerializeField]private float speed = 5.0f;
    [SerializeField]private int initialSize = 4;

    private float timer = 0.0f;
    private void Start()
    {
        ResetState();
    }
    private void Awake()
    {
        state = State.Alive;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Alive:
                SnakeMoveMent();
                canMovement = true;
                break;
            case State.Dead:
                canMovement = false;
                break;
        }
        
    }
    private void SnakeMoveMent()
    {
        if (canChangeDirection)
        {
            if (_direction != Vector2.down && Input.GetKeyDown(KeyCode.W))
            {
                _direction = Vector2.up;
                canChangeDirection = false;
            }
            else if (_direction != Vector2.up && Input.GetKeyDown(KeyCode.S))
            {
                _direction = Vector2.down;
                canChangeDirection = false;
            }
            else if (_direction != Vector2.right && Input.GetKeyDown(KeyCode.A))
            {
                _direction = Vector2.left;
                canChangeDirection = false;
            }
            else if (_direction != Vector2.left && Input.GetKeyDown(KeyCode.D))
            {
                _direction = Vector2.right;
                canChangeDirection = false;
            }
        }
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (canMovement)
        {
            if (timer >= 1.0f / speed)
            {
                timer = 0.0f;

                for (int i = _segments.Count - 1; i > 0; i--)
                {
                    _segments[i].position = _segments[i - 1].position;
                    //float angleDifference = Quaternion.Angle(_segments[i - 1].rotation, _segments[i].rotation);
                    //if (_segments[i - 1].rotation.z > _segments[i].rotation.z) {
                    //    if (_segments[i - 1].rotation.z == 180 && _segments[i].rotation.z != 90)
                    //    {
                    //        _segments[i].Rotate(0, 0, -45);
                    //    }
                    //    _segments[i].Rotate(0, 0, 45);
                    //}
                    //else if (_segments[i - 1].rotation.z < _segments[i].rotation.z )
                    //{
                    //    if(_segments[i - 1].rotation.z == -90 && _segments[i].rotation.z != 0)
                    //    {
                    //        _segments[i].Rotate(0, 0, 45);
                    //    }
                    //    _segments[i].Rotate(0, 0, -45);
                    //}
                    _segments[i].rotation = _segments[i - 1].rotation;
                    //print(angleDifference);
                }
                Vector3 oldPosition = this.transform.position;
                this.transform.position = new Vector3(
                    Mathf.Round(this.transform.position.x + _direction.x),
                    Mathf.Round(this.transform.position.y + _direction.y),
                    0.0f
                );
                if (this.transform.position != oldPosition)
                {
                    SoundManager.PlaySound(SoundManager.Sound.SnakeMove);
                    canChangeDirection = true;
                }
                if (_direction == Vector2.up)
                {
                    this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
                else if (_direction == Vector2.down)
                {
                    this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180f);
                }
                else if (_direction == Vector2.right)
                {
                    this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90f);
                }
                else if (_direction == Vector2.left)
                {
                    this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);
                }
            }
        }
        
    }
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = new Vector3(100,100,0);
        //_segments[_segments.Count - 1].position
        _segments.Add( segment );
    }
    private void ResetState()
    {
        for(int i=1;i<_segments.Count;i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(transform);
        for(int i =1;i<this.initialSize;i++)
        {
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = new Vector3(100, 100, 0);
            _segments.Add(segment);
        }

        this.transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            SoundManager.PlaySound(SoundManager.Sound.SnakeEat);
            Grow();
            GameHandler.AddScore();
        } 
        else if (collision.tag == "Wall" || collision.tag == "Duoi")
        {
            SoundManager.PlaySound(SoundManager.Sound.SnakeDie);
            CMDebug.TextPopup("DEAD!", transform.position);
            state = State.Dead;
            GameHandler.SnakeDied();
        }

    }
}
