using Newtonsoft.Json;
using SampleApp.ViewModels;

namespace SampleApp.Helpers
{
    public static class ServiceCoreHelper
    {
        public static bool IsAuthorize(HttpContext context)
        {
            if (context.Session.TryGetValue(ConstantHelper.SESSION_USERID, out var userBytes) && context.Session.TryGetValue(ConstantHelper.SESSION_MENUS, out var menusBytes) && context.Request.Path.HasValue)
            {
                List<MenuDto> menus = JsonConvert.DeserializeObject<List<MenuDto>>(System.Text.Encoding.UTF8.GetString(menusBytes)) ?? new List<MenuDto>();
                if (menus.Exists(e => context.Request.Path.Value.ToLower().Contains(e.Url.ToLower())))
                    return true;
            }

            return false;
        }

        public static string CurrentUser(HttpContext context)
        {
            if (context.Session.TryGetValue(ConstantHelper.SESSION_USERID, out var userBytes))
            {
                var user = JsonConvert.DeserializeObject<UsersLoginDto>(System.Text.Encoding.UTF8.GetString(userBytes)) ?? new ();
                return user.Username;
            }

            return "System";
        }
    }
}
