import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FoodType, RecipeDetailModel, RecipeListIngredientModel, RecipeListModel } from '../api/models';
import { RecipeService } from '../api/services';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.less']
})
export class RecipeComponent implements OnInit {

  constructor(
    private recipeService: RecipeService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  public foodTypes = FoodType;

  loadRecipeError = false;
  editMode = false;
  model: RecipeDetailModel = { ingredients: []};

  ngOnInit(): void {
    const id = this.route.snapshot.params.id;
    if (id !== undefined && id !== 'create') {
      const request = { id };

      this.recipeService.recipeGetById(request).subscribe(
        providedRecipe => this.model = providedRecipe,
        error => this.loadRecipeError = true);

      this.editMode = true;
    }
  }

  onSave(): void {
    const model = this.model;
    const request = { body: model };

    if (model.id !== undefined) {
      this.recipeService.recipeUpdate(request).subscribe();
    } else {
      this.recipeService.recipeCreate(request).subscribe(
        complete => this.router.navigate(['/recipes']));
    }
  }

  onDelete(): void {
    if (this.model?.id !== undefined) {
      const id: string = this.model.id;
      const request = { id };

      this.recipeService.recipeDelete(request).subscribe(complete => this.router.navigate(['/recipes']));
    }
  }
  ingredientRemoved(ingredient: RecipeListIngredientModel): void {
    if (this.model.ingredients != null) {
      const index = this.model.ingredients.indexOf(ingredient);
      this.model.ingredients.splice(index, 1);
    }
  }

  addIngredient(ingredient: RecipeListIngredientModel): void {
    this.model.ingredients?.push(ingredient);
  }
}
