using Abp.Web.Mvc.Views;

namespace QuinelaWeb.Web.Views
{
    public abstract class QuinelaWebWebViewPageBase : QuinelaWebWebViewPageBase<dynamic>
    {

    }

    public abstract class QuinelaWebWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected QuinelaWebWebViewPageBase()
        {
            LocalizationSourceName = QuinelaWebConsts.LocalizationSourceName;
        }
    }
}