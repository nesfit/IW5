/* tslint:disable */
/* eslint-disable */
import { IngredientListModel } from './ingredient-list-model';
import { Unit } from './unit';
export interface RecipeListIngredientModel {
  amount?: number;
  ingredient?: null | IngredientListModel;
  unit?: Unit;
  unitText?: null | string;
}
