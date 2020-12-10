/* tslint:disable */
/* eslint-disable */
import { FoodType } from './food-type';
import { RecipeListIngredientModel } from './recipe-list-ingredient-model';
export interface RecipeDetailModel {
  description?: null | string;
  duration?: string;
  foodType?: FoodType;
  id?: string;
  ingredients?: null | Array<RecipeListIngredientModel>;
  name?: null | string;
}
