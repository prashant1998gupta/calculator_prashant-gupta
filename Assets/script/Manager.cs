using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Manager : MonoBehaviour {

   /* public VerticalLayoutGroup  buttonGroup;
    public HorizontalLayoutGroup bottonRow;
    public RectTransform canvasRect;
    CalcButton [] bottomButtons;*/

    public Text digitLabel;
    public Text operatorLabel;
    public Text resultLabel;
    bool errorDisplayed;
    bool displayValid;
    bool specialAction;
    double currentVal;
    double storedVal;
    double result;
    char stroredOperator;
    string expretion;
    int n = 123;


   // bool canvasChanged;
/*
    private void awake()
    {
        bottomButtons = bottonRow.GetComponentsInChildren<CalcButton>();
    }
*/
      
    

	// Use this for initialization
	void Start () {
       /* bottonRow.childControlWidth = false;
        canvasChanged = true;*/
        buttonTapped('c');

        

	}
	
	// Update is called once per frame
	void Update () {
/*
        if (canvasChanged)
        {
            canvasChanged = false;
            adjustButtons();
        }*/

    }

   /* private void OnRectTransformDimensionsionsChange()
    {
        canvasChanged = true;
    }

    void adjustButtons()
    {
        if (bottomButtons == null || bottomButtons.Length == 0)
            return;
        float buttonSize = canvasRect.sizeDelta.x / 4;
        float bWidth = buttonSize - bottonRow.spacing;
        for(int i=1; i<bottomButtons.Length; i++)
        {
            bottomButtons[i].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bWidth);
        }
        bottomButtons[0].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bWidth * 2 + bottonRow.spacing);
            
             
    }
*/
    void clearcalc()
    {
        digitLabel.text = "";
        operatorLabel.text = "";
        resultLabel.text = "";
        specialAction = displayValid = errorDisplayed = false;
        currentVal = result = storedVal = 0;
        stroredOperator = ' ';

    }
    
    void backSpace()
    {


    }

    void updateDigitlabel()
    {
        if (!errorDisplayed)
            digitLabel.text = currentVal.ToString();
        displayValid = false;
    }

    void calcResult(char activeOp)
    {
        switch (activeOp)
        {
            case '=':
                result = currentVal;
                resultLabel.text =  result + "";
                break;
            case '+':
                result = storedVal + currentVal;
                  resultLabel.text = result + "";
                break;
            case '-':
                result = storedVal - currentVal;
                resultLabel.text = result + "";
                break;
            case 'x':
                result = storedVal * currentVal;
                resultLabel.text = result + "";
                break;
            case '÷':
                if(currentVal!=0)
                {
                    result = storedVal / currentVal;
                    resultLabel.text = result + "";
                }
                else
                {
                    errorDisplayed = true;
                    resultLabel.text = "ERROR";
                }
                break;
            default:
                Debug.Log("unknown:" + activeOp);
                break;
        }
        //currentVal = result;
        updateDigitlabel();
        
    }
    public void buttonTapped( char caption)
    {

        if (errorDisplayed)
            clearcalc();
        if ((caption >= '0' && caption <= '9') || caption == '.')
        {
            if (digitLabel.text.Length < 15 || !displayValid)
            {
                if (!displayValid)
                    digitLabel.text = (caption == '.' ? "0" : "");
                else if (digitLabel.text == "0" && caption != '.')
                    digitLabel.text = "";
                digitLabel.text += caption;
                displayValid = true;
            }
        }
        else if(caption == '←')
        {
            backSpace();
        }
        else if (caption == 'c')
        {
            clearcalc();
        }
        else if (caption == '±')
        {
            currentVal = -double.Parse(digitLabel.text);
            updateDigitlabel();
            specialAction = true;
        }
        else if (caption == '%')
        {
            currentVal = double.Parse(digitLabel.text) / 100.0d;
            updateDigitlabel();
            specialAction = true;
        }
        else if (displayValid || stroredOperator == '=' || specialAction)
        {
            currentVal = double.Parse(digitLabel.text);
            displayValid = false;
            if (stroredOperator != ' ')
            {
                calcResult(stroredOperator);
                stroredOperator = ' ';
            }
            operatorLabel.text = caption.ToString();    //operatorLabel
            stroredOperator = caption;
            storedVal = currentVal;
            updateDigitlabel();
            specialAction = false;
        }

    }
}
