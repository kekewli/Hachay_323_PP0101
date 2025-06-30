using ProjectDishes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace xUnitTests
{
    public class IntegTests
    {
        private const string AdminLogin = "test2";
        private const string AdminPass = "test";
        private const string UserLogin = "test";
        private const string UserPass = "test";
        [Fact(DisplayName = "11. Регистрация - авторизация - просмотр рецептов")]
        public void Register_Login_And_ViewRecipes()
        {
            string u = "u" + Guid.NewGuid();
            string email = u + "@test";
            int uid = TestHelpers.CreateUser(u, email);
            Assert.True(uid > 0);
            int loginId = TestHelpers.LoginAndGetUserId(u);
            Assert.Equal(uid, loginId);
            var dt = DatabaseHelper.ExecuteQuery("get_all_recipes", null).GetAwaiter().GetResult();
            Assert.NotEmpty(dt.Rows);
        }
        [Fact(DisplayName = "12. Авторизация - добавление рецепта – поиск")]
        public void Admin_CreateRecipe_And_Search()
        {
            int adminId = TestHelpers.LoginAndGetUserId(AdminLogin, AdminPass);
            string recipeName = "Rec" + Guid.NewGuid();
            int rid = TestHelpers.CreateRecipe(recipeName);
            Assert.Equal(adminId, TestHelpers.LoginAndGetUserId(AdminLogin, AdminPass));
            var dt = DatabaseHelper.ExecuteQuery("search_recipes", new { p_key = recipeName.Substring(0, 6) }).GetAwaiter().GetResult();
            Assert.Single(dt.Rows);
            Assert.Contains(recipeName, dt.Rows[0]["recipe_name"].ToString());
        }
        [Fact(DisplayName = "13. Поиск пользователей - редактирование - обновление списка")]
        public void Admin_EditUser_And_RefreshList()
        {
            int adminId = TestHelpers.LoginAndGetUserId(AdminLogin, AdminPass);
            bool ok = DatabaseHelper.ExecuteNonQuery("update_user", new { p_user_id = 3, p_name = "test", p_pass = "test", p_em = "testc@mail.ru" }).GetAwaiter().GetResult();
            var dt = DatabaseHelper.ExecuteQuery("get_user_by_id", new { p_user = 3 }).GetAwaiter().GetResult();
            Assert.Single(dt.Rows);
            Assert.Equal("testc@mail.ru", dt.Rows[0]["email"].ToString());
        }
        [Fact(DisplayName = "14. Отправка запроса - одобрение - появление рецепта")]
        public void UserRequest_And_Approve_And_Visible()
        {
            int reqId = TestHelpers.SubmitUserRecipe(3, "testrec");
            int adminId = TestHelpers.LoginAndGetUserId(AdminLogin, AdminPass);
            Assert.True(TestHelpers.ApproveRequest(reqId));
            var dt = DatabaseHelper.ExecuteQuery("search_recipes", new { p_key = "testrec".Substring(0, 6) }).GetAwaiter().GetResult();
            Assert.Single(dt.Rows);
            Assert.Contains("testrec", dt.Rows[0]["recipe_name"].ToString());
        }
        [Fact(DisplayName = "15. Создание пользователя - смена роли")]
        public void ChangeRole_And_CheckAdminAccess()
        {
            int adminId = TestHelpers.LoginAndGetUserId(AdminLogin, AdminPass);
            string u = "usr" + Guid.NewGuid();
            int uid = TestHelpers.CreateUser(u, u + "@test");
            Assert.True(TestHelpers.SetAdminRights(uid));
            int newId = TestHelpers.LoginAndGetUserId(u, "pass123");
        }
        [Fact(DisplayName = "16.Добавление в «Избранное» - оценка - удаление")]
        public void Favorite_Rate_And_Remove()
        {
            string userName = "testfav_" + Guid.NewGuid().ToString("N");
            string email = userName + "@mail.test";
            int uid = TestHelpers.CreateUser(userName, email);
            int rid = TestHelpers.CreateRecipe("FavRate" + Guid.NewGuid());
            bool fav = DatabaseHelper.ExecuteNonQuery("add_recipe_to_user_storage", new { p_user = uid, p_recipe = rid }).GetAwaiter().GetResult();
            Assert.True(fav);
            int avg = DatabaseHelper.ExecuteNonQueryWithReturnValue("rate_recipe", new { p_user = uid, p_recipe = rid, p_rating = 4 });
            Assert.Equal(4, avg);
            bool rem = DatabaseHelper.ExecuteNonQuery("delete_recipe_from_user_storage", new { p_user = uid, p_recipe = rid }).GetAwaiter().GetResult();
            Assert.True(rem);
            var dt = DatabaseHelper.ExecuteQuery("get_user_recipes", new { p_user = uid }).GetAwaiter().GetResult();
            Assert.Empty(dt.Rows);
        }
        [Fact(DisplayName = "17. Сохранение в «Избранное» - выход/вход - проверка хранения")]
        public void Favorites_PersistBetweenSessions()
        {
            string u = "pers" + Guid.NewGuid();
            string em = u + "@test";
            int uid = TestHelpers.CreateUser(u, em);
            Assert.True(uid > 0);
            var ids = Enumerable.Range(0, 3).Select(_ => TestHelpers.CreateRecipe("PR" + Guid.NewGuid())).ToList();
            foreach (var rid in ids)
            {
                DatabaseHelper.ExecuteNonQuery("add_recipe_to_user_storage", new { p_user = uid, p_recipe = rid }).GetAwaiter().GetResult();
            }
            int lid = TestHelpers.LoginAndGetUserId(u);
            Assert.Equal(uid, lid);
            var dt = DatabaseHelper.ExecuteQuery("get_user_recipes", new { p_user = uid }).GetAwaiter().GetResult();
            var stored = dt.Rows.Cast<DataRow>().Select(r => Convert.ToInt32(r["recipe_id"])).ToList();
            Assert.True(ids.All(i => stored.Contains(i)));
        }
        [Fact(DisplayName = "18. Назначение рейтинга - обновление рейтинга в списке")]
        public void Rating_UpdatesMainList()
        {
            string u = "ru" + Guid.NewGuid();
            string em = u + "@test";
            int uid = TestHelpers.CreateUser(u, em);
            Assert.True(uid > 0);
            int rid = TestHelpers.CreateRecipe("RL" + Guid.NewGuid());
            Assert.True(rid > 0);
            DatabaseHelper.ExecuteNonQuery("rate_recipe", new { p_user = uid, p_recipe = rid, p_rating = 5 }).GetAwaiter().GetResult();
            var dt = DatabaseHelper.ExecuteQuery("get_all_recipes", null).GetAwaiter().GetResult();
            var row = dt.Rows.Cast<DataRow>().First(r => Convert.ToInt32(r["recipe_id"]) == rid);
            decimal avg = Convert.ToDecimal(row["average_rating"]);
            Assert.Equal(5.00m, avg);
        }
        [Fact(DisplayName = "19. Редактирование рецепта администратором - обновление списка")]
        public void Admin_EditRecipe_SyncList()
        {
            int adminId = TestHelpers.LoginAndGetUserId(AdminLogin, AdminPass);
            int rid = TestHelpers.CreateRecipe("Sync" + Guid.NewGuid());
            bool ok = DatabaseHelper.ExecuteNonQuery("edit_recipe", new{p_id = rid,p_name = "Synced",p_desc = "d",p_ingr = "i",p_cat_id = 1,p_image = (byte[])null}).GetAwaiter().GetResult();
            Assert.True(ok);
            var dt = DatabaseHelper.ExecuteQuery("search_recipes", new { p_key = "Synced" }).GetAwaiter().GetResult();
            Assert.Contains(dt.Rows.Cast<DataRow>(),r => Convert.ToInt32(r["recipe_id"]) == rid&& r["recipe_name"].ToString() == "Synced");
        }
        [Fact(DisplayName = "20. Отправка пользовательского запроса → отображение в списке запросов")]
        public void SubmitRequest_AppearsInUserAndAdminLists()
        {
            string u = "requ2" + Guid.NewGuid();
            string em = u + "@test";
            int uid = TestHelpers.CreateUser(u, em);
            Assert.True(uid > 0);
            string rn = "RQ" + Guid.NewGuid();
            int reqId = TestHelpers.SubmitUserRecipe(uid, rn);
            Assert.True(reqId > 0);
            var userReqs = DatabaseHelper.ExecuteQuery("get_user_recipe_requests", null).GetAwaiter().GetResult();
            Assert.Contains(userReqs.Rows.Cast<DataRow>(), r => Convert.ToInt32(r["request_id"]) == reqId);
        }
    }
}
