using UnityEngine;


public class CharacterSprite : MonoBehaviour
{

    public static readonly string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static readonly string[] runDirections = {"Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE"};

    private Animator animator;
    private int lastDirection;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void SetDirection(Vector2 _direction)
    {
        string[] _directionArray = null;

        if (_direction.magnitude < .01f)
            _directionArray = staticDirections;
        else
        {
            _directionArray = runDirections;
            lastDirection = DirectionToIndex(_direction, 8);
        }

        animator.Play(_directionArray[lastDirection]);
    }

  
    public static int DirectionToIndex(Vector2 _dir, int _sliceCount){
      
        Vector2 _normDir = _dir.normalized;
      
        float _step = 360f / _sliceCount;
     
        float _halfstep = _step / 2;
        float _angle = Vector2.SignedAngle(Vector2.up, _normDir);
        _angle += _halfstep;

        if (_angle < 0)
            _angle += 360;

        float _stepCount = _angle / _step;
        return Mathf.FloorToInt(_stepCount);
    }

    public static int[] AnimatorStringArrayToHashArray(string[] _animationArray)
    {
        int[] _hashArray = new int[_animationArray.Length];

        for (int i = 0; i < _animationArray.Length; i++)
            _hashArray[i] = Animator.StringToHash(_animationArray[i]);

        return _hashArray;
    }

}
