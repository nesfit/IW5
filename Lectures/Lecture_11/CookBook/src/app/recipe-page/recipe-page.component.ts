import { Component, OnInit } from '@angular/core';
import { RecipeListModel } from '../api/models';
import { RecipeService } from '../api/services';

@Component({
  selector: 'app-recipe-page',
  templateUrl: './recipe-page.component.html',
  styleUrls: ['./recipe-page.component.less']
})
export class RecipePageComponent implements OnInit {

  constructor(private recipeService: RecipeService) { }
  recipes: Array<RecipeListModel> | undefined;

  ngOnInit(): void {
    this.recipeService.recipeGetAll().subscribe(loadedRecipes => this.recipes = loadedRecipes);
  }

}
