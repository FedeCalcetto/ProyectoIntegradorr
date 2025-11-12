using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProyectoIntegrador_Web.Sesion
{
    public class AtributoPrivado : ActionFilterAttribute
    {
        public AtributoPrivado(string roles)
        {
            RolesAutorizados = roles;
        }

        public string RolesAutorizados { get; set; } = string.Empty;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var email = context.HttpContext.Session.GetString("email");
            var rol = context.HttpContext.Session.GetString("rol");

            if (string.IsNullOrWhiteSpace(email))

                context.Result = new RedirectToActionResult("Login", "Home", null);
            else if (RolesAutorizados.Contains(rol))
                base.OnActionExecuting(context);
            else
                context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
