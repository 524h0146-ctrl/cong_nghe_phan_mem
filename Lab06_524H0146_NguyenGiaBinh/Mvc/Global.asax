<%@ Application Language="C#" %><%@ Import Namespace="System.Web.Mvc" %><%@ Import Namespace="System.Web.Routing" %><script runat="server">
void Application_Start(){System.Data.Entity.Database.SetInitializer<School.SchoolDb>(null);RouteTable.Routes.MapRoute("default","{controller}/{action}/{id}",new {controller="Departments",action="Index",id=UrlParameter.Optional});}
</script>
