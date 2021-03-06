// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
#pragma warning disable 1591, 3008, 3009
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace GoldenCityShop.Controllers
{
    public partial class ProductController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ProductController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> SaveRatings()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveRatings);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> AddToWishList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddToWishList);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> RemoveFromWishList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemoveFromWishList);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AddToCompareList()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddToCompareList);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetSelections()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetSelections);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetProperties()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetProperties);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetRelatedProducts()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetRelatedProducts);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Index()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProductController Actions { get { return MVC.Product; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Product";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Product";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string SaveRatings = "SaveRatings";
            public readonly string AddToWishList = "AddToWishList";
            public readonly string RemoveFromWishList = "RemoveFromWishList";
            public readonly string AddToCompareList = "AddToCompareList";
            public readonly string ShowCompareList = "ShowCompareList";
            public readonly string GetSelections = "GetSelections";
            public readonly string GetProperties = "GetProperties";
            public readonly string GetRelatedProducts = "GetRelatedProducts";
            public readonly string Index = "Index";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string SaveRatings = "SaveRatings";
            public const string AddToWishList = "AddToWishList";
            public const string RemoveFromWishList = "RemoveFromWishList";
            public const string AddToCompareList = "AddToCompareList";
            public const string ShowCompareList = "ShowCompareList";
            public const string GetSelections = "GetSelections";
            public const string GetProperties = "GetProperties";
            public const string GetRelatedProducts = "GetRelatedProducts";
            public const string Index = "Index";
        }


        static readonly ActionParamsClass_SaveRatings s_params_SaveRatings = new ActionParamsClass_SaveRatings();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveRatings SaveRatingsParams { get { return s_params_SaveRatings; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveRatings
        {
            public readonly string productId = "productId";
            public readonly string value = "value";
            public readonly string sectionType = "sectionType";
        }
        static readonly ActionParamsClass_AddToWishList s_params_AddToWishList = new ActionParamsClass_AddToWishList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddToWishList AddToWishListParams { get { return s_params_AddToWishList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddToWishList
        {
            public readonly string productId = "productId";
        }
        static readonly ActionParamsClass_RemoveFromWishList s_params_RemoveFromWishList = new ActionParamsClass_RemoveFromWishList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_RemoveFromWishList RemoveFromWishListParams { get { return s_params_RemoveFromWishList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_RemoveFromWishList
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_AddToCompareList s_params_AddToCompareList = new ActionParamsClass_AddToCompareList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddToCompareList AddToCompareListParams { get { return s_params_AddToCompareList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddToCompareList
        {
            public readonly string productId = "productId";
        }
        static readonly ActionParamsClass_GetSelections s_params_GetSelections = new ActionParamsClass_GetSelections();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetSelections GetSelectionsParams { get { return s_params_GetSelections; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetSelections
        {
            public readonly string categoryId = "categoryId";
        }
        static readonly ActionParamsClass_GetProperties s_params_GetProperties = new ActionParamsClass_GetProperties();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetProperties GetPropertiesParams { get { return s_params_GetProperties; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetProperties
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetRelatedProducts s_params_GetRelatedProducts = new ActionParamsClass_GetRelatedProducts();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetRelatedProducts GetRelatedProductsParams { get { return s_params_GetRelatedProducts; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetRelatedProducts
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string id = "id";
            public readonly string name = "name";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _AttributesPartial = "_AttributesPartial";
                public readonly string _RelatedProductsPartial = "_RelatedProductsPartial";
                public readonly string _SelactionProductsPartial = "_SelactionProductsPartial";
                public readonly string Index = "Index";
                public readonly string ShowCompareList = "ShowCompareList";
            }
            public readonly string _AttributesPartial = "~/Views/Product/_AttributesPartial.cshtml";
            public readonly string _RelatedProductsPartial = "~/Views/Product/_RelatedProductsPartial.cshtml";
            public readonly string _SelactionProductsPartial = "~/Views/Product/_SelactionProductsPartial.cshtml";
            public readonly string Index = "~/Views/Product/Index.cshtml";
            public readonly string ShowCompareList = "~/Views/Product/ShowCompareList.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProductController : GoldenCityShop.Controllers.ProductController
    {
        public T4MVC_ProductController() : base(Dummy.Instance) { }

        [NonAction]
        partial void SaveRatingsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? productId, double? value, string sectionType);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> SaveRatings(long? productId, double? value, string sectionType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SaveRatings);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "value", value);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sectionType", sectionType);
            SaveRatingsOverride(callInfo, productId, value, sectionType);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void AddToWishListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? productId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> AddToWishList(long? productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddToWishList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            AddToWishListOverride(callInfo, productId);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void RemoveFromWishListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> RemoveFromWishList(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemoveFromWishList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            RemoveFromWishListOverride(callInfo, id);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void AddToCompareListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? productId);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddToCompareList(long? productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddToCompareList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            AddToCompareListOverride(callInfo, productId);
            return callInfo;
        }

        [NonAction]
        partial void ShowCompareListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ShowCompareList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ShowCompareList);
            ShowCompareListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetSelectionsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? categoryId);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetSelections(long? categoryId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetSelections);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryId", categoryId);
            GetSelectionsOverride(callInfo, categoryId);
            return callInfo;
        }

        [NonAction]
        partial void GetPropertiesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetProperties(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetProperties);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetPropertiesOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetRelatedProductsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetRelatedProducts(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetRelatedProducts);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetRelatedProductsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id, string name);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(long? id, string name)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            IndexOverride(callInfo, id, name);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
