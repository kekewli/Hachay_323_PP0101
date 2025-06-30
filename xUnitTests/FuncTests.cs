using ProjectDishes;
using System.Data;
using System.Windows.Forms;
namespace xUnitTests
{
    public class FuncTests
    {
        [Fact(DisplayName = "1. Регистрация нового пользователя")]
        public void Register_NewUser_ShouldSucceed()
        {
            string name = $"u{Guid.NewGuid():N}";
            string email = $"{name}@test.test";
            int userId = TestHelpers.CreateUser(name, email);
            Assert.True(userId > 0, "Ожидалось создание пользователя и получение его ID.");
        }

        [Fact(DisplayName = "2. Регистрация дублирующим email должна провалиться")]
        public void Register_DuplicateEmail_ShouldFail()
        {
            string baseEmail = $"dup{Guid.NewGuid():N}@test";
            int first = TestHelpers.CreateUser($"u1_{Guid.NewGuid():N}", baseEmail);
            Assert.True(first > 0);
            int secondCode = TestHelpers.RegisterUser($"u2_{Guid.NewGuid():N}", baseEmail);
            Assert.Equal(2, secondCode);
        }

        [Fact(DisplayName = "3. Успешная авторизация")]
        public void Login_WithValidCredentials_ShouldSucceed()
        {
            string name = $"l{Guid.NewGuid():N}";
            string email = $"{name}@test";
            string pass = "p123";
            int id = TestHelpers.CreateUser(name, email, pass);
            Assert.True(id > 0);
            int loginId = TestHelpers.LoginAndGetUserId(name, pass);
            Assert.Equal(id, loginId);
        }

        [Fact(DisplayName = "4. Создание рецепта")]
        public void CreateRecipe_ShouldAppearInList()
        {
            string recipeName = $"R{Guid.NewGuid():N}";
            int recId = TestHelpers.CreateRecipe(recipeName);
            Assert.True(recId > 0);
            var all = DatabaseHelper.ExecuteQuery("get_all_recipes").GetAwaiter().GetResult();
            Assert.Contains(all.Rows.Cast<DataRow>(), r => r["recipe_name"].ToString() == recipeName);
        }

        [Fact(DisplayName = "5. Редактирование рецепта")]
        public void EditRecipe_ShouldUpdateName()
        {
            int recId = TestHelpers.CreateRecipe("OldName");
            Assert.True(recId > 0);
            bool ok = DatabaseHelper.ExecuteNonQuery("edit_recipe",new { p_id = recId, p_name = "NewName", p_desc = "d", p_ingr = "i", p_cat_id = 1, p_image = (byte[])null }).GetAwaiter().GetResult();
            Assert.True(ok);
            var dt = DatabaseHelper.ExecuteQuery("get_recipe_by_id", new { p_recipe_id = recId }).GetAwaiter().GetResult();
            Assert.Equal("NewName", dt.Rows[0]["recipe_name"]);
        }

        [Fact(DisplayName = "6. Удаление рецепта")]
        public void DeleteRecipe_ShouldRemoveFromList()
        {
            int recId = TestHelpers.CreateRecipe($"ToDelete{Guid.NewGuid():N}");
            Assert.True(recId > 0);
            bool delOk = TestHelpers.DeleteRecipe(recId);
            Assert.True(delOk);
            var all = DatabaseHelper.ExecuteQuery("get_all_recipes").GetAwaiter().GetResult();
            Assert.DoesNotContain(all.Rows.Cast<DataRow>(),r => Convert.ToInt32(r["recipe_id"]) == recId);
        }

        [Fact(DisplayName = "7. Поиск рецепта по названию")]
        public void SearchRecipes_ShouldReturnMatching()
        {
            string target = $"Target{Guid.NewGuid():N}";
            TestHelpers.CreateRecipe(target);
            TestHelpers.CreateRecipe("Other");
            var dt = DatabaseHelper.ExecuteQuery("search_recipes", new { p_key = target.Substring(0, 6) }).GetAwaiter().GetResult();
            Assert.Single(dt.Rows);
            Assert.Contains(target, dt.Rows[0]["recipe_name"].ToString());
        }

        [Fact(DisplayName = "8. Добавление в избранное")]
        public void User_CanAddToFavorites()
        {
            int userId = TestHelpers.CreateUser($"fav{Guid.NewGuid():N}", $"{Guid.NewGuid():N}@test");
            int recId = TestHelpers.CreateRecipe("FavRecipe");
            bool favOk = DatabaseHelper.ExecuteNonQuery("add_recipe_to_user_storage", new { p_user = userId, p_recipe = recId }).GetAwaiter().GetResult();
            Assert.True(favOk);
            var dt = DatabaseHelper.ExecuteQuery("get_user_recipes", new { p_user = userId }).GetAwaiter().GetResult();
            Assert.Contains(dt.Rows.Cast<DataRow>(),r => Convert.ToInt32(r["recipe_id"]) == recId);
        }

        [Fact(DisplayName = "9. Удаление из избранного")]
        public void User_CanRemoveFromFavorites()
        {
            int userId = TestHelpers.CreateUser($"rem{Guid.NewGuid():N}", $"rem{Guid.NewGuid()}@test");
            int recId = TestHelpers.CreateRecipe("RemRecipe");
            DatabaseHelper.ExecuteNonQuery("add_recipe_to_user_storage",new { p_user = userId, p_recipe = recId }).GetAwaiter().GetResult();
            bool remOk = DatabaseHelper.ExecuteNonQuery("delete_recipe_from_user_storage",new { p_user = userId, p_recipe = recId }).GetAwaiter().GetResult();
            Assert.True(remOk);
            var dt = DatabaseHelper.ExecuteQuery("get_user_recipes", new { p_user = userId }).GetAwaiter().GetResult();
            Assert.DoesNotContain(dt.Rows.Cast<DataRow>(), r => (int)r["recipe_id"] == recId);
        }

        [Fact(DisplayName = "10. Выставление оценки рецепту")]
        public void User_CanRateRecipe_AndAverageUpdates()
        {
            int userId = TestHelpers.CreateUser($"rate{Guid.NewGuid():N}", $"{Guid.NewGuid():N}@test");
            int recId = TestHelpers.CreateRecipe("RateRecipe");
            int avg1 = DatabaseHelper.ExecuteNonQueryWithReturnValue("rate_recipe", new { p_user = userId, p_recipe = recId, p_rating = 5 });
            Assert.Equal(5, avg1);
            int avg2 = DatabaseHelper.ExecuteNonQueryWithReturnValue("rate_recipe", new { p_user = userId, p_recipe = recId, p_rating = 3 });
            Assert.Equal(3, avg2);
        }
    }
}