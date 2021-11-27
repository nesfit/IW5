using DotVVM.Framework.Compilation;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.App
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);

            config.Markup.ImportedNamespaces.Add(new NamespaceImport("CookBook.App.Resources.Texts"));
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Home", "", "Views/Home.dothtml");

            config.RouteTable.Add("Recipes", "recipes", "Views/Recipes.dothtml");
            config.RouteTable.Add("RecipeDetail", "recipe/{id?}", "Views/RecipeDetail.dothtml");

            config.RouteTable.Add("Ingredients", "ingredients", "Views/Ingredients.dothtml");
            config.RouteTable.Add("IngredientDetail", "ingredient/{id?}", "Views/IngredientDetail.dothtml");
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.AddMarkupControl("cc", "NavMenu", "Controls/NavMenu.dotcontrol");
            config.Markup.AddMarkupControl("cc", "IngredientEditor", "Controls/IngredientEditor.dotcontrol");
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.Register("Styles", new StylesheetResource()
            {
                Location = new UrlResourceLocation("~/Resources/style.css")
            });
        }

		public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("temp");
            options.AddHotReload();
		}
    }
}
