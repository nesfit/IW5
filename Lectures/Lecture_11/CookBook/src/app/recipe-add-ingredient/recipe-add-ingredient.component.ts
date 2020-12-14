import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { IngredientListModel, RecipeListIngredientModel, Unit } from '../api/models';
import { IngredientService } from '../api/services';

@Component({
  selector: 'app-recipe-add-ingredient',
  templateUrl: './recipe-add-ingredient.component.html',
  styleUrls: ['./recipe-add-ingredient.component.less']
})
export class RecipeAddIngredientComponent implements OnInit {

  constructor(
    private ingredientService: IngredientService,
  ) { }

  amount = 0;
  selectedUnit = Unit.Unknown;
  selectedIngredient: IngredientListModel = {};
  public units = Unit;
  ingredients: IngredientListModel[] = [];

  @Output() addIngredient = new EventEmitter<RecipeListIngredientModel>();

  ngOnInit(): void {
    this.ingredientService.ingredientGetAll().subscribe(
      loadedIngredients => {
        this.ingredients = loadedIngredients, this.selectedIngredient = this.ingredients[0];
      }
    );
  }

  onAdd(): void {
    const ingredientModel: RecipeListIngredientModel =
      { ingredient: this.selectedIngredient, amount: this.amount, unit: this.selectedUnit, unitText: Unit[this.selectedUnit] };
    this.addIngredient.emit(ingredientModel);
  }
}
