using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;

public class ErrorPopup : TargetBaseViewer<ErrorPopup.Popup>{
    [SerializeField]
    TMP_Text textmessage;
    [SerializeField]
    Button yesbutton;
    [SerializeField]
    Button nobutton;
    [SerializeField]
    Button okbutton;


    public Action noaction = delegate { };
    private ErrorPopup.Popup _targetpopup;
    void Start()
    {
        noaction += NoActionOperation;
    }
    void NoActionOperation()
    {
        if(isOn)
        {
            if(_targetpopup!=null)
            {
                EventSender.SendEvent(_targetpopup.noaction);
                base.Hide();
                isOn = false;
            }
        }
    }

    //Loop hole check here tomorrow
    private static ErrorPopup.Popup popup;
    public bool isOn;

    void ForceCloseOpoeration()
    {
        base.Hide();
        isOn = false;
    }
	void PlaySound()
	{
	}
    /// <summary>
    /// Show a popup by target data 
    /// </summary>
    /// <param name="tTarget"></param>
    protected override void Show(ErrorPopup.Popup tTarget)
    {
        base.Show(popup);
        isOn = true;
        _targetpopup = tTarget;
        yesbutton.onClick.RemoveAllListeners();
        nobutton.onClick.RemoveAllListeners();
        okbutton.onClick.RemoveAllListeners();
		textmessage.SetText(target.Message);
	
      
        if (target.yesaction != null && target.noaction != null)
        {
            yesbutton.onClick.AddListener(() =>
            {
                EventSender.SendEvent(target.yesaction);
                base.Hide();
                isOn = false;
					PlaySound();

            });
            nobutton.onClick.AddListener(() =>
            {
                EventSender.SendEvent(target.noaction);
                base.Hide();
                isOn = false;
					PlaySound();
            });

            yesbutton.gameObject.SetActive(true);
            nobutton.gameObject.SetActive(true);
            okbutton.gameObject.SetActive(false);

        }        
        if (target.okaction != null)
        {
            okbutton.onClick.AddListener(() =>
            {
                EventSender.SendEvent(target.okaction);
                base.Hide();
                isOn = false;
					PlaySound();
            });
            yesbutton.gameObject.SetActive(false);
            nobutton.gameObject.SetActive(false);
            okbutton.gameObject.SetActive(true);

        }
       

    }

    public void Congrats()
    {
        
    }
    void LateUpdate()
    {
      
    }

    /// <summary>
    /// This is sealed popup where we used any type of message from this ,
    /// so be careful about this and placed at right position or  event.
    /// </summary>
    public  class Popup
    {
        /// <summary>
        /// Gset the message be empty
        /// </summary>
        private string message = string.Empty;
        /// <summary>
        /// yes action be declared 
        /// </summary>
        public readonly Action yesaction = delegate { };
        /// <summary>
        /// no action be declared
        /// </summary>
        public readonly Action noaction = delegate { };
        /// <summary>
        /// ok action be declared 
        /// </summary>
        public readonly Action okaction = delegate { };
        /// <summary>
        /// Constructor to create a msg for  no actioon 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="_noaction"></param>
        public Popup(string msg, Action _okaction)
        {
            this.message = msg;
            this.okaction = _okaction;
            yesaction = noaction = null;
            popup = GetPopup;
        }
        /// <summary>
        /// Pass a msg 
        /// plus yes action 
        /// or no action 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="_yesaction"></param>
        /// <param name="_noaction"></param>
        public Popup(string msg, Action _yesaction, Action _noaction)
        {
            this.message = msg;
            this.okaction = null;
            this.yesaction = _yesaction;
            this.noaction = _noaction;
            popup = GetPopup;
        }
        /// <summary>
        /// Get the Message and pass to the UI
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }
        }
        /// <summary>
        /// Get the popup 
        /// </summary>
        public Popup GetPopup
        {
            get {
                return this;
            }
        }
    }
}
