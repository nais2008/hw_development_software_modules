using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp6
{
    public partial class Window2 : Window
    {
        public enum ProductCategory
        {
            Овощи,
            Фрукты,
            Мясо,
            Молочное,
            Крупы,
            Специи
        }

        public enum DishType
        {
            Салат,
            Суп,
            Гарнир,
            Десерт,
            Напиток
        }

        public class Product
        {
            public string Name { get; set; }
            public ProductCategory Category { get; set; }

            public override string ToString()
            {
                return Name + " (" + Category + ")";
            }
        }

        public class RecipeIngredient
        {
            public string Name { get; set; }
        }

        public class Recipe
        {
            public string Name { get; set; }
            public DishType Type { get; set; }
            public List<RecipeIngredient> Ingredients { get; set; }
            public int CookingTime { get; set; }
            public int Difficulty { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private readonly List<Product> products = new List<Product>();
        private readonly List<Recipe> recipes = new List<Recipe>();
        private readonly List<Recipe> visibleRecipes = new List<Recipe>();
        private readonly Dictionary<string, string> synonyms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "помидор", "томат" },
            { "помидоры", "томат" },
            { "томаты", "томат" },
            { "лук", "луковица" },
            { "луковицы", "луковица" },
            { "картошка", "картофель" },
            { "картошки", "картофель" },
            { "масло сливочное", "сливочное масло" }
        };

        public Window2()
        {
            InitializeComponent();
            foreach (ProductCategory category in Enum.GetValues(typeof(ProductCategory)))
            {
                ProductCategoryComboBox.Items.Add(category);
            }
            foreach (DishType type in Enum.GetValues(typeof(DishType)))
            {
                DishTypeComboBox.Items.Add(type);
            }
            ProductCategoryComboBox.SelectedIndex = 0;
            LoadRecipes();
            RefreshProducts();
            RefreshRecipes();
        }

        private void LoadRecipes()
        {
            recipes.Add(new Recipe
            {
                Name = "Овощной салат",
                Type = DishType.Салат,
                Ingredients = Ingredients("томат", "огурец", "луковица", "масло растительное"),
                CookingTime = 15,
                Difficulty = 1
            });
            recipes.Add(new Recipe
            {
                Name = "Салат с курицей",
                Type = DishType.Салат,
                Ingredients = Ingredients("курица", "огурец", "сыр", "сметана"),
                CookingTime = 30,
                Difficulty = 2
            });
            recipes.Add(new Recipe
            {
                Name = "Борщ",
                Type = DishType.Суп,
                Ingredients = Ingredients("свёкла", "капуста", "картофель", "морковь", "луковица", "говядина"),
                CookingTime = 90,
                Difficulty = 4
            });
            recipes.Add(new Recipe
            {
                Name = "Куриный суп",
                Type = DishType.Суп,
                Ingredients = Ingredients("курица", "морковь", "луковица", "лапша", "зелень"),
                CookingTime = 60,
                Difficulty = 3
            });
            recipes.Add(new Recipe
            {
                Name = "Гречка с грибами",
                Type = DishType.Гарнир,
                Ingredients = Ingredients("гречка", "грибы", "луковица", "масло растительное"),
                CookingTime = 35,
                Difficulty = 2
            });
            recipes.Add(new Recipe
            {
                Name = "Рис с овощами",
                Type = DishType.Гарнир,
                Ingredients = Ingredients("рис", "морковь", "горошек", "кукуруза", "масло растительное"),
                CookingTime = 30,
                Difficulty = 2
            });
            recipes.Add(new Recipe
            {
                Name = "Сырники",
                Type = DishType.Десерт,
                Ingredients = Ingredients("творог", "яйцо", "мука", "сахар", "сметана"),
                CookingTime = 25,
                Difficulty = 3
            });
            recipes.Add(new Recipe
            {
                Name = "Яблочный десерт",
                Type = DishType.Десерт,
                Ingredients = Ingredients("яблоко", "творог", "мёд", "корица"),
                CookingTime = 20,
                Difficulty = 2
            });
            recipes.Add(new Recipe
            {
                Name = "Ягодный смузи",
                Type = DishType.Напиток,
                Ingredients = Ingredients("банан", "ягоды", "йогурт", "мёд"),
                CookingTime = 10,
                Difficulty = 1
            });
            recipes.Add(new Recipe
            {
                Name = "Лимонад",
                Type = DishType.Напиток,
                Ingredients = Ingredients("лимон", "вода", "сахар", "мята"),
                CookingTime = 10,
                Difficulty = 1
            });
            recipes.Add(new Recipe
            {
                Name = "Паста с сыром",
                Type = DishType.Гарнир,
                Ingredients = Ingredients("макароны", "сыр", "сливочное масло", "молоко"),
                CookingTime = 25,
                Difficulty = 2
            });
            recipes.Add(new Recipe
            {
                Name = "Фруктовый салат",
                Type = DishType.Салат,
                Ingredients = Ingredients("яблоко", "банан", "апельсин", "йогурт"),
                CookingTime = 15,
                Difficulty = 1
            });
        }

        private List<RecipeIngredient> Ingredients(params string[] names)
        {
            List<RecipeIngredient> result = new List<RecipeIngredient>();
            foreach (string name in names)
            {
                result.Add(new RecipeIngredient { Name = name });
            }
            return result;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductNameTextBox.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("Введите название продукта.");
                return;
            }

            if (!(ProductCategoryComboBox.SelectedItem is ProductCategory))
            {
                MessageBox.Show("Выберите категорию продукта.");
                return;
            }

            products.Add(new Product
            {
                Name = name,
                Category = (ProductCategory)ProductCategoryComboBox.SelectedItem
            });
            ProductNameTextBox.Clear();
            RefreshProducts();
            RefreshRecipes();
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = ProductsListBox.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите продукт для удаления.");
                return;
            }

            products.Remove(selectedProduct);
            RefreshProducts();
            RefreshRecipes();
        }

        private void RefreshProducts()
        {
            ProductsListBox.ItemsSource = null;
            ProductsListBox.ItemsSource = products;
        }

        private void FindRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshRecipes();
        }

        private void DishTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
            {
                RefreshRecipes();
            }
        }

        private void RecipeFilter_Changed(object sender, RoutedEventArgs e)
        {
            if (IsInitialized)
            {
                RefreshRecipes();
            }
        }

        private void RefreshRecipes()
        {
            if (RecipesListBox == null)
            {
                return;
            }

            List<Recipe> filteredRecipes = new List<Recipe>();
            foreach (Recipe recipe in recipes)
            {
                if (DishTypeComboBox.SelectedItem is DishType && recipe.Type != (DishType)DishTypeComboBox.SelectedItem)
                {
                    continue;
                }

                int missingCount = GetMissingIngredients(recipe).Count;
                if (OnlyFullRecipesCheckBox.IsChecked == true && missingCount > 0)
                {
                    continue;
                }

                filteredRecipes.Add(recipe);
            }

            filteredRecipes = filteredRecipes
                .OrderBy(recipe => GetMissingIngredients(recipe).Count == 0 ? 0 : GetMissingIngredients(recipe).Count <= 2 ? 1 : 2)
                .ThenBy(recipe => GetMissingIngredients(recipe).Count)
                .ThenBy(recipe => recipe.Name)
                .ToList();

            visibleRecipes.Clear();
            foreach (Recipe recipe in filteredRecipes)
            {
                visibleRecipes.Add(recipe);
            }

            RecipesListBox.Items.Clear();
            foreach (Recipe recipe in visibleRecipes)
            {
                int missingCount = GetMissingIngredients(recipe).Count;
                string availability;
                if (missingCount == 0)
                {
                    availability = "можно приготовить полностью";
                }
                else if (missingCount <= 2)
                {
                    availability = "не хватает " + missingCount + " ингредиент(а)";
                }
                else
                {
                    availability = "недоступен, не хватает " + missingCount + " ингредиентов";
                }
                RecipesListBox.Items.Add(recipe.Name + " — " + availability);
            }

            int fullRecipes = filteredRecipes.Count(recipe => GetMissingIngredients(recipe).Count == 0);
            AvailableRecipesTextBlock.Text = "Полностью доступных рецептов: " + fullRecipes + " из " + filteredRecipes.Count;
            RecipesListBox.SelectedIndex = -1;
            ClearRecipeDetails();
        }

        private List<string> GetMissingIngredients(Recipe recipe)
        {
            List<string> availableNames = new List<string>();
            foreach (Product product in products)
            {
                string normalizedName = NormalizeName(product.Name);
                if (!availableNames.Contains(normalizedName, StringComparer.OrdinalIgnoreCase))
                {
                    availableNames.Add(normalizedName);
                }
            }

            List<string> missing = new List<string>();
            foreach (RecipeIngredient ingredient in recipe.Ingredients)
            {
                string normalizedIngredient = NormalizeName(ingredient.Name);
                if (!availableNames.Contains(normalizedIngredient, StringComparer.OrdinalIgnoreCase))
                {
                    missing.Add(ingredient.Name);
                }
            }
            return missing;
        }

        private string NormalizeName(string name)
        {
            string normalized = (name ?? "").Trim().ToLowerInvariant();
            if (synonyms.ContainsKey(normalized))
            {
                return synonyms[normalized].ToLowerInvariant();
            }
            return normalized;
        }

        private void RecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipesListBox.SelectedIndex < 0 || RecipesListBox.SelectedIndex >= visibleRecipes.Count)
            {
                ClearRecipeDetails();
                return;
            }

            Recipe recipe = visibleRecipes[RecipesListBox.SelectedIndex];
            List<string> missing = GetMissingIngredients(recipe);
            MissingIngredientsTextBlock.Text = missing.Count == 0
                ? "Отсутствующие ингредиенты: нет"
                : "Отсутствующие ингредиенты: " + string.Join(", ", missing) + " (всего: " + missing.Count + ")";
            CookingTimeTextBlock.Text = "Время приготовления: " + recipe.CookingTime + " мин";
            DifficultyTextBlock.Text = "Сложность: " + recipe.Difficulty + " из 5";
        }

        private void ClearRecipeDetails()
        {
            if (MissingIngredientsTextBlock == null)
            {
                return;
            }
            MissingIngredientsTextBlock.Text = "Отсутствующие ингредиенты выбранного рецепта: —";
            CookingTimeTextBlock.Text = "Время приготовления: —";
            DifficultyTextBlock.Text = "Сложность: —";
        }
    }
}
