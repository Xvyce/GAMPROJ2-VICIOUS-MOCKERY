using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FlipPage : MonoBehaviour
{
    public enum ButtonType
    {
        NextButton,
        BackButton
    }

    [SerializeField] Button nextBtn;
    [SerializeField] Button backBtn;
    [SerializeField] Button closeBtn;

    private Vector3 rotationVector;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool isClicked;

    private DateTime startTime;
    private DateTime endTime;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;

        if (nextBtn != null)
            nextBtn.onClick.AddListener(() => turnOnePageBtn_Click(ButtonType.NextButton));

        if (backBtn != null)
            backBtn.onClick.AddListener(() => turnOnePageBtn_Click(ButtonType.BackButton));

        if (closeBtn != null)
            closeBtn.onClick.AddListener(() => closeBookBtn_Click());
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            transform.Rotate(rotationVector * Time.deltaTime);

            endTime = DateTime.Now;
            if((endTime - startTime).TotalSeconds >= 1)
            {
                isClicked = false;
                transform.rotation = startRotation;
                transform.position = startPosition;
            }
        }
    }

    public void turnOnePageBtn_Click(ButtonType type)
    {
        isClicked = true;
        startTime = DateTime.Now;

        if (type  == ButtonType.NextButton)
        {
            rotationVector = new Vector3(0, 180, 0);
        }
        else if (type == ButtonType.BackButton)
        {
            Vector3 newRotation = new Vector3(startRotation.x, 180, startRotation.z);
            transform.rotation = Quaternion.Euler(newRotation);

            rotationVector = new Vector3(0, -180, 0);
        }

        FindObjectOfType<AudioManager>().Play("PageFlip_SFX");
    }

    public void closeBookBtn_Click()
    {
        AppEvents.CloseBookFunction();
    }
}
