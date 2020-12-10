import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IngredientComponent } from './ingredient/ingredient.component';
import { IngredientPageComponent } from './ingredient-page/ingredient-page.component';
import { HomeComponent } from './home/home.component';
import { RecipePageComponent } from './recipe-page/recipe-page.component';
import { RecipeComponent } from './recipe/recipe.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'ingredient/:id', component: IngredientComponent },
  { path: 'ingredients', component: IngredientPageComponent },
  { path: 'recipes', component: RecipePageComponent },
  { path: 'recipe/:id', component: RecipeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
