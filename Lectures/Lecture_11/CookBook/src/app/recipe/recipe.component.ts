import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeDetailModel } from '../api/models';
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

  loadIngredientError = false;
  editMode = false;
  model: RecipeDetailModel = {};

  ngOnInit(): void {
    const id = this.route.snapshot.params.id;
    if (id !== undefined && id !== 'create') {
      const request = { id };

      this.recipeService.recipeGetById(request).subscribe(
        providedRecipe => this.model = providedRecipe,
        error => this.loadIngredientError = true);

      this.editMode = true;
    }
  }

  onSave(): void{
  }

  onDelete(): void {
    if (this.model?.id !== undefined) {
      const id: string = this.model.id;
      const request = { id };

      this.recipeService.recipeDelete(request).subscribe(complete => this.router.navigate(['/recipes']));
    }
  }

}
