using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public delegate void OnFirstClickDelegate();
    public static OnFirstClickDelegate OnFirstClickEvent;

    #region Configuration
    [Header("References")]//REF
    public GameManager _gameManager;

    [Header("Movement")]
    [SerializeField] private Vector2 randomMovementBoxSize;
    [SerializeField] private Vector2 randomMovementBoxOffset;
    [SerializeField] private float movementSpeed;
    [SerializeField] private AnimationCurve randomMovementPlayerAnimCurve;
    /// <summary>
    /// Position in time of the curve, for player movement
    /// </summary>
    private float animCurveTimePosition;
    private bool gotToTarget;

    [Header("Start Support")]
    [SerializeField] private AnimationCurve getToCenterAnimCurve;
    [SerializeField] private float getToCenterSpeed;
    private float getToCenterAnimCurveTime;
    private Coroutine waitCoroutine;
    private Coroutine moveCoroutine;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private Material allInOneMaterial;

    [Header("Win The Game Support")]
    [SerializeField] private Transform skyLocation;
    [SerializeField] private Animator splashAnimator;
    [SerializeField] private float getToSkySpeed;
    [SerializeField] private AnimationCurve movingToSkyAnimationCurve;

    #endregion

    //[Header("Debug Box")]
    //public Vector3 cubeSize;
    //public Vector3 cubePos;

    private void Awake()
    {
        allInOneMaterial = GetComponent<Renderer>().material;
    }

    #region PlayerMovement

    public void MovePlayerToSky()
    {
        StartCoroutine(MovePlayerToSkyCoroutine());
    }

    IEnumerator MovePlayerToSkyCoroutine()
    {
        var skyAnimationCurveTime = 0f;
        Vector3 targetPosition = skyLocation.position;

        Vector3 startPos = gameObject.transform.position;

        while (transform.position != targetPosition)
        {
            skyAnimationCurveTime += Time.deltaTime * getToSkySpeed;
            gameObject.transform.position = Vector3.Lerp(startPos, targetPosition, movingToSkyAnimationCurve.Evaluate(skyAnimationCurveTime));
            yield return null;
        }
    }

    public void MovePlayerToCenter()
    {
        StartCoroutine(MovePlayerToStartingLocation());
    }

    IEnumerator MovePlayerToStartingLocation()
    {
        getToCenterAnimCurveTime = 0f;
        Vector3 targetPosition = _gameManager.startingPosition.transform.position;

        //save starting position
        Vector3 startPos = gameObject.transform.position;

        while (transform.position != targetPosition)
        {
            getToCenterAnimCurveTime += Time.deltaTime * getToCenterSpeed;
            gameObject.transform.position = Vector3.Lerp(startPos, targetPosition, getToCenterAnimCurve.Evaluate(getToCenterAnimCurveTime));
            yield return null;
        }
    }

    public void MovePlayerAround()
    {
        waitCoroutine = StartCoroutine(MovePlayerAroundScreenCoroutine());
    }

    IEnumerator MovePlayerAroundScreenCoroutine()
    {
        while (_gameManager.playing)
        {
            gotToTarget = false;
            var timeToWaitBetweenMoving = _gameManager.randomMovementTime / _gameManager.currentTimeBetweenMovementMultiplier;
            yield return new WaitForSeconds(timeToWaitBetweenMoving);          

            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            animCurveTimePosition = 0f;
            moveCoroutine = StartCoroutine(MovePlayerCoroutine());

            while (!gotToTarget)
            {
                yield return null;
            }
        }       
    }
    
    private IEnumerator MovePlayerCoroutine()
    {
        //scale size of box depending on dificulty
        var boxSizeX = randomMovementBoxSize.x * _gameManager.currentMoveDistanceMultiplier;
        var boxSizeY = randomMovementBoxSize.y * _gameManager.currentMoveDistanceMultiplier;

        //choose destination
        var xPos = Random.Range((-boxSizeX / 2) + randomMovementBoxOffset.x, (boxSizeX / 2) + randomMovementBoxOffset.x);
        var yPos = Random.Range((-boxSizeY / 2) + randomMovementBoxOffset.y, (boxSizeY / 2) + randomMovementBoxOffset.y);
        
        Vector3 destination = new Vector3(xPos, yPos, gameObject.transform.position.z);

        //save starting position
        Vector3 startPos = gameObject.transform.position;

        while(gameObject.transform.position != destination)
        {
            animCurveTimePosition += Time.deltaTime * movementSpeed * _gameManager.currentPlayerMovementMultiplier;
            gameObject.transform.position = Vector3.Lerp(startPos, destination, randomMovementPlayerAnimCurve.Evaluate(animCurveTimePosition));
            yield return null;
        }

        gotToTarget = true;
    }

    public void CancelMovement()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
        }
    }

    #endregion

    #region Animation
    public void Swim()
    {
        animator.SetTrigger("Swim");
    }

    public void StopSwimming()
    {
        animator.SetTrigger("Idle");
    }

    public void IncrementSwimmingSpeedAnimation(float speed)
    {
        animator.speed = speed;
    }

    public void DoSplashAnimation()
    {
        splashAnimator.SetTrigger("DoAnimation");
    }

    public void TriggerAnimation(string animationString)
    {
        animator.SetTrigger(animationString);
    }
    #endregion

    #region Shader Effects
    public void PlayHitEffectAnimation(float duration)
    {
        StartCoroutine(HitEffectCoroutine(duration));
    }

    IEnumerator HitEffectCoroutine(float TimeToHalf)
    {
        float timer = 0;

        while (timer < TimeToHalf)
        {
            var current = Mathf.InverseLerp(0, TimeToHalf, timer);
            allInOneMaterial.SetFloat("_HitEffectBlend", current);

            timer += Time.deltaTime;
            yield return null;
        }

        timer = TimeToHalf;

        while (timer >= TimeToHalf)
        {
            var current = Mathf.InverseLerp(TimeToHalf, 0, timer);
            allInOneMaterial.SetFloat("_HitEffectBlend", current);

            timer -= Time.deltaTime;
            yield return null;
        }
    }

    public void MissClickShaderEffect(float duration)
    {
        StartCoroutine(MissClickShaderEffectCoroutine(duration));
    }

    IEnumerator MissClickShaderEffectCoroutine(float duration)
    {
        allInOneMaterial.SetFloat("_HsvShift", 174);
        allInOneMaterial.SetFloat("_HsvSaturation", 0.435f);
        allInOneMaterial.SetFloat("_ShakeUvSpeed", 20);
        yield return new WaitForSeconds(duration);
        allInOneMaterial.SetFloat("_HsvShift", 0);
        allInOneMaterial.SetFloat("_HsvSaturation", 1);
        allInOneMaterial.SetFloat("_ShakeUvSpeed", 0);
    }

    public void SetAllInOneMaterialFloat(string parameter, float value)
    {
        allInOneMaterial.SetFloat(parameter, value);
    }

    #endregion

    //private void OnDrawGizmos()
    //{
    //    var centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(cubePos, cubeSize);
    //}
}
