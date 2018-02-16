using System;
using System.Threading.Tasks;
using EUJIT.Interface;
using Xamarin.Forms;

namespace EUJIT.Services
{
    public class MessageHandler : IMessageHandler
    {
        private ContentPage _contentPage;
        private static MessageHandler _messaegHandler;

        public static MessageHandler GetInstance(ContentPage contentPage)
        {
            _messaegHandler = new MessageHandler(contentPage);
            return _messaegHandler;
        }

        public MessageHandler(ContentPage contentPage)
        {
            _contentPage = contentPage;
        }
        private EventHandler _PositiveButtonHandler;
        private EventHandler _NegativeButtonHandler;

        public EventHandler PositiveButtonHandler
        {
            get
            {
                return this._PositiveButtonHandler;
            }
            set
            {
                this._PositiveButtonHandler = value;
            }
        }

        public EventHandler NegativeButtonHandler
        {
            get
            {
                return this._NegativeButtonHandler;
            }
            set
            {
                this._NegativeButtonHandler = value;
            }
        }
        public void init<T>(T source)
        {
            //Write code here to assign target source
            _contentPage = (source as ContentPage);
        }

        public async Task ShowMessage(string message)
        {
            await _contentPage.DisplayAlert("", message, Constants.strOK);
        }

        public async Task ShowMessage(string message, string title)
        {
            await _contentPage.DisplayAlert(title, message, Constants.strOK);
        }

        public async Task<bool> ShowMessageConfirm(string message, string title, string buttonYesTitle, string buttonNoTitle)
        {
            bool retVal = await _contentPage.DisplayAlert(title, message, buttonYesTitle, buttonNoTitle);
            if (retVal && null != _PositiveButtonHandler)
            {
                _PositiveButtonHandler(this, new EventArgs());
            }
            else if (null != _NegativeButtonHandler)
            {
                _NegativeButtonHandler(this, new EventArgs());
            }
            return retVal;
        }

        public async Task<string> ShowMessageList(string title, string[] listString)
        {
            return await _contentPage.DisplayActionSheet(String.Format("{0}:{1}", Constants.strAction, title),
                                                         Constants.strCancel,
                                                         null, //MessageConstants.strDelete, 
                                                         listString);
        }

        public void Dispose()
        {
            _messaegHandler = null;
        }

        public Task PickDate(string existingDate)
        {
            //TODO: Show PCL Date picker here
            return null;
        }

        public async Task ShowProgressDialog(string message)
        {

        }

        public async Task HideProgressDialog()
        {

        }
    }
}

