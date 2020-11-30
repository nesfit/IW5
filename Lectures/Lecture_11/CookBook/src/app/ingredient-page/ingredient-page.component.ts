import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IngredientListModel } from '../api/models';
import { IngredientService } from '../api/services';

@Component({
  selector: 'app-ingredient-page',
  templateUrl: './ingredient-page.component.html',
  styleUrls: ['./ingredient-page.component.less']
})
export class IngredientPageComponent implements OnInit {

  constructor(private ingredientService: IngredientService) { }
  ingredients: Observable<Array<IngredientListModel>> | undefined;

  ngOnInit(): void {
    this.ingredients = this.ingredientService.ingredientGetAll();
  }

}
