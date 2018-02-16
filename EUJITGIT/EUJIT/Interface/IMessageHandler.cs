using System;
using System.Threading.Tasks;

namespace EUJIT.Interface
{
    public interface IMessageHandler : IDisposable
    {
        EventHandler PositiveButtonHandler { set; get; }
        EventHandler NegativeButtonHandler { set; get; }
        void init<T>(T source);
        Task ShowMessage(string message);
        Task ShowMessage(string message, string title);
        Task<string> ShowMessageList(string title, string[] listString);
        Task<bool> ShowMessageConfirm(string message, string title, string buttonYesTitle, string buttonNoTitle);

        Task PickDate(string existingDate);

        Task ShowProgressDialog(string message);
        Task HideProgressDialog();
    }
}

