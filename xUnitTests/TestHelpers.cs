using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDishes;
namespace ProjectDishes
{
    public static class TestHelpers
    {
        public static int CreateUser(string name, string email, string pass = "pass123")
        {
            int code = RegisterUser(name, email, pass);
            if (code != 0) return -1;
            return LoginAndGetUserId(name, pass);
        }
        public static int RegisterUser(string name, string email, string pass = "pass123")
        {
            return DatabaseHelper.ExecuteNonQueryWithReturnValue("register_user",new { p_name = name, p_pass = pass, p_email = email });
        }
        public static int LoginAndGetUserId(string name, string pass = "pass123")
        {
            var dt = DatabaseHelper.ExecuteQuery("login_user", new { p_name = name, p_pass = pass }).GetAwaiter().GetResult();
            if (dt.Rows.Count == 0) return -1;
            return Convert.ToInt32(dt.Rows[0]["user_id"]);
        }
        public static int CreateRecipe(string name)
        {
            var ok = DatabaseHelper.ExecuteNonQuery("add_recipe",new { p_name = name, p_desc = "descr", p_ingr = "ingr", p_cat_id = 1, p_image = (byte[])null }).GetAwaiter().GetResult();
            if (!ok) return -1;
            var dt = DatabaseHelper.ExecuteQuery("search_recipes", new { p_key = name }).GetAwaiter().GetResult();
            if (dt.Rows.Count == 0) return -1;
            return Convert.ToInt32(dt.Rows[0]["recipe_id"]);
        }
        public static bool DeleteRecipe(int recipeId)
        {
            return DatabaseHelper.ExecuteNonQuery("delete_recipe", new { p_recipe = recipeId }).GetAwaiter().GetResult();
        }
        public static int SubmitUserRecipe(int userId, string name)
        {
            DatabaseHelper.ExecuteNonQuery("submit_user_recipe",new { p_user = userId, p_name = name, p_desc = "d", p_ingr = "i", p_cat_id = 1, p_image = (byte[])null }).GetAwaiter().GetResult();
            var dt = DatabaseHelper.ExecuteQuery("get_user_recipe_requests", null).GetAwaiter().GetResult();
            return Convert.ToInt32(dt.Rows.Cast<DataRow>().Last()["request_id"]);
        }
        public static bool ApproveRequest(int requestId) => DatabaseHelper.ExecuteNonQuery("approve_recipe_request", new { p_request = requestId }).GetAwaiter().GetResult();

        public static bool SetAdminRights(int userId)=> DatabaseHelper.ExecuteNonQuery("set_admin_rights", new { p_user_id = userId, p_new_role_id = 1 }).GetAwaiter().GetResult();
    }
}


