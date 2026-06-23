using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class HitFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField, Min(0.01f)] private float flashDuration = 0.08f;

    private SpriteRenderer _spriteRenderer;
    private Color _baseColor;
    private Coroutine _flashRoutine;




    private void Awake()
    {
        

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;
    }


    private void Update()
    {
  
        if (Keyboard.current.eKey.isPressed)
        {
     
            Hit();
        }
    }



    public void Hit()
    {
       

        if (!isActiveAndEnabled || _spriteRenderer == null)
            return;

        if (_flashRoutine != null)
            StopCoroutine(_flashRoutine);

        _flashRoutine = StartCoroutine(FlashRoutine());

      

    }

    private IEnumerator FlashRoutine()
    {
        _spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.color = _baseColor;
        _flashRoutine = null;
    }

    private void OnDisable()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.color = _baseColor;

        _flashRoutine = null;
    }
}
