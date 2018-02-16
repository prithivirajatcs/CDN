using EUJIT.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EUJIT.Interface
{
    public interface INavigationService
    {
        void SetAppContext(object context);
        void NavigateTo(PageName page, object param, bool isStartPage = false);
        void GotoHomePage();
        void GoBack();
        void CreatePageMap();
        T GetPageInstance<T>(Enum.PageName page) where T : new();
    }
}
