import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IngredientDetailModel } from '../api/models';
import { IngredientService } from '../api/services';

@Component({
  selector: 'app-ingredient',
  templateUrl: './ingredient.component.html',
  styleUrls: ['./ingredient.component.less']
})
export class IngredientComponent implements OnInit {

  constructor(private ingredientService: IngredientService, private route: ActivatedRoute) { }

  loadIngredientError = '';
  errorMessage = '';
  ingredient: IngredientDetailModel | undefined;

  ngOnInit(): void {
    const id = this.route.snapshot.params.id;
    const request = { id };

    this.ingredientService.ingredientGetById(request).subscribe(
      providedIngredient => this.ingredient = providedIngredient,
      error => this.loadIngredientError = 'Unable to load given ingredient');
  }
}
