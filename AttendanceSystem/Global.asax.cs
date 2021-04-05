using AttendanceSystem.Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace AttendanceSystem
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
		{
			if (FormsAuthentication.CookiesSupported == true)
			{
				if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
				{
					try
					{
						string Email = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
						string roles = string.Empty;


						Users Loginemployee = JsonConvert.DeserializeObject<Users>(Email);
						roles = Loginemployee.UserRole;
						e.User = new System.Security.Principal.GenericPrincipal(
						new System.Security.Principal.GenericIdentity(Email, "Forms"), roles.Split(';'));
					}
					catch (Exception)
					{
					}
				}
			}
		}
	}
}
